using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class level2hover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject level2Image;
    public void OnPointerEnter(PointerEventData eventData){
        level2Image.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData){
        level2Image.SetActive(false);
    }
}
