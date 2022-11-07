using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartEnemyPrefabGenerator : MonoBehaviour
{
    private Vector2 screenBounds;
    private static System.Random random = new System.Random();

    public GameObject smartEnemyPrefab;
    public float enemyReAppearTime=8.0f;
    public int maxEnemyAtTime = 1;

    // Start is called before the first frame update
    void Start()
    {
        screenBounds=Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        StartCoroutine(enemyLoop());
    }
    
    private void createEnemiesDelayed()
    {
        for (int i = 0; i < random.Next(1, maxEnemyAtTime); i++)
        {
            if(GameManager.instance.wordCompleted == false){
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
        GameObject enemy=Instantiate(smartEnemyPrefab) as GameObject;
        enemy.transform.position=new Vector2(GameManager.instance.getRandomRange(-screenBounds.x, screenBounds.x), screenBounds.y);
    }

    IEnumerator enemyLoop(){
         while(GameManager.instance.wordCompleted == false){
            yield return new WaitForSeconds(enemyReAppearTime);
            createEnemiesDelayed();
        }
    }
}
