using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Threading.Tasks;


public class GameScreenScript : MonoBehaviour
{
    // Start is called before the first frame update
    private string SolutionString;
    private int Level;
    [SerializeField] private GameObject ChancePrefab;
    [SerializeField] private GameObject ChancePrefabParent;
    [SerializeField] private GameObject GameOverScreenPrefab;
    [SerializeField] private TMP_Text UserNameText;
    [SerializeField] private TMP_Text HintText;
    [SerializeField] private AudioSource SubmitButtonAudio;

    [SerializeField] private Button SubmitButton;
   // [SerializeField] private GameObject KeyBoardKeyPrefab;
   // [SerializeField] private GameObject KeyBoardKeysParent;
   // private GameObject[] KeyBoardKeysInstances = new GameObject[26];


    private ChanceScript[] AllChances = new ChanceScript[6];

    

    [SerializeField] private Button[] KeyBoard;

    private int CurrentChanceNumberIndex = 0;
    private int CurrentChanceIndex = 0;

    private bool IsUserPlayingLatestLevel = false;
    
    //1.first i have to get that user have selected which level .
    //    if user selected current level I have to populate users data and render the gamescreen from user .............

    public void Update()
    {
        HighlightCurrentActiveRow();
        SetSubmitButtonState();

    }

    public void HighlightCurrentActiveRow()
    {
        if (CurrentChanceNumberIndex >= 0 && CurrentChanceNumberIndex <= 5)
        {
            AllChances[CurrentChanceNumberIndex].HighlightCurrentActiveRow();
        }
    }

    private void SetSubmitButtonState()
    {
        if (CurrentChanceIndex == 5)
        {
            SubmitButton.interactable = true;
        }
        else
        {
            SubmitButton.interactable = false;
        }
    }
    
    


    public void InitGameboard()
    {
        if (User.Instance.Chances.Count == 0 )
        {
            CurrentChanceNumberIndex = 0;
        }
        else if(Level==User.Instance.CurrentLevel)
        {
            CurrentChanceNumberIndex = User.Instance.Chances.Count;
        }
        else
        {
            CurrentChanceNumberIndex = 0;
        }

        CurrentChanceIndex = 0;
        
        
        //to add listentens to the keyboard buttons
        AddListenersToAllTheButtonsOnKeyBoard();
        
        //to initialize all the chances
        InstantiateAllChances();
        
        
        //Render game board
        RenderGameBoard();
        
        //Render user Name
        RenderUserName();
        
        
        //initialise key board

        // for (int i = 0; i < 26; i++)
        // {
        //     KeyBoardKeysInstances[i] = Instantiate(KeyBoardKeyPrefab, KeyBoardKeysParent.transform);
        // }
    }

    void InstantiateAllChances()
    {
        for (int i = 0; i < 6; i++)
        {
            if (AllChances[i] != null)
            {
                Destroy(AllChances[i].gameObject);
            }
            GameObject ChanceInstanceReference = Instantiate(ChancePrefab, ChancePrefabParent.transform);
            AllChances[i] = ChanceInstanceReference.GetComponent<ChanceScript>();
        }
        
    }

