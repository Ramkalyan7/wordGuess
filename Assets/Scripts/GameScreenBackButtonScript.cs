using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScreenBackButtonScript : MonoBehaviour
{

    [SerializeField] private Button BackButton;
    [SerializeField] private GameObject HomeScreenPrefab;
    
    void Start()
    {
     BackButton.onClick.AddListener(HandleBackButtonClick);   
    }

    void HandleBackButtonClick()
    {
        //instantiate home screen 
        
       Instantiate(HomeScreenPrefab,GameObject.FindGameObjectWithTag("Canvas").transform);
        
        
        //Destroy Game Screen
        
        Destroy(transform.parent.gameObject);
    }
   
}
