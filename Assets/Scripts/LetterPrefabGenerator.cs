using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterPrefabGenerator : MonoBehaviour
{
    public GameObject letterPrefab;
    public float letterReAppearTime=4.0f;
    private Vector2 screenBounds;
    private static System.Random random = new System.Random();


    // Start is called before the first frame update
    void Start()
    {
        screenBounds=Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        StartCoroutine(letterLoop());
    }
    

     private char getLetter()
    {
        // This method returns a random lowercase letter
        // ... Between 'a' and 'z' inclusize.
        int num = random.Next(0, 26); // Zero to 25
        char let = (char)('A' + num);
        return let;
    }

    private void createLetters(){
        //GameObject letter=Instantiate(letterPrefab) as GameObject;
        
        
        GameObject letter = Instantiate(Resources.Load("Letters/"+getLetter()) as GameObject);
        //Debug.Log("Tag : " +letter.tag);
        Debug.Log("Name : " + letter.name);
        letter.transform.position=new Vector2(Random.Range(-screenBounds.x, screenBounds.x), screenBounds.y);
        
        GameObject letter2 = Instantiate(Resources.Load("Letters/"+getLetter()) as GameObject);
        //Debug.Log("Tag : " +letter.tag);
        Debug.Log("Name : " + letter2.name);
        letter2.transform.position=new Vector2(Random.Range(-screenBounds.x, screenBounds.x), screenBounds.y);

    }

    IEnumerator letterLoop(){
        while(true){
            yield return new WaitForSeconds(letterReAppearTime);
            createLetters();
        }
    }
}
