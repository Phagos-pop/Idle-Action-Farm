using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bamboo : MonoBehaviour
{
    public int p;
    public int o;
    public event Action<Bamboo> OnDeadEvent; 

    private void OnCollisionEnter(Collision collision)
    {
        var knife = collision.collider.gameObject.GetComponent<Knife>();
        if (knife == null)
            return;
        var player = collision.gameObject.GetComponent<Character>();
        if (player != null)
            player.AddBamboo();
        OnDeadEvent.Invoke(this);
        Destroy(this.gameObject);
    }

    public void SetPos(int p, int o)
    {
        this.o = o;
        this.p = p;
    }
}
