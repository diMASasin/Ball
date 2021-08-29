using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerMovement))]
public class Player : MonoBehaviour
{
    private int _coins;

    public event UnityAction<int> CoinCollected;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Coin>(out Coin coin))
        {
            Destroy(coin.gameObject);
            _coins++;
            CoinCollected?.Invoke(_coins);
        }
    }
}
