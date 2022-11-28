using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public GameObject anchor = null;
    public float velocityRotate =0.0f;
    public Boolean RoatationPlane = false;
    public int state = 0;
    
    
    private Vector2[] objs = new Vector2[12];
    
    
    public Boolean restructure = false;
    public char letterChar;
    private float initialPosition = 0.3f;
    private GameObject explosion;
    private GameObject shuffleExplosion;
    private Scene scene;



    private Vector3 startPosition;
    public GameObject letter = null;
    public Boolean droppedLetter = false;
    private Renderer renderer;


    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        renderer = GetComponent<Renderer>();
        scene = SceneManager.GetActiveScene();
        if (scene.name.Equals("Planet") | scene.name.Equals("Sport"))
        {
            explosion = Resources.Load<GameObject>("Explosion/BurstEffect(Planets)");
        }
        else if (scene.name.Equals("Tutorial"))
        {
            explosion = Resources.Load<GameObject>("Explosion/BurstEffect (0)");
        }
        else if (scene.name.Equals("Animals"))
        {
            explosion = Resources.Load<GameObject>("Explosion/BurstEffect(Animals)");
        }
        else
        {
            explosion = Resources.Load<GameObject>("Explosion/BurstEffect(Planets)");
        }
        shuffleExplosion = Resources.Load<GameObject>("Explosion/AllBurst");
        objs[0] = new Vector2(7f, 2.9f);
        objs[1] = new Vector2(3.5f, 2.9f);
        objs[2] = new Vector2(0.0f, 2.9f);
        objs[3] = new Vector2(-3.5f, 2.9f);
        objs[4] = new Vector2(-7f, 2.9f);
        objs[5] = new Vector2(-8.0f, 2.0f);
        objs[6] = new Vector2(-7f, 1.4f);
        objs[7] = new Vector2(-3.5f, 1.4f);
        objs[8] = new Vector2(0.0f, 1.4f);
        objs[9] = new Vector2(3.5f, 1.4f);
        objs[10] = new Vector2(7.0f, 1.4f);
        objs[11] = new Vector2(8.0f, 2.0f);
        if (RoatationPlane)
        {
            transform.position = objs[state];
            StartCoroutine(LoopMove());
        }

    }

    // Update is called once per frame
    void Update() {
    }

    // void FixedUpdate()
    // {
    //     state++;
    //     if (state >= objs.Length)
    //         state = 0;
    //     
    //     transform.position = Vector2.MoveTowards (transform.position, objs [state], velocityRotate * Time.deltaTime);
    // }

    public void createLettersDelayed()
    {
        // if (letter && !droppedLetter)
        // {
        //     Destroy(letter.gameObject);
        // }
        //
        foreach (Transform child in transform) {
            GameObject.Destroy(child.gameObject);
        }
        droppedLetter = false;
        
        if (GameManager.instance.Level == 3)
            letter = Instantiate(Resources.Load("Letters/orange/" + letterChar) as GameObject);
        else if (GameManager.instance.Level == 1) {
            letter = Instantiate(Resources.Load("Letters/orange-red/" + letterChar) as GameObject);
        }
        else if (GameManager.instance.Level == 4)
            letter = Instantiate(Resources.Load("Letters/white/" + letterChar) as GameObject);
        else {
            letter = Instantiate(Resources.Load("Letters/" + letterChar) as GameObject);
        }
        if (scene.name.Equals("Planet") | scene.name.Equals("Sport") | scene.name.Equals("Country"))
        {
            Instantiate(shuffleExplosion, transform.position, transform.rotation);
        }
        else
        {
            Instantiate(shuffleExplosion, new Vector3(transform.position.x + 1, transform.position.y + 1, transform.position.z), transform.rotation);
        }
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
        while (GameManager.instance.wordCompleted == false)
        {
            //createLetters(LevelManagerLevel1.instance.levelWord);
            //Debug.Log("Level Word : "+LevelManagerLevel1.instance.levelWord);
            yield return new WaitForSeconds(0.5f);
            createLettersDelayed();
            yield return new WaitForSeconds(letterReAppearTime);
            droppedLetter = false;
        }
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
    
    IEnumerator LoopMove()
    {
        while (true)
        {
            if (transform.position != (Vector3)objs[state])
            {
                transform.position = Vector2.MoveTowards(transform.position, objs[state], velocityRotate * Time.deltaTime);
            }
            else
            {
                state++;
            }
            if (state >= objs.Length)
                state = 0;

            //yield return new WaitForSeconds(2.0f);
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "bullet")
        {
            //Debug.Log("Collision with bullet - letter cloud")
            Destroy(col.gameObject);
            if (transform.childCount == 0)
                return;
            Rigidbody2D rb = letter.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(0, -GameManager.instance.LetterSpeed);
            letter.transform.localScale = new Vector3(letterScale, letterScale, letterScale);
            letter.transform.parent = null; 
            droppedLetter = true;
            //Debug.Log(rb);
            if (scene.name.Equals("Planet") | scene.name.Equals("Sport") | scene.name.Equals("Country"))
            {
                Instantiate(explosion, transform.position, transform.rotation);
            }
            else
            {
                Instantiate(explosion, new Vector3(transform.position.x + 1, transform.position.y + 1, transform.position.z), transform.rotation);
            }
            StopAllCoroutines();
            if (RoatationPlane)
            {
                StartCoroutine(LoopMove());
            }

            //createLettersDelayed();
            Invoke("createLettersDelayed",1f);
        }
    }
}