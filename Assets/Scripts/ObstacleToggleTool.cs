using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class ObstacleToggleTool : EditorWindow
{
    public ObstacleSO obstacleData;

    [MenuItem("Tool/ObstacleToggle")]
    public static void ShowWindow()
    {
        GetWindow<ObstacleToggleTool>("ObstcleToggle");
    }

    private void OnEnable()
    {
        obstacleData = AssetDatabase.LoadAssetAtPath<ObstacleSO>("Assets/ObstacleData.asset");
    }

    private void OnGUI()
    {
        // obstacleData = (ObstacleSO)EditorGUILayout.ObjectField("ScriptableObject", obstacleData, typeof(ObstacleSO), false);

        int currentColumn = 0;
        GUILayout.BeginHorizontal();
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                if (obstacleData != null)
                {
                    obstacleData.ObstacleOnGrid[i*10+j] = GUILayout.Toggle(obstacleData.ObstacleOnGrid[i*10+j], $"{i} {j}");
                    currentColumn ++;
                    if(currentColumn >= 10)
                    {
                        GUILayout.EndHorizontal();
                        GUILayout.BeginHorizontal();
                        currentColumn = 0;
                    }
                }
            }
        }
        GUILayout.EndHorizontal();
    }
}
