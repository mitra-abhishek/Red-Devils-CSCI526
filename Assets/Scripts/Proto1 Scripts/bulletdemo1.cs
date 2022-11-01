using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletdemo1 : MonoBehaviour
{
    private Camera mainCam;

    public GameObject arrow;

    public Transform arrowTransform;

    public float timeBetweenShots = 0.5f;

    private Boolean canShoot = true;

    private float timeElapseSinceLastShot;
    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouse_pos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        Vector3 actual_posn = mouse_pos - transform.position;
        
        float angle = Mathf.Atan2(actual_posn.y, actual_posn.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        if (!canShoot)
        {
            timeElapseSinceLastShot += Time.deltaTime;
            if (timeElapseSinceLastShot > timeBetweenShots)
            {
                canShoot = true;
                timeElapseSinceLastShot = 0.0f;
            }
        }

        if (Input.GetMouseButton(0) && canShoot)
        {
            canShoot = false;
            Instantiate(arrow, arrowTransform.position, Quaternion.identity);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        //Debug.Log("Collision Detected");
    }
}