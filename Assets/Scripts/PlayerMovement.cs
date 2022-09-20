using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float xMin;
    public float xMax;
    public float y;
    private Vector2 screenBounds;

    public GameObject bulletPrefab;


    void FixedUpdate() {
        screenBounds=Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        float moveHorizontal = Input.GetAxis("Horizontal");

        Vector2 movement = new Vector2(moveHorizontal, 0.0f);
        GetComponent<Rigidbody2D>().velocity = movement*speed;

        GetComponent<Rigidbody2D>().position = new Vector2(
            Mathf.Clamp(GetComponent<Rigidbody2D>().position.x, -screenBounds.x, screenBounds.x), 
            y
        );
        
    }

    public void shoot(){
        GameObject bullet=Instantiate(bulletPrefab) as GameObject;
        bullet.transform.position=GetComponent<Rigidbody2D>().position;
        // Debug.Log(GetComponent<Rigidbody2D>().position);
    }

    void Update(){
        if(Input.GetKeyDown("space")){
            shoot();
        }
    }

}
