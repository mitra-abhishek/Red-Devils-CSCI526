using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class coinCount : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject numCoins;
    private static int coins;
    private Text coinText;
    public void setNumCoins(int num)
    {
        coins = num;
        print("Checking coin count here");
        print(coins);
    }
    public int getNumCoins()
    {
        return coins;
    }
    // Update is called once per frame
    void Update()
    {
        coinText = numCoins.GetComponent<Text>();
        string helperText = coins.ToString();
        print("printing HelperText here");
        print(helperText);
        coinText.text = helperText;
    }
}
