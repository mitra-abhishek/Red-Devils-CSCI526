using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;



public class BulletPowerUpController : MonoBehaviour
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
        Debug.Log("PowerUp bullet Count" + totalPowerUpsGenerated);

    }

    public void addTotalPowerUpsCollected()
    {
        totalPowerUpsCollected += 1;
        Debug.Log("Collected bullet Powerups" + totalPowerUpsCollected);


    }
    public int getTotalPowerUpsGenerated()
    {
        Debug.Log("The total powreup generated for bullet are" + totalPowerUpsGenerated);
        return totalPowerUpsGenerated;
    }
    public int getTotalPowerUpsCollected()
    {
        Debug.Log("The total powreup collected for bullet are" + totalPowerUpsCollected);
        return totalPowerUpsCollected;
    }
}