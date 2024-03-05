using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LoginScript : MonoBehaviour
{
    // Start is called before the first frame update
     [SerializeField] private TMP_InputField LoginUserNameInputField;
     [SerializeField] private Button LoginButton;
    void Start()
    {
        LoginButton.onClick.AddListener(handleLogin);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void handleLogin()
    {
        if (LoginUserNameInputField.text.Length == 0) return;
        
        
    }
}
