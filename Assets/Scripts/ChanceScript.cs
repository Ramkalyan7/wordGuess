using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChanceScript : MonoBehaviour
{
    [SerializeField] private TMP_Text[] textItems = new TMP_Text[5];

    public void setText(string enteredAlphabet,int index)
    {
        textItems[index].text = enteredAlphabet;
    }
}
