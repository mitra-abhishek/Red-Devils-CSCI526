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
        // all_level_words.Add(
        //     1, 
        //     new List<string> { 
        //         "CAT", "DOG", "PIN" 
        //     }
        // );
        //
        // all_level_words.Add(
        //     2, 
        //     new List<string> { 
        //         "FIVE", "WOLF", "LION" 
        //     }
        // );
        //
        // all_level_words.Add(
        //     3, 
        //     new List<string> { 
        //         "TIGER", "APPLE", "CHIEF" 
        //     }
        // );

        screenBounds=Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        StartCoroutine(letterLoop()); // assuming level 2
    }
    

     private string getLetter(string word)
    {
        int index = random.Next(word.Length);
        char let =  word[index];
        
        int num = random.Next(0, 26); // Zero to 25
        char random_let = (char)('A' + num);
        // Debug.Log("Word : " + word + ", Letter : " + let);

        int random_num = random.Next(1, 6);
        if (random_num <= 2)
        {
            return random_let.ToString().ToUpper();
        }
        
        return let.ToString().ToUpper();
    }

    private void createLetters(string word){
        //GameObject letter=Instantiate(letterPrefab) as GameObject;
        
        
        GameObject letter = Instantiate(Resources.Load("Letters/"+getLetter(word)) as GameObject);
        //Debug.Log("Tag : " +letter.tag);
        Debug.Log("Name : " + letter.name);
        letter.transform.position=new Vector2(Random.Range(-screenBounds.x, screenBounds.x), screenBounds.y);
        
        GameObject letter2 = Instantiate(Resources.Load("Letters/"+getLetter(word)) as GameObject);
        //Debug.Log("Tag : " +letter.tag);
        //Debug.Log("Name : " + letter2.name);
        letter2.transform.position=new Vector2(Random.Range(-screenBounds.x, screenBounds.x), screenBounds.y);

    }

    private void createLettersDelayed()
    {
        for (int i = 0; i < random.Next(1, 4); i++)
        {
            StartCoroutine(createLettersDelayedCoRoutine());
            //StopCoroutine(createLettersDelayedCoRoutine());
        }
        
    }

    private IEnumerator createLettersDelayedCoRoutine()
    {
        yield return new WaitForSeconds(0.2f*random.Next(1,10));

        GameObject letter;
        // if (GameManager.instance.Level == 3) 
        //     letter = Instantiate(Resources.Load("Letters/"+getLetter(LevelManagerLevel3.instance.levelWord)) as GameObject);
        // else if(GameManager.instance.Level ==2)
        //     letter = Instantiate(Resources.Load("Letters/"+getLetter(LevelManagerLevel3.instance.levelWord)) as GameObject);
        // else
        //     letter = Instantiate(Resources.Load("Letters/"+getLetter(LevelManagerLevel1.instance.levelWord)) as GameObject);
        
        letter = Instantiate(Resources.Load("Letters/"+getLetter(GameManager.instance.LevelWord)) as GameObject);
        

        //Debug.Log("Tag : " +letter.tag);
        //Debug.Log("createLettersDelayedCoRoutine Name : " + letter.name);
        letter.transform.position=new Vector2(Random.Range(-screenBounds.x+0.5f, screenBounds.x-0.5f), screenBounds.y);;
        
    }
    

    IEnumerator letterLoop(){

        while(true){
            yield return new WaitForSeconds(letterReAppearTime);
            //createLetters(LevelManagerLevel1.instance.levelWord);
            //Debug.Log("Level Word : "+LevelManagerLevel1.instance.levelWord);
            createLettersDelayed();
        }
    }
}
