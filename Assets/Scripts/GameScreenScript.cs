using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScreenScript : MonoBehaviour
{
    // Start is called before the first frame update
    private string SolutionString;
    private int Level;
    
    //1.first i have to get that user have selected which level .
    //    if user selected current level I have to populate users data and render the gamescreen from user .............
    
    void Start()
    {
   
    }

    // Update is called once per frame
    void Update()
    {
        
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
    
}


//next steps first make the game board interactive.
