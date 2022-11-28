using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TutorialLevelManager : MonoBehaviour
{
    public static TutorialLevelManager instance;
    [SerializeField] SendToGoogle sendToGoogle;
    [SerializeField] BulletController bulletController;
    [SerializeField] BulletPowerUpController bulletPowerUpController;

    public static String levelWord = "";
    public List<TMP_Text> blankList = new List<TMP_Text>();
    public List<TMP_Text> correctLetterList = new List<TMP_Text>();

    public GameObject blankPrefab;
    public GameObject correctLetterPrefab;

    public Transform blankHolder;
    public Transform correctLetterHolder;

    public Dictionary<int, Char> letterMap = new Dictionary<int, Char>();
    public float letterSpeed = 1.5f;
    public int level3Bullets = 100;
    public float rockSpeed = 3.5f;
    public int availableBullets;
    public int totalLettersShot = 0;
    public int characterShot = 0;


    public float timeStart;
    public float timeFinished;
    public double timeToComplete;
    private float checktime;
    public Dictionary<String, int> pairs = new Dictionary<String, int>()
       {
        {"Tutorial",0},{ "Planet", 1 }, { "Animals", 2 },{"Country",3},{"Sport",4}
    };
    public Dictionary<TMP_Text, bool> correctLetterPairs = new Dictionary<TMP_Text, bool>();

    private int currentLevel = 1;
    public PlayerMain playerMain;
    public Timer timer;

    public GameObject rock;
    public GameObject enemies;
    public GameObject letters;
    public float rockShootingTime = 2f;
    public float enemyShootingTime = 2f;
    public float waitTimeForEnemies = 2f;
    public float waitTimeForLetterse = 2f;
    public bool isEnemiesSpawnOver = false;

    public bool rockActive = false;
    public bool enemiesActive = false;
    private bool lettersActive = false;

    public bool rockTutorialCompleted = false;
    public bool enemiesTutorialCompleted = false;
    public int globalPopUpIndex = 1;


    //private static Dictionary<int, List<string>> all_level_words = new Dictionary<int, List<string>>();

    private static System.Random random = new System.Random();






    public List<GameObject> popUps = new List<GameObject>();
    private int popUpIndex;
    // public GameObject rocks;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        List<string> level_words = new List<string>
        {
            "ATLANTIC","ARCTIC","PACIFIC","INDIAN"
        };

        int index = random.Next(level_words.Count);
        levelWord = level_words[index];

        // Pass Values to GameManager
        GameManager.instance.Level = 0;
        GameManager.instance.switchColor = true;
        GameManager.instance.LevelWord = levelWord;
        GameManager.instance.LetterSpeed = letterSpeed;
        GameManager.instance.RockSpeed = rockSpeed;
        GameManager.instance.bullets = level3Bullets;
        GameManager.instance.genWordDistanceDictionary();
        availableBullets = level3Bullets;

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
            if (levelWord[itr] == val.name[0] && letterMap[itr] != val.name[0])
            {
                letterMap[itr] = val.name[0];
                break;
            }
        }

    }

    public void setLetterFromHint(Dictionary<int, Char> letterMap)
    {
        // Debug.Log("Inside set letter from Hint" + letterMap.Count);
        // print("Entering this 5");
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
    void Start()
    {
        Initialise();
        timeStart = Time.time;
        GameManager.instance.bullets = level3Bullets;
        GameManager.instance.Start();
    }
    void Update()
    {
        for (int i = 0; i < popUps.Count; i++)
        {
            if (i == popUpIndex)
            {
                popUps[i].gameObject.SetActive(true);
            }
            else
            {
                popUps[i].gameObject.SetActive(false);
            }
        }
        if (popUpIndex == 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                Debug.Log("Inside the Index 0 showing Movement" + popUpIndex);
                popUpIndex++;
            }
        }
        else if (popUpIndex == 1)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.LeftArrow))
                {
                    popUpIndex++;

                }

                // if ((Input.GetKeyDown(KeyCode.LeftArrow) && (Input.GetKeyDown(KeyCode.UpArrow))) || (Input.GetKeyDown(KeyCode.RightArrow) && (Input.GetKeyDown(KeyCode.UpArrow))))
                // {
                //     Debug.Log("Inside the Index 1 Showing Rotation" + popUpIndex);

                //     popUpIndex++;
                // }
            }
        }
        else if (popUpIndex == 2)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //Debug.Log("Inside the Index 2 Showing to shoot the bullet" + popUpIndex);
                popUpIndex++;
                rockActive = true;
            }
        }
        else if (popUpIndex == 3 && rockActive == true && rockTutorialCompleted == false)
        {
            popUps[3].gameObject.SetActive(false);
            rockShootingTime += Time.deltaTime;
            Debug.Log("Inside the Rocks Shooting" + rockShootingTime);
            rock.SetActive(true);
            if (rockShootingTime >= 8f && playerMain.currentHealth >= 80)
            {
                rockActive = false;
                rockTutorialCompleted = true;
                rock.SetActive(false);
                popUps[3].gameObject.SetActive(true);
            }
            else
            {
                playerMain.currentHealth = 100;
            }
        }
        else if (popUpIndex == 3 && rockTutorialCompleted == true && rockActive == false)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (waitTimeForEnemies < 3f)
                {
                    StartCoroutine(HandleWaitTime());
                }
                else if (waitTimeForEnemies >= 3f && Input.GetKey(KeyCode.Space))
                {
                    popUpIndex += 1;
                    popUps[3].gameObject.SetActive(false);
                    enemiesActive = true;
                    rockActive = false;
                    isEnemiesSpawnOver = true;
                }
                //Debug.Log("Inside the Index 3 Showing Enemies" + popUpIndex);
                //Debug.Log("Inside the Index 3 The wait time is" + waitTimeForEnemies);

            }
        }
        else if (popUpIndex == 4 && enemiesActive == true && enemiesTutorialCompleted == false)
        {
            //Debug.Log("Inside the Index 4");
            popUps[4].gameObject.SetActive(false);
            enemyShootingTime += Time.deltaTime;
            Debug.Log("Inside the Enemy Shooting Time" + enemyShootingTime);
            enemies.SetActive(true);
            if (enemyShootingTime >= 8f && playerMain.currentHealth >= 80)
            {
                enemiesActive = false;
                isEnemiesSpawnOver = true;
                enemiesTutorialCompleted = true;
                enemies.SetActive(false);
                popUps[4].gameObject.SetActive(true);
            }
            else
            {
                playerMain.currentHealth = 100;
            }
        }

        else if (popUpIndex == 4 && enemiesTutorialCompleted == true && enemiesActive == false)
        {
            if (waitTimeForLetterse < 4f)
            {
                StartCoroutine(HandleWaitTime());
            }
            else if (isEnemiesSpawnOver == true && Input.GetKey(KeyCode.Space))
            {
                popUpIndex += 1;
                popUps[4].gameObject.SetActive(false);
                lettersActive = true;
                letters.SetActive(true);
            }
        }
        else
        {
            // 
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
            //Debug.Log("The character shot count is" + characterShot);
            GameManager.instance.characterShotCount = characterShot;
            if (count == levelWord.Length)
            {
                GameManager.instance.wordCompleted = true;
                GameManager.instance.gameWon = true;
                if (GameManager.instance.gameWon == true)
                {
                    StartCoroutine(SetWinText());
                }
            }
            // 
        }

    }
    private IEnumerator HandleWaitTime()
    {
        if (isEnemiesSpawnOver == false)
        {
            yield return new WaitForSeconds(1.5f);
            waitTimeForEnemies = 3f;
        }
        if (isEnemiesSpawnOver == true)
        {
            yield return new WaitForSeconds(2f);
            waitTimeForLetterse = 4f;
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
        //Debug.Log("The whole letter is complete");
        GameManager.instance.wordCompleted = false;
        GameManager.instance.gameWon = false;
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
        GameManager.instance.winScreen2();
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
}

