using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;



public class ShieldPowerUpController : MonoBehaviour
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
        Debug.Log("PowerUp shield Count" + totalPowerUpsGenerated);
    }

    public void addTotalPowerUpsCollected()
    {
        totalPowerUpsCollected += 1;
        Debug.Log("Collected shield Powerups" + totalPowerUpsCollected);

    }
    public int getTotalPowerUpsGenerated()
    {
        Debug.Log("The total poweup generated for shield are" + totalPowerUpsGenerated);
        return totalPowerUpsGenerated;
    }
    public int getTotalPowerUpsCollected()
    {
        Debug.Log("The total poweup generated for shield are" + totalPowerUpsCollected);
        return totalPowerUpsCollected;
    }
}