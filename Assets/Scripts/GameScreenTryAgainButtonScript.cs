using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameScreenTryAgainButtonScript : MonoBehaviour
{
    [SerializeField] private GameObject GameScreenPrefab;
    
    public void TryAgainButtonClickHandlerFromGameScreen()
    {
  
        
        
        // GameObject GameScreenPrefabInstance=Instantiate(GameScreenPrefab, GameObject.FindGameObjectWithTag("Canvas").transform);
        if (true)
        {
            var GameScreenScriptReference = transform.parent.GetComponent<GameScreenScript>();
            if (GameScreenScriptReference != null)
            {
                User.Instance.Chances.Clear();
                string jsonString = JsonUtility.ToJson(User.Instance);
                File.WriteAllText(Constants.SaveFile,jsonString);
                
                int LevelNumber = User.Instance.CurrentLevel;
                string CurrentLevelSolution = Words.WordsInstance.WordsList[LevelNumber - 1].word;
                GameScreenScriptReference.SetGameBoard(CurrentLevelSolution,LevelNumber);
            }
        }
        
        // Destroy(transform.parent.gameObject);

        
        Debug.Log("clicked on try again");
    }
}
