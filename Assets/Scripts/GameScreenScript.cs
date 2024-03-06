using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameScreenScript : MonoBehaviour
{
    // Start is called before the first frame update
    private string SolutionString;
    private int Level;
    [SerializeField] private GameObject ChancePrefab;
    [SerializeField] private GameObject ChancePrefabParent;
    [SerializeField] private Button CurrentChanceSubmitButton;

    private GameObject[] AllChances = new GameObject[6];

    

    [SerializeField] private Button[] KeyBoard;

    private int CurrentChanceNumberIndex = 0;
    private int CurrentChanceIndex = 0;
    
    //1.first i have to get that user have selected which level .
    //    if user selected current level I have to populate users data and render the gamescreen from user .............
    
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
        
        //to initialize all the chances
        InstantiateAllChances();
        
        //add listener to the CurrentChanceSubmitButton
        CurrentChanceSubmitButton.onClick.AddListener(HandleCurrentChanceSubmitButtonClick);
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

        RenderGameBoard();
    }

    private void RenderGameBoard()
    {
        if (Level == User.Instance.CurrentLevel)
        {
            //render game board accordingle
            //else show empty game board
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
        AllChances[CurrentChanceNumberIndex].GetComponent<ChanceScript>().setText(alphabet, CurrentChanceIndex);
        if (CurrentChanceIndex < 4) CurrentChanceIndex++;
        else if (CurrentChanceNumberIndex < 5)
        {
            CurrentChanceNumberIndex++;
            CurrentChanceIndex = 0;
        }
    }

    void HandleCurrentChanceSubmitButtonClick()
    {
        
    }
    
}


//next steps first make the game board interactive.
