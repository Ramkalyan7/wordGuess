using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SplashScreenScript : MonoBehaviour
{
    
    [SerializeField] private Button EnterButton;
    private string saveFile;
    [SerializeField] private GameObject LoginScreenPrefab;
    [SerializeField] private GameObject HomeScreenPrefab;
    [SerializeField] private GameObject SplashScreen;
    private void Awake()
    {
        saveFile= Application.persistentDataPath + "/gamedata.json";
    }

    void Start()
    {
        EnterButton.onClick.RemoveAllListeners();
        EnterButton.onClick.AddListener(HandleEnterButtonClick);
    }


    private void HandleEnterButtonClick()
    {
        if (File.Exists(saveFile))
        {
           Instantiate(HomeScreenPrefab,transform.parent);
        }
        else
        {
            Instantiate(LoginScreenPrefab, transform.parent);
        }
        Destroy(gameObject);
        Debug.Log("Enter button is clicked");
    }
}
