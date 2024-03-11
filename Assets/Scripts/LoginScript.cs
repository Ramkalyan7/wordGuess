using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LoginScript : MonoBehaviour
{
     [SerializeField] private TMP_InputField LoginUserNameInputField;
     [SerializeField] private Button LoginButton;
     private string saveFile;
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
        User.Instance.UserName = LoginUserNameInputField.text;
        string jsonString = JsonUtility.ToJson(User.Instance);
        File.WriteAllText(saveFile,jsonString);
        Destroy(gameObject);
        Instantiate(HomeScreenPrefab,transform.parent);
    }
}
