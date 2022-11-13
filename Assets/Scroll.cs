using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    public float scrollSpeed = 4.0f;

    private Vector3 startPostion;
    // Start is called before the first frame update
    void Start()
    {
        startPostion = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down*scrollSpeed*Time.deltaTime);
        if (transform.position.y < -15.5f)
            transform.position = startPostion;
    }
}
