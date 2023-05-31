using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CellController : MonoBehaviour, IClickable
{
    public RectTransform rectTransform;

    public Image xImage;

    [HideInInspector] public List<CellController> neighbours;

    private bool _clicked;

    public void OnPointerClick(PointerEventData eventData)
    {
        CellClicked();
    }

    private void CellClicked()
    {
        if (!_clicked)
        {
            xImage.enabled = true;
            _clicked = true;
            EventManager.CheckForMatch();
        }
    }
    

    private void OnEnable()
    {
        EventManager.CheckForMatch += CheckForMatch;
    }

    private void OnDisable()
    {
        EventManager.CheckForMatch -= CheckForMatch;
    }

    void CheckForMatch()
    {
        if (_clicked)
        {
            int clickedAmount = 0;

            foreach (var neighbour in neighbours)
            {
                if (neighbour._clicked)
                    clickedAmount++;
            }

            if (clickedAmount >= 2)
            {
                EventManager.CellMatched();
                CellMatched();
                
            }
        }
    }

    void CellMatched()
    {
        xImage.enabled = false;
        _clicked = false;

        foreach (var neighbour in neighbours)
        {
            if (neighbour._clicked)
                neighbour.CellMatched();
        }
    }
}