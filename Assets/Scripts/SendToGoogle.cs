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
    [SerializeField]private string deathReasonAnalyticsURL;
    [SerializeField]private string correctLettersShotAnalytics;

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

    //deathReason Analytics
    private string _deathReason;

    //total vs correct character shot analytics:
    private int _totalShotsCount;
    private int _correctShotCount;

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

    public void SendResonForDeathAnalytics(){
        if(this.gameObject!=null){
        StartCoroutine(PostResonForDeathAnalytics(_sessionID.ToString(), _currentLevel.ToString(),_deathReason));
        }
    }

    private IEnumerator PostResonForDeathAnalytics(string sessionID, string currentLevel,string deathReason)
    {
        //Create Form and enter responses
        Debug.Log("The reason for the death is"+currentLevel+"----"+deathReason);
        WWWForm form = new WWWForm();
        form.AddField("entry.1761087184", sessionID);
        form.AddField("entry.2142992592",currentLevel);
        form.AddField("entry.2025417393",deathReason);
        //Send responses and verify result
        using (UnityWebRequest www = UnityWebRequest.Post(deathReasonAnalyticsURL, form))
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

    public void SendCorrectLettersShotAnalytics(){
        if(this.gameObject!=null){
        StartCoroutine(PostCorrectLettersShotAnalytics(_sessionID.ToString(), _currentLevel.ToString(),_totalShotsCount.ToString(),_correctShotCount.ToString()));
        }
    }

    private IEnumerator PostCorrectLettersShotAnalytics(string sessionID, string currentLevel,string totalShotsCount,string correctShotCount)
    {
        //Create Form and enter responses
        WWWForm form = new WWWForm();
        form.AddField("entry.298398774", sessionID);
        form.AddField("entry.692591639",currentLevel);
        form.AddField("entry.561116185",totalShotsCount);
        form.AddField("entry.352160701",correctShotCount);
        //Send responses and verify result
        using (UnityWebRequest www = UnityWebRequest.Post(correctLettersShotAnalytics, form))
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

    public void UpdateResonForDeathAnalytics(int current_level,string deathReason){
        _deathReason=deathReason;
        _currentLevel=current_level;
        if(this!=null){
            SendResonForDeathAnalytics();
        }
    }
    public void UpdateCorrectLettersShotAnalytics(int currentLevel,int totalShotsCount,int correctShotCount){
     _totalShotsCount=totalShotsCount;
     _correctShotCount=correctShotCount;  
     if(this!=null){
        SendCorrectLettersShotAnalytics();
     } 
    }
// Compiling
}
