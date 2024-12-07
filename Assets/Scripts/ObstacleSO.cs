using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="ObstacleData", menuName ="ObstacleSO")]
public class ObstacleSO : ScriptableObject
{
    public GameObject obstacleObject;
    public bool[] ObstacleOnGrid = new bool[100];
}
