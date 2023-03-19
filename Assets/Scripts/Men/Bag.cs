using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag : MonoBehaviour
{
    [SerializeField] private List<GameObject> bambooItems;
    
    private float bagFill;
    private int maxValue;
    private float oldValueBagFill;
    private float oneItemValue;

    public void SetMaxValue(int value)
    {
        maxValue = value;
        SetBag();
    }

    private void Start()
    {
        oneItemValue = 1 / bambooItems.Count;
    }

    private void Update()
    {
        if (bagFill != oldValueBagFill)
        {
            SetBag();
        }
    }

    private void SetBag()
    {
        float currentValue = bagFill;
        oneItemValue = 1f / (float)bambooItems.Count;
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
        oldValueBagFill = bagFill;
    }

    public void AddValue()
    {
        bagFill += 1f / (float)maxValue;
        if (bagFill > 1)
        {
            bagFill = 1;
        }
    }

    public void RemoveValue()
    {
        bagFill -= 1f / (float)maxValue;
        if (bagFill < 0)
        {
            bagFill = 0;
        }
    }
}
