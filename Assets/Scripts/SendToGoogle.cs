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

    private long _sessionID;
    private int _testUserHealth;
    public PlayerMain playerMain;

    // Unsuccesful Attempts
    private int _currentLevel;


    // Finishing Time Analytics:
    private double _completionTime;
    private int _completedLevel;

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
        StartCoroutine(PostUnsuccessfulTriesAnalytics(_sessionID.ToString(), _currentLevel.ToString()));
        }
    }
    
    private IEnumerator PostUnsuccessfulTriesAnalytics(string sessionID, string currentLevel)
    {
        //Create Form and enter responses
        WWWForm form = new WWWForm();
        form.AddField("entry.1366828310", sessionID);
        form.AddField("entry.363749641",currentLevel);
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

    public void  UpdateLevelAnalytics(int completedLevel,double completionTime){
        _completionTime=completionTime;
        _completedLevel=completedLevel;
        if(this!=null){
        SendLevelCompletionAnalytics();
        }
    }

    public void UpdateUnsuccessfulTriesAnalytics(int current_level){
        _currentLevel=current_level;
        SendUnsuccessfulTriesAnalytics();

    }

}
