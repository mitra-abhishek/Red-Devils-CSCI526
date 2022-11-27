using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;



public class HealthPowerUpController : MonoBehaviour
{
    public int totalPowerUpsGenerated;
    public int totalPowerUpsCollected;

    public void setTotalPowerGenerated()
    {
        totalPowerUpsGenerated = 0;
    }

    public void setTotalPowerUpsCollected()
    {
        totalPowerUpsCollected = 0;
    }

    public void addTotalPowerUpsGenerated()
    {
        totalPowerUpsGenerated += 1;
        Debug.Log("PowerUp health Count" + totalPowerUpsGenerated);

    }

    public void addTotalPowerUpsCollected()
    {
        totalPowerUpsCollected += 1;
        Debug.Log("Collected health Powerups" + totalPowerUpsCollected);


    }
    public int getTotalPowerUpsGenerated()
    {
        return totalPowerUpsGenerated;
    }
    public int getTotalPowerUpsCollected()
    {
        return totalPowerUpsCollected;
    }
}