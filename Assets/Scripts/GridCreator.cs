using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;

public class GridCreator : MonoBehaviour
{
    public CellController cellPrefab;
    CellController[,] _cells;
    public int size;


    private void OnEnable()
    {
        EventManager.RebuildButtonClicked += RebuildButtonClicked;
    }


    private void OnDisable()
    {
        EventManager.RebuildButtonClicked -= RebuildButtonClicked;
    }

    private void RebuildButtonClicked()
    {
        size = EventManager.GetSize();
        CreateCell();
    }

    [Button]
    public void CreateCell() //gamepanelin boyutlarına göre grid oluşturdum
    {
        _cells = new CellController[size, size];

        foreach (var cell in GetComponentsInChildren<CellController>())
        {
            if (Application.isPlaying)
                Destroy(cell.gameObject);
            else if(Application.isEditor)
                DestroyImmediate(cell.gameObject);
        }
                    

     
        var height = GetComponent<RectTransform>().rect.height;
        var width = GetComponent<RectTransform>().rect.width;
        
        if (height > width)
            height = width;
        else
            width = height;
        
        var cellWidth = width / (size);
        var cellHeight = height / (size);
        
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                var pos = new Vector3(-(width / 2) + (j * cellWidth), -(height / 2) + (i * cellHeight), 0);
                var cell = Instantiate(cellPrefab.gameObject, pos, quaternion.identity, transform).GetComponent<CellController>();
                cell.rectTransform.sizeDelta = new Vector2(cellWidth, cellHeight);
                cell.transform.localPosition = pos;
                _cells[i,j] = cell;
            }
        }
        
        SetNeighbours();
    }

    void SetNeighbours() //her cell in komşularını atadım. match için 
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if ((i + 1) < size)
                    _cells[i, j].neighbours.Add(_cells[i + 1, j]);
                if ((i - 1) >= 0)
                    _cells[i, j].neighbours.Add(_cells[i - 1, j]);
                if ((j + 1) < size)
                    _cells[i, j].neighbours.Add(_cells[i, j+1]);
                if ((j - 1) >= 0)
                    _cells[i, j].neighbours.Add(_cells[i, j-1]);

            }
        }
    }

    private void Start()
    {
        CreateCell();
    }
}