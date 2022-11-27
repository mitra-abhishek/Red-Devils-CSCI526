using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPowerup : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 screenBounds;

    public float healthPowerUpSpeed;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.healthPowerUpController.addTotalPowerUpsGenerated();
        rb = this.GetComponent<Rigidbody2D>();
        // Change to add bullet speed if needed
        rb.velocity = new Vector2(0, -healthPowerUpSpeed);
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < screenBounds.y * -1)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("player"))
        {
            FindObjectOfType<PlayerMain>().TakeDamage(-10);
            GameManager.instance.healthPowerUpController.addTotalPowerUpsCollected();
            //Debug.Log("Health Powerup detected");
            GameManager.instance.playPowerupCollect();
            //GameManager.instance.bulletController.addBullets(5);
            Destroy(this.gameObject);
        }
    }
}
