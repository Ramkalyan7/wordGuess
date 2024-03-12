using UnityEngine;

public class GameScreenBackButtonScript : MonoBehaviour
{

    [SerializeField] private GameObject HomeScreenPrefab;
    
    

   public void HandleBackButtonClick()
    {
        //instantiate home screen 
        var parentGameObject = transform.parent.gameObject;
        
      Instantiate(HomeScreenPrefab,parentGameObject.transform.parent);
       
        
        
        //Destroy Game Screen
        
        Destroy(parentGameObject);
    }
   
}
