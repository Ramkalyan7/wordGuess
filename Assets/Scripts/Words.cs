using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Word
{
    public string word;
    public string hint;

    public Word(string word,string hint)
    {
        this.word = word;
        this.hint = hint;
    }
}

[Serializable]
public class Words
{
    public List<Word> WordsList;

    private static Words _wordsInstance;

    public static Words WordsInstance
    {
        get
        {
            if (_wordsInstance == null)
            {
                _wordsInstance = new Words();
            }

            return _wordsInstance;
        }

        set
        {
            _wordsInstance = value;
        }
    }

    public Words()
    {
        WordsList = new List<Word>();
    }
    public Words(List<Word> l)
    {
        this.WordsList = new List<Word>(l);
    }
}
