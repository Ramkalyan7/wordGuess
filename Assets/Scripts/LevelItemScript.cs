using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelItemScript : MonoBehaviour
{

    public static Action onButtonClicked;
    public static Action LockedLevelIsClicked;

    // Start is called before the first frame update
    [SerializeField] private TMP_Text LevelText;
    [SerializeField] private Image StatusImage;
    [SerializeField] private Button LevelItem;
    [SerializeField] private Sprite CurrentLevelSprite;
    [SerializeField] private Sprite LockedLevelSprite;
    [SerializeField] private Sprite CompletedLevelSprite;
   
    
    private int LevelNumber;
    
    

  

  

    public void SetLevelsTextAndStatus(int  levelIndex)
    {

        LevelText.text = "Level "+(levelIndex+1);
        LevelNumber = levelIndex + 1;
      
        //StatusText.text = statusText;
        Image LevelItemImageComponentReference = LevelItem.GetComponent<Image>();

        var currentLevelOfUser = User.Instance.CurrentLevel;
        if (levelIndex + 1 > currentLevelOfUser)
        {
            //StatusText.text = "Locked";
            StatusImage.sprite = LockedLevelSprite;
            StatusImage.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
            LevelItemImageComponentReference.color=new Color(0.5f, 0.5f, 0.5f,0.5f);
        }
        else if (levelIndex + 1 < currentLevelOfUser)
        {
           // StatusText.text = "Completed";
           StatusImage.sprite = CompletedLevelSprite;
           StatusImage.color = new Color(0, 1, 0.5f, 1);
            LevelItemImageComponentReference.color = new Color(0, 1, 0, 0.5f);
            
        } 
        else
        {
            //StatusText.text = "current";
            StatusImage.sprite = CurrentLevelSprite;
            StatusImage.gameObject.transform.localRotation = Quaternion.Euler(0, 0, 180);
            
            StatusImage.color = new Color(1, 0.92f, 0.016f, 0.5f);
            LevelItemImageComponentReference.color = new Color(1, 0.92f, 0.016f, 0.5f);
        }
    }

    public void HandleLevelItemClick()
    {
        if (LevelNumber > User.Instance.CurrentLevel)
        {
          LockedLevelIsClicked?.Invoke();
            
        }
        else
        {
            User.Instance.CurrentPlayingLevel = LevelNumber;
            onButtonClicked?.Invoke();
        }
        
    }

  
}


//steps :
//show the status text according to the user current level