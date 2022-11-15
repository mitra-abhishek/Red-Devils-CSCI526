using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial1 : MonoBehaviour
{
    public Button NextButton;

    // Start is called before the first frame update
    void Start()
    {
        NextButton.onClick.AddListener(loadTutorial2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void loadTutorial2()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Tutorial");
    }
}
