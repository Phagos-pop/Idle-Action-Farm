using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Farm : MonoBehaviour
{
    [SerializeField] private FarmTrigger farmTrigger;
    [SerializeField] private int moneyForBamboo = 15;
    [SerializeField] private GameObject bambooPrefab;

    public event Action<int,Vector3> MoneyChangeEvent;
    

    private void Start()
    {
        farmTrigger.CharacterDetectEvent += FarmTrigger_CharacterDetectEvent;
    }

    private void FarmTrigger_CharacterDetectEvent(Character character)
    {
        int value = character.CurrentBambooValue;
        character.RemoveBamboo();
        
        StartCoroutine(BambooInMoneyCouratine(value,character.transform));
    }

    private IEnumerator BambooInMoneyCouratine(int value, Transform CharacterTransform)
    {
        for (int i = 0; i < value; i++)
        {
            var bambooObj = Instantiate(bambooPrefab, CharacterTransform.position, Quaternion.identity);
            DOTween.Sequence()
                .Append(bambooObj.transform.DOMove(this.transform.position, 1f))
                .AppendInterval(0.5f)
                .AppendCallback(() => Destroy(bambooObj.gameObject));
            yield return new WaitForSeconds(0.01f);
        }
        while (value > 0)
        {
            value--;
            MoneyChangeEvent?.Invoke(moneyForBamboo,transform.position);
            yield return new WaitForSeconds(0.05f);
        }
    }
}
