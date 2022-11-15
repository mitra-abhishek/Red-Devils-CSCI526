using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpperLetterGen : MonoBehaviour
{
    private float letterReAppearTime=15.0f;


    private Vector3 startPosition;
    private GameObject letter = null;
    private Boolean droppedLetter = false;
    private Renderer renderer;
    

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        renderer = GetComponent<Renderer>();
        StartCoroutine(letterLoop());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void createLettersDelayed()
    {
        // if (letter != null || letter.GetComponent<Rigidbody2D>().velocity.y == null || letter.GetComponent<Rigidbody2D>().velocity.y == null )
        // {
        //     Destroy(letter);
        // } 
        // if (letter != null)
        // {
        //     Destroy(letter);
        // }
        if (letter && !droppedLetter)
        {
            Destroy(letter.gameObject);
        }

        letter = Instantiate(Resources.Load("Letters/"+GameManager.instance.getLetterPrimary()) as GameObject);
        //Debug.Log("This is generated "+ letter.gameObject.name);
        //letter.transform.position= transform.position;
        letter.transform.localPosition = renderer.bounds.center;
        //letter.transform.parent = this.transform;
        //letter.transform.position  = GameObject.Find("UpperLetter").transform.position;
        //letter.transform.localPosition = Vector3.zero;

    }
    
    IEnumerator letterLoop(){
        //Debug.Log(GameManager.instance.wordCompleted);
        while(GameManager.instance.wordCompleted == false){
            Debug.Log(GameManager.instance.wordCompleted);
            //createLetters(LevelManagerLevel1.instance.levelWord);
            //Debug.Log("Level Word : "+LevelManagerLevel1.instance.levelWord);
            yield return new WaitForSeconds(0.5f);
            createLettersDelayed();
            yield return new WaitForSeconds(letterReAppearTime);
            droppedLetter = false;
        }
        
       // Debug.Log(GameManager.instance.wordCompleted);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "bullet")
        {
            Debug.Log("Collision with bullet - letter cloud");
            Destroy(col.gameObject);
            Rigidbody2D rb = letter.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(0, -GameManager.instance.LetterSpeed);
            letter.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
            droppedLetter = true;
            Debug.Log(rb);
            StopAllCoroutines();
            StartCoroutine(letterLoop());
        }
    }
}
