using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class level4hover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject level4Image;
    public void OnPointerEnter(PointerEventData eventData){
        level4Image.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData){
        level4Image.SetActive(false);
    }
}
