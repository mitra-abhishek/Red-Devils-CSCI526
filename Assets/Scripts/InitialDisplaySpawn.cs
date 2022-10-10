using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialDisplaySpawn : MonoBehaviour
{
    public GameObject hangmanDisplayLetter; 
    // Start is called before the first frame update
    void Start()
    {
        GameObject spawnLine = GameObject.FindGameObjectWithTag("Spawn Line");
        Vector3 spawnLineTransform = spawnLine.transform.position;
        
        String word = "butter";
        for (int inx=0; inx < word.Length; inx++)
        {
            char c = word[inx];
            Instantiate(hangmanDisplayLetter,
                new Vector3(spawnLineTransform.x + 0.75f * inx, spawnLineTransform.y, spawnLineTransform.z),
                Quaternion.identity);

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
