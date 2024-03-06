using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelItemScript : MonoBehaviour
{

    public static Action onButtonClicked;

    // Start is called before the first frame update
    [SerializeField] private TMP_Text LevelText;
    [SerializeField] private TMP_Text StatusText;
    [SerializeField] private Button LevelItem;
    [SerializeField] private GameObject GameScreenPrefab;
    [SerializeField] private RectTransform CanvasRectTransform;

    private int LevelNumber;
    private string CurrentLevelSolution;
    void Start()
    {
       LevelItem.onClick.AddListener(HandleLevelItemClick);   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetValuesOfText(int  levelIndex , string currentSolution)
    {
        LevelText.text = (levelIndex+1).ToString();
        CurrentLevelSolution = currentSolution;
        LevelNumber = levelIndex + 1;
        //StatusText.text = statusText;

        var currentLevelOfUser = User.Instance.CurrentLevel;
        if (levelIndex + 1 > currentLevelOfUser)
        {
            StatusText.text = "Locked";
        }
        else if (levelIndex + 1 < currentLevelOfUser)
        {
            StatusText.text = "Completed";
        }
        else
        {
            StatusText.text = "current";
        }
    }

    public void HandleLevelItemClick()
    {
        GameObject GameScreenPrefabInstance=Instantiate(GameScreenPrefab,GameObject.FindGameObjectWithTag("Canvas").transform);
        if (GameScreenPrefabInstance != null)
        {
            var GameScreenScriptReference = GameScreenPrefabInstance.GetComponent<GameScreenScript>();
            if (GameScreenScriptReference != null)
            {
                GameScreenScriptReference.SetGameBoard(CurrentLevelSolution,LevelNumber);
            }
            
        }
        
        onButtonClicked?.Invoke();
    }
}


//steps :
//show the status text according to the user current level