using System.IO;
using UnityEngine;

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
            GameObject LoginScreenPrefabReference=Instantiate(LoginScreenPrefab, transform.parent);
            LoginScreenPrefabReference.GetComponent<LoginScript>().InitLoginScreen();
        }
        Destroy(gameObject);
        Debug.Log("Enter button is clicked");
    }
}
