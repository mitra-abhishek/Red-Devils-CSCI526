using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{  
    public static GameManager instance;

    public int Level = 1;

    public String LevelWord;

    public float LetterSpeed = 2.0f;
    
    public float RockSpeed = 2.0f;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        DontDestroyOnLoad(instance);
    }

    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
