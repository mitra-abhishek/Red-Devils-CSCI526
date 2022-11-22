
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public float currentTime = 0f;
    public float startingTime = 120f;
    public Dictionary<string, int> pairs = new Dictionary<string, int>()
    {
        { "SampleScene 2", 1 }, { "Level 2", 2 },{"Level 3",3}
    };

    [SerializeField] Text countdownText;
    [SerializeField] SendToGoogle sendToGoogle;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = startingTime;

    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= Time.deltaTime;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        //countdownText.text = currentTime.ToString("0");
        countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (currentTime <= 30) {
            countdownText.color = Color.red;
        }

        if (currentTime <= 60) {
            GameManager.instance.oneMinLeft = true;
        }

        if (currentTime <= 0) {
            currentTime = 0;
            sendToGoogle.UpdateUnsuccessfulTriesAnalytics(pairs[UnityEngine.SceneManagement.SceneManager.GetActiveScene().name],false);
            sendToGoogle.UpdateResonForDeathAnalytics(pairs[UnityEngine.SceneManagement.SceneManager.GetActiveScene().name],"Time Finished");
            sendToGoogle.UpdateCorrectLettersShotAnalytics(pairs[UnityEngine.SceneManagement.SceneManager.GetActiveScene().name],GameManager.instance.totalLettersShot,GameManager.instance.characterShotCount,"healthbar finished");
            sendToGoogle.UpdatePowerUpsUsageAnalytics(pairs[UnityEngine.SceneManagement.SceneManager.GetActiveScene().name],GameManager.instance.bulletPowerUpController.getTotalPowerUpsGenerated(),GameManager.instance.bulletPowerUpController.getTotalPowerUpsCollected());
            Destroy(this.gameObject);
            UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene 2");
        }
    }
}