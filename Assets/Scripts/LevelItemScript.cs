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
    [SerializeField] private Image StatusImage;
    [SerializeField] private Button LevelItem;
    [SerializeField] private GameObject GameScreenPrefab;
    [SerializeField] private RectTransform CanvasRectTransform;
    [SerializeField] private Sprite CurrentLevelSprite;
    [SerializeField] private Sprite LockedLevelSprite;
    [SerializeField] private Sprite CompletedLevelSprite;
    

    private int LevelNumber;
    private string CurrentLevelSolution;
  

  

    public void SetValuesOfText(int  levelIndex , string currentSolution)
    {
        
        LevelItem.onClick.AddListener(HandleLevelItemClick); 
        LevelText.text = "Level "+(levelIndex+1).ToString();
        CurrentLevelSolution = currentSolution;
        LevelNumber = levelIndex + 1;
        //StatusText.text = statusText;
        Image LevelItemImageComponentReference = LevelItem.GetComponent<Image>();

        var currentLevelOfUser = User.Instance.CurrentLevel;
        if (levelIndex + 1 > currentLevelOfUser)
        {
            //StatusText.text = "Locked";
            StatusImage.sprite = LockedLevelSprite;
            LevelItem.onClick.RemoveAllListeners();
            StatusImage.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
            LevelItemImageComponentReference.color=new Color(0.5f, 0.5f, 0.5f,0.5f);
        }
        else if (levelIndex + 1 < currentLevelOfUser)
        {
           // StatusText.text = "Completed";
           StatusImage.sprite = CompletedLevelSprite;
           LevelItem.onClick.RemoveAllListeners();
           StatusImage.color = new Color(0, 1, 0.5f, 1);
            LevelItemImageComponentReference.color = new Color(0, 1, 0, 0.5f);
            
        }
        else
        {
            //StatusText.text = "current";
            StatusImage.sprite = CurrentLevelSprite;
            StatusImage.color = new Color(1, 0.92f, 0.016f, 0.5f);
            LevelItemImageComponentReference.color = new Color(1, 0.92f, 0.016f, 0.5f);
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