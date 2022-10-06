using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Letters : MonoBehaviour
{
    private float speed=2.0f;
    public Boolean moving = false;
    public float pingPongSpeed = 0.25f;
    public Boolean displacementX = true;
    public float displacementParam1 = 0.3f;
    public float displacementParam2 = 0.1f;
    private float initialPosition = 0.3f ;

    private Rigidbody2D rb;
    private Vector2 screenBounds;
    

    // Start is called before the first frame update
    void Start()
    {
        rb=this.GetComponent<Rigidbody2D>();
        rb.velocity=new Vector2(0,-GameManager.instance.LetterSpeed);
        screenBounds=Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        if (displacementX)
            initialPosition = transform.position.x;
        else
            initialPosition = transform.position.y;
        
        if (moving)
            StartCoroutine(RunLoop());
    } 

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y<screenBounds.y*-1){
            Destroy(this.gameObject);
        }
    }

    private void letterCollidedwithPlayerorBullet()
    {
        // char letter_collided = gameObject.name[0];
        // if (LevelManagerLevel1.instance.levelWord.ToUpper().Contains(letter_collided.ToString()))
        // {
        //     // True Condition then reveal the letter
        // }
        // else
        // {
        //     // Decrease Health
        // }
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Collision for Letter");
        Debug.Log(col.gameObject.tag);
        if (col.gameObject.tag == "bullet" )
        {
            Destroy(col.gameObject);
            Destroy(gameObject);
            letterCollidedwithPlayerorBullet();
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Trigger for Letter");
        Debug.Log(col.gameObject.tag);
        if (col.gameObject.tag == "player")
        {
            // Destroy(gameObject);
            letterCollidedwithPlayerorBullet();
        }
    }

    private IEnumerator RunLoop() {
        for (float t=0f; ; t += Time.deltaTime) { 
            
            Vector3 currenrtTransformPosition = transform.position;

            float currentDisplacement = Mathf.PingPong(Time.time * pingPongSpeed, 1) * displacementParam1 - displacementParam2;

            if (displacementX)
                transform.position = new Vector3(  initialPosition + currentDisplacement, currenrtTransformPosition.y, currenrtTransformPosition.z);
            else
                transform.position = new Vector3(currenrtTransformPosition.x,  initialPosition + currentDisplacement, currenrtTransformPosition.z);
            
            yield return null; // "wait for a frame"
        }
    }

}
