using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerMain : MonoBehaviour
{
    public int maxHealth=100;
    public int currentHealth;
    public Dictionary<string, int> pairs = new Dictionary<string, int>()
    {
        { "SampleScene 2", 1 }, { "Level 2", 2 },{"Level 3",3}
    };
    [SerializeField] SendToGoogle sendToGoogle;

    public HealthBar healthBar;
    public Timer timer;
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
        Debug.Log("The scence health is"+currentHealth);
        if (currentHealth<=0){
            sendToGoogle.UpdateUnsuccessfulTriesAnalytics(pairs[UnityEngine.SceneManagement.SceneManager.GetActiveScene().name],false);
            Destroy(this.gameObject);
            UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene 2");
        }
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag=="rock"){
            TakeDamage(20);
        }
        //  if(other.tag == "Letter") {
        //     EventManager.TriggerEvent ("test", new Dictionary<string, object> { { "amount", other } });
        // }
    }
}
