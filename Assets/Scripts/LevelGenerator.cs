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
        GenerateLevelInRadius(_spawnRadius);
    }

    private void GenerateLevelInRadius(int radius)
    {
        int playerX = (int)_player.transform.position.x;
        for (int i = playerX; i < playerX + radius; i++)
        {
            GenerateGridObject(new Vector3(i, (float)GridLayer.Ground, 0));
            if(i != playerX)
                GenerateGridObject(new Vector3(i, (float)GridLayer.OnGround, 0));
        }
    }

    private void GenerateGridObject(Vector3 spawnPoint)
    {
        if (_occupiedPositions.Contains(WorldToGrid(spawnPoint)))
            return;

        _occupiedPositions.Add(WorldToGrid(spawnPoint));

        int templateIndex;
        if (spawnPoint.y == (float)GridLayer.Ground)
            templateIndex = 0;
        else
            templateIndex = GetRandomTemplateIndex();

        if (templateIndex < 0 || templateIndex >= _templates.Length)
            return;

        Instantiate(_templates[templateIndex], spawnPoint, Quaternion.identity);

        if(templateIndex == 2)
        {
            int numberOfCoins = Random.Range(1, 5);
            for (int i = 1; i < numberOfCoins; i++)
            {
                _occupiedPositions.Add(WorldToGrid(spawnPoint + new Vector3(i, 0, 0)));
                Instantiate(_templates[templateIndex], spawnPoint + new Vector3(i, 0, 0), Quaternion.identity);
            }
        }

    }

    private int GetRandomTemplateIndex()
    {
        for (int i = 1; i < _templates.Length; i++)
        {
            if (_templates[i].Chance >= Random.Range(1, 101))
                return i;
        }
        return -1;
    }

    private Vector2Int WorldToGrid(Vector3 vector)
    {
        return new Vector2Int((int)vector.x, (int)vector.y);
    }
}
