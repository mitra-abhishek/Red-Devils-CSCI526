using System;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Networking;


public class SendToGoogle : MonoBehaviour
{

    [SerializeField]private string URL;

    private long _sessionID;
    private int _testInt;
    private bool _testBool;
    private float _testFloat;

    private void Awake()
    {
        Debug.Log("SendToGoogle: Awake Called.");
        _sessionID = DateTime.Now.Ticks;
        Debug.Log("_sessionID = " + _sessionID);
        
        Send();
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("SendToGoogle: Start Called.");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Send()
    {
        //Assign variables
        _testInt = UnityEngine.Random.Range(0, 101);
        _testBool = true;
        _testFloat = UnityEngine.Random.Range(0.0f, 10.0f);

        StartCoroutine(Post(_sessionID.ToString(), _testInt.ToString(), 
                        _testBool.ToString(), _testFloat.ToString()));
    }

    private IEnumerator Post(string sessionID, string testInt, string testBool, 
                                string testFloat)
    {
        //Create Form and enter responses
        WWWForm form = new WWWForm();
        form.AddField("entry.1975679220", sessionID);
        form.AddField("entry.276344474", testInt);
        form.AddField("entry.2137653498", testBool);
        form.AddField("entry.562767763", testFloat);

        //Send responses and verify result
        using (UnityWebRequest www = UnityWebRequest.Post(URL, form))
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

}
