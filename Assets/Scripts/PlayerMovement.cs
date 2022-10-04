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
    private float angle = 0;
    public float bulletSpeed = 7f;

    public GameObject bulletPrefab;
    public Camera sceneCam;
    public Rigidbody2D rb;
    private int curBullets;

    void Start()
    {   
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
    }

    public void shoot(){
        GameObject bullet=Instantiate(bulletPrefab) as GameObject;
        bullet.transform.position=GetComponent<Rigidbody2D>().position;
        Vector2 bulletDirection = new Vector2(Mathf.Sin(Mathf.Deg2Rad * -angle), Mathf.Cos(Mathf.Deg2Rad * -angle));
        bullet.GetComponent<Rigidbody2D>().velocity = bulletDirection * bulletSpeed;
        bullet.transform.eulerAngles = new Vector3(0, 0, angle);
    }

    void Update(){
        if (Input.GetKey(KeyCode.RightArrow)) {
            if(angle>= -90)
            angle -= 3.7f;
            transform.eulerAngles = new Vector3(0, 0, angle);
            print(angle);
        }

        if (Input.GetKey(KeyCode.LeftArrow)) {
            
            if(angle<= 90)
            angle += 3.7f;
            transform.eulerAngles = new Vector3(0, 0, angle);
            print(angle);
        }


        if(GameManager.instance.bulletController.getBullets()>0 && (Input.GetKeyDown("space") || Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.UpArrow))){
            shoot();
            GameManager.instance.bulletController.subtractBullet();
        }
        if(GameManager.instance.bulletController.getBullets()==0){
            Destroy(this.gameObject);
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        }
    }

}
