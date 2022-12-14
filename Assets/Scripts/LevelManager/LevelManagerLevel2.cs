using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelManagerLevel2 : MonoBehaviour
{
    public static LevelManagerLevel2 instance;

    // Other Game parameters can be added here! like health, time, etc;
    [SerializeField] SendToGoogle sendToGoogle;
    [SerializeField] BulletController bulletController;
    [SerializeField] BulletPowerUpController bulletPowerUpController;


    public static String levelWord = "";
    public List<TMP_Text> blankList = new List<TMP_Text>();
    public List<TMP_Text> correctLetterList = new List<TMP_Text>();
    public GameObject blankPrefab;
    public GameObject correctLetterPrefab;
    public GameObject hintImage;

    public Transform blankHolder;
    public Transform correctLetterHolder;

    public static Dictionary<int, Char> letterMap = new Dictionary<int, Char>();
    public float letterSpeed = 1.5f;
    public float rockSpeed = 2.0f;
    public Boolean altVersion = false;


    public float timeStart;
    public float timeFinished;
    public double timeToComplete;
    public int level2Bullets = 20;
    public int availableBullets;
    public int totalLettersShot = 0;
    public int characterShot = 0;

    public Dictionary<String, int> pairs = new Dictionary<String, int>()
    {
        {"Tutorial",0},{ "Planet", 1 }, { "Animals", 2 },{"Country",3},{"Sport",4}
    };
    public Dictionary<TMP_Text, bool> correctLetterPairs = new Dictionary<TMP_Text, bool>();

    private int currentLevel = 1;
    private int totalPowerUpsCollected;
    private int totalPowerUpsGenerated;
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

        List<string> level_words = new List<string>
        {
            "HORSE", "DOG", "TIGER", "LION","SNAKE"
        };

        // HORSE : A knight rides on a ...
        // DOG : Man's best friend
        // TIGER : The largest cat species
        // LION : King of the jungle
        // SNAKE : Related to Slytherin

        int index = random.Next(level_words.Count);
        levelWord = level_words[index];

        // Pass Values to GameManager
        GameManager.instance.Level = 2;
        GameManager.instance.switchColor = true;
        GameManager.instance.LevelWord = levelWord;
        GameManager.instance.LetterSpeed = letterSpeed;
        GameManager.instance.RockSpeed = rockSpeed;
        GameManager.instance.bullets = level2Bullets;
        GameManager.instance.genWordDistanceDictionary();

        GameManager.instance.altVersion = altVersion;

        availableBullets = level2Bullets;

        GameManager.instance.penultimate = false;
        GameManager.instance.wordCompleted = false;
        GameManager.instance.gameWon = false;
        GameManager.instance.oneMinLeft = false;

    }

    void OnEnable()
    {
        EventManager.StartListening("test", SomeFunction);
    }

    void OnDisable()
    {
        EventManager.StopListening("test", SomeFunction);
    }

    void SomeFunction(Dictionary<string, object> message)
    {
        //Debug.Log(message);
        var val = (Collider2D)message["amount"];
        // Debug.Log ($"{val.name[0]} received test!");
        // Debug.Log ("Some Function was called!: ");
        totalLettersShot += 1;
        GameManager.instance.totalLettersShot = totalLettersShot;
        Boolean letterMatched = false;
        for (int itr = 0; itr < levelWord.Length; itr++)
        {
            if (levelWord[itr] == val.name[0] && letterMap[itr] != val.name[0])
            {
                letterMap[itr] = val.name[0];
                break;
            }
        }

    }

    private IEnumerator timeDelayHintHide()
    {

        hintImage = FindAllObject(GameObject.Find("Hints/Canvas"), levelWord);
        if(hintImage.activeSelf == true)
        {
            yield return new WaitForSeconds(8.0f);
            hintImage.SetActive(false);
        }
        
    }

    public static GameObject FindAllObject(GameObject parent, string name)
    {
     Transform[] trs= parent.GetComponentsInChildren<Transform>(true);
     foreach(Transform t in trs){
         if(t.name == name){
              return t.gameObject;
         }
     }
     return null;
    }

    public void showHint()
    {   
        hintImage = FindAllObject(GameObject.Find("Hints/Canvas"), levelWord);
        hintImage.SetActive(true);
    }

    public void setLetterFromHint()
    {
        print("Entering this 5");
        print(levelWord.Length);
        for (int itr = 0; itr < levelWord.Length; itr++)
        {
            print(letterMap[itr]);
            if (letterMap[itr] == '/')
            {
                letterMap[itr] = levelWord[itr];
                print("LetterMap");
                print(letterMap[itr]);
                break;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Initialise();
        timeStart = Time.time;
        GameManager.instance.bullets = level2Bullets;
        GameManager.instance.Start();
    }

    // Update is called once per frame
    void Update()
    {
        GameManager.instance.LevelWord = levelWord;
        for (int i = 0; i < blankList.Count; i++)
        {
            if (letterMap[i] != '/')
            {
                TMP_Text blankUpdate = blankList[i].GetComponent<TMP_Text>();
                TMP_Text correctLetterUpdate = correctLetterList[i].GetComponent<TMP_Text>();

                blankUpdate.text = letterMap[i].ToString();
                blankUpdate.fontSize = 100f;
                if (correctLetterPairs[correctLetterList[i].GetComponent<TMP_Text>()] == false)
                {
                    // Debug.Log("Inside the first condition");
                    StartCoroutine(HandleIt(i));
                }
                else
                {
                    // Debug.Log("Inside the else condition");
                    correctLetterList[i].GetComponent<TMP_Text>().color = new Color32(164, 164, 164, 0);
                }
                correctLetterUpdate.text = "O".ToString();
                correctLetterUpdate.fontSize = 180f;

                RectTransform RectTransform = blankList[i].GetComponent<RectTransform>();
                RectTransform.offsetMax = new Vector2(RectTransform.offsetMax.x, -60);
                RectTransform.offsetMin = new Vector2(RectTransform.offsetMin.x, -60);

                RectTransform CorrectLetterRectTransform = correctLetterList[i].GetComponent<RectTransform>();
                CorrectLetterRectTransform.offsetMax = new Vector2(CorrectLetterRectTransform.offsetMax.x, -70);
                CorrectLetterRectTransform.offsetMin = new Vector2(CorrectLetterRectTransform.offsetMin.x, -110);
            }

        }

        int count = 0;
        int hintThreshold = blankList.Count / 2;
        for (int i = 0; i < blankList.Count; i++)
        {
            if (letterMap[i] != '/')
            {
                count = count + 1;
            }
            if (count >= hintThreshold && GameManager.instance.switchColor == true)
            {
                GameManager.instance.switchColor = false;
            }
        }
        characterShot = count;
        GameManager.instance.characterShotCount = characterShot;

        if (count == levelWord.Length - 1)
        {
            GameManager.instance.penultimate = true;
        }

        if (count == levelWord.Length)
        {
            GameManager.instance.wordCompleted = true;
            if (GameManager.instance.gameWon == true)
            {
                StartCoroutine(SetWinText());
            }
        }
        StartCoroutine(timeDelayHintHide());
    }
    private IEnumerator HandleIt(int i)
    {
        correctLetterList[i].GetComponent<TMP_Text>().color = new Color32(164, 164, 164, 255);
        yield return new WaitForSeconds(1.0f);
        correctLetterPairs[correctLetterList[i].GetComponent<TMP_Text>()] = true;
    }

    IEnumerator SetWinText()
    {
        GameManager.instance.penultimate = false;
        GameManager.instance.wordCompleted = false;
        GameManager.instance.gameWon = false;
        GameManager.instance.oneMinLeft = false;
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);

        //UnityEngine.SceneManagement.SceneManager.LoadScene(pairs[UnityEngine.SceneManagement.SceneManager.GetActiveScene().name]+1);
        GameManager.instance.winScreen();

    }
    void Initialise()
    {

        for (int i = 0; i < levelWord.Length; i++)
        {
            letterMap.Add(i, '/');
            GameObject blankHelper = Instantiate(blankPrefab, blankHolder, false);
            GameObject correctLetterHelper = Instantiate(correctLetterPrefab, correctLetterHolder, false);

            RectTransform RectTransform = blankHelper.GetComponent<RectTransform>();
            RectTransform CorrectLetterRectTransform = correctLetterHelper.GetComponent<RectTransform>();

            float divider = (float)1 / levelWord.Length;
            float x_minval = i * divider;
            float x_maxval = (i + 1) * divider;
            RectTransform.anchorMin = new Vector2(x_minval, 0);
            RectTransform.anchorMax = new Vector2(x_maxval, 1);
            RectTransform.offsetMin = new Vector2(-400, RectTransform.offsetMin.y);
            RectTransform.offsetMax = new Vector2(-407, RectTransform.offsetMax.y);
            RectTransform.offsetMax = new Vector2(RectTransform.offsetMax.x, -40);
            RectTransform.offsetMin = new Vector2(RectTransform.offsetMin.x, -40);

            CorrectLetterRectTransform.anchorMin = new Vector2(x_minval, 0);
            CorrectLetterRectTransform.anchorMax = new Vector2(x_maxval, 1);
            CorrectLetterRectTransform.offsetMin = new Vector2(-400, CorrectLetterRectTransform.offsetMin.y);
            CorrectLetterRectTransform.offsetMax = new Vector2(-407, CorrectLetterRectTransform.offsetMax.y);
            CorrectLetterRectTransform.offsetMax = new Vector2(CorrectLetterRectTransform.offsetMax.x, -40);
            CorrectLetterRectTransform.offsetMin = new Vector2(CorrectLetterRectTransform.offsetMin.x, -40);

            blankList.Add(blankHelper.GetComponent<TMP_Text>());
            correctLetterList.Add(correctLetterHelper.GetComponent<TMP_Text>());
            correctLetterPairs.Add(correctLetterHelper.GetComponent<TMP_Text>(), false);
        }
    }

    private void OnDestroy()
    {
        // End Analytics Call here
        if (this != null)
        {
            timeFinished = Time.time;
            availableBullets = bulletController.availableBullets;
            timeToComplete = Math.Round(timeFinished - timeStart, 2);
            if (timeToComplete > 0 && timer.currentTime > 0 && playerMain.currentHealth > 0)
            {
                if (availableBullets > 1)
                {
                    totalPowerUpsGenerated = GameManager.instance.bulletPowerUpController.getTotalPowerUpsGenerated() + GameManager.instance.healthPowerUpController.getTotalPowerUpsGenerated() + GameManager.instance.shieldPowerUpController.getTotalPowerUpsGenerated();
                    totalPowerUpsCollected = GameManager.instance.bulletPowerUpController.getTotalPowerUpsCollected() + GameManager.instance.healthPowerUpController.getTotalPowerUpsCollected() + GameManager.instance.shieldPowerUpController.getTotalPowerUpsCollected();
                    currentLevel = pairs[UnityEngine.SceneManagement.SceneManager.GetActiveScene().name];
                    sendToGoogle.UpdateLevelAnalytics(currentLevel, timeToComplete);
                    sendToGoogle.UpdateUnsuccessfulTriesAnalytics(pairs[UnityEngine.SceneManagement.SceneManager.GetActiveScene().name], true);
                    sendToGoogle.UpdateHealthbarAnalytics(currentLevel, playerMain.currentHealth);
                    sendToGoogle.UpdateCorrectLettersShotAnalytics(pairs[UnityEngine.SceneManagement.SceneManager.GetActiveScene().name], totalLettersShot, characterShot, "level2 source");
                    sendToGoogle.UpdatePowerUpsUsageAnalytics(pairs[UnityEngine.SceneManagement.SceneManager.GetActiveScene().name], totalPowerUpsGenerated, totalPowerUpsCollected);
                }
            }
        }
        letterMap = new Dictionary<int, char>();
    }
}
