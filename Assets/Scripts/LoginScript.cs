
using System;
using System.IO;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LoginScript : MonoBehaviour
{
     [SerializeField] private TMP_InputField LoginUserNameInputField;
     [SerializeField] private Button LoginButton;
     [SerializeField] private Button SaveButton;
     [SerializeField] private Button CancelButton;
     [SerializeField] private GameObject HomeScreenPrefab;
     [SerializeField] private TMP_Text EditUserNameText;
     
     public static Action DestroyUserNameEditGameOject ;

     
     private void Start()
     {
         SaveButton.interactable = false;
     }

     private void Update()
     {
         SetSaveButtonInteractableState();
     }

     private void SetSaveButtonInteractableState()
     {
         if (LoginUserNameInputField.text.Length > 0)
         {
             SaveButton.interactable = true;
         }
         else
         {
             SaveButton.interactable = false;
         }
     }

     public void SaveUserName()
     {
         User.Instance.UserName = LoginUserNameInputField.text;
       
         string jsonString = JsonUtility.ToJson(User.Instance);
        
         File.WriteAllText(Constants.SAVEFILE,jsonString);
        
         DestroyUserNameEditGameOject?.Invoke();
     }

     public void CancelEditUserName()
     {
         DestroyUserNameEditGameOject?.Invoke();
     }

   
     
    public void HandleLogin()
    {
        if (LoginUserNameInputField.text.Length == 0)
        {
            return;
        }
        User.Instance.UserName = LoginUserNameInputField.text;
        string jsonString = JsonUtility.ToJson(User.Instance);
        File.WriteAllText(Constants.SAVEFILE,jsonString);
        Destroy(gameObject);
        Instantiate(HomeScreenPrefab,transform.parent);
    }
    
    public void InitLoginScreen()
    {
        SaveButton.gameObject.SetActive(false);
        CancelButton.gameObject.SetActive(false);
      
    }

    public void InitEditUserNameScreen()
    {   
        SaveButton.gameObject.SetActive(true);
        CancelButton.gameObject.SetActive(true);
        EditUserNameText.gameObject.SetActive(true);
        
        LoginButton.gameObject.SetActive(false);   
    }
}
