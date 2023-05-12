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

    bool clicked;

    public void OnPointerClick(PointerEventData eventData)
    {
        xImage.enabled = true;
        clicked = true;
        EventManager.CheckForMatch();
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
        if (clicked)
        {
            int clickedAmount = 0;

            foreach (var neighbour in neighbours)
            {
                if (neighbour.clicked)
                    clickedAmount++;
            }

            if (clickedAmount >= 2)
            {
                EventManager.CellMatched();
                CellMatched();
                foreach (var neighbour in neighbours)
                {
                    if (neighbour.clicked)
                        neighbour.CellMatched();
                }
            }
        }
    }

    void CellMatched()
    {
        xImage.enabled = false;
        clicked = false;

        foreach (var neighbour in neighbours)
        {
            if (neighbour.clicked)
                neighbour.CellMatched();
        }
    }
}