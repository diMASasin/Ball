using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject : MonoBehaviour
{
    [SerializeField] private int _chance;

    public int Chance => _chance;

    private void OnValidate()
    {
        _chance = Mathf.Clamp(_chance, 1, 100);
    }
}
