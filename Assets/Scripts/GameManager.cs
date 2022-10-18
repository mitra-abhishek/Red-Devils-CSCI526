using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class GameManager : MonoBehaviour
{  
    public static GameManager instance;
    public int Level = 1;
    public String LevelWord;
    public float LetterSpeed = 2.0f;
    
    public float RockSpeed = 2.0f;
    public int bullets;

    public int totalLettersShot;
    public int characterShotCount;

    public BulletController bulletController;
    public BulletPowerUpController bulletPowerUpController;

    public int ScreenDivisionNumber = 10;
    private List<int> previousScreenPositionSelected = new List<int>(10);
    
    public Dictionary<char, int> wordDistanceDict = new Dictionary<char, int>();

    public Boolean switchColor = true;

    private static System.Random random = new System.Random();
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        DontDestroyOnLoad(instance);
    }
    public void Start()
    {
        Debug.Log("Game Manager Started");
        bulletController=new BulletController();
        bulletController.setBullets(bullets);
        bulletPowerUpController=new BulletPowerUpController();
        bulletPowerUpController.setTotalPowerGenerated();
        bulletPowerUpController.setTotalPowerUpsCollected();

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

    public void lossScreen()
    {
        SceneManager.LoadScene("Level Failure");
    }

    public void goToHome()
    {
        SceneManager.LoadScene("Main Screen Demo");
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
                Debug.Log(("Curr DIst is " + currentDist + " Numeric Value "+ Char.GetNumericValue(c)));
                minDist = Math.Min(currentDist, minDist);
            }
            Debug.Log("Distance for " + c + " is  : " + minDist);
            wordDistanceDict.Add(c, minDist);
        } 
        Debug.Log(wordDistanceDict);

    }

    public String getColorLevel(String currentChar)
    {
        int dist = wordDistanceDict[currentChar.ToUpper()[0]];
        Debug.Log("Letter is "+ currentChar + "Distance is " + dist);
 
        if (dist < 2)
        {
            return "green/";
        }
        if(dist < 4)
        {
            return "orange/";
        }
       
        return "red/";
    }
    
    
}