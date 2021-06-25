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
        GenerateLevelInRadius(_spawnRadius, (int)_player.transform.position.x);
    }

    private void GenerateLevelInRadius(int radius, int playerX)
    {
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

        int templateIndex = GetTemplateIndex(spawnPoint);
        if (templateIndex < 0)
            return;

        int numberOfCoins = GetNumberOfCoins(templateIndex);

        for (int i = 0; i < numberOfCoins; i++)
        {
            _occupiedPositions.Add(WorldToGrid(spawnPoint + new Vector3(i, 0, 0)));
            Instantiate(_templates[templateIndex], spawnPoint + new Vector3(i, 0, 0), Quaternion.identity);
        }
    }

    private int GetNumberOfCoins(int templateIndex)
    {
        if (templateIndex == 2)
            return Random.Range(1, 5);
        else
            return 1;
    }

    private int GetTemplateIndex(Vector3 spawnPoint)
    {
        if (spawnPoint.y == (float)GridLayer.Ground)
            return 0;
        else
            return GetRandomTemplateIndex();
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
