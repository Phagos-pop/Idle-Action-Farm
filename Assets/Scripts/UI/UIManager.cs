using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Character character;
    [SerializeField] private Farm farm;

    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private TextMeshProUGUI bambooText;

    private int coins;

    private void Start()
    {
        character.ChangeBambooValueEvent += Character_ChangeBambooValueEvent;
        coins = 0;
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
