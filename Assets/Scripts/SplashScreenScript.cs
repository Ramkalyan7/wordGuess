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
        EnterButton.onClick.AddListener(handleEnterButtonClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void handleEnterButtonClick()
    {
        if (File.Exists(saveFile))
        {
            GameObject HomeScreenInstance = Instantiate(HomeScreenPrefab,GameObject.FindGameObjectWithTag("Canvas").transform);
            Destroy(gameObject);
        }
        else
        {
            GameObject LoginScreenInstance = Instantiate(LoginScreenPrefab, GameObject.FindGameObjectWithTag("Canvas").transform);
            Destroy(gameObject);
        }
    }
}
