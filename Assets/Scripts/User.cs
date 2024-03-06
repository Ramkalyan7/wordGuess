using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using JetBrains.Annotations;
using UnityEngine;


[Serializable]
public struct Chance
{
    public int ChanceNumber;
    public string StringEntered;
    //different states of StateOfTheChance are used , unused and inbetween.
    public string StateOfTheChance;
}


[Serializable]
public class User 
{ 
    public string UserName;
    public int CurrentLevel=0;
    //different states are notattempted , incomplete and failed.
    //public string StateOfTheLevel;
     public List<Chance> Chances;
     
     //single ton Instance
     private static User _instance;
     public static User Instance {
         get
         {
             if (_instance == null)
             {
                 _instance = new User();
             }

             return _instance;
         }

         set
         {
             _instance = value;
         }
         
     }
     

}


