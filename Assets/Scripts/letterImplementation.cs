using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class letterImplementation : MonoBehaviour
{
    public GameObject letterPrefab;
    public float letterReAppearTime=4.0f;
    private Vector2 screenBounds;

    // Start is called before the first frame update
    void Start()
    {
        screenBounds=Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        StartCoroutine(letterLoop());
    }

    private void createLetters(){
        GameObject letter=Instantiate(letterPrefab) as GameObject;
        letter.transform.position=new Vector2(Random.Range(-screenBounds.x, screenBounds.x), screenBounds.y);
    }

    IEnumerator letterLoop(){
        while(true){
            yield return new WaitForSeconds(letterReAppearTime);
            createLetters();
        }
    }
}
