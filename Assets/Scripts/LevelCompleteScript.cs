using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelCompleteScript : MonoBehaviour
{

    GameObject rockScoreObj;
    GameObject enemyScoreObj;
    GameObject scoreObj;

    private static System.Random random = new System.Random();

    // Start is called before the first frame update
    void Start()
    {
        rockScoreObj = GameObject.FindWithTag("rock_score");
        enemyScoreObj = GameObject.FindWithTag("enemy_score");
        scoreObj = GameObject.FindWithTag("score");
        
        TextMeshProUGUI rockScoreObjGui = rockScoreObj.GetComponent<TextMeshProUGUI>();
        rockScoreObjGui.text = PlayerStats.rockScore.ToString();

        TextMeshProUGUI enemyScoreObjGui = enemyScoreObj.GetComponent<TextMeshProUGUI>();
        enemyScoreObjGui.text = PlayerStats.enemyScore.ToString();
        
        int totalScore = PlayerStats.rockScore + PlayerStats.enemyScore;
        TextMeshProUGUI scoreObjGui = scoreObj.GetComponent<TextMeshProUGUI>();
        scoreObjGui.text = totalScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
