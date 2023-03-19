using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceBamboo : MonoBehaviour
{
    public event Action<Vector3> KnifeColiderEvent;

    private void OnCollisionEnter(Collision collision)
    {
        Knife knife = collision.collider.GetComponent<Knife>();
        if (knife == null)
            return;
        ContactPoint contact = collision.contacts[0];
        var worldPosition = contact.point;
        knife.Cut();
        KnifeColiderEvent?.Invoke(worldPosition);
    }
}
