using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class level1hover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject level1Image;
    public void OnPointerEnter(PointerEventData eventData){
        level1Image.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData){
        level1Image.SetActive(false);
    }
}
