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

    public static String levelWord = "";
    public List<TMP_Text> blankList = new List<TMP_Text>();
    public List<TMP_Text> correctLetterList = new List<TMP_Text>();

    public GameObject blankPrefab;
    public GameObject correctLetterPrefab;

    public Transform blankHolder;
    public Transform correctLetterHolder;

    public Dictionary<int, Char> letterMap = new Dictionary<int, Char>();
    public float timeStart;
    public float timeFinished;
    public double timeToComplete;
    public int totalLettersShot = 0;
    public int characterShot = 0;
    public Boolean altVersion = true;


    [SerializeField] SendToGoogle sendToGoogle;
    [SerializeField] BulletController bulletController;
    [SerializeField] BulletPowerUpController bulletPowerUpController;

    public PlayerMain playerMain;
    public Timer timer;

    public float letterSpeed = 1.5f;
    public float rockSpeed = 2.5f;
    public int level1Bullets = 20;
    // public float bulletPowerUpSpeed = 20.0f;

    public int availableBullets;
    public Dictionary<String, int> pairs = new Dictionary<String, int>()
        {
            {"Tutorial",0},{ "SampleScene 2", 1 }, { "Level 2", 2 },{"Level 3",3}
            //{"Tutorial",0},{ "Planet", 1 }, { "Country", 2 },{"Animals",3},{"Sport", 4}
        };
    public Dictionary<TMP_Text, bool> correctLetterPairs = new Dictionary<TMP_Text, bool>();

    private int currentLevel = 1;


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
            "EARTH", "MARS" ,"VENUS","SATURN"
        };

        int index = random.Next(level_words.Count);
        levelWord = level_words[index];

        // Pass Values to GameManager
        GameManager.instance.Level = 1;
        GameManager.instance.switchColor = true;
        GameManager.instance.LevelWord = levelWord;
        GameManager.instance.LetterSpeed = letterSpeed;
        GameManager.instance.RockSpeed = rockSpeed;
        GameManager.instance.bullets = level1Bullets;
        // GameManager.instance.bulletPowerUpSpeed = bulletPowerUpSpeed;
        GameManager.instance.genWordDistanceDictionary();
        GameManager.instance.createLetterSpawnArrayInitial();
        GameManager.instance.altVersion = altVersion;
        availableBullets = level1Bullets;

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
        Boolean letterMatched = false;
        totalLettersShot += 1;
        GameManager.instance.totalLettersShot = totalLettersShot;
        for (int itr = 0; itr < levelWord.Length; itr++)
        {
            if (levelWord[itr] == val.name[0])
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
        timeStart = Time.time;
        GameManager.instance.bullets = level1Bullets;
        GameManager.instance.Start();
    }

    // Update is called once per frame
    void Update()
    {
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
                    Debug.Log("Inside the first condition");
                    StartCoroutine(HandleIt(i));
                }
                else
                {
                    Debug.Log("Inside the else condition");
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
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
        //     if (pairs[UnityEngine.SceneManagement.SceneManager.GetActiveScene().name]+1 <=3){
        //     UnityEngine.SceneManagement.SceneManager.LoadScene(pairs[UnityEngine.SceneManagement.SceneManager.GetActiveScene().name]+1);
        // }
        // else{
        //      UnityEngine.SceneManagement.SceneManager.LoadScene(1);   
        //     }
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
        // count gives the total number of the correct characters shot:
        if (this != null)
        {
            timeFinished = Time.time;
            timeToComplete = Math.Round(timeFinished - timeStart, 2);
            availableBullets = bulletController.availableBullets;         // Available bullets==Bullets Shot

            //Debug.Log("The characters total shot"+totalLettersShot);
            if (timeToComplete > 0 && timer.currentTime > 0 && playerMain.currentHealth > 0)
            {
                if (availableBullets > 1)
                {
                    currentLevel = pairs[UnityEngine.SceneManagement.SceneManager.GetActiveScene().name];
                    sendToGoogle.UpdateLevelAnalytics(currentLevel, timeToComplete);
                    sendToGoogle.UpdateUnsuccessfulTriesAnalytics(pairs[UnityEngine.SceneManagement.SceneManager.GetActiveScene().name], true);
                    sendToGoogle.UpdateHealthbarAnalytics(currentLevel, playerMain.currentHealth);
                    sendToGoogle.UpdateCorrectLettersShotAnalytics(pairs[UnityEngine.SceneManagement.SceneManager.GetActiveScene().name], totalLettersShot, characterShot, "level1 source");
                    sendToGoogle.UpdatePowerUpsUsageAnalytics(pairs[UnityEngine.SceneManagement.SceneManager.GetActiveScene().name], GameManager.instance.bulletPowerUpController.getTotalPowerUpsGenerated(), GameManager.instance.bulletPowerUpController.getTotalPowerUpsCollected());
                }
                else
                {
                    // All bulltes over so available bullets=level1bullets;
                    sendToGoogle.UpdateUnsuccessfulTriesAnalytics(pairs[UnityEngine.SceneManagement.SceneManager.GetActiveScene().name], false);
                    sendToGoogle.UpdateResonForDeathAnalytics(pairs[UnityEngine.SceneManagement.SceneManager.GetActiveScene().name], "Bullet Finished");
                    //Debug.Log("THe total bullets shot is"+totalLettersShot+"Correct character shot"+characterShot);
                    sendToGoogle.UpdateCorrectLettersShotAnalytics(pairs[UnityEngine.SceneManagement.SceneManager.GetActiveScene().name], totalLettersShot, characterShot, "level1 source");
                    sendToGoogle.UpdatePowerUpsUsageAnalytics(pairs[UnityEngine.SceneManagement.SceneManager.GetActiveScene().name], bulletPowerUpController.getTotalPowerUpsGenerated(), bulletPowerUpController.getTotalPowerUpsCollected());
                    //UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene 2");
                    GameManager.instance.lossScreen();

                }
            }
        }
    }
}