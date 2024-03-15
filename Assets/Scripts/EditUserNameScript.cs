
using System;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EditUserNameScript : MonoBehaviour
{
    [SerializeField] private Button SaveButton;
    [SerializeField] private Button CancelButton;
    [SerializeField] private Button SubmitButton;

    [SerializeField] private TMP_InputField UserNameInputField;

    public static Action  DestroyUserNameEditGameOject ;

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
        if (UserNameInputField.text.Length > 0)
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
        User.Instance.UserName = UserNameInputField.text;
       
        string jsonString = JsonUtility.ToJson(User.Instance);
        
        File.WriteAllText(Constants.SAVEFILE,jsonString);
        
        DestroyUserNameEditGameOject?.Invoke();
    }

    public void CancelEditUserName()
    {
        DestroyUserNameEditGameOject?.Invoke();
    }



}
