using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 screenBounds;
    public GameObject enemy_bulletPrefab;
    public float enemy_bulletSpeed = 7f;
    private float angle = 0;
    private float enemy_bullet_ReAppearTime=1.0f;
    private float speed = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        rb=this.GetComponent<Rigidbody2D>();
        rb.velocity=new Vector2(0,-speed);
        screenBounds=Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        StartCoroutine(enemy_bulletLoop());
    }

    public void shoot(){
        GameObject bullet=Instantiate(enemy_bulletPrefab) as GameObject;
        bullet.transform.position=GetComponent<Rigidbody2D>().position;
        Vector2 bulletDirection = new Vector2(Mathf.Sin(Mathf.Deg2Rad * -angle), Mathf.Cos(Mathf.Deg2Rad * -angle));
        bullet.GetComponent<Rigidbody2D>().velocity = -bulletDirection * enemy_bulletSpeed;
        bullet.transform.eulerAngles = new Vector3(0, 0, angle);
    }

    IEnumerator enemy_bulletLoop(){
        while(true)
        {
            yield return new WaitForSeconds(0.05f);
            shoot();
            yield return new WaitForSeconds(enemy_bullet_ReAppearTime);

        }
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y<screenBounds.y*-1){
            Destroy(this.gameObject);
        }
    }
}
