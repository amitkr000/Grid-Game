using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridUnit : MonoBehaviour
{
    public Vector2Int cell;                     //Store (x,y) position on grid
    public bool isWalkable;                     //Store info of gridunit is walkable or not
    public int gCost;
    public int hCost;
    public int FCost {get => gCost + hCost;}
    public GridUnit parent;                     // used to track the gridUnit from goal to start

}
