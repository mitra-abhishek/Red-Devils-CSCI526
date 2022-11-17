using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 screenBounds;
    private Camera mainCam;
    private Rigidbody2D rigidBullet;
    public float speed = 7f;

    Vector3 pos_target;
    
    // Start is called before the first frame update
    void Start()
    {
        pos_target =  FindObjectOfType<PlayerMain>().transform.position;

        screenBounds=Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));   
    }

    // Update is called once per frame
    void Update() 
    {

        transform.position = Vector2.MoveTowards(transform.position, pos_target, speed * Time.deltaTime);

        if(-transform.position.y>screenBounds.y*1){
            Destroy(this.gameObject);
        }
        if (transform.position.y < pos_target.y) {
            Destroy(this.gameObject);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Collision with Ice");
        if (col.gameObject.tag == "player")
        {
            PlayerMovement.instance.decreasePlayerSpeedCaller();
            if (ShieldMovement.instance)
                ShieldMovement.instance.decreaseShieldSpeedCaller();
            Destroy(this.gameObject);
        }
    }
}