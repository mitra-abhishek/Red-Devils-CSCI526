using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{   
    public float speed=15.0f;
    private Rigidbody2D rb;
    private Vector2 screenBounds;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("hello");
        rb=this.GetComponent<Rigidbody2D>();
        rb.velocity=new Vector2(0, speed);
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
        // Debug.Log(other.tag);
        // if(other.tag == "Letter") {
        //     EventManager.TriggerEvent ("test", new Dictionary<string, object> { { "amount", other } });
        // }
        if(other.tag=="rock"){
            // Debug.Log(other.gameObject);
            Destroy(other.gameObject);

        }
    }
}

// {
//     public float speed=50.0f;
//     private Rigidbody2D rb;
//     private Vector2 screenBounds;
    
//     // Start is called before the first frame update
//     void Start()
//     {
//         rb=this.GetComponent<Rigidbody2D>();
//         rb.velocity=new Vector2(0, speed);
//         screenBounds=Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         // if(transform.position.y<screenBounds.y*-2){
//         //     Debug.Log(transform.position.y);
//         //     Destroy(this.gameObject);
//         // }
//     }
// }