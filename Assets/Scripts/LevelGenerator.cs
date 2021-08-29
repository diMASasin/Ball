using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private GridObject[] _templates;
    [SerializeField] private Player _player;
    [SerializeField] private int _spawnRadius;

    private HashSet<Vector2Int> _occupiedPositions = new HashSet<Vector2Int>();

    private void Update()
    {
        GenerateLevelInRadius(_player.transform.position, _spawnRadius);
    }

    private void GenerateLevelInRadius(Vector3 center, int radius)
    {
        for (int i = (int)center.x; i < (int)center.x + radius; i++)
        {
            GenerateGridObject(new Vector3(i, (float)GridLayer.Ground, 0));
            if(i != (int)center.x)
                GenerateGridObject(new Vector3(i, (float)GridLayer.OnGround, 0));
        }
    }

    private void GenerateGridObject(Vector3 spawnPoint)
    {
        int templateIndex = GetTemplateIndex(spawnPoint);
        if (templateIndex < 0)
        {
            _occupiedPositions.Add(WorldToGrid(spawnPoint));
            return;
        }

        for (int i = 0; i < _templates[templateIndex].Quantity; i++)
            if (_occupiedPositions.Contains(WorldToGrid(spawnPoint + new Vector3(i, 0, 0))))
                return;

        for (int i = 0; i < _templates[templateIndex].Quantity; i++)
        {
            _occupiedPositions.Add(WorldToGrid(spawnPoint + new Vector3(i, 0, 0)));
            Instantiate(_templates[templateIndex], spawnPoint + new Vector3(i, 0, 0), Quaternion.identity);
        }
    }

    private int GetTemplateIndex(Vector3 spawnPoint)
    {
        for (int i = 0; i < _templates.Length; i++)
        {
            if (_templates[i].Chance >= Random.Range(1, 101) && spawnPoint.y == (float)_templates[i].GridLayer)
                return i;
        }
        return -1;
    }

    private Vector2Int WorldToGrid(Vector3 vector)
    {
        return new Vector2Int((int)vector.x, (int)vector.y);
    }
}
