using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpperLetterGen : MonoBehaviour
{
    private float letterReAppearTime = 15.0f;

    public float speed = 2.0f;
    public Boolean moving = true;
    public float pingPongSpeed = 0.25f;
    public Boolean displacementX = true;
    public float displacementParam1 = 0.3f;
    public float displacementParam2 = 0.1f;
    public float letterScale = 1.5f;
    private float initialPosition = 0.3f ;
    private GameObject explosion;



    private Vector3 startPosition;
    private GameObject letter = null;
    private Boolean droppedLetter = false;
    private Renderer renderer;


    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        renderer = GetComponent<Renderer>();
        explosion = Resources.Load<GameObject>("Explosion/BurstEffect");
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

        letter = Instantiate(Resources.Load("Letters/" + GameManager.instance.getLetterPrimary()) as GameObject);
        //Debug.Log("This is generated "+ letter.gameObject.name);
        letter.transform.position = transform.position;
        letter.transform.localPosition = renderer.bounds.center;
        letter.transform.parent = this.transform;
        //letter.transform.position  = GameObject.Find("UpperLetter").transform.position;
        //letter.transform.localPosition = Vector3.zero;
        if (displacementX)
            initialPosition = transform.position.x;
        else
            initialPosition = transform.position.y;

        if (moving)
            StartCoroutine(MovementLoop());
    }

    IEnumerator letterLoop()
    {
        //Debug.Log(GameManager.instance.wordCompleted);
        while (GameManager.instance.wordCompleted == false)
        {
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

    private IEnumerator MovementLoop()
    {
        for (float t = 0f; ; t += Time.deltaTime)
        {

            Vector3 currenrtTransformPosition = transform.position;

            float currentDisplacement = Mathf.PingPong(Time.time * pingPongSpeed, 1) * displacementParam1 - displacementParam2;

            if (displacementX)
                transform.position = new Vector3(initialPosition + currentDisplacement, currenrtTransformPosition.y, currenrtTransformPosition.z);
            else
                transform.position = new Vector3(currenrtTransformPosition.x, initialPosition + currentDisplacement, currenrtTransformPosition.z);

            yield return null; // "wait for a frame"
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "bullet")
        {
            Debug.Log("Collision with bullet - letter cloud");
            Destroy(col.gameObject);
            Rigidbody2D rb = letter.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(0, -GameManager.instance.LetterSpeed);
            letter.transform.localScale = new Vector3(letterScale, letterScale, letterScale);
            droppedLetter = true;
            Debug.Log(rb);
            Instantiate(explosion, new Vector3(transform.position.x + 1, transform.position.y + 1, transform.position.z), transform.rotation);
            // Instantiate(explosion, transform.position, transform.rotation);
            StopAllCoroutines();
            StartCoroutine(letterLoop());
        }
    }
}
