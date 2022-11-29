using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedLetterGen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.createLetterSpawnArrayInitial();
        StartCoroutine(letterLoop());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator letterLoop()
    {
        //Debug.Log(GameManager.instance.wordCompleted);
        while (GameManager.instance.wordCompleted == false)
        {
            yield return new WaitForSeconds(0.5f);
            GameManager.instance.createLetterSpawnArrayInitial();
            int itr = 0;
            foreach (Transform child in transform) {
                child.gameObject.GetComponent<UpperLetterGen>().letterChar = GameManager.instance.datalist[itr];
                child.gameObject.GetComponent<UpperLetterGen>().createLettersDelayed();
                itr++;
            }
            yield return new WaitForSeconds(15.0f);
        }
    }
}
