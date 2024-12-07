using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    // Stores the Grid Info
    public static GridUnit[,] grid;

    // To get a gridUnit given (X,y) location on Grid
    public GridUnit GetGridUnit(int x, int y)
    {
        return grid[x,y];
    }

    // Return the List of neighbouring gridUnit, support ajacent and diagonal neighbour.    
    public List<GridUnit> GetNeighbourUnits(GridUnit unit, bool diagonalMoveAllowed)
    {
        List<GridUnit> neighbourUnits = new List<GridUnit>();

        for(int i = -1; i <= 1; i++)
        {
            for(int j = -1; j <= 1; j++)
            {
                if(Mathf.Abs(i) == Mathf.Abs(j) && !diagonalMoveAllowed) continue;
                int x = unit.cell.x + i;
                int y = unit.cell.y + j;

                if(x >= 0 && x < grid.GetLength(0) && y >= 0 && y < grid.GetLength(1))
                {
                    neighbourUnits.Add(grid[x,y]);
                }
            }
        }

        return neighbourUnits;
    }

}
