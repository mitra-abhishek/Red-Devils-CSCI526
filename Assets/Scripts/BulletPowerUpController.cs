using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;



public class BulletPowerUpController : MonoBehaviour
{
    public int totalPowerUpsGenerated;
    public int totalPowerUpsCollected;

    public void setTotalPowerGenerated(){
        Debug.Log("Game Manager Set Total Power ");
        totalPowerUpsGenerated=0;
    }

    public void setTotalPowerUpsCollected(){
        Debug.Log("Game Manager Set Total Power Collected");
        totalPowerUpsCollected=0;
    }

    public void addTotalPowerUpsGenerated(){
        totalPowerUpsGenerated+=1;
    }

    public void addTotalPowerUpsCollected(){
        totalPowerUpsCollected+=1;

    }
    public int getTotalPowerUpsGenerated(){
        return totalPowerUpsGenerated;  
    }
    public int getTotalPowerUpsCollected() {
        return totalPowerUpsCollected;
    }
}