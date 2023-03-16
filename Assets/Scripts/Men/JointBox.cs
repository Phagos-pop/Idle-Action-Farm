using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointBox : MonoBehaviour
{
    [SerializeField] private Transform playerTranform;
    [SerializeField] private Transform bodyTranform;

    [SerializeField] private float rotationKoefficient;

    private Character character;

    private void Start()
    {
        character = playerTranform.GetComponent<Character>();
    }

    void Update()
    {
        if (!character.IsCut)
        {
            Vector3 relativePosition = playerTranform.InverseTransformPoint(transform.position);
            if (relativePosition.sqrMagnitude > 1)
                return;
            bodyTranform.localEulerAngles = new Vector3(relativePosition.z, 0, -relativePosition.x) * rotationKoefficient;
        }
    }
}
