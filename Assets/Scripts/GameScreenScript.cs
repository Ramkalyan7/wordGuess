using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class GameScreenScript : MonoBehaviour
{
    // Start is called before the first frame update
    private string SolutionString;
    private int Level;
    [SerializeField] private GameObject ChancePrefab;
    [SerializeField] private GameObject ChancePrefabParent;
    [SerializeField] private Button CurrentChanceSubmitButton;
    [SerializeField] private GameObject GameOverScreenPrefab;
    [SerializeField] private Button BackSpaceButton;
    [SerializeField] private TMP_Text UserNameText;
    [SerializeField] private TMP_Text HintText;
    private string saveFile;


    private GameObject[] AllChances = new GameObject[6];

    

    [SerializeField] private Button[] KeyBoard;

    private int CurrentChanceNumberIndex = 0;
    private int CurrentChanceIndex = 0;
    
    //1.first i have to get that user have selected which level .
    //    if user selected current level I have to populate users data and render the gamescreen from user .............

    private void Awake()
    {
        saveFile=Application.persistentDataPath + "/gamedata.json";
    }

    void Start()
    {
        //initialize class members according to the user data

        if (User.Instance.Chances.Count == 0)
        {
            CurrentChanceNumberIndex = 0;
        }
        else
        {
            CurrentChanceNumberIndex = User.Instance.Chances.Count;
        }

        CurrentChanceIndex = 0;
        
        
        //to add listentens to the keyboard buttons
        AddListenersToAllTheButtonsOnKeyBoard();
        BackSpaceButton.onClick.AddListener(BackSpaceButtonOnClickHandler);
        
        //to initialize all the chances
        InstantiateAllChances();
        
        //add listener to the CurrentChanceSubmitButton
        CurrentChanceSubmitButton.onClick.AddListener(HandleCurrentChanceSubmitButtonClick);
        
        //Render game board
        RenderGameBoard();
        
        //Render user Name
        RenderUserName();
    }

    void InstantiateAllChances()
    {
        for (int i = 0; i < 6; i++)
        {
            AllChances[i] = Instantiate(ChancePrefab, ChancePrefabParent.transform);
        }
        
    }

    public void SetGameBoard(string solutionString,int level)
    {
        SolutionString = solutionString;
        Level = level;
        Debug.Log(solutionString + " "+ level);
        HintText.text = Words.WordsInstance.WordsList[level - 1].hint;

    }

    private void RenderGameBoard()
    {
        
        if (Level == User.Instance.CurrentLevel && User.Instance.Chances.Count>0)
        {
            //render game board accordingly
            //else show empty game board
            //color the string

            for (int i = 0; i < User.Instance.Chances.Count; i++)
            {
                Chance currentChance = User.Instance.Chances[i];
                string currentChanceString = currentChance.StringEntered;
                var currentChanceScript=  AllChances[i].GetComponent<ChanceScript>();
                for (int j = 0; j < currentChanceString.Length; j++)
                {
                 
                  currentChanceScript.SetText(currentChanceString[j].ToString(), j);
                }
                
                currentChanceScript.ColorTheString(SolutionString);
            }
        }
    }

    void AddListenersToAllTheButtonsOnKeyBoard()
    {   
        for (int i = 0; i < KeyBoard.Length; i++)
        {
          GameObject textReference= KeyBoard[i].transform.GetChild(0).gameObject;
          string alphabet = textReference.GetComponent<Text>().text;
            KeyBoard[i].onClick.AddListener(delegate
            {
                HandleKeyBoardButtonClick(alphabet);
            });
        }
    }

    void HandleKeyBoardButtonClick(string alphabet)
    {
        if(CurrentChanceIndex<5)AllChances[CurrentChanceNumberIndex].GetComponent<ChanceScript>().SetText(alphabet, CurrentChanceIndex);
        if (CurrentChanceIndex < 5) CurrentChanceIndex++;
    }

    void HandleCurrentChanceSubmitButtonClick()
    {
        
        
 
        
        if (CurrentChanceIndex == 5 && CurrentChanceNumberIndex <= 5)
        {   //increase chance number number
            //we have to create an object chance class initialise it with the fields required for it
            //push the chance object to the User.Chances List
            //then save the user to the json.
            //color the text .

            
            Chance currentChanceObject = new Chance();
            currentChanceObject.ChanceNumber = CurrentChanceNumberIndex + 1;
             var CurrentChanceScriptReference = AllChances[CurrentChanceNumberIndex].GetComponent<ChanceScript>();
            currentChanceObject.StringEntered =CurrentChanceScriptReference.GetString();
            
            User.Instance.Chances.Add(currentChanceObject);
            
           
            CurrentChanceNumberIndex++;
            CurrentChanceIndex = 0;

            if (currentChanceObject.StringEntered.ToUpper().Equals(SolutionString.ToUpper()))
            {
                EndCurrentLevel();
                return;
            }
            
            string jsonString = JsonUtility.ToJson(User.Instance);
            File.WriteAllText(saveFile,jsonString);
            CurrentChanceScriptReference.ColorTheString(SolutionString);
        }

        if (CurrentChanceNumberIndex == 6)
        {
            //it means user have completed the level .
            //I have to empty the chances list.
            //I have to increase the value of current level.
            //then save the user to the json.
            //color the text.
            //show user game over screen
            
         EndCurrentLevel();
            
            
         
        }
        
    }


    public void EndCurrentLevel()
    {
        
        
        var CurrentChanceScriptReference = AllChances[CurrentChanceNumberIndex-1].GetComponent<ChanceScript>();
        string lastChanceString =CurrentChanceScriptReference.GetString().ToUpper();


            
        //initialise game over screen.
        GameObject GameOverScreenPrefabInstance= Instantiate(GameOverScreenPrefab, transform.parent);
           
        Debug.Log(lastChanceString+" "+SolutionString);
        if (lastChanceString.Equals(SolutionString.ToUpper()))
        {
            GameOverScreenPrefabInstance.GetComponent<GameOverScreenScript>().setGameOverScreen(true,SolutionString);
            User.Instance.CurrentLevel++;
        }
        else
        { 
            GameOverScreenPrefabInstance.GetComponent<GameOverScreenScript>().setGameOverScreen(false,SolutionString);
       
        }
        User.Instance.Chances.Clear();
        string jsonString = JsonUtility.ToJson(User.Instance);
        File.WriteAllText(saveFile,jsonString);
        Destroy(gameObject);
    }


    public void DisableKeyBoardKey(string keyToBeDisabled)
    {
        for (int i = 0; i < KeyBoard.Length; i++)
        {
            GameObject textReference= KeyBoard[i].transform.GetChild(0).gameObject;
            string alphabet = textReference.GetComponent<Text>().text;
            if (alphabet.Equals(keyToBeDisabled))
            {
                KeyBoard[i].onClick.RemoveAllListeners();
                KeyBoard[i].GetComponent<Image>().color=new Color(0.5f, 0.5f, 0.5f, 1);
            }
        }
    }

    private void BackSpaceButtonOnClickHandler()
    {
        if (CurrentChanceIndex > 0)
        {
            CurrentChanceIndex--; 
            AllChances[CurrentChanceNumberIndex].GetComponent<ChanceScript>().SetText("", CurrentChanceIndex);
        }
        else if (CurrentChanceIndex == 0)
        {
            AllChances[CurrentChanceNumberIndex].GetComponent<ChanceScript>().SetText("", CurrentChanceIndex);
        }
        
        
    }
    
    void RenderUserName()
    {
        UserNameText.text = User.Instance.UserName;
    }
    
}


//next steps first make the game board interactive.
