using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LiveScore : MonoBehaviour
{
    [SerializeField] Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        PlayerStats.rockScore = 0;
        PlayerStats.enemyScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        int score = PlayerStats.rockScore + PlayerStats.enemyScore;
        scoreText.text = score.ToString();
    }
}
