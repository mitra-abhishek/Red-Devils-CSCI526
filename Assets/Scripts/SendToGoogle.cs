using System;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Networking;


public class SendToGoogle : MonoBehaviour
{

    [SerializeField]private string levelCompletionURL;
    [SerializeField]private string levelAttemptsURL;
    [SerializeField]private string averageHealthbarAnalyticsURL;

    private long _sessionID;
    private int _testUserHealth;
    public PlayerMain playerMain;

    // Unsuccesful Attempts
    private int _currentLevel;
    private bool _isLevelCompleted;


    // Finishing Time Analytics:
    private double _completionTime;
    private int _completedLevel;

    // Health Analytics:
    private int _currentHealth;

    private void Awake()
    {
        Debug.Log("SendToGoogle: Awake Called.");
        _sessionID = DateTime.Now.Ticks;
        Debug.Log("_sessionID = " + _sessionID);
        
    }
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SendLevelCompletionAnalytics()
    {
        //Assign variables

        // StartCoroutine(Post(_sessionID.ToString(), _testUserHealth.ToString(),"1"));
        if(this.gameObject!=null){
        StartCoroutine(PostLevelCompletionAnalytics(_sessionID.ToString(), _completionTime.ToString(),_completedLevel.ToString()));
        }
    }

    private IEnumerator PostLevelCompletionAnalytics(string sessionID, string completionTime,string completedLevel)
    {
        //Create Form and enter responses
        WWWForm form = new WWWForm();
        form.AddField("entry.152211496", sessionID);
        form.AddField("entry.294203487",completedLevel);
        form.AddField("entry.1334543222", completionTime);

        //Send responses and verify result
        using (UnityWebRequest www = UnityWebRequest.Post(levelCompletionURL, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("SendToGoogle.Post: Form upload completed.");
            }
        }

    }

    public void SendUnsuccessfulTriesAnalytics()
    {
        //Assign variables

        // StartCoroutine(Post(_sessionID.ToString(), _testUserHealth.ToString(),"1"));
        if(this.gameObject!=null){
        StartCoroutine(PostUnsuccessfulTriesAnalytics(_sessionID.ToString(), _currentLevel.ToString(),_isLevelCompleted.ToString()));
        }
    }
    
    private IEnumerator PostUnsuccessfulTriesAnalytics(string sessionID, string currentLevel,string isLevelCompleted)
    {
        //Create Form and enter responses
        Debug.Log("Unsuccessful Tries Analytics");
        WWWForm form = new WWWForm();
        form.AddField("entry.1366828310", sessionID);
        form.AddField("entry.363749641",currentLevel);
        form.AddField("entry.1458904313",isLevelCompleted);
        //Send responses and verify result
        using (UnityWebRequest www = UnityWebRequest.Post(levelAttemptsURL, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("SendToGoogle.Post: Form upload completed.");
            }
        }

    }

    public void SendHealthbarAnalytics(){
        if(this.gameObject!=null){
        StartCoroutine(PostHealthbarAnalytics(_sessionID.ToString(), _completedLevel.ToString(),_currentHealth.ToString()));
        }
    }

    private IEnumerator PostHealthbarAnalytics(string sessionID, string completedLevel,string currentHealth)
    {
        Debug.Log("The Healthbar Analytics"+completedLevel);
        //Create Form and enter responses
        WWWForm form = new WWWForm();
        form.AddField("entry.628208234", sessionID);
        form.AddField("entry.836810572",completedLevel);
        form.AddField("entry.1135885735",currentHealth);
        //Send responses and verify result
        using (UnityWebRequest www = UnityWebRequest.Post(averageHealthbarAnalyticsURL, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("SendToGoogle.Post: Form upload completed.");
            }
        }

    }

    public void  UpdateLevelAnalytics(int completedLevel,double completionTime){
        _completionTime=completionTime;
        _completedLevel=completedLevel;
        if(this!=null){
        SendLevelCompletionAnalytics();
        }
    }

    public void UpdateUnsuccessfulTriesAnalytics(int current_level,bool isLevelCompleted){
        _currentLevel=current_level;
        _isLevelCompleted=isLevelCompleted;
        Debug.Log("Unsuccessful Tries Function");
        if(this!=null){
            SendUnsuccessfulTriesAnalytics();
        }
    }

    public void UpdateHealthbarAnalytics(int completedLevel, int currentHealth){
        _completedLevel=completedLevel;
        _currentHealth=currentHealth;
        if(this!=null){
            SendHealthbarAnalytics();
        }
    }
// Compiling
}
