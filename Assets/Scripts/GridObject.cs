using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject : MonoBehaviour
{
    [SerializeField] private int _chance;
    [SerializeField] private int _quantity = 1;
    [SerializeField] private GridLayer _gridLayer;

    public int Quantity => _quantity;
    public int Chance => _chance;
    public GridLayer GridLayer => _gridLayer;

    private void OnValidate()
    {
        _chance = Mathf.Clamp(_chance, 1, 100);
    }
}
