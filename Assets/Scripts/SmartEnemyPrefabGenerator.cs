using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SmartEnemyPrefabGenerator : MonoBehaviour
{
    private Vector2 screenBounds;
    private static System.Random random = new System.Random();

    public GameObject smartEnemyPrefab;
    public GameObject freezeEnemyPrefab;
    public float enemyReAppearTime=8.0f;
    public int maxEnemyAtTime = 1;
    public float reduceSpeedBy = 10.0f;
    public float freezeTime = 3.0f;

    public static SmartEnemyPrefabGenerator instance;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        
        screenBounds=Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        StartCoroutine(enemyLoop());
    }
    
    private void createEnemiesDelayed()
    {
        for (int i = 0; i < random.Next(1, maxEnemyAtTime); i++)
        {
            if(GameManager.instance.penultimate == false){
                StartCoroutine(createEnemiesDelayedCoRoutine());
            }
            //StopCoroutine(createLettersDelayedCoRoutine());
        }
    }

    void createEnemies()
    {
        GameObject enemy=Instantiate(smartEnemyPrefab) as GameObject;
        enemy.transform.position=new Vector2(GameManager.instance.getRandomRange(-screenBounds.x+0.5f, screenBounds.x-0.5f), screenBounds.y);
    }

    private IEnumerator createEnemiesDelayedCoRoutine()
    {
        yield return new WaitForSeconds(0.2f*random.Next(1,10));
        int choice = random.Next(1, 10);
        GameObject enemy;
        if (choice >= 5)
            enemy = Instantiate(smartEnemyPrefab);
        else
            enemy = Instantiate(freezeEnemyPrefab);
        
        enemy.transform.position=new Vector2(GameManager.instance.getRandomRange(-screenBounds.x, screenBounds.x), screenBounds.y);
    }

    IEnumerator enemyLoop(){
         while(GameManager.instance.penultimate == false){
            yield return new WaitForSeconds(enemyReAppearTime);
            createEnemiesDelayed();
        }
    }
}
