using System.IO;
using TMPro;
using UnityEngine;

public class HomeScreenScript : MonoBehaviour
{
        
        
    //In home screen i have to get the user data that exists and render it eg :user name
    //then I  have to parse the config file and render the levels
    //then by using the current level field of the user i have to render the status of the text accordingly.
    
    [SerializeField] private TMP_Text UserNameText;
    [SerializeField] private GameObject LevelItemPrefab;
    [SerializeField] private RectTransform ParentRectTransform;
    
    
    private void Awake()
    {
       LevelItemScript.onButtonClicked += DestroyGameObject;
    }

    public void DestroyGameObject()
    {
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
            leveItemScriptReference.SetValuesOfText(index, wordItem.word, transform.parent);
            index++;
        }
    }
    

    private void OnDestroy()
    {
        LevelItemScript.onButtonClicked -= DestroyGameObject;
    }
}
