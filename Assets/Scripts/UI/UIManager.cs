using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Character character;
    [SerializeField] private Farm farm;

    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private TextMeshProUGUI bambooText;

    [SerializeField] private Image coinImage;
    [SerializeField] private Canvas canvas;

    private int coins;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
        character.ChangeBambooValueEvent += Character_ChangeBambooValueEvent;
        farm.MoneyChangeEvent += Farm_MoneyChangeEvent;
        coins = 0;
    }

    private void Farm_MoneyChangeEvent(int value, Vector3 position)
    {
        var coin = Instantiate(coinImage, mainCamera.WorldToScreenPoint(position), Quaternion.identity, canvas.transform);
        DOTween.Sequence()
            .Append(coin.rectTransform.DOMove(coinsText.rectTransform.position,0.5f))
            .AppendCallback(() => Destroy(coin.gameObject));
        AddCoins(value);
    }

    private void OnDestroy()
    {
        character.ChangeBambooValueEvent -= Character_ChangeBambooValueEvent;
    }

    private void Character_ChangeBambooValueEvent(int value)
    {
        SetBambooValue(value);
    }

    private void AddCoins(int value)
    {
        coins += value;
        coinsText.text = $"Coins {coins}";
    }

    private void SetBambooValue(int value)
    {
        bambooText.text = $"Bamboo {value}";
    }
}
