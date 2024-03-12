
using System.IO;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LoginScript : MonoBehaviour
{
     [SerializeField] private TMP_InputField LoginUserNameInputField;
     [SerializeField] private Button LoginButton;
     
     [SerializeField] private GameObject HomeScreenPrefab;

   
     
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
}
