using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldMovement : MonoBehaviour
{
    public float speed;
    public float xMin;
    public float xMax;
    public float y;
    private Vector2 screenBounds;

    public Camera sceneCam;
    public Rigidbody2D rb;
    
    public static ShieldMovement instance;

    
    void Start()
    {   
        if (instance == null)
        {
            instance = this;
        }
    }


    void FixedUpdate() {
        if(!Input.GetKey(KeyCode.UpArrow))
        {
            screenBounds=Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
            float moveHorizontal = Input.GetAxis("Horizontal");

            Vector2 movement = new Vector2(moveHorizontal, 0.0f);
            GetComponent<Rigidbody2D>().velocity = movement*speed;

            GetComponent<Rigidbody2D>().position = new Vector2(
                Mathf.Clamp(GetComponent<Rigidbody2D>().position.x, -screenBounds.x, screenBounds.x), 
                y
            );
        }
        else
        {
            Vector2 movement = new Vector2(0.0f, 0.0f);
            GetComponent<Rigidbody2D>().velocity = movement*speed;

            GetComponent<Rigidbody2D>().position = new Vector2(
                Mathf.Clamp(GetComponent<Rigidbody2D>().position.x, -screenBounds.x, screenBounds.x), 
                y
            );
        }
    }

    void Update(){}

    public void decreaseShieldSpeedCaller()
    {
        StartCoroutine(decreaseShieldSpeed());
    }
    public IEnumerator decreaseShieldSpeed()
    {
        speed /= SmartEnemyPrefabGenerator.instance.reduceSpeedBy;
        yield return new WaitForSeconds(SmartEnemyPrefabGenerator.instance.freezeTime);
        Debug.Log("Increase Speed"); 
        speed *= SmartEnemyPrefabGenerator.instance.reduceSpeedBy;
    }
}
