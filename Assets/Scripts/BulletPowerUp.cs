using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPowerUp : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 screenBounds;

    public float bulletPowerUpSpeed = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Bullet Powerup Created");
        rb=this.GetComponent<Rigidbody2D>();
        // Change to add bullet speed if needed
        rb.velocity=new Vector2(0,-bulletPowerUpSpeed);
        screenBounds=Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y<screenBounds.y*-1){
            Destroy(this.gameObject);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("player"))
        {
            Debug.Log("Bullet Powerup added to Player");
        }
    }
}
