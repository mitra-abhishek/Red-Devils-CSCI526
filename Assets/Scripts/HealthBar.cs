using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

   public void setMaxHealth(int health){
       slider.maxValue=health;
       slider.value=health;
       fill.color=gradient.Evaluate(1f);
   }
   public void setHealth(int health){
       slider.value=health;
    //    Instead of slider.value, we used slider.normalized because it normalizes the slider value from 0 to 1
        print("Printing text here");
a        print(slider.normalizedValue);
       fill.color=gradient.Evaluate(slider.normalizedValue);
   }
}
