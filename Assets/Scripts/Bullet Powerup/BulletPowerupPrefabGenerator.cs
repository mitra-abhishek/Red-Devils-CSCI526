using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPowerupPrefabGenerator : MonoBehaviour
{
    private Vector2 screenBounds;
    private static System.Random random = new System.Random();

    public GameObject bulletPowerupPrefab;
    public float bulletPowerupReAppearTime=4.0f;
    public int maxbulletPowerupAtTime = 1;

    // Start is called before the first frame update
    void Start()
    {
        screenBounds=Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        StartCoroutine(bulletPowerupLoop());
    }
    
    private void createbulletPowerupsDelayed()
    {
        for (int i = 0; i < random.Next(1, maxbulletPowerupAtTime); i++)
        {
            StartCoroutine(createbulletPowerupssDelayedCoRoutine());
            //StopCoroutine(createLettersDelayedCoRoutine());
        }
    }

    void createbulletPowerups()
    {
        GameObject bulletPowerup=Instantiate(bulletPowerupPrefab) as GameObject;
        bulletPowerup.transform.position=new Vector2(GameManager.instance.getRandomRange(-screenBounds.x, screenBounds.x), screenBounds.y);
    }

    private IEnumerator createbulletPowerupssDelayedCoRoutine()
    {
        yield return new WaitForSeconds(0.2f*random.Next(1,10));
        GameObject bulletPowerup=Instantiate(bulletPowerupPrefab) as GameObject;
        bulletPowerup.transform.position=new Vector2(GameManager.instance.getRandomRange(-screenBounds.x, screenBounds.x), screenBounds.y);
    }

    IEnumerator bulletPowerupLoop(){
        while(true){
            yield return new WaitForSeconds(bulletPowerupReAppearTime);
            createbulletPowerupsDelayed();
        }
    }
}
