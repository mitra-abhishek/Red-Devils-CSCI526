using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class HandleLoseScreen : MonoBehaviour
{
    GameObject reasonOfDeath;
    void Start()
    {
        reasonOfDeath = GameObject.FindWithTag("death_reason");
        TextMeshProUGUI reasonOfDeathGui = reasonOfDeath.GetComponent<TextMeshProUGUI>();
        reasonOfDeathGui.text = GameManager.instance.reasonOfDeath.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
