using UnityEngine;

public class ChanceScript : MonoBehaviour
{
    [SerializeField] private GameScreenTextBoxScript[] textItems = new GameScreenTextBoxScript[5];

    public void SetText(string enteredAlphabet,int index)
    {
        textItems[index].text.text = enteredAlphabet;
        if (index == 4)
        {
            textItems[index].image.color = Constants.WHITECOLOR;
        }
    }

    public string GetString()
    {
        string enteredString = "";
        for (int i = 0; i < 5; i++)
        {
            enteredString += textItems[i].text.text;
        }

        return enteredString;
    }

    public void HighlightCurrentActiveRow()
    {
      
        for (int i = 0; i < 5; i++)
        {
            textItems[i].image.color = Constants.SKYBLUECOLOR;
        }
        
    }

    public void ColorTheString(string solutionString)
    {
        var GameScreenScriptReference = gameObject.gameObject.GetComponentInParent<GameScreenScript>();
        solutionString = solutionString.ToUpper();
        for (int i = 0; i < 5; i++)
        {
            if (textItems[i].text.text[0].Equals( solutionString[i]))
            {
                textItems[i].image.color = Constants.GREENCOLOR;
                textItems[i].text.color = Constants.WHITECOLOR;
                textItems[i].state = States.CorrectlyPositionedText;
                //textItems[i].text

            }
            else if (!solutionString.Contains(textItems[i].text.text))
            {
                textItems[i].image.color = Constants.REDCOLOR;
                textItems[i].text.color = Constants.WHITECOLOR;
                textItems[i].state = States.NotExistingText;
                

                //disable this key

                GameScreenScriptReference.ColorKeyBoardKeys(textItems[i].text.text,textItems[i].state);
            }
            else
            {
                textItems[i].image.color = Constants.GRAYCOLOR;
                textItems[i].text.color = Constants.WHITECOLOR;
                textItems[i].state = States.None;
            }

        }

        for (int i = 0; i < 5; i++)
        {
           // Image currentTextsImageReference = textItems[i].image;

            int actualCount = 0;
            int countInEnteredString = 0;
            int markedGreenCount = 0;

            for (int j = 0; j <i; j++)
            {
                var currentColor = textItems[j].image.color;

                if (textItems[j].text.text.ToUpper().Equals(textItems[i].text.text.ToUpper()) &&
                    !textItems[j].state.Equals(States.CorrectlyPositionedText))
                {
                    countInEnteredString++;                
                }
            }

            for (int j = 0; j < 5; j++)
            {
                
                if (textItems[i].text.text.ToUpper().Equals(textItems[j].text.text.ToUpper()) && textItems[j].state.Equals(States.CorrectlyPositionedText))
                {   
                    markedGreenCount++;
                   
                }
                if (solutionString[j].ToString().ToUpper().Equals(textItems[i].text.text.ToUpper()))
                {
                    actualCount++;
                }
            }

            if ((markedGreenCount + countInEnteredString) < actualCount && solutionString.ToUpper().Contains(textItems[i].text.text.ToUpper()) 
                                                                        && !textItems[i].state.Equals(States.CorrectlyPositionedText))
            {
                textItems[i].image.color = Constants.YELLOWCOLOR;
                textItems[i].text.color = Constants.WHITECOLOR;
                textItems[i].state = States.CorrectEnteredButWrongPositionText;
                GameScreenScriptReference.ColorKeyBoardKeys(textItems[i].text.text,textItems[i].state);

            } 
          
        }

    }
}
