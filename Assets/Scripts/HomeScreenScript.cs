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
    private string saveFile;
    // private string configFilePath = Application.dataPath + "JSON/guess.json";
    
    private void Awake()
    {
       saveFile=Application.persistentDataPath + "/gamedata.json";
       LevelItemScript.onButtonClicked += DestroyGameObject;
    }

    public void DestroyGameObject()
    {
        Debug.Log("destroy game object .....");
        Destroy(gameObject);
    }
    void Start()
    {
        if (File.Exists(saveFile))
        {
            ReadUserData();
        }
        ReadConfigFile();

        if (File.Exists(saveFile))
        {
           //render game levels
           RenderGameLevels();
        }
    }

    void ReadUserData()
    {
        // Debug.LogError($"reached here {Application.persistentDataPath}");
        string fileContents = File.ReadAllText(saveFile);
        User.Instance = JsonUtility.FromJson<User>(fileContents);
        RenderUserName(); 
    }

    void RenderUserName()
    {
        UserNameText.text = User.Instance.UserName;
    }

    void ReadConfigFile()
    {
        var a= Resources.Load<TextAsset>($"guess");
        string configFileContents = a.text;
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
            leveItemScriptReference.SetValuesOfText(index,wordItem.word);
            index++;
        }
    }
    

    private void OnDestroy()
    {
        LevelItemScript.onButtonClicked -= DestroyGameObject;
    }
}
