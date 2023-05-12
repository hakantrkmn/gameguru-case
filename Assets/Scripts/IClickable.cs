using System.Collections;
using System.Collections.Generic;
using UnityEngine;using UnityEngine.EventSystems;

public interface IClickable : IPointerClickHandler
{
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        
    }
}
