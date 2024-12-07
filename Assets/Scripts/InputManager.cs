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
                OnLeftMouseDown?.Invoke(hit.transform);
            }
        }
    }
}
