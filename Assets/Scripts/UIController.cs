using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public int matchCount;
    public TextMeshProUGUI matchText;

    private void OnEnable()
    {
        EventManager.CellMatched += CellMatched;
    }

    private void OnDisable()
    {
        EventManager.CellMatched -= CellMatched;
    }

    private void CellMatched()
    {
        matchCount++;
        matchText.text = "Match Count : " + matchCount.ToString();
    }
}
