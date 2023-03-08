using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEditor.Experimental.SceneManagement;
using UnityEngine;

[EditorTool("Custom Snap Tool", typeof(CustomSnap))]
public class CustomSnapingTool : EditorTool
{
    public Texture2D icon;

    private Transform oldTarget;
    private CustomSnapPoint[] allPoints;
    private CustomSnapPoint[] targetPoints;


    private void OnEnable()
    {
        
    }

    public override GUIContent toolbarIcon
    {
        get
        {
            return new GUIContent
            {
                image = icon,
                text = "Custom Snap Tool",
                tooltip = "To Move gameobject with CustomSnap script"
            };
        }
    }

    public override void OnToolGUI(EditorWindow window)
    {
        Transform targetTransform = ((CustomSnap)target).transform;

        if (targetTransform != oldTarget)
        {
            //PrefabStage prefabStage = PrefabStageUtility.GetPrefabStage(targetTransform.gameObject);

            //if (prefabStage == null)
            //{
            //    allPoints = prefabStage.prefabContentsRoot.GetComponentsInChildren<CustomSnapPoint>();
            //}
            //else
                allPoints = FindObjectsOfType<CustomSnapPoint>();

            targetPoints = targetTransform.GetComponentsInChildren<CustomSnapPoint>();

            oldTarget = targetTransform;
        }

        EditorGUI.BeginChangeCheck();
        Vector3 newPosition = Handles.PositionHandle(targetTransform.position, Quaternion.identity);

        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(targetTransform, "Move with snap tool");
            MoveWithSnapTool(targetTransform, newPosition);
        }

    }

    private void MoveWithSnapTool(Transform targetTransform, Vector3 newPosition)
    {
        Vector3 bestPosition = newPosition;
        float closestDistance = float.PositiveInfinity;

        foreach (CustomSnapPoint point in allPoints)
        {
            if (point.transform.parent == targetTransform)
            {
                continue;
            }

            foreach (CustomSnapPoint ownPoint in targetPoints)
            {

                if (ownPoint.type != point.type)
                {
                    continue;
                }

                Vector3 targetPosition = point.transform.position - (ownPoint.transform.position - targetTransform.position);
                float distance = Vector3.Distance(targetPosition, newPosition);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    bestPosition = targetPosition;
                }
            }
        }

        if (closestDistance < 1.5f)
        {
            targetTransform.position = bestPosition;
        }
        else
        {
            targetTransform.position = newPosition;
        }
    }
}
