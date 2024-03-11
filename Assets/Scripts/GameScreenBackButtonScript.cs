using UnityEngine;
using UnityEngine.UI;

public class GameScreenBackButtonScript : MonoBehaviour
{

    [SerializeField] private GameObject HomeScreenPrefab;
    
    

   public void HandleBackButtonClick()
    {
        //instantiate home screen 
        
      Instantiate(HomeScreenPrefab,GameObject.FindGameObjectWithTag("Canvas").transform);
       
        
        
        //Destroy Game Screen
        
        Destroy(transform.parent.gameObject);
    }
   
}
