using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class level3hover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject level3Image;
    public void OnPointerEnter(PointerEventData eventData){
        level3Image.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData){
        level3Image.SetActive(false);
    }
}
