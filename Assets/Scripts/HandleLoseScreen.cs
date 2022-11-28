using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class HandleLoseScreen : MonoBehaviour
{
    GameObject reasonOfDeath;
    GameObject correctLevelWord;

    void Start()
    {
        reasonOfDeath = GameObject.FindWithTag("death_reason");
        correctLevelWord = GameObject.FindWithTag("correct_word");
        TextMeshProUGUI reasonOfDeathGui = reasonOfDeath.GetComponent<TextMeshProUGUI>();
        reasonOfDeathGui.text = GameManager.instance.reasonOfDeath.ToString();
        Debug.Log("The check" + GameManager.instance.LevelWord.ToUpper());
        TextMeshProUGUI correctWordGui = correctLevelWord.GetComponent<TextMeshProUGUI>();
        correctWordGui.text = "  Correct Word: " + GameManager.instance.LevelWord.ToUpper();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
