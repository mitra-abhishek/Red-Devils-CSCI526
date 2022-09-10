using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float xMin;
    public float xMax;
    public float y;

    public GameObject bulletPrefab;


    void FixedUpdate() {
        float moveHorizontal = Input.GetAxis("Horizontal");

        Vector2 movement = new Vector2(moveHorizontal, 0.0f);
        GetComponent<Rigidbody>().velocity = movement*speed;

        GetComponent<Rigidbody>().position = new Vector2(
            Mathf.Clamp(GetComponent<Rigidbody>().position.x, xMin, xMax), 
            y
        );
        
    }

    public void shoot(){
        GameObject bullet=Instantiate(bulletPrefab) as GameObject;
        bullet.transform.position=GetComponent<Rigidbody>().position;
        // Debug.Log(GetComponent<Rigidbody>().position);
    }

    void Update(){
        if(Input.GetKeyDown("space")){
            shoot();
        }
    }

}
