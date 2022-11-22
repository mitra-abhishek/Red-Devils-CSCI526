using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyPrefabGenerator : MonoBehaviour
{
    public static IEnumerator enemyLoopCoroutine2;
    private Vector2 screenBounds;
    private static System.Random random = new System.Random();

    public GameObject enemyPrefab;
    public float enemyReAppearTime=1.0f;
    public int maxEnemyAtTime = 1;



    // Start is called before the first frame update
    void Start()
    {
        screenBounds=Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        enemyLoopCoroutine2 = enemyLoop2();
        StartCoroutine(enemyLoopCoroutine2);
        //StartCoroutine(enemyLoop());
    }

    void createEnemies()
    {
        GameObject enemy=Instantiate(enemyPrefab) as GameObject;
        enemy.transform.position=new Vector2(0, screenBounds.y-3.00f);
    }

    IEnumerator enemyLoop2(){
        while(GameManager.instance.penultimate == false && GameManager.instance.oneMinLeft == false){
            yield return new WaitForSeconds(enemyReAppearTime);
        }
        Debug.Log("***************");
        Debug.Log(GameManager.instance.penultimate);
        Debug.Log(GameManager.instance.oneMinLeft);
        Debug.Log("***************");
        createEnemies();
    }  
}
