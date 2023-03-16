using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : MonoBehaviour
{
    [SerializeField] private FarmTrigger farmTrigger;
    [SerializeField] private int moneyForBamboo = 15;

    public event Action<int> MoneyChangeEvent;
    

    private void Start()
    {
        farmTrigger.CharacterDetectEvent += FarmTrigger_CharacterDetectEvent;
    }

    private void FarmTrigger_CharacterDetectEvent(Character character)
    {
        int value = character.CurrentBambooValue;
        character.RemoveBamboo();
        StartCoroutine(BambooInMoneyCouratine(value));
    }

    private IEnumerator BambooInMoneyCouratine(int value)
    {
        yield return new WaitForSeconds(1f);

        while (value > 0)
        {
            value--;
            MoneyChangeEvent?.Invoke(moneyForBamboo);
        }
    }
}
