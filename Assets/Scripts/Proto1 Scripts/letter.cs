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

       
    void Start()
    {
        if (moving)
            StartCoroutine(RunLoop());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Collision for bubble");
        Debug.Log(col.gameObject.tag);
        if (col.gameObject.tag == "Bullet")
        {
            Destroy(col.gameObject);
            Destroy(gameObject);
        }
    }
    
    private IEnumerator RunLoop() {
        for (float t=0f; ; t += Time.deltaTime) { 
            
            float currentDisplacement = Mathf.PingPong(Time.time * pingPongSpeed, 1) * displacementParam1 - displacementParam2;

            Vector3 currenrtTransformPosition = transform.position;
            if (displacementX)
                transform.position = new Vector3(currentDisplacement, currenrtTransformPosition.y, currenrtTransformPosition.z);
            else
                transform.position = new Vector3(currenrtTransformPosition.x, currentDisplacement, currenrtTransformPosition.z);
            
            yield return null; // "wait for a frame"
        }
    }
    
}
