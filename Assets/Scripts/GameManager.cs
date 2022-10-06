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

    public int bullets;

    public BulletController bulletController;

    public int ScreenDivisionNumber = 10;

    private List<int> previousScreenPositionSelected = new List<int>(10);
    
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
        bulletController=new BulletController();
        bulletController.setBullets(bullets);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float getRandomRange(float start, float end)
    {
        //float start = -screenBounds.x; float end = screenBounds.x;
        start = start + 0.25f;
        end = end - 0.25f;
        float diff_per_screen = (end - start)/ScreenDivisionNumber;

        int random_number = random.Next(ScreenDivisionNumber);

        while (previousScreenPositionSelected.Contains(random_number))
        {
            random_number = random.Next(ScreenDivisionNumber);
        }

        if (previousScreenPositionSelected.Count >= 10)
        {
            previousScreenPositionSelected.RemoveAt(0);
        }
        
        previousScreenPositionSelected.Add(random_number);

        float final_position = start + random_number * diff_per_screen;

        return final_position;
    }
}
