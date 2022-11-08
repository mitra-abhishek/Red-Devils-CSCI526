using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPowerupPrefabGenerator : MonoBehaviour
{
    private Vector2 screenBounds;
    private static System.Random random = new System.Random();

    public GameObject shieldPowerupPrefab;
    public float shieldPowerupReAppearTime=4.0f;
    public int maxshieldPowerupAtTime = 1;

    // Start is called before the first frame update
    void Start()
    {
        screenBounds=Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        StartCoroutine(shieldPowerupLoop());
    }
    
    private void createshieldPowerupsDelayed()
    {
        for (int i = 0; i < random.Next(1, maxshieldPowerupAtTime); i++)
        {
            StartCoroutine(createshieldPowerupssDelayedCoRoutine());
        }
    }

    void createshieldPowerups()
    {
        GameObject shieldPowerup=Instantiate(shieldPowerupPrefab) as GameObject;
        shieldPowerup.transform.position=new Vector2(GameManager.instance.getRandomRange(-screenBounds.x+0.5f, screenBounds.x-0.5f), screenBounds.y);
    }

    private IEnumerator createshieldPowerupssDelayedCoRoutine()
    {
        yield return new WaitForSeconds(0.2f*random.Next(1,10));
        GameObject shieldPowerup=Instantiate(shieldPowerupPrefab) as GameObject;
        shieldPowerup.transform.position=new Vector2(GameManager.instance.getRandomRange(-screenBounds.x, screenBounds.x), screenBounds.y);
    }

    IEnumerator shieldPowerupLoop(){
        while(true){
            yield return new WaitForSeconds(shieldPowerupReAppearTime);
            createshieldPowerupsDelayed();
        }
    }
}
