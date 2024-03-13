using UnityEngine;

public class GameScreenBackButtonScript : MonoBehaviour
{

    [SerializeField] private GameObject HomeScreenPrefab;
    
    

   public void HandleBackButtonClick()
    {
        //instantiate home screen 
        var gameScreenGameOject = transform.parent.gameObject;
        
      Instantiate(HomeScreenPrefab,gameScreenGameOject.transform.parent);
       
        
        
        //Destroy Game Screen
        
        Destroy(gameScreenGameOject);
    }
   
}
