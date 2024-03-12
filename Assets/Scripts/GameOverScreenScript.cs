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
    [SerializeField] private GameObject GameScreenPrefab;
    [SerializeField] private GameObject HomeScreenPrefab;
    
    private void Start()
    {
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

    public void TryAgainButtonClickHandler()
    {
        GameObject GameScreenPrefabInstance=Instantiate(GameScreenPrefab,transform.parent);
        if (GameScreenPrefabInstance != null)
        {
            var GameScreenScriptReference = GameScreenPrefabInstance.GetComponent<GameScreenScript>();
            if (GameScreenScriptReference != null)
            {
                int LevelNumber = User.Instance.CurrentLevel;
                string CurrentLevelSolution = Words.WordsInstance.WordsList[LevelNumber - 1].word;
                GameScreenScriptReference.SetGameBoard(CurrentLevelSolution,LevelNumber);
            }
        }
        Destroy(gameObject);
    }
    
    

    public void GoBackToHomeScreenButtonClickHandler()
    {
        
        GameObject HomeScreenInstance = Instantiate(HomeScreenPrefab,transform.parent);
        Destroy(gameObject);
    }

    
}
