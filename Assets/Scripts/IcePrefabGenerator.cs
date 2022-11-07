using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcePrefabGenerator : MonoBehaviour
{
    private Vector2 screenBounds;
    private static System.Random random = new System.Random();

    public GameObject rockPrefab;
    public float rockReAppearTime=2.0f;
    public int maxRockAtTime = 1;
    public float freezeTime = 4.0f;
    public float reduceSpeedBy = 10.0f;

    public static IcePrefabGenerator instance;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        screenBounds=Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        StartCoroutine(rockLoop());
    }
    
    private void createRocksDelayed()
    {
        for (int i = 0; i < random.Next(1, maxRockAtTime); i++)
        {
            StartCoroutine(createRockssDelayedCoRoutine());
            //StopCoroutine(createLettersDelayedCoRoutine());
        }
    }

    void createRocks()
    {
        GameObject rock=Instantiate(rockPrefab) as GameObject;
        rock.transform.position=new Vector2(GameManager.instance.getRandomRange(-screenBounds.x+0.5f, screenBounds.x-0.5f), screenBounds.y);
    }

    private IEnumerator createRockssDelayedCoRoutine()
    {
        yield return new WaitForSeconds(0.2f*random.Next(1,10));
        GameObject rock=Instantiate(rockPrefab) as GameObject;
        rock.transform.position=new Vector2(GameManager.instance.getRandomRange(-screenBounds.x, screenBounds.x), screenBounds.y);
    }

    IEnumerator rockLoop(){
        while(true){
            yield return new WaitForSeconds(rockReAppearTime);
            createRocksDelayed();
        }
    }

   

    
}
