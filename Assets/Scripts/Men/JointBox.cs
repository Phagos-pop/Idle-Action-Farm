using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointBox : MonoBehaviour
{
    [SerializeField] private Transform playerTranform;
    [SerializeField] private Transform bodyTranform;

    [SerializeField] private float rotationKoefficient;

    void Update()
    {
        Vector3 relativePosition = playerTranform.InverseTransformPoint(transform.position);
        bodyTranform.localEulerAngles = new Vector3(relativePosition.z, 0, -relativePosition.x) * rotationKoefficient;
    }
}
