using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGridUnits : MonoBehaviour
{
    [SerializeField]private GameObject gridUnitObject;
    [SerializeField]private Vector2Int gridSize;     

    void Start()
    {
        SpawnGrid();
    }
    
    // Spawn gridUnitObject Gameobjects to make a Grid
    void SpawnGrid()
    {
        Grid.grid = new GridUnit[gridSize.x, gridSize.y];

        float halfGridSizeX = gridSize.x /2;
        float halfGridSizeY = gridSize.y /2;
        for(int i = 0; i < gridSize.x; i++)
        {
            for(int j = 0; j < gridSize.y; j++)
            {
                GameObject unitObject = Instantiate(gridUnitObject, transform);
                unitObject.transform.position = new Vector3(i - halfGridSizeX, 0, j - halfGridSizeY);
                
                GridUnit gridUnit = unitObject.GetComponent<GridUnit>();
                gridUnit.cell = new Vector2Int(i, j);
                gridUnit.isWalkable = true;
                Grid.grid[i,j] = gridUnit;
            }
        }
    }
}
