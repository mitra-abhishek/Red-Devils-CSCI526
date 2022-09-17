using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagerLevel1 : MonoBehaviour
{
    public static LevelManagerLevel1 instance;
    
    // Other Game parameters can be added here! like health, time, etc;

    public String levelWord = "";
    
    //private static Dictionary<int, List<string>> all_level_words = new Dictionary<int, List<string>>();
    
    private static System.Random random = new System.Random();

    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        //
        // all_level_words.Add(
        //         1, 
        //         new List<string> { 
        //             "CAT", "DOG", "PIN" 
        //         }
        //     );
        
        //List<string> level_words = all_level_words[1];

        List<string> level_words = new List<string>
        {
            "CAT", "DOG", "PIN"
        };
        
        int index = random.Next(level_words.Count);
        levelWord =  level_words[index];
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
