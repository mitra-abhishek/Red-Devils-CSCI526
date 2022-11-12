using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;

public class ButtonHandlerLev3 : MonoBehaviour
{
    // Start is called before the first frame update
    private coinCount coin_count;
    private Bullets bulletHandler;
    private LevelManagerLevel3 levelManagerLevel3;
    public GameObject popUpMessage;
    public KeyCode _Key;

    public Button button;

    public void onClickFun()
    {
        print("We enter this");
        int currentCoins = coin_count.getNumCoins();
        if(currentCoins>=3)
        {
            print("Can we enter this if");
            currentCoins-=3;
            coin_count.setNumCoins(currentCoins);
            bulletHandler.SetEnemiesDestroyed(currentCoins);
            levelManagerLevel3.setLetterFromHint();
        }
        else{
            popUpMessage.SetActive(true);
            StartCoroutine(timeDelay());

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
        levelManagerLevel3 = new LevelManagerLevel3();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(_Key))
        {
            button.onClick.Invoke();
        }
    }
}
