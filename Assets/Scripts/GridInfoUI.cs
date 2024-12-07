using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GridInfoUI : MonoBehaviour
{
    public GameObject textX;
    public GameObject textY;
    private Transform previousTransform;
    private int times = 0;

    public void OnEnable()
    {
        InputManager.OnMouseHover += UpdateUI;
    }

    public void OnDisable()
    {
        InputManager.OnMouseHover -= UpdateUI;
    }
    
    void UpdateUI(Transform transform)
    {
        if(transform.CompareTag("Grid"))
        {
            if(previousTransform == null)
            {
                textX.GetComponent<TextMeshProUGUI>().text = "X : " + transform.GetComponent<GridUnit>().cell.x;
                textY.GetComponent<TextMeshProUGUI>().text = "Y : " + transform.GetComponent<GridUnit>().cell.y;
                // Debug.Log("Updated UI" + times++);
                previousTransform = transform;
            }
            else if (previousTransform != null && previousTransform != transform)
            {
                textX.GetComponent<TextMeshProUGUI>().text = "X : " + transform.GetComponent<GridUnit>().cell.x;
                textY.GetComponent<TextMeshProUGUI>().text = "Y : " + transform.GetComponent<GridUnit>().cell.y;
                // Debug.Log("Updated UI" + times++);
                previousTransform = transform;
            }
        }
        
        
    }
}
