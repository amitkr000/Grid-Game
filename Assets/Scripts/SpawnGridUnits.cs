using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGridUnits : MonoBehaviour
{
    [SerializeField]private GameObject gridUnitObject;
    [SerializeField]private GameObject playerPrefab;
    [SerializeField]private GameObject enemyPrefab;
    [SerializeField]private int enemyCount;
    [SerializeField]private Vector2Int gridSize;     

    void Start()
    {
        GenerateGrid();
        SpawnPlayer();
        SpawnEnemy();
    }
    
    // Spawn gridUnitObject Gameobjects to make a Grid
    void GenerateGrid()
    {
        Grid.instance.grid = new GridUnit[gridSize.x, gridSize.y];

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
                Grid.instance.grid[i,j] = gridUnit;
            }
        }
    }

    void SpawnPlayer()
    {
        Vector3 position = Grid.instance.grid[0,0].transform.position + new Vector3(0, 1.5f, 0);
        Instantiate(playerPrefab, position, Quaternion.identity);
    }

    void SpawnEnemy()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            while(true)
            {
                int x = Random.Range(1,9);
                int y = Random.Range(1,9);

                if(Grid.instance.grid[x,y].isWalkable)
                {
                    Vector3 position = Grid.instance.grid[x,y].transform.position + new Vector3(0, 1.5f, 0);
                    Instantiate(enemyPrefab, position, Quaternion.identity);
                    break;
                }
            }
        }
    }
}
