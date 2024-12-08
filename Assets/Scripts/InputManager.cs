using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField]private Camera _camera;
    [SerializeField]private LayerMask layerMask;
    public static event Action<Transform> OnMouseHover;
    public static event Action<Transform> OnLeftMouseDown;

    public GameObject player;
    public GameObject[] enemies;
    public int speed = 5;
    public GridUnit targetUnit;

    public static InputManager instance;

    void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        instance = this;
    }

    void Update()
    {
        MouseHover();
        LeftMouseDown();
    }

    // Capture the mouse position and check if there is a gridUnit or not. If then
    // invoke the event OnMouseHover.
    void MouseHover()
    {
        Vector3 mousePosition = Input.mousePosition;
        Ray ray = _camera.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, ~layerMask))
        {
            OnMouseHover?.Invoke(hit.transform);
        }
    }

    // Capture the mouse Left Click and check if there is a gridUnit or not. If then
    // invoke the event OnLeftMouseDown.
    void LeftMouseDown()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, ~layerMask))
            {
                // OnLeftMouseDown?.Invoke(hit.transform);
                if(player == null)
                {
                    Debug.Log("Seraching");
                    player = GameObject.FindGameObjectWithTag("Player");
                    enemies = GameObject.FindGameObjectsWithTag("Enemy");
                }

                Debug.Log(player);
                //Perform player and enemies movement
                targetUnit = hit.transform.GetComponent<GridUnit>();
                if(player.GetComponent<Player>().canPlayerMove)
                {
                    StartCoroutine(MoveSequence());
                }
                // List<GridUnit> playerPath = player.GetComponent<Player>().GetPathToTarget(hit.transform.GetComponent<GridUnit>());
                // if(playerPath != null)
                // {
                //     StartCoroutine(MoveToPositon(player.transform, playerPath));
                // }
                
                
            }
        }
    }

    IEnumerator MoveSequence()
    {
        List<GridUnit> playerPath = player.GetComponent<Player>().GetPathToTarget(targetUnit);
        if(playerPath != null)
        {
            yield return MoveToPositon(player.transform, playerPath);

            player.GetComponent<Player>().canPlayerMove = false;
            //Move each enemy
            foreach(GameObject enemy in enemies)
            {
                yield return StartCoroutine(enemy.GetComponent<AI>().MoveTowardsPlayer(targetUnit));
            }
        }
        yield return null;
        player.GetComponent<Player>().canPlayerMove = true;
    }

    IEnumerator MoveToPositon(Transform objTransform, List<GridUnit> path)
    {
        Debug.Log("Coroutine Starts");

        for(int i = 0; i < path.Count; i++)
        {
            GridUnit nextLocation = path[i];
            Vector3 nextPosition = new Vector3(nextLocation.transform.position.x, objTransform.position.y, nextLocation.transform.position.z);
            while(Vector3.Distance(objTransform.position, nextPosition) >= 0.05f)
            {
                objTransform.position = Vector3.MoveTowards(objTransform.position, nextPosition, speed*Time.deltaTime);
                yield return null;
            }
            
        }

        Debug.Log("Coroutine Ends");
    }

    // void PlayerMovement(GameObject playerObject, Transform targetGridUnitTransform)
    // {
    //     if(playerObject.GetComponent<Player>().FindPathNMove(targetGridUnitTransform))
    //     {
    //         GridUnit playerCurrentLocation = targetGridUnitTransform.GetComponent<GridUnit>();
    //         // Now move enemies toward player
    //         // MoveEnemies(enemies, playerCurrentLocation);
    //     }
    // }

    // public void MoveEnemies(GameObject[] enemies, GridUnit playerGridUnit)
    // {
    //     player.GetComponent<Player>().canPlayerMove = false;
    //     foreach(GameObject enemy in enemies)
    //     {
    //         enemy.GetComponent<AI>().MoveTowardsPlayer(playerGridUnit);
    //     }

    // }

}
