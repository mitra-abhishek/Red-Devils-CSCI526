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
    private Vector2 posMouse;
    private Vector2 mouseMove;

    public GameObject bulletPrefab;
    public Camera sceneCam;
    public Rigidbody2D rb;

    void mouseMovement(){
        float xAxis=Input.GetAxisRaw("Horizontal");
        float yAxis=Input.GetAxisRaw("Vertical");
        mouseMove=new Vector2(xAxis, yAxis).normalized; 
        posMouse=sceneCam.ScreenToWorldPoint(Input.mousePosition);
    }


    void FixedUpdate() {
        screenBounds=Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        float moveHorizontal = Input.GetAxis("Horizontal");

        Vector2 movement = new Vector2(moveHorizontal, 0.0f);
        GetComponent<Rigidbody2D>().velocity = movement*speed;

        GetComponent<Rigidbody2D>().position = new Vector2(
            Mathf.Clamp(GetComponent<Rigidbody2D>().position.x, -screenBounds.x, screenBounds.x), 
            y
        );

        rb.velocity=new Vector2(mouseMove.x*5, mouseMove.y*5);
        Vector2 aimDirection=posMouse-rb.position;
        float aimAngle=Mathf.Atan2(aimDirection.y, aimDirection.x)*Mathf.Rad2Deg-90f;
        rb.rotation=aimAngle;
        
    }

    public void shoot(){
        GameObject bullet=Instantiate(bulletPrefab) as GameObject;
        bullet.transform.position=GetComponent<Rigidbody2D>().position;
    }

    void Update(){
        if(Input.GetKeyDown("space") || Input.GetMouseButtonDown(0)){
            shoot();
        }
        mouseMovement();
    }

}
