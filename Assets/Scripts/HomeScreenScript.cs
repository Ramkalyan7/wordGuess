using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HomeScreenScript : MonoBehaviour
{
        
        
    //In home screen i have to get the user data that exists and render it eg :user name
    //then I  have to parse the config file and render the levels
    //then by using the current level field of the user i have to render the status of the text accordingly.
    
    [SerializeField] private TMP_Text UserNameText;
    [SerializeField] private GameObject LevelItemPrefab;
    [SerializeField] private RectTransform ParentRectTransform;
    [SerializeField] private GameObject GameScreenPrefab;
    [SerializeField] private GameObject ToastPrefab;
    private GameObject ToastPrefabReference;
    [SerializeField] private GameObject LoginScreenPrefab;
    private GameObject LoginScreenPrefabReference;
    [SerializeField] private Button EditButton;
    [SerializeField] private ScrollRect LevelItemsScrollRect;
    
    
    
    private void Awake()
    {
       LevelItemScript.onButtonClicked += InstantiateGameScreenAndDestroyHomeScreenGameObject;
       LevelItemScript.LockedLevelIsClicked += ShowToast;
       LoginScript.DestroyUserNameEditGameOject += DestroyEditUserNameReference;
       //load dictionary
       
       LoadDictionary();
    }

    public void InstantiateGameScreenAndDestroyHomeScreenGameObject()
    {
        GameObject GameScreenPrefabInstance = Instantiate(GameScreenPrefab,transform.parent);
        if (GameScreenPrefabInstance != null)
        {
            var GameScreenScriptReference = GameScreenPrefabInstance.GetComponent<GameScreenScript>();
            if (GameScreenScriptReference != null)
            {
                var currentLevel = User.Instance.CurrentPlayingLevel;
                GameScreenScriptReference.SetGameBoard(Words.WordsInstance.WordsList[currentLevel-1].word,currentLevel);
            }
           
        }
        Debug.Log("destroy game object .....");
        Destroy(gameObject);
    }
    void Start()
    {
        if (File.Exists(Constants.SAVEFILE))
        {
            ReadUserData();
            ReadConfigFile();

            RenderGameLevels();
        }

    }

    void ReadUserData()
    {
        string fileContents = File.ReadAllText(Constants.SAVEFILE);
        User.Instance = JsonUtility.FromJson<User>(fileContents);
        User.Instance.CurrentPlayingLevel = User.Instance.CurrentLevel;
        RenderUserName(); 
    }

    void RenderUserName()
    {
        UserNameText.text = User.Instance.UserName;
    }

    void ReadConfigFile()
    {
        var configFile= Resources.Load<TextAsset>($"guess");
        string configFileContents = configFile.text;
        Debug.LogError($"config data :- {configFileContents}");
         Words.WordsInstance = JsonUtility.FromJson<Words>(configFileContents);
    }

    void RenderGameLevels()
    {
        var index = 0;
        foreach (var wordItem in Words.WordsInstance.WordsList)
        {   
            var levelItemInstance= Instantiate(LevelItemPrefab, ParentRectTransform);
            if (levelItemInstance == null) continue;
            var leveItemScriptReference = levelItemInstance.GetComponent<LevelItemScript>();
            if (leveItemScriptReference == null) continue;
            leveItemScriptReference.SetLevelsTextAndStatus(index);
            index++;
        }
        LevelItemsScrollRect.verticalNormalizedPosition = 1-(float)User.Instance.CurrentLevel/Words.WordsInstance.WordsList.Count;
    }
    

    private void OnDestroy()
    {
        if (true)
        {
            ShowToast();
        }
        ShowToast();
        LevelItemScript.onButtonClicked -= InstantiateGameScreenAndDestroyHomeScreenGameObject;
        LevelItemScript.LockedLevelIsClicked -= ShowToast;
        LoginScript.DestroyUserNameEditGameOject -= DestroyEditUserNameReference;

    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif

    } 

    void ShowToast()
    {
        if (ToastPrefabReference == null)
        {
            ToastPrefabReference = Instantiate(ToastPrefab,gameObject.transform);
        }
        if (ToastPrefabReference != null)
        {
            Invoke(nameof(DestroyToast),1.5f);
        }
    }

    void DestroyToast()
    {
        Destroy(ToastPrefabReference);
    }

    private void LoadDictionary()
    {
        var textFile = Resources.Load("Dictionary") as TextAsset;
        string dictionaryWords = textFile.text;
        DictionaryWords.Instance.dictionary = new List<string>(dictionaryWords.Split('\n'));
    }

    public void ShowEditUserNameGameObject()
    {
        LoginScreenPrefabReference = Instantiate(LoginScreenPrefab, gameObject.transform);
        LoginScreenPrefabReference.GetComponent<LoginScript>().InitEditUserNameScreen();
        EditButton.gameObject.SetActive(false);
    }

    public void DestroyEditUserNameReference()
    {
        Destroy(LoginScreenPrefabReference);
        EditButton.gameObject.SetActive(true);
        RenderUserName();
    }
}
