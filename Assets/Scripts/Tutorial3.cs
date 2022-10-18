using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial3 : MonoBehaviour
{
    public Button StartButton;

    // Start is called before the first frame update
    void Start()
    {
        StartButton.onClick.AddListener(loadMainScreen);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void loadMainScreen()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main Screen Demo");
    }
}
