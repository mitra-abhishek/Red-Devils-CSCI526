using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterPrefabGenerator : MonoBehaviour
{
    public GameObject letterPrefab;
    public float letterReAppearTime=4.0f;
    private Vector2 screenBounds;
    private static System.Random random = new System.Random();
    private static Dictionary<int, List<string>> all_level_words = new Dictionary<int, List<string>>();

    // Start is called before the first frame update
    void Start()
    {
        all_level_words.Add(
            1, 
            new List<string> { 
                "CAT", "DOG", "PIN" 
            }
        );

        all_level_words.Add(
            2, 
            new List<string> { 
                "FIVE", "WOLF", "LION" 
            }
        );

        all_level_words.Add(
            3, 
            new List<string> { 
                "TIGER", "APPLE", "CHIEF" 
            }
        );

        screenBounds=Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        StartCoroutine(letterLoop(2)); // assuming level 2
    }
    

     private char getLetter(string word)
    {
        int index = random.Next(word.Length);
        char let =  word[index];
        
        // int num = random.Next(0, 26); // Zero to 25
        // char let = (char)('A' + num);
        Debug.Log("Word : " + word + ", Letter : " + let);
        return let;
    }

    private void createLetters(string word){
        //GameObject letter=Instantiate(letterPrefab) as GameObject;
        
        
        GameObject letter = Instantiate(Resources.Load("Letters/"+getLetter(word)) as GameObject);
        //Debug.Log("Tag : " +letter.tag);
        Debug.Log("Name : " + letter.name);
        letter.transform.position=new Vector2(Random.Range(-screenBounds.x, screenBounds.x), screenBounds.y);
        
        GameObject letter2 = Instantiate(Resources.Load("Letters/"+getLetter(word)) as GameObject);
        //Debug.Log("Tag : " +letter.tag);
        Debug.Log("Name : " + letter2.name);
        letter2.transform.position=new Vector2(Random.Range(-screenBounds.x, screenBounds.x), screenBounds.y);

    }

    IEnumerator letterLoop(int level){
        
        List<string> level_words = all_level_words[level];
        int index = random.Next(level_words.Count);
        string word =  level_words[index];

        while(true){
            yield return new WaitForSeconds(letterReAppearTime);
            createLetters(word);
        }
    }
}
