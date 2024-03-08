using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SplashScreenScript : MonoBehaviour
{
    // Start is called before the first frame update
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
        EnterButton.onClick.AddListener(handleEnterButtonClick);
    }


    private void handleEnterButtonClick()
    {
        if (File.Exists(saveFile))
        {
            GameObject HomeScreenInstance = Instantiate(HomeScreenPrefab,transform.parent);
        }
        else
        {
            GameObject LoginScreenInstance = Instantiate(LoginScreenPrefab, transform.parent);
        }
        Destroy(gameObject);
        Debug.Log("Enter button is clicked");
    }
}
