using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 screenBounds;
    private Camera mainCam;
    private Rigidbody2D rigidBullet;
    public float speed = 7f;
    
    // Start is called before the first frame update
    void Start()
    {
        screenBounds=Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));   
    }

    // Update is called once per frame
    void Update() 
    {
        if(-transform.position.y>screenBounds.y*1){
            Destroy(this.gameObject);
        }
    }
}
