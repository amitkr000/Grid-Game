using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGridUnits : MonoBehaviour
{
    [SerializeField]private GameObject gridUnitObject;
    [SerializeField]private Vector2 gridSize;

    void Start()
    {
        SpawnGrid();
    }
    
    void SpawnGrid()
    {
        float halfGridSizeX = gridSize.x /2;
        float halfGridSizeY = gridSize.y /2;
        for(int i = 0; i < gridSize.x; i++)
        {
            for(int j = 0; j < gridSize.y; j++)
            {
                GameObject gridUnit = Instantiate(gridUnitObject, transform);
                gridUnit.transform.position = new Vector3(i - halfGridSizeX, 0, j - halfGridSizeY);
                gridUnit.GetComponent<GridUnit>().unit = new Vector2(i, j);
            }
        }
    }
}
