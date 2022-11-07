using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossEnemyHealthBehavior : MonoBehaviour
{
    public Slider Slider;
    public Color low;
    public Color high;
    public Vector3 offset;

    public void SetHealth(float health, float maxHealth){
        Slider.gameObject.SetActive(health <= maxHealth);
        Slider.value=health;
        Slider.maxValue=maxHealth;
        Slider.fillRect.GetComponentInChildren<Image>().color=Color.Lerp(Color.red, Color.green, Slider.normalizedValue);
    }

    void Update()
    {
        Slider.transform.position=Camera.main.WorldToScreenPoint(transform.parent.position+offset);
    }

}
