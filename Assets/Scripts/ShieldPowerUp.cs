using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPowerUp : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 screenBounds;

    public float shieldPowerUpSpeed;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.shieldPowerUpController.addTotalPowerUpsGenerated();
        rb = this.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, -shieldPowerUpSpeed);
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
            GameObject parent = GameObject.Find("ShieldParent");
            GameObject shield = parent.transform.Find("Shield").gameObject;
            shield.transform.position = GameObject.Find("player").transform.position;
            shield.SetActive(true);
            GameManager.instance.shieldPowerUpController.addTotalPowerUpsCollected();
            GameObject shieldTimer = parent.transform.Find("ShieldTimer").gameObject;
            shieldTimer.SetActive(true);
            GameManager.instance.playPowerupCollect();
            Destroy(this.gameObject);
        }
    }
}
