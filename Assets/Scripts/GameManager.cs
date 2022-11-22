using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UIElements;


public static class PlayerStats {
    public static int rockScore { get; set; }
    public static int enemyScore { get; set; }
}

public class GameManager : MonoBehaviour
{  
    public static GameManager instance;
    public bool wordCompleted;
    public bool penultimate;
    public bool oneMinLeft;
    public bool gameWon;
    public int Level = 1;
    public String LevelWord;
    public float LetterSpeed = 2.0f;
    
    public float RockSpeed = 2.0f;
    public int bullets;

    // public float bulletPowerUpSpeed = 2.0f;

    public int totalLettersShot;
    public int characterShotCount;

    public BulletController bulletController;
    public BulletPowerUpController bulletPowerUpController;

    public int ScreenDivisionNumber = 10;
    private List<int> previousScreenPositionSelected = new List<int>(10);
    
    public Dictionary<char, int> wordDistanceDict = new Dictionary<char, int>();

    public Boolean switchColor = true;
    public Boolean altVersion = false;

    private static System.Random random = new System.Random();
    
    private List<char> datalist = new List<char>();
    private int indexLetter = 0;
    private string secondaryChars = "cgjklmpquvwxyzh";
    private string primaryChars = "cdfghjklmnpqrstvwxyz";
    // char[] primaryCharsListHelper = { "c","d","f","g","h","j","k","l","m","n","p","q","r","s","t","v","w","x","y","z" };
    private List<char> primaryCharsList = new List<char>();
    private string finalChars = "";
    private AudioClip laserClip;
    private AudioClip bulletImpactClip;
    private AudioClip coinCollectClip;
    private AudioClip letterCollectClip;
    private AudioClip selfDamageClip;
    private AudioClip powerupCollectClip;


