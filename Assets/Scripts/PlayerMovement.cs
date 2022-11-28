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
    public Transform firePoint;
    private int totalPowerUpsCollected;
    private int totalPowerUpsGenerated;
    [SerializeField] SendToGoogle sendToGoogle;

    public Dictionary<string, int> pairs = new Dictionary<string, int>()
        {
            {"Tutorial",0},{ "Planet", 1 }, { "Animals", 2 },{"Country",3},{"Sport",4}
        };


    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

    }


    void FixedUpdate()
    {

        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        if (Input.GetKey(KeyCode.UpArrow))
        {
            // switch to rotation mode
            if (Input.GetKey(KeyCode.LeftArrow))
            {

                if (angle <= 90)
                    angle += ANGULAR_SPEED * WEBGL_MULTIPLIER;
                transform.eulerAngles = new Vector3(0, 0, angle);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {

                if (angle >= -90)
                    angle -= ANGULAR_SPEED * WEBGL_MULTIPLIER;
                transform.eulerAngles = new Vector3(0, 0, angle);
            }
            Vector2 movement = new Vector2(0.0f, 0.0f);
            GetComponent<Rigidbody2D>().velocity = movement * speed;
        }
        else
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            // switch to horizontal movement after resetting angle
            angle = 0;
            transform.eulerAngles = new Vector3(0, 0, angle);


            Vector2 movement = new Vector2(moveHorizontal, 0.0f);
            GetComponent<Rigidbody2D>().velocity = movement * speed;

        }
        GetComponent<Rigidbody2D>().position = new Vector2(
                Mathf.Clamp(GetComponent<Rigidbody2D>().position.x, -screenBounds.x, screenBounds.x),
                y
            );


    }

    public void shoot()
    {
        GameManager.instance.playLaserSound();
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation) as GameObject;
        bullet.transform.position = firePoint.position;
        Vector2 bulletDirection = new Vector2(Mathf.Sin(Mathf.Deg2Rad * -angle), Mathf.Cos(Mathf.Deg2Rad * -angle));
        bullet.GetComponent<Rigidbody2D>().velocity = bulletDirection * bulletSpeed;
        bullet.transform.eulerAngles = new Vector3(0, 0, angle);
    }

    void Update()
    {
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


        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            Input.ResetInputAxes();
        }

        if (GameManager.instance.bulletController.getBullets() > 0 && Input.GetKeyDown("space"))
        {
            shoot();
            GameManager.instance.bulletController.subtractBullet();
        }
        if (GameManager.instance.bulletController.getBullets() == 0)
        {
            totalPowerUpsGenerated = GameManager.instance.bulletPowerUpController.getTotalPowerUpsGenerated() + GameManager.instance.healthPowerUpController.getTotalPowerUpsGenerated() + GameManager.instance.shieldPowerUpController.getTotalPowerUpsGenerated();
            totalPowerUpsCollected = GameManager.instance.bulletPowerUpController.getTotalPowerUpsCollected() + GameManager.instance.healthPowerUpController.getTotalPowerUpsCollected() + GameManager.instance.shieldPowerUpController.getTotalPowerUpsCollected();

            sendToGoogle.UpdateUnsuccessfulTriesAnalytics(pairs[UnityEngine.SceneManagement.SceneManager.GetActiveScene().name], false);
            sendToGoogle.UpdateResonForDeathAnalytics(pairs[UnityEngine.SceneManagement.SceneManager.GetActiveScene().name], "Ammo Finished");
            //Debug.Log("Check letter count when health is over" + GameManager.instance.totalLettersShot + "---- correct" + GameManager.instance.characterShotCount + "level---" + pairs[UnityEngine.SceneManagement.SceneManager.GetActiveScene().name]);
            sendToGoogle.UpdateCorrectLettersShotAnalytics(pairs[UnityEngine.SceneManagement.SceneManager.GetActiveScene().name], GameManager.instance.totalLettersShot, GameManager.instance.characterShotCount, "healthbar finished");
            sendToGoogle.UpdatePowerUpsUsageAnalytics(pairs[UnityEngine.SceneManagement.SceneManager.GetActiveScene().name], totalPowerUpsGenerated, totalPowerUpsCollected);
            //GameManager.instance.wordCompleted = false;
            Destroy(this.gameObject);
            //UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene 2");
            GameManager.instance.lossScreen("Out of Ammo");
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
        //Debug.Log("Increase Speed"); 
        speed *= SmartEnemyPrefabGenerator.instance.reduceSpeedBy;
    }

}
