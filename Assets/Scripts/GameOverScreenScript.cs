using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreenScript : MonoBehaviour
{
    //if GameResult is True it means user won and else he lost 
    private bool IsWon;
    private string SolutionString;
    [SerializeField] private TMP_Text ResultText;
    [SerializeField] private Button TryAgainButton;
    [SerializeField] private Button PlayNextLevelButton;
    [SerializeField] private GameObject GameScreenPrefab;
    [SerializeField] private GameObject HomeScreenPrefab;
    
    private void Start()
    {
        if (IsWon )
        {
            ResultText.text = Constants.GAMEOVERWINWISHTEXT+ SolutionString;
            TryAgainButton.gameObject.SetActive(false);
        }
        else
        {
            ResultText.text =Constants.GAMEOVERLOSTWISHTEXT;
            PlayNextLevelButton.gameObject.SetActive(false);
        }
    }

    public void setGameOverScreen(bool result , string solString)
    {
        IsWon = result;
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
        
        Instantiate(HomeScreenPrefab,transform.parent);
        Destroy(gameObject);
    }
    
    
    
    public void InstantiateGameScreenAndDestroyGameOverScreenGameObject()
    {
        GameObject GameScreenPrefabInstance = Instantiate(GameScreenPrefab,transform.parent);
        if (GameScreenPrefabInstance != null)
        {
            var GameScreenScriptReference = GameScreenPrefabInstance.GetComponent<GameScreenScript>();
            if (GameScreenScriptReference != null)
            {
                var currentLevel = User.Instance.CurrentLevel;
                GameScreenScriptReference.SetGameBoard(Words.WordsInstance.WordsList[currentLevel-1].word,currentLevel);
            }
           
        }
        Debug.Log("destroy game object .....");
        Destroy(gameObject);
    }
}
