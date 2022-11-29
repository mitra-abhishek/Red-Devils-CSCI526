using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerMain : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public GameObject gotHitScreen;
    private GameObject explosion;
    private int totalPowerUpsCollected;
    private int totalPowerUpsGenerated;
    public Dictionary<string, int> pairs = new Dictionary<string, int>()
    {
        {"Tutorial",0},{ "Planet", 1 }, { "Animals", 2 },{"Country",3},{"Sport",4}
    };
    [SerializeField] SendToGoogle sendToGoogle;

    public HealthBar healthBar;
    public Timer timer;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);
        explosion = Resources.Load<GameObject>("Explosion/DustExplosion");
    }

    // Update is called once per frame
    void Update()
    {
        if (gotHitScreen != null)
        {
            if (gotHitScreen.GetComponent<Image>().color.a > 0)
            {
                var color = gotHitScreen.GetComponent<Image>().color;
                color.a -= 0.005f;
                gotHitScreen.GetComponent<Image>().color = color;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth > 100)
        {
            currentHealth = 100;
        }
        healthBar.setHealth(currentHealth);
        //Debug.Log("The scence health is" + currentHealth);
        if (currentHealth <= 0)
        {
            totalPowerUpsGenerated = GameManager.instance.bulletPowerUpController.getTotalPowerUpsGenerated() + GameManager.instance.healthPowerUpController.getTotalPowerUpsGenerated() + GameManager.instance.shieldPowerUpController.getTotalPowerUpsGenerated();
            totalPowerUpsCollected = GameManager.instance.bulletPowerUpController.getTotalPowerUpsCollected() + GameManager.instance.healthPowerUpController.getTotalPowerUpsCollected() + GameManager.instance.shieldPowerUpController.getTotalPowerUpsCollected();

            sendToGoogle.UpdateUnsuccessfulTriesAnalytics(pairs[UnityEngine.SceneManagement.SceneManager.GetActiveScene().name], false);
            sendToGoogle.UpdateResonForDeathAnalytics(pairs[UnityEngine.SceneManagement.SceneManager.GetActiveScene().name], "Health Finished");
            //Debug.Log("Check letter count when health is over" + GameManager.instance.totalLettersShot + "---- correct" + GameManager.instance.characterShotCount + "level---" + pairs[UnityEngine.SceneManagement.SceneManager.GetActiveScene().name]);
            sendToGoogle.UpdateCorrectLettersShotAnalytics(pairs[UnityEngine.SceneManagement.SceneManager.GetActiveScene().name], GameManager.instance.totalLettersShot, GameManager.instance.characterShotCount, "healthbar finished");
            sendToGoogle.UpdatePowerUpsUsageAnalytics(pairs[UnityEngine.SceneManagement.SceneManager.GetActiveScene().name], totalPowerUpsGenerated, totalPowerUpsCollected);
            //GameManager.instance.wordCompleted = false;
            Destroy(this.gameObject);
            //UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene 2");
            GameManager.instance.lossScreen("Out of Health");
        }
    }

    private bool hasCollectedLetter(char letterShot) {
        if(GameManager.instance.Level == 1) {
            bool wrongLetter = true;
            for(int i=0; i<GameManager.instance.LevelWord.Length; i++) {
                if(GameManager.instance.LevelWord[i] == letterShot && LevelManagerLevel1.letterMap[i] == '/') {
                    wrongLetter = false;
                    break;
                }
            }
            return wrongLetter;
        } else if (GameManager.instance.Level == 2) {
            bool wrongLetter = true;
            for(int i=0; i<GameManager.instance.LevelWord.Length; i++) {
                if(GameManager.instance.LevelWord[i] == letterShot && LevelManagerLevel2.letterMap[i] == '/') {
                    wrongLetter = false;
                    break;
                }
            }
            return wrongLetter;
        } else if (GameManager.instance.Level == 3) {
            bool wrongLetter = true;
            for(int i=0; i<GameManager.instance.LevelWord.Length; i++) {
                if(GameManager.instance.LevelWord[i] == letterShot && LevelManagerLevel3.letterMap[i] == '/') {
                    wrongLetter = false;
                    break;
                }
            }
            return wrongLetter;
        } else if (GameManager.instance.Level == 4) {
            bool wrongLetter = true;
            for(int i=0; i<GameManager.instance.LevelWord.Length; i++) {
                if(GameManager.instance.LevelWord[i] == letterShot && LevelManagerLevel4.letterMap[i] == '/') {
                    wrongLetter = false;
                    break;
                }
            }
            return wrongLetter;
        }

        return false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var color = gotHitScreen.GetComponent<Image>().color;
        color.a = 0.8f;
        if (other.tag == "rock")
        {
            gotHitScreen.GetComponent<Image>().color = color;
            Destroy(other.gameObject);
            Instantiate(explosion, transform.position, transform.rotation);
            TakeDamage(10);
        }
        if (other.tag == "Letter")
        {
            bool alreadyCollected = hasCollectedLetter(other.name[0]);
            EventManager.TriggerEvent("test", new Dictionary<string, object> { { "amount", other } });
            if (GameManager.instance.LevelWord.IndexOf(other.name[0]) == -1 || alreadyCollected)
            {
                gotHitScreen.GetComponent<Image>().color = color;
                TakeDamage(10);
            }
            Destroy(other.gameObject);
            // Destroy(this.gameObject);
        }
        if (other.tag == "enemy_bullet")
        {
            gotHitScreen.GetComponent<Image>().color = color;
            //Debug.Log("enemy bullet detected");
            TakeDamage(10);
        }
        if (other.tag == "smart_enemy_bullet")
        {
            gotHitScreen.GetComponent<Image>().color = color;
            TakeDamage(10);
        }
    }
}