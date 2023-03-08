using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomSnapPoint : MonoBehaviour
{
    public enum ConectionType
    {
        Platform,
        Other
    }

    public ConectionType type;

    private void OnDrawGizmos()
    {
        switch (type)
        {
            case ConectionType.Platform:
                Gizmos.color = Color.yellow;
                break;
            case ConectionType.Other:
                Gizmos.color = Color.blue;
                break;
            default:
                break;
        }

        Gizmos.DrawSphere(transform.position, 0.5f);
    }
}
