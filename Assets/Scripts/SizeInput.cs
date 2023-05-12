using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SizeInput : MonoBehaviour
{

    private void OnEnable()
    {
        EventManager.GetSize += GetSize;
    }

    private int GetSize()
    {
        return int.Parse(GetComponent<TMP_InputField>().text);
    }

    private void OnDisable()
    {
        EventManager.GetSize -= GetSize;
    }
}
