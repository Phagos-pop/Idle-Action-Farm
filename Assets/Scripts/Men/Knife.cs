using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Knife : MonoBehaviour
{
    public event Action OnCutEvent;

    public void Cut()
    {
        OnCutEvent?.Invoke();
    }
}
