using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SplashScreenScript : MonoBehaviour
{
    
    [SerializeField] private GameObject LoginScreenPrefab;
    [SerializeField] private GameObject HomeScreenPrefab;
    [SerializeField] private GameObject SplashScreen;
  
  


    public void HandleEnterButtonClick()
    {
        if (File.Exists(Constants.SAVEFILE))
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
