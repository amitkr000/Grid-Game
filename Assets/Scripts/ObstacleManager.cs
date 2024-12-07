using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public ObstacleSO obstacleData;  //Have the info of spawnpoint on grid for spawning obstacle

    void Start()
    {
        SpawnObstacle();
    }

    // Spawn obstacle based on info on obstacleData Scriptable Object and set that gridunit not walkable
    void SpawnObstacle()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                if(obstacleData.ObstacleOnGrid[i*10+j])
                {
                    Instantiate(obstacleData.obstacleObject, new Vector3(i - 5, 1, j - 5), Quaternion.identity);
                    Grid.grid[i,j].isWalkable = false;
                }
            }
        }
    }
}
