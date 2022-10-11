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
    
    private List<string> lettersDropped = new List<string>(15);
    private Dictionary<string, int> lettersDroppedDict = new Dictionary<string, int>();

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
    

     private string getLetterRandom(string word)
    {
        int index = random.Next(word.Length);
        char let =  word[index];
        
        int num = random.Next(0, 26); // Zero to 25
        char random_let = (char)('A' + num);
        // Debug.Log("Word : " + word + ", Letter : " + let);

        int random_num = random.Next(1, 6);
        if (random_num <= 3)
        {
            return random_let.ToString().ToUpper();
        }
        
        return let.ToString().ToUpper();
    }

     // private string getLetter(string word)
     // {
     //     if (lettersDropped.Count < 15)
     //     {
     //         string char_chosen = getLetterRandom(word);
     //         lettersDropped.Add(char_chosen);
     //         if (lettersDroppedDict.ContainsKey(char_chosen))
     //         {
     //             lettersDroppedDict[char_chosen] += 1;
     //         }
     //         else
     //         {
     //             lettersDroppedDict.Add(char_chosen,1);
     //         }
     //
     //         return char_chosen;
     //     }
     //     else
     //     {
     //         bool letterDroppedorNot = false;
     //         foreach (var a in word.ToUpper())
     //         {
     //             if (!lettersDroppedDict.ContainsKey(a.ToString()) || lettersDroppedDict[a.ToString()] <= 0)
     //             {
     //                 lettersDropped.RemoveAt(0);
     //                 lettersDropped.Add(a.ToString());
     //                 if (!lettersDroppedDict.ContainsKey(a.ToString()))
     //                 {
     //                     lettersDroppedDict.Add(a.ToString(), 1);
     //                 }
     //                 else
     //                 {
     //                     lettersDroppedDict[a.ToString()] += 1;
     //                 }
     //
     //                 letterDroppedorNot = true;
     //                 return a.ToString().ToUpper();
     //             }
     //         }
     //         if (!letterDroppedorNot)
     //         {
     //             string char_chosen = getLetterRandom(word);
     //             lettersDropped.Add(char_chosen);
     //             if (lettersDroppedDict.ContainsKey(char_chosen))
     //             {
     //                 lettersDroppedDict[char_chosen] += 1;
     //             }
     //             else
     //             {
     //                 lettersDroppedDict.Add(char_chosen,1);
     //             }
     //
     //             return char_chosen;
     //         }
     //         
     //     }
     //     
     // }
     //
     private string getLetter(string word)
     {
         if (lettersDropped.Count == 15)
         {
             foreach (var a in word.ToUpper())
             {
                 if (!lettersDroppedDict.ContainsKey(a.ToString()) || lettersDroppedDict[a.ToString()] <= 0)
                 {
                     lettersDropped.RemoveAt(0);
                     lettersDropped.Add(a.ToString());
                     if (!lettersDroppedDict.ContainsKey(a.ToString()))
                     {
                         lettersDroppedDict.Add(a.ToString(), 1);
                     }
                     else
                     {
                         lettersDroppedDict[a.ToString()] += 1;
                     }
                     return a.ToString().ToUpper();
                 }
             }
         }
         
         string charChosen = getLetterRandom(word);
         lettersDropped.Add(charChosen);
         if (lettersDroppedDict.ContainsKey(charChosen))
         {
             lettersDroppedDict[charChosen] += 1;
         }
         else
         {
             lettersDroppedDict.Add(charChosen,1);
         }

         return charChosen;
         
     }

     private void createLetters(string word){
        //GameObject letter=Instantiate(letterPrefab) as GameObject;
        
        
        GameObject letter = Instantiate(Resources.Load("Letters/"+getLetter(word)) as GameObject);
        //Debug.Log("Tag : " +letter.tag);
        Debug.Log("Name : " + letter.name);
        letter.transform.position=new Vector2(GameManager.instance.getRandomRange(-screenBounds.x, screenBounds.x), screenBounds.y);
        
        GameObject letter2 = Instantiate(Resources.Load("Letters/"+getLetter(word)) as GameObject);
        //Debug.Log("Tag : " +letter.tag);
        //Debug.Log("Name : " + letter2.name);
        letter2.transform.position=new Vector2(GameManager.instance.getRandomRange(-screenBounds.x, screenBounds.x), screenBounds.y);

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
        letter.transform.position=new Vector2(GameManager.instance.getRandomRange(-screenBounds.x+0.5f, screenBounds.x-0.5f), screenBounds.y);;
        
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
