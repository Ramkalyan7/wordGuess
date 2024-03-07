using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChanceScript : MonoBehaviour
{
    [SerializeField] private TMP_Text[] textItems = new TMP_Text[5];

    public void SetText(string enteredAlphabet,int index)
    {
        textItems[index].text = enteredAlphabet;
    }

    public string GetString()
    {
        string enteredString = "";
        for (int i = 0; i < 5; i++)
        {
            enteredString += textItems[i].text;
        }

        return enteredString;
    }

    public void ColorTheString(string solutionString)
    {
        solutionString = solutionString.ToUpper();
        for (int i = 0; i < 5; i++)
        {
            Image currentTextsImageReference =textItems[i].gameObject.GetComponentInParent<Image>();
            if (textItems[i].text[0]==solutionString[i])
            {
                currentTextsImageReference.color = new Color(0, 1, 0, 1);
                
            }
            else if (solutionString.Contains(textItems[i].text))
            {
               currentTextsImageReference.color = new Color(1, 0.92f, 0.016f, 1);
            }
            
            else
            {
                currentTextsImageReference.color = new Color(0.5f, 0.5f, 0.5f, 1);
                //disble this key
                
                gameObject.GetComponentInParent<GameScreenScript>().DisableKeyBoardKey(textItems[i].text);
            }
          
        }
    }
}
