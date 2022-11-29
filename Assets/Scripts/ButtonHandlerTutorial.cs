using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;
// using UnityEngine.InputSystem;

public class ButtonHandlerTutorial : MonoBehaviour
{
    // Start is called before the first frame update
    private coinCount coin_count;
    private Bullets bulletHandler;
    private TutorialLevelManager tutorialLevelManager;
    public GameObject popUpMessage;
    public KeyCode _Key;

    public Button button;

    // void awake()
    // {
    //     button = GetComponent<Button>();
    //     print("Getting Button Here");
    //     print(button);
    // } 

    public void onClick()
    {
        if (GameManager.instance.checkIfAllowedToPressHint)
        {
            int currentCoins = coin_count.getNumCoins();
            if (currentCoins >= 3)
            {
                currentCoins -= 3;
                coin_count.setNumCoins(currentCoins);
                bulletHandler.SetEnemiesDestroyed(currentCoins);
                // tutorialLevelManager.setLetterFromHint();
                tutorialLevelManager.showHint();

            }
            else
            {
                popUpMessage.SetActive(true);
                StartCoroutine(timeDelay());

            }

        }
    }

    IEnumerator timeDelay()
    {
        yield return new WaitForSeconds(3);
        popUpMessage.SetActive(false);
    }

    void Start()
    {
        coin_count = new coinCount();
        bulletHandler = new Bullets();
        tutorialLevelManager = new TutorialLevelManager();
    }

    // Update is called once per frame check
    void Update()
    {
        if (Input.GetKeyDown(_Key))
        {
            button.onClick.Invoke();
        }
    }
}
