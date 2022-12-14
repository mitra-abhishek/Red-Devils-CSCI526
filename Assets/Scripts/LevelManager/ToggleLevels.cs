using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleLevels : MonoBehaviour
{
    public Button level1Button;

    public Button level2Button;

    public Button level3Button;

    public Button level4Button;
    
    // Start is called before the first frame update
    void Start()
    {
        level1Button.onClick.AddListener(loadLevel1);
        level2Button.onClick.AddListener(loadLevel2);
        level3Button.onClick.AddListener(loadLevel3);
        level4Button.onClick.AddListener(loadLevel4);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void loadLevel1()
    {
        GameManager.instance.Level = 1;
        // Change Name
        UnityEngine.SceneManagement.SceneManager.LoadScene("Planet");
    }
    void loadLevel2()
    {
        GameManager.instance.Level = 2;
        
        UnityEngine.SceneManagement.SceneManager.LoadScene("Animals");
    }
    void loadLevel3()
    {
        GameManager.instance.Level = 3;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Country");
    }
    void loadLevel4()
    {
        GameManager.instance.Level = 4;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Sport");
    }
}
