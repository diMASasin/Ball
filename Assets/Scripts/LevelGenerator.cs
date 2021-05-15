using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private GridObject[] _templates;
    [SerializeField] private float _cellSize;
    [SerializeField] private Player _player;
    [SerializeField] private float _viewRadius;

    private HashSet<Vector3Int> _collisionsMatrix = new HashSet<Vector3Int>();

    private void Update()
    {
        FillRadius(_player.transform.position, _viewRadius); 
    }

    private void FillRadius(Vector3 center, float radius)
    {
        var cellCount = (int)(radius / _cellSize);
        var fillAreaCenter = WorldToGridPosition(center);

        for (int x = -cellCount; x < cellCount; x++)
        {

            TryCreate(GridLayer.Ground, fillAreaCenter + new Vector3Int(x, 0, 0));

            if (x != 0)
                TryCreate(GridLayer.OnGround, fillAreaCenter + new Vector3Int(x, 0, 0));
        }
    }

    private void TryCreate(GridLayer layer, Vector3Int gridPosition)
    {
        gridPosition.y = (int)layer;

        int sizeOfLine = Random.Range(1, 6);


        if (_collisionsMatrix.Contains(gridPosition))
            return;

        _collisionsMatrix.Add(gridPosition);

        var template =  GetRandomTemplate(layer);

        if (template == null)
            return;

        if(!template.TryGetComponent<Coin>(out Coin coin))
            sizeOfLine = 1;

        for (int i = 0; i < sizeOfLine; i++)
        {
            _collisionsMatrix.Add(gridPosition + new Vector3Int(i, 0, 0));
            var position = GridToWorldPosition(gridPosition + new Vector3Int(i, 0, 0));
            Instantiate(template, position, Quaternion.identity, transform);
        }
    }

    private GridObject GetRandomTemplate(GridLayer layer)
    {
        var variants = _templates.Where(template => template.Layer == layer);

        if (variants.Count() == 1)
            return variants.First();

        foreach (var template in variants)
        {
            if(template.Chance > Random.Range(0, 100))
            {
                return template;
            }
        }

        return null;
    }

    private Vector3 GridToWorldPosition(Vector3Int gridPosition)
    {
        return new Vector3(
            gridPosition.x / _cellSize,
            gridPosition.y / _cellSize,
            gridPosition.z / _cellSize);
    }

    private Vector3Int WorldToGridPosition(Vector3 worldPosition)
    {
        return new Vector3Int(
            (int)(worldPosition.x / _cellSize),
            (int)(worldPosition.y / _cellSize),
            (int)(worldPosition.z / _cellSize));
    }
}
