using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;


public struct Chance
{
    public int ChanceNumber;
    public string StringEntered;
    //different states of StateOfTheChance are used , unused and inbetween.
    public string StateOfTheChance;
}


class User
{ 
    public string UserName;
    public int CurrentLevel;
    //different states are notattempted , incomplete and failed.
    //public string StateOfTheLevel;
     public List<Chance> Chances;
}


