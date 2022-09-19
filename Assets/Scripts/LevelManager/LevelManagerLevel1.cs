using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManagerLevel1 : MonoBehaviour
{
    public static LevelManagerLevel1 instance;
    
    // Other Game parameters can be added here! like health, time, etc;

    public String levelWord = "";
    public List<TMP_Text> blankList = new List<TMP_Text>();
    public GameObject blankPrefab;
    public Transform blankHolder;
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

    void OnEnable ()
    {
        EventManager.StartListening ("test", SomeFunction);
    }

    void OnDisable ()
    {
        EventManager.StopListening ("test", SomeFunction);
    }

    void SomeFunction (Dictionary<string, object> message)
    {
        Debug.Log(message);
        var val = (Collider2D)message["amount"];
        Debug.Log ($"{val.name[0]} received test!");
        Debug.Log ("Some Function was called!: ");
    }

    // Start is called before the first frame update
    void Start()
    {
        Initialise();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Initialise(){
        for(int i = 0;i<3;i++)
        {
            GameObject blankHelper = Instantiate(blankPrefab,blankHolder,false);
            blankList.Add(blankHelper.GetComponent<TMP_Text>());
        }
    }
}
