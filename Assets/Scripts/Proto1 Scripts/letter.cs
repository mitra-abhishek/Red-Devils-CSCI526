using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class letter : MonoBehaviour
{
    // Start is called before the first frame update\
    public Boolean moving = true;
    public float pingPongSpeed = 0.25f;
    public Boolean displacementX = true;
    public float displacementParam1 = 0.3f;
    public float displacementParam2 = 0.1f;
    private float initialPosition = 0.3f ;

       
    void Start()
    {
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
        
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Collision for Letter");
        Debug.Log(col.gameObject.tag);
        if (col.gameObject.tag == "Bullet")
        {
            Destroy(col.gameObject);
            Destroy(gameObject);
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
