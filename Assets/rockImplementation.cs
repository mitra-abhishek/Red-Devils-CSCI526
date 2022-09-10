using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rockImplementation : MonoBehaviour
{
    public GameObject rockPrefab;
    public float rockReAppearTime=1.0f;
    private Vector2 screenBounds;

    // Start is called before the first frame update
    void Start()
    {
        screenBounds=Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        StartCoroutine(rockLoop());
    }

    private void createRocks(){
        GameObject rock=Instantiate(rockPrefab) as GameObject;
        rock.transform.position=new Vector2(Random.Range(-screenBounds.x, screenBounds.x), screenBounds.y);
    }

    IEnumerator rockLoop(){
        while(true){
            yield return new WaitForSeconds(rockReAppearTime);
            createRocks();
        }
    }
}
