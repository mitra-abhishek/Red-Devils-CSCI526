using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class delayHelper : MonoBehaviour
{
    public GameObject coinVal;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void callCoroutine(GameObject coin)
    {
        StartCoroutine(timeDelay(coin));
    }
    public IEnumerator timeDelay(GameObject coin)
    {
        print("Here we are");
        yield return new WaitForSecondsRealtime(1);
        print("Here2");
        coin.SetActive(false);
        
    }

}
