using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelCompleteScript : MonoBehaviour
{

    GameObject scoreObj;

    private static System.Random random = new System.Random();

    // Start is called before the first frame update
    void Start()
    {
        int totalScore = PlayerStats.rockScore + PlayerStats.enemyScore;
        scoreObj = GameObject.FindWithTag("score");
        TextMeshProUGUI scoreObjGui = scoreObj.GetComponent<TextMeshProUGUI>();
        scoreObjGui.text = getRandomScore(GameManager.instance.Level);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private string getRandomScore(int level) {
        int multiplier = random.Next(10, 21);
        int finalScore = 0;

        if(level == 1) {
            finalScore = 300 + (multiplier*5);
        } else if (level == 2) {
            finalScore = 600 + (multiplier*5);
        } else {
            finalScore = 900 + (multiplier*5);
        }

        string str = finalScore.ToString();
        return str;
    }
}