    private AudioSource audioSource;


    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        laserClip = Resources.Load<AudioClip>("Sounds/LaserShot");
        bulletImpactClip = Resources.Load<AudioClip>("Sounds/BulletImpact");
        coinCollectClip = Resources.Load<AudioClip>("Sounds/CoinCollect");
        letterCollectClip = Resources.Load<AudioClip>("Sounds/LetterCollect");
        powerupCollectClip = Resources.Load<AudioClip>("Sounds/PowerupCollect");
        audioSource = this.GetComponent<AudioSource>();
        DontDestroyOnLoad(instance);
    }

    public void ResetScore()
    {
        PlayerStats.rockScore = 0;
        PlayerStats.enemyScore = 0;
    }

    public void Start()
    {
        // ResetScore();
        bulletController=new BulletController();
        bulletController.setBullets(bullets);
        bulletPowerUpController=new BulletPowerUpController();
        bulletPowerUpController.setTotalPowerGenerated();
        bulletPowerUpController.setTotalPowerUpsCollected();
        wordCompleted = false;
        penultimate = false;
        gameWon = false;
        oneMinLeft = false;
        laserClip = Resources.Load<AudioClip>("Sounds/LaserShot");
        audioSource = this.GetComponent<AudioSource>();
        primaryCharsList.AddRange(primaryChars);
        int itr = 0;
        ShuffleMe(primaryCharsList);
        while(finalChars.Length<11-LevelWord.Length && itr < primaryCharsList.Count)
        {
            if (LevelWord.ToLower().IndexOf(primaryCharsList[itr]) == -1)
            {
                finalChars +=primaryCharsList[itr];
                
            }
            itr+=1;
        }
        createLetterSpawnArrayInitial();

    }
    private void OnDisable()
    {
        previousScreenPositionSelected = new List<int>(10);
    }
    private void OnEnable()
    {
        previousScreenPositionSelected = new List<int>(10);
    }
    // Update is called once per frame
    void Update()
    {
        //ShuffleMe(datalist);
    }
    public float getRandomRange(float start, float end)
    {
        //float start = -screenBounds.x; float end = screenBounds.x;
        start = start + 0.5f;
        end = end - 0.5f;
        float diff_per_screen = (end - start)/ScreenDivisionNumber;
        int random_number = random.Next(ScreenDivisionNumber);
        while (previousScreenPositionSelected.Contains(random_number))
        {
            random_number = random.Next(ScreenDivisionNumber);
        }
        if (previousScreenPositionSelected.Count >= 5)
        {
            previousScreenPositionSelected.RemoveAt(0);
        }
        
        previousScreenPositionSelected.Add(random_number);
        float final_position = start + random_number * diff_per_screen;
        return final_position;
    }

    public void winScreen()
    {
        SceneManager.LoadScene("Level Complete");
    }

    public void winScreen2()
    {
        SceneManager.LoadScene("Level Complete2");
    }

    public void lossScreen()
    {
        SceneManager.LoadScene("Level Failure");
    }

    public void goToHome()
    {
        SceneManager.LoadScene("Start Screen");
    }

    public void gameOverScreen(){
        SceneManager.LoadScene("Game Over");
    }
    
    public void genWordDistanceDictionary()
    {
        wordDistanceDict = new Dictionary<char, int>();
        String tempUpperCase = LevelWord.ToUpper();
        for (char c = 'A'; c <= 'Z'; c++)
        {
            //do something with letter 
            int minDist = 30;
            foreach (char wordChar in tempUpperCase)
            {
                int currentDist = Math.Abs((int)(c-wordChar));
                // Debug.Log(("Curr DIst is " + currentDist + " Numeric Value "+ Char.GetNumericValue(c)));
                minDist = Math.Min(currentDist, minDist);
            }
            // Debug.Log("Distance for " + c + " is  : " + minDist);
            wordDistanceDict.Add(c, minDist);
        } 
        // Debug.Log(wordDistanceDict);

    }

    public String getColorLevel(String currentChar)
    {
        int dist = wordDistanceDict[currentChar.ToUpper()[0]];
        // Debug.Log("Letter is "+ currentChar + "Distance is " + dist);
 
        if (dist == 0)
        {
            return "green/";
        }
        // if(dist < 2)
        // {
        //     return "orange/";
        // }
       
        return "red/";
    }
    
    public static char GetRandomCharacter(string text)
    {
        int index = random.Next(text.Length);
        return text[index];
    }

    private void IncrementDataList()
    {
        indexLetter = (indexLetter + 1);
        // % datalist.Count();
        // if (indexLetter == 0)
        // {
        //     createLetterSpawnArrayInitial();
        // }
        if(indexLetter == 11)
        {
            indexLetter = 0;
            ShuffleMe(datalist);
        }
    }

    public void createLetterSpawnArrayInitial()
    {
        datalist = new List<char>();
        datalist.AddRange(LevelWord);
        print(LevelWord);
        print(GameManager.instance.LevelWord);
        // datalist.AddRange("o");
        datalist.AddRange(finalChars.ToUpper());
        for(int itr = 0;itr<datalist.Count;itr++)
        {
            print(datalist[itr]);
        }
        print("Checking vals here");
        print(finalChars.ToUpper());
        ShuffleMe(datalist);
    }
    public char getLetterPrimary()
    {
        // char randomChar;
        // int randINT = random.Next(1, 10);
        // if (randINT <= GameManager.instance.LevelWord.Length)
        // {
        //     randomChar = datalist[indexLetter];
        //     IncrementDataList();
        // }
        // else
        // {
        //     randomChar = GetRandomCharacter(primaryChars);
        // }
        
        char randomChar = datalist[indexLetter];
        IncrementDataList();

        return randomChar;
    }
    
    public char getLetterSecondary()
    {
        char randomChar;
        int randINT = random.Next(1, 11);
        if (randINT >= 5)
        {
            randomChar = datalist[indexLetter];
            IncrementDataList();
        }
        else if (randINT >= 3)
        { 
            var common = LevelWord.ToUpper().Intersect(secondaryChars.ToUpper());
            int index = random.Next(common.Count());
            randomChar =  common.ElementAt(index);
        }
        else
        {
            randomChar = GetRandomCharacter(secondaryChars);
        }

        return randomChar;
    }
    
    public static void ShuffleMe<T>(IList<T> list)  
    {
        int n = list.Count;
        for(int i= list.Count - 1; i > 1; i--)
        {
            int rnd = random.Next(i + 1);
            (list[rnd], list[i]) = (list[i], list[rnd]);
        }
    }

    public void playLaserSound()
    {
        if (!laserClip)
            laserClip = Resources.Load<AudioClip>("Sounds/LaserShot");
        if (laserClip)
            audioSource.PlayOneShot(laserClip);
    }


    public void playBulletImpact()
    {
        if (!bulletImpactClip)
            bulletImpactClip = Resources.Load<AudioClip>("Sounds/BulletImpact");
        if (bulletImpactClip)
            audioSource.PlayOneShot(bulletImpactClip);
    }

    public void playCoinCollect()
    {
        if (!coinCollectClip)
            coinCollectClip = Resources.Load<AudioClip>("Sounds/CoinCollect");
        if (coinCollectClip)
            audioSource.PlayOneShot(coinCollectClip);
    }
    

    public void playLetterCollect()
    {
        if (!letterCollectClip)
            letterCollectClip = Resources.Load<AudioClip>("Sounds/LetterCollect");
        if (letterCollectClip)
            audioSource.PlayOneShot(letterCollectClip);
    }
    
    public void playSelfDamage()
    {
        if (!selfDamageClip)
            selfDamageClip = Resources.Load<AudioClip>("Sounds/SelfDamage");
        if (selfDamageClip)
            audioSource.PlayOneShot(selfDamageClip);
    }

    public void playPowerupCollect()
    {
        if (!powerupCollectClip)
            powerupCollectClip = Resources.Load<AudioClip>("Sounds/PowerupCollect");
        if (powerupCollectClip)
            audioSource.PlayOneShot(powerupCollectClip);
    }
    
    
}