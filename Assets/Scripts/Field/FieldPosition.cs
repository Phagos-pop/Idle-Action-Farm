using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldPosition : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(this.transform.position, this.transform.localScale);
    }
}
