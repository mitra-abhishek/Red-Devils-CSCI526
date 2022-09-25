using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelManagerLevel1 : MonoBehaviour
{
    public static LevelManagerLevel1 instance;
    
    // Other Game parameters can be added here! like health, time, etc;

    public String levelWord = "";
    public List<TMP_Text> blankList = new List<TMP_Text>();
    public GameObject blankPrefab;
    public Transform blankHolder;
    public Dictionary<int,Char> letterMap = new Dictionary<int,Char>();
    public float timeStart;
    public float timeFinished;
    public double timeToComplete;

    [SerializeField] SendToGoogle sendToGoogle;
    
    public float letterSpeed = 1.5f;
    public float rockSpeed = 2.5f;
    public Dictionary<String, int> pairs = new Dictionary<String, int>()
        {
            { "SampleScene 2", 1 }, { "Level 2", 2 },{"Level 3",3}
        };
    private int currentLevel=1;

    
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
        
        // Pass Values to GameManager
        GameManager.instance.Level = 1;
        GameManager.instance.LevelWord = levelWord;
        GameManager.instance.LetterSpeed = letterSpeed;
        GameManager.instance.RockSpeed = rockSpeed;

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
        Boolean letterMatched = false;
       for(int itr = 0;itr<levelWord.Length;itr++)
       {
           if(levelWord[itr]==val.name[0])
           {
               letterMap[itr] = val.name[0];
               break;
           }
       }

    }

    // Start is called before the first frame update
    void Start()
    {
        Initialise();
        // Start Monitoring for Analytics Here!
        timeStart=Time.time;

    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0;i<blankList.Count;i++)
        {
            if(letterMap[i]!='/')
            {
                TMP_Text blankUpdate = blankList[i].GetComponent<TMP_Text>();
                blankUpdate.text = letterMap[i].ToString();
                blankUpdate.fontSize = 110f;
            }
            
        }

        int count = 0;
        for(int i = 0;i<blankList.Count;i++)
        {    
            if(letterMap[i]!='/')
            {
                count = count + 1;
            }
        }
        if (count == levelWord.Length) {    
            StartCoroutine(SetWinText ());
        }
    }

    IEnumerator SetWinText () {
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
        if (pairs[UnityEngine.SceneManagement.SceneManager.GetActiveScene().name]+1 <=3){
        UnityEngine.SceneManagement.SceneManager.LoadScene(pairs[UnityEngine.SceneManagement.SceneManager.GetActiveScene().name]+1);
    }
    else{
         UnityEngine.SceneManagement.SceneManager.LoadScene(1);   
        }
    }
        
    void Initialise(){
        
        for(int i = 0;i<levelWord.Length;i++)
        {
            letterMap.Add(i,'/');
            GameObject blankHelper = Instantiate(blankPrefab,blankHolder,false);
            blankList.Add(blankHelper.GetComponent<TMP_Text>());
        }
        Debug.Log(letterMap);
    }

    private void OnDestroy()
    {   
        // End Analytics Call here
        if (this != null)
        {
        timeFinished=Time.time;
        timeToComplete=Math.Round(timeFinished-timeStart,2);
        if (timeToComplete>0){
             currentLevel=pairs[UnityEngine.SceneManagement.SceneManager.GetActiveScene().name];            
             sendToGoogle.UpdateLevelAnalytics(currentLevel,timeToComplete);
        }
        }
        
    }
}
