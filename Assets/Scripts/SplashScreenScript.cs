using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SplashScreenScript : MonoBehaviour
{
    
    private string saveFile;
    [SerializeField] private GameObject LoginScreenPrefab;
    [SerializeField] private GameObject HomeScreenPrefab;
    [SerializeField] private GameObject SplashScreen;
    private void Awake()
    {
        saveFile = Constants.SaveFile;
    }

  


    public void HandleEnterButtonClick()
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
