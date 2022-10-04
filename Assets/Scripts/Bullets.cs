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
            // Destroy(this.gameObject);

        }
        if(other.tag == "Letter") {
            EventManager.TriggerEvent ("test", new Dictionary<string, object> { { "amount", other } });
            Destroy(other.gameObject);
            // Destroy(this.gameObject);
        }

        if(other.tag == "enemy"){
            Destroy(other.gameObject);
        }
    }
}