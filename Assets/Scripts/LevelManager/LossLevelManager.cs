using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class LossLevelManager : MonoBehaviour
{
    public Button nextLevel;
    public Button mainHome;
    public TextMeshPro scoreText;
    
    private Dictionary<int, String> pairs = new Dictionary<int, String>()
    {
        {  1, "SampleScene 2" }, { 2, "Level 2" },{3, "Level 3"}
    };
    
    // Start is called before the first frame update
    void Start()
    {
        nextLevel.onClick.AddListener(nextLevelOnClick);
        mainHome.onClick.AddListener(goToHome);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void nextLevelOnClick()
    {
        SceneManager.LoadScene(pairs[GameManager.instance.Level]);
    }

    void goToHome()
    {
        GameManager.instance.goToHome();
    }
}