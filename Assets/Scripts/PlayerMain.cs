using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerMain : MonoBehaviour
{
    public int maxHealth=100;
    public int currentHealth;

    public HealthBar healthBar;
    void Start()
    {
       currentHealth=maxHealth; 
       healthBar.setMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public void TakeDamage(int damage){
        currentHealth-=damage;
        healthBar.setHealth(currentHealth);
        if (currentHealth<=0){
            Destroy(this.gameObject);
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag=="rock"){
            TakeDamage(20);
        }
         if(other.tag == "Letter") {
            EventManager.TriggerEvent ("test", new Dictionary<string, object> { { "amount", other } });
        }
    }
}
