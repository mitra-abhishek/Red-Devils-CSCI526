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

    public float ANGULAR_SPEED = 3.7f;

    public float WEBGL_MULTIPLIER = 1.0f;

    public static PlayerMovement instance;




    void Start()
    {   
        if (instance == null)
        {
            instance = this;
        }

    }


    void FixedUpdate() {

        screenBounds=Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        
        if(Input.GetKey(KeyCode.UpArrow))
        {
            // switch to rotation mode
            if (Input.GetKey(KeyCode.LeftArrow)) 
            {
            
                if(angle<= 90)
                    angle += ANGULAR_SPEED*WEBGL_MULTIPLIER;
                transform.eulerAngles = new Vector3(0, 0, angle);
            }
            else if (Input.GetKey(KeyCode.RightArrow)) 
            {
            
                if(angle>= -90)
                    angle -= ANGULAR_SPEED*WEBGL_MULTIPLIER;
                transform.eulerAngles = new Vector3(0, 0, angle);
            }
            Vector2 movement = new Vector2(0.0f, 0.0f);
            GetComponent<Rigidbody2D>().velocity = movement*speed;
        }
        else
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            // switch to horizontal movement after resetting angle
            angle = 0;
            transform.eulerAngles = new Vector3(0, 0, angle);

            
            Vector2 movement = new Vector2(moveHorizontal, 0.0f);
            GetComponent<Rigidbody2D>().velocity = movement*speed;
  
        }
        GetComponent<Rigidbody2D>().position = new Vector2(
                Mathf.Clamp(GetComponent<Rigidbody2D>().position.x, -screenBounds.x, screenBounds.x),
                y
            );
        
        
    }

    public void shoot(){
        GameManager.instance.playLaserSound();
        GameObject bullet=Instantiate(bulletPrefab) as GameObject;
        bullet.transform.position=GetComponent<Rigidbody2D>().position;
        Vector2 bulletDirection = new Vector2(Mathf.Sin(Mathf.Deg2Rad * -angle), Mathf.Cos(Mathf.Deg2Rad * -angle));
        bullet.GetComponent<Rigidbody2D>().velocity = bulletDirection * bulletSpeed;
        bullet.transform.eulerAngles = new Vector3(0, 0, angle);
    }

    void Update(){
        // if (Input.GetKey(KeyCode.RightArrow)) {
        //     if(angle>= -90)
        //         angle -= ANGULAR_SPEED*WEBGL_MULTIPLIER;
            
        //     print(angle);
        // }

        // else if (Input.GetKey(KeyCode.LeftArrow)) {
            
        //     if(angle<= 90)
        //         angle += ANGULAR_SPEED*WEBGL_MULTIPLIER;
        //     transform.eulerAngles = new Vector3(0, 0, angle);
        //     print(angle);
        // }

        // else
        // {
        //     if(angle>= 0)
        //         angle -= ANGULAR_SPEED*WEBGL_MULTIPLIER;
            
        //     if(angle<= 0)
        //         angle += ANGULAR_SPEED*WEBGL_MULTIPLIER;

        //     transform.eulerAngles = new Vector3(0, 0, angle);
        // }

        
        if(Input.GetKeyUp(KeyCode.UpArrow))
        {
            Input.ResetInputAxes();
        }

        if(GameManager.instance.bulletController.getBullets()>0 && Input.GetKeyDown("space")){
            shoot();
            GameManager.instance.bulletController.subtractBullet();
        }
        if(GameManager.instance.bulletController.getBullets()==0){
            Destroy(this.gameObject);
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void decreasePlayerSpeedCaller()
    {
        StartCoroutine(decreasePlayerSpeed());
    }
    public IEnumerator decreasePlayerSpeed()
    {
        speed /= SmartEnemyPrefabGenerator.instance.reduceSpeedBy;
        yield return new WaitForSeconds(SmartEnemyPrefabGenerator.instance.freezeTime);
        Debug.Log("Increase Speed"); 
        speed *= SmartEnemyPrefabGenerator.instance.reduceSpeedBy;
    }

}
