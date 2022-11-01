using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandlerLev3 : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject buttonText;
    private coinCount coin_count;
    private Bullets bulletHandler;
    private LevelManagerLevel3 levelManagerLevel3;

    public void onClick()
    {
        int currentCoins = coin_count.getNumCoins();
        if(currentCoins>=3)
        {
            currentCoins-=3;
            coin_count.setNumCoins(currentCoins);
            bulletHandler.SetEnemiesDestroyed(currentCoins);
            levelManagerLevel3.setLetterFromHint();
        }


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
        
    }
}
