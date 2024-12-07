using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PathFinder
{
    private Grid grid;

    public PathFinder(Grid grid)
    {
        this.grid = grid;
    }

    public List<GridUnit> FindPath(GridUnit startGridUnit, GridUnit goalGridUnit, bool diagonalMoveAllowed = default)
    {
        if(!goalGridUnit.isWalkable)
        {
            Debug.LogWarning("Not walkable");
            return null;
        }

        // openList stores the nodes Which has to travel ; closedList stores the nodes which has already travelled.
        List<GridUnit> openList = new List<GridUnit>();
        HashSet<GridUnit> closedList = new HashSet<GridUnit>();

        openList.Add(startGridUnit);

        while(openList.Count > 0)
        {
            GridUnit currentNode = openList[0];
            currentNode = LeastFCostNode(openList);

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            if(currentNode == goalGridUnit)
            {
                return RetracePath(startGridUnit, currentNode);
            }

            foreach( GridUnit neighbour in grid.GetNeighbourUnits(currentNode, diagonalMoveAllowed))
            {
                if(!neighbour.isWalkable || closedList.Contains(neighbour))
                {
                    continue;
                }

                int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour, diagonalMoveAllowed);

                if(newMovementCostToNeighbour < neighbour.gCost || !openList.Contains(neighbour))
                {
                    neighbour.gCost = newMovementCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, goalGridUnit, diagonalMoveAllowed);
                    neighbour.parent = currentNode;

                    if(!openList.Contains(neighbour))
                    {
                        openList.Add(neighbour);
                    }
                }
            }
        }

        return null;
    }

    private int GetDistance(GridUnit nodeA, GridUnit nodeB, bool diagonalAllowed)
    {
        int distX = Mathf.Abs(nodeA.cell.x - nodeB.cell.x);
        int distY = Mathf.Abs(nodeA.cell.y - nodeB.cell.y);

        // 8-Direction movement Allowed
        if(diagonalAllowed)
        {
            if(distX > distY)
            {
                return 14*distY + 10*(distX - distY);
            }
            return 14*distX + 10*(distY - distX);
        }

        // 4-Direction movement Allowed
        return 10*(distX + distY);
    }

    private GridUnit LeastFCostNode(List<GridUnit> Nodes)
    {
        GridUnit leastFnode = Nodes[0];
        for (int i=1; i < Nodes.Count; i++)
        {
            if(leastFnode.FCost > Nodes[i].FCost || leastFnode.FCost == Nodes[i].FCost && leastFnode.hCost > Nodes[i].hCost)
            {
                leastFnode = Nodes[i];
            }
        }

        return leastFnode;
    }

    private List<GridUnit> RetracePath(GridUnit startNode, GridUnit goalNode)
    {
        List<GridUnit> path = new List<GridUnit>();

        GridUnit currentNode = goalNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();

        return path;
    }
}
