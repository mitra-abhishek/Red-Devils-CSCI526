using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Start_Screen_buttons : MonoBehaviour
{
    public Button StartButton;

    public Button TutorialButton;

    // Start is called before the first frame update
    void Start()
    {
        StartButton.onClick.AddListener(loadMainScreen);
        
        TutorialButton.onClick.AddListener(loadTutorial1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void loadMainScreen()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main Screen Demo");
    }

    void loadTutorial1()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Tutorial1");
    }
}
