using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class MyButton : MonoBehaviour, IPointerEnterHandler
{
    public Action<PointerEventData> OnPointerEnterEvent;

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnPointerEnterEvent?.Invoke(eventData);
    }
}