    public void SetGameBoard(string solutionString,int level)
    {
        SolutionString = solutionString;
        Level = level;
        if (Level == User.Instance.CurrentLevel)
        {
            IsUserPlayingLatestLevel = true;
        }
        Debug.Log(solutionString + " "+ level);
        HintText.text ="Hint:"+ Words.WordsInstance.WordsList[level - 1].hint;
        InitGameboard();
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
                var currentChanceScript=  AllChances[i];
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
          KeyBoard[i].GetComponent<Image>().color = new Color(1, 1, 1, 1);
            KeyBoard[i].onClick.RemoveAllListeners();
            KeyBoard[i].onClick.AddListener(delegate
            {
                HandleKeyBoardButtonClick(alphabet);
            });
        }
    }

    void HandleKeyBoardButtonClick(string alphabet)
    {
        Debug.Log("clicked on handlekeyboardclick");
        if(CurrentChanceIndex<5)AllChances[CurrentChanceNumberIndex].SetText(alphabet, CurrentChanceIndex);
        if (CurrentChanceIndex < 5) CurrentChanceIndex++;
    }

    public void HandleCurrentChanceSubmitButtonClick()
    {
        SubmitButtonAudio.Play();
        if (CurrentChanceIndex == 5 && CurrentChanceNumberIndex <= 5)
        {   //increase chance number number
            //we have to create an object chance class initialise it with the fields required for it
            //push the chance object to the User.Chances List
            //then save the user to the json.
            //color the text .

            
            Chance currentChanceObject = new Chance();
            currentChanceObject.ChanceNumber = CurrentChanceNumberIndex + 1;
             var CurrentChanceScriptReference = AllChances[CurrentChanceNumberIndex];
            currentChanceObject.StringEntered =CurrentChanceScriptReference.GetString();
            
           if(User.Instance.CurrentLevel==Level) User.Instance.Chances.Add(currentChanceObject);
            
           
            CurrentChanceNumberIndex++;
            CurrentChanceIndex = 0;

            if (currentChanceObject.StringEntered.ToUpper().Equals(SolutionString.ToUpper()))
            {
                EndCurrentLevel();
                return;
            }

            if(User.Instance.CurrentLevel==Level){
                string jsonString = JsonUtility.ToJson(User.Instance);
                File.WriteAllText(Constants.SAVEFILE, jsonString);
            }
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


    public  void EndCurrentLevel()
    {
        var CurrentChanceScriptReference = AllChances[CurrentChanceNumberIndex-1];
        CurrentChanceScriptReference.ColorTheString(SolutionString);
        Invoke(nameof(ShowGameOverScreen),1.5f);
    }

    private void ShowGameOverScreen()
    {
        //initialise game over screen.
        var CurrentChanceScriptReference = AllChances[CurrentChanceNumberIndex - 1];
        string lastChanceString = CurrentChanceScriptReference.GetString().ToUpper();
        GameObject GameOverScreenPrefabInstance = Instantiate(GameOverScreenPrefab, transform.parent);

        Debug.Log(lastChanceString + " " + SolutionString);
        if (lastChanceString.Equals(SolutionString.ToUpper()))
        {
            GameOverScreenPrefabInstance.GetComponent<GameOverScreenScript>().setGameOverScreen(true, SolutionString);
            if (User.Instance.CurrentLevel == Level)
            {
                
                Debug.Log(User.Instance.CurrentLevel +" "+Level);
                User.Instance.CurrentLevel++;
            }
        }
        else
        {
            GameOverScreenPrefabInstance.GetComponent<GameOverScreenScript>().setGameOverScreen(false, SolutionString);

        }

        if (User.Instance.CurrentLevel == Level+1 && IsUserPlayingLatestLevel)
        {
            User.Instance.Chances.Clear();
            string jsonString = JsonUtility.ToJson(User.Instance);
            File.WriteAllText(Constants.SAVEFILE, jsonString);
        }
        
        Debug.Log(User.Instance.CurrentLevel+" "+Level);

        Destroy(gameObject);
    }


    public void ColorKeyBoardKeys(string key , States currentLetterState)
    {
        for (int i = 0; i < KeyBoard.Length; i++)
        {
            GameObject textReference= KeyBoard[i].transform.GetChild(0).gameObject;
            string alphabet = textReference.GetComponent<Text>().text;
            if (alphabet.Equals(key))
            {
                if (currentLetterState.Equals(States.NotExistingText))
                {
                    KeyBoard[i].GetComponent<Image>().color=Constants.GRAYCOLOR;
                }
                else if (currentLetterState.Equals(States.CorrectEnteredButWrongPositionText))
                {
                    KeyBoard[i].GetComponent<Image>().color = Constants.SKYBLUECOLOR;
                }
            }
        }
    }

    public void BackSpaceButtonOnClickHandler()
    {
        if (CurrentChanceIndex > 0)
        {
            CurrentChanceIndex--; 
            AllChances[CurrentChanceNumberIndex].SetText("", CurrentChanceIndex);
        }
        else if (CurrentChanceIndex == 0)
        {
            AllChances[CurrentChanceNumberIndex].SetText("", CurrentChanceIndex);
        }
        
        
    }
    
    void RenderUserName()
    {
        UserNameText.text = User.Instance.UserName;
    }
}


//next steps first make the game board interactive.
