using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmTrigger : MonoBehaviour
{
    public event Action<Character> CharacterDetectEvent;

    private void OnTriggerEnter(Collider other)
    {
        var player = other.gameObject.GetComponent<Character>();
        if (player == null)
            return;
        CharacterDetectEvent?.Invoke(player);
    }
}
