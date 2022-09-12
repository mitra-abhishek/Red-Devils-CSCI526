using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    private Camera mainCam;
    private Rigidbody2D bulletBody;
    private Vector3 mousePosn;
    public float force = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        bulletBody = GetComponent<Rigidbody2D>();
        mousePosn = mainCam.ScreenToWorldPoint(Input.mousePosition);

        Vector3 bodyPosn = transform.position;
        
        Vector3 rotation = bodyPosn - mousePosn;
        Vector3 directionFire = mousePosn - bodyPosn;

        bulletBody.velocity = new Vector2(directionFire.x, directionFire.y).normalized * force;
        
        float angle = Mathf.Atan2 (rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler (0, 0, angle+90);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

   
}
