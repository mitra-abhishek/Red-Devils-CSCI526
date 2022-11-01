using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem;
    
public class ButtonHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject buttonText;
    private coinCount coin_count;
    private Bullets bulletHandler;
    private LevelManagerLevel2 levelManagerLevel2;
    public GameObject popUpMessage;
    
    public void onClick()
    {
        int currentCoins = coin_count.getNumCoins();
        if(currentCoins>=3)
        {
            currentCoins-=3;
            coin_count.setNumCoins(currentCoins);
            bulletHandler.SetEnemiesDestroyed(currentCoins);
            levelManagerLevel2.setLetterFromHint();
        }
        // else{
        //     // popUpMessage.SetActive(true);
        //     // StartCoroutine(timeDelay());
        //     // // Mouse mouse = new Mouse();
        //     // Mouse.current.WarpCursorPosition(new Vector2(100, 100));

        // }


    }

    IEnumerator timeDelay()
    {
        yield return new WaitForSeconds(2);
        popUpMessage.SetActive(false);
    }

    void Start()
    {
        coin_count = new coinCount();
        bulletHandler = new Bullets();
        levelManagerLevel2 = new LevelManagerLevel2();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
