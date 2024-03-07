using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreenScript : MonoBehaviour
{
    //if GameResult is True it means user won and else he lost 
    private bool GameResult;
    private string SolutionString;
    [SerializeField] private TMP_Text ResultText;
    [SerializeField] private Button TryAgainButton;
    [SerializeField] private Button GoBackToHomeScreenButton;
    
    private void Start()
    {
        //add listeners to button 
        TryAgainButton.onClick.AddListener(TryAgainButtonClickHandler);
        GoBackToHomeScreenButton.onClick.AddListener(GoBackToHomeScreenButtonClickHandler);

        if (GameResult == true)
        {
            ResultText.text = "Congrats You won ! Word is " + SolutionString;
            TryAgainButton.gameObject.SetActive(false);
        }
        else
        {
            ResultText.text = "Sorry , You Lost !";
        }
    }

    public void setGameOverScreen(bool result , string solString)
    {
        GameResult = result;
        SolutionString = solString;
    }

    void TryAgainButtonClickHandler()
    {
        
    }

    void GoBackToHomeScreenButtonClickHandler()
    {
        
    }

    
}
