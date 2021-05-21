using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WalletView : MonoBehaviour
{
    [SerializeField] private Text _text;
    [SerializeField] private Player _player;

    private void OnEnable()
    {
        _player.CoinCollected += OnCoinCollected; 
    }

    private void OnDisable()
    {
        _player.CoinCollected -= OnCoinCollected;
    }

    private void OnCoinCollected(int collectedCoins)
    {
        _text.text = collectedCoins.ToString();
    }
}
