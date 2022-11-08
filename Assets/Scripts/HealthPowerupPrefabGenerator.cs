using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPowerupPrefabGenerator : MonoBehaviour
{
    private Vector2 screenBounds;
    private static System.Random random = new System.Random();

    public GameObject healthPowerupPrefab;
    public float healthPowerupReAppearTime=4.0f;
    public int maxbulletPowerupAtTime = 1;

    // Start is called before the first frame update
    void Start()
    {
        screenBounds=Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        StartCoroutine(healthPowerupLoop());
    }
    
    private void createHealthPowerupsDelayed()
    {
        for (int i = 0; i < random.Next(1, maxbulletPowerupAtTime); i++)
        {
            if(GameManager.instance.wordCompleted == false){
                StartCoroutine(createHealthPowerupssDelayedCoRoutine());
            }
        }
    }

    void createHealthPowerups()
    {
        GameObject HealthPowerup=Instantiate(healthPowerupPrefab) as GameObject;
        HealthPowerup.transform.position=new Vector2(GameManager.instance.getRandomRange(-screenBounds.x+0.5f, screenBounds.x-0.5f), screenBounds.y);
    }

    private IEnumerator createHealthPowerupssDelayedCoRoutine()
    {
        yield return new WaitForSeconds(0.2f*random.Next(1,10));
        GameObject healthPowerup=Instantiate(healthPowerupPrefab) as GameObject;
        healthPowerup.transform.position=new Vector2(GameManager.instance.getRandomRange(-screenBounds.x, screenBounds.x), screenBounds.y);
    }

    IEnumerator healthPowerupLoop(){
        while(GameManager.instance.wordCompleted == false){
            yield return new WaitForSeconds(healthPowerupReAppearTime);
            createHealthPowerupsDelayed();
        }
    }
}
