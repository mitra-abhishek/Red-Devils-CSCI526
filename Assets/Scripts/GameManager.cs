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
    public BulletPowerUpController bulletPowerUpController;


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

        bulletPowerUpController=new BulletPowerUpController();
        bulletPowerUpController.setTotalPowerGenerated();
        bulletPowerUpController.setTotalPowerUpsCollected();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
