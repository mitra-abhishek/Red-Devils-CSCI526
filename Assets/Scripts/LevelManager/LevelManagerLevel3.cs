using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelManagerLevel3 : MonoBehaviour
{
    public static LevelManagerLevel3 instance;
    
    // Other Game parameters can be added here! like health, time, etc;
    [SerializeField] SendToGoogle sendToGoogle;
    [SerializeField] BulletController bulletController;

    public String levelWord = "";
    public List<TMP_Text> blankList = new List<TMP_Text>();
    public GameObject blankPrefab;
    public Transform blankHolder;
    public Dictionary<int,Char> letterMap = new Dictionary<int,Char>();
    public float letterSpeed = 1.5f;
    public float rockSpeed = 3.5f;
    public int level3Bullets = 40;
    public int availableBullets;

    public float timeStart;
    public float timeFinished;
    public double timeToComplete;
    private float checktime;
     public Dictionary<String, int> pairs = new Dictionary<String, int>()
    {
        { "SampleScene 2", 1 }, { "Level 2", 2 },{"Level 3",3}
    };
    private int currentLevel=1;
    public PlayerMain playerMain;
    public Timer timer;


    
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

        // List<string> level_words = new List<string>
        // {
        //     "ACUTE", "BROAD", "CRAZY", "EXTRA", "FIFTH", "GROSS", "HARSH","YOUNG", "VAGUE", "TIGHT", "RURAL", "SMART","PRIME", "NAVAL"
        // };

        List<string> level_words = new List<string>
        {
            "GRAPE", "MANGO", "PEACH"
        };
       
        int index = random.Next(level_words.Count);
        levelWord =  level_words[index];
        
        // Pass Values to GameManager
        GameManager.instance.Level = 1;
        GameManager.instance.LevelWord = levelWord;
        GameManager.instance.LetterSpeed = letterSpeed;
        GameManager.instance.RockSpeed = rockSpeed;
        GameManager.instance.bullets = level3Bullets;
        availableBullets=level3Bullets;

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
        timeStart=Time.time;
        GameManager.instance.bullets = level3Bullets;
        GameManager.instance.Start();
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
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene 2");
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
        availableBullets=bulletController.availableBullets;
        Debug.Log("The available Bullets are"+availableBullets);
        if (timeToComplete>0 && timer.currentTime>0 && playerMain.currentHealth>0){
            if (availableBullets>1){
            currentLevel=pairs[UnityEngine.SceneManagement.SceneManager.GetActiveScene().name];            
            sendToGoogle.UpdateLevelAnalytics(currentLevel,timeToComplete);
            sendToGoogle.UpdateUnsuccessfulTriesAnalytics(pairs[UnityEngine.SceneManagement.SceneManager.GetActiveScene().name],true);
            sendToGoogle.UpdateHealthbarAnalytics(currentLevel,playerMain.currentHealth);
            }
            else{
                sendToGoogle.UpdateUnsuccessfulTriesAnalytics(pairs[UnityEngine.SceneManagement.SceneManager.GetActiveScene().name],false);
                UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene 2");
                }
        }
        }
        
    }
}
