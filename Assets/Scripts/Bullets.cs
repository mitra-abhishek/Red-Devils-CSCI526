using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{   
    private Rigidbody2D rb;
    private Vector2 screenBounds;
    private Camera mainCam;
    private Rigidbody2D rigidBullet;
    private Vector3 posMouse;
    public float speed = 7f;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidBullet = this.GetComponent<Rigidbody2D>();
        posMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 posBody = transform.position;
        Vector3 rotation = posBody - posMouse;
        Vector3 bulletShootDirection = posMouse - posBody;
        rigidBullet.velocity = new Vector2(bulletShootDirection.x, bulletShootDirection.y).normalized * speed;
        float bulletTransformAngle = Mathf.Atan2 (rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler (0, 0, bulletTransformAngle+90);
        screenBounds=Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));   
    }

    // Update is called once per frame
    void Update() 
    {
        if(transform.position.y>screenBounds.y*1){
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag=="rock"){
            Destroy(other.gameObject);

        }
    }
}