using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private GridUnit locationOnGrid;
    public GridUnit targetGrid;
    public Grid grid;
    public int speed;
    public bool canPlayerMove = true;

    void Start()
    {
        grid = GameObject.FindGameObjectWithTag("Grid").GetComponent<Grid>();
    }

    GridUnit GetCurrentLocationOnGrid()
    {
        if(Physics.Raycast(transform.position, 2*Vector3.down, out RaycastHit hit))
        {
            return hit.transform.GetComponent<GridUnit>();
        }

        return null;
    }

    public bool FindPathNMove(Transform targetGridUnitTransform)
    {
        targetGrid = targetGridUnitTransform.GetComponent<GridUnit>();
        locationOnGrid = GetCurrentLocationOnGrid();

        if(locationOnGrid != null && targetGrid != null)
        {
            PathFinder pathFinder = new PathFinder(grid);
            List<GridUnit> path = pathFinder.FindPath(locationOnGrid, targetGrid, false);
            if(path != null)
            {
                StartCoroutine(PlayerMovement(path));
                return true;
            }
        }
        return false;
    }

    IEnumerator PlayerMovement(List<GridUnit> path)
    {
        Debug.Log("Coroutine Starts");

        for(int i = 0; i < path.Count; i++)
        {
            GridUnit nextLocation = path[i];
            Vector3 nextPosition = new Vector3(nextLocation.transform.position.x, transform.position.y, nextLocation.transform.position.z);
            while(Vector3.Distance(transform.position, nextPosition) >= 0.05f)
            {
                transform.position = Vector3.MoveTowards(transform.position, nextPosition, speed*Time.deltaTime);
                yield return null;
            }
            
        }
        InputManager.instance.MoveEnemies(InputManager.instance.enemies, GetCurrentLocationOnGrid());

        Debug.Log("Coroutine Ends");
    }

}
