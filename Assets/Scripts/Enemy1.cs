using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour, AI
{
    [SerializeField] private int moveStep;
    [SerializeField] private int speed;
    // [SerializeField] private Grid grid;
    public bool isMoving;

    void Start()
    {
        MakeGridUnitUnWalkable();
        // grid = GameObject.FindGameObjectWithTag("Grid").GetComponent<Grid>();
    }

    private void MakeGridUnitUnWalkable()
    {
        GridUnit currentUnit = GetCurrentPosition();
        currentUnit.isWalkable = false;
    }

    public GridUnit GetCurrentPosition()
    {
        if(Physics.Raycast(transform.position, 2*Vector3.down, out RaycastHit hit))
        {
            return hit.transform.GetComponent<GridUnit>();
        }

        return null;
    }    

    public IEnumerator MoveTowardsPlayer(GridUnit playerGridUnit)
    {
        GridUnit currentUnit = GetCurrentPosition();

        if(currentUnit != null && playerGridUnit != null)
        {
            PathFinder pathFinder = new PathFinder(Grid.instance);
            List<GridUnit> path = pathFinder.FindPath(currentUnit, playerGridUnit, false);
            if(path != null)
            {
                currentUnit.isWalkable = true;
                // StartCoroutine(MoveOnPath(path));
                 int step = path.Count - 1;

                for(int i = 0; i < step; i++)
                {
                    GridUnit nextLocation = path[i];
                    Vector3 nextPosition = new Vector3(nextLocation.transform.position.x, transform.position.y, nextLocation.transform.position.z);
                    while(Vector3.Distance(transform.position, nextPosition) >= 0.05f)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, nextPosition, speed*Time.deltaTime);
                        yield return null;
                    }
                    nextLocation.isWalkable = false;
                    if(i > 0)
                    {
                        path[i - 1].isWalkable = true;
                    }
                    
                }
            }
        }
    }

    // IEnumerator MoveOnPath(List<GridUnit> path)
    // {
    //     Debug.Log("Coroutine Starts");
    //     isMoving = true;

    //     int step = Mathf.Min(moveStep, path.Count-1);

    //     for(int i = 0; i < step; i++)
    //     {
    //         GridUnit nextLocation = path[i];
    //         Vector3 nextPosition = new Vector3(nextLocation.transform.position.x, transform.position.y, nextLocation.transform.position.z);
    //         while(Vector3.Distance(transform.position, nextPosition) >= 0.05f)
    //         {
    //             transform.position = Vector3.MoveTowards(transform.position, nextPosition, speed*Time.deltaTime);
    //             yield return null;
    //         }
    //         nextLocation.isWalkable = false;
    //         if(i > 0)
    //         {
    //             path[i - 1].isWalkable = true;
    //         }
            
    //     }
    //     isMoving = false;

    //     Debug.Log("Coroutine Ends");
    // }
}
