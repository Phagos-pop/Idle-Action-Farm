using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag : MonoBehaviour
{
    [Range(0,1)]
    [SerializeField] private float bagFill;
    [SerializeField] private List<GameObject> bambooItems;

    public event Action FullBagEvent;
    public event Action EmptyBagEvent;

    private int Max = 40;
    private float oldValuebagFill;
    private float oneItemValue;

    private void Start()
    {
        bagFill = 0;
        oldValuebagFill = bagFill;
        oneItemValue = 1 / bambooItems.Count;
        SetBag();
    }

    private void Update()
    {
        if (bagFill != oldValuebagFill)
        {
            SetBag();
        }
    }

    private void SetBag()
    {
        float currentValue = bagFill;
        oneItemValue = 1f / 20f;
        foreach (var item in bambooItems)
        {
            if (currentValue > 0)
            {
                item.gameObject.SetActive(true);
            }
            else
            {
                item.gameObject.SetActive(false);
            }
            currentValue -= oneItemValue;
        }
        oldValuebagFill = bagFill;
    }

    public void AddValue()
    {
        bagFill += 1f / (float)Max;
        if (bagFill > 1)
        {
            FullBagEvent?.Invoke();
            bagFill = 1;
        }
    }

    public void RemoveValue()
    {
        bagFill -= 1f / (float)Max;
        if (bagFill < 0)
        {
            EmptyBagEvent?.Invoke();
            bagFill = 0;
        }
    }
}
