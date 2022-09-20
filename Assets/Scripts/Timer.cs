
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    float currentTime = 0f;
    public float startingTime = 120f;

    [SerializeField] Text countdownText;

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

        if (currentTime <= 0) {
            currentTime = 0;
            Destroy(this.gameObject);
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        }
    }
}