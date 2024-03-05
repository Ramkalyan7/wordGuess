using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using UnityEngine.SubsystemsImplementation;
using UnityEngine.UI;

public class LoginScript : MonoBehaviour
{
     [SerializeField] private TMP_InputField LoginUserNameInputField;
     [SerializeField] private Button LoginButton;
     private string saveFile;
     private User userData = new User();
     [SerializeField] private GameObject HomeScreenPrefab;

     private void Awake()
     {
         saveFile=Application.persistentDataPath + "/gamedata.json";
     }

     void Start()
    {
        LoginButton.onClick.AddListener(HandleLogin);
    }
     

    private void HandleLogin()
    {
        if (LoginUserNameInputField.text.Length == 0) return;
        userData.UserName = LoginUserNameInputField.text;
        string jsonString = JsonUtility.ToJson(userData);
        File.WriteAllText(saveFile,jsonString);
        Destroy(gameObject);
        GameObject HomeScreenInstance = Instantiate(HomeScreenPrefab,GameObject.FindGameObjectWithTag("Canvas").transform);
    }
}
