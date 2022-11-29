using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;
// using UnityEngine.InputSystem;
    
public class ButtonHandler : MonoBehaviour
{
    // Start is called before the first frame update
    private coinCount coin_count;
    private Bullets bulletHandler;
    private LevelManagerLevel1 levelManagerLevel1;
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
        int currentCoins = coin_count.getNumCoins();
        if(currentCoins>=3)
        {
            currentCoins-=3;
            
            levelManagerLevel1.showHint();
            coin_count.setNumCoins(currentCoins);
            bulletHandler.SetEnemiesDestroyed(currentCoins);
            
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
        levelManagerLevel1 = new LevelManagerLevel1();
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
