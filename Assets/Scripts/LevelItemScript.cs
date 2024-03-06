using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelItemScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private TMP_Text LevelText;
    [SerializeField] private TMP_Text StatusText;
    private string CurrentLevelSolution;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetValuesOfText(int  levelIndex , string currentSolution)
    {
        LevelText.text = (levelIndex+1).ToString();
        CurrentLevelSolution = currentSolution;
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
}


//steps :
//show the status text according to the user current level