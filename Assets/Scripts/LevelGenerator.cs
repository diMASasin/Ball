using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _groundTemplate;
    [SerializeField] private GridObject _barrierTemplate;
    [SerializeField] private GridObject _coinTemplate;
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
            GenerateGround(new Vector3(i, (float)GridLayer.Ground, 0));
            if(i != playerX)
                GenerateOnGround(new Vector3(i, (float)GridLayer.OnGround, 0));
        }
    }

    private void GenerateGround(Vector3 spawnPoint)
    {
        if (_occupiedPositions.Contains(WorldToGrid(spawnPoint)))
            return;

        _occupiedPositions.Add(WorldToGrid(spawnPoint));
        Instantiate(_groundTemplate, spawnPoint, Quaternion.identity);
    }

    private void GenerateOnGround(Vector3 spawnPoint)
    {
        if (_occupiedPositions.Contains(WorldToGrid(spawnPoint)))
            return;

        _occupiedPositions.Add(WorldToGrid(spawnPoint));
        var randomTemplate = GetRandomTemplate();

        if (randomTemplate == null)
            return;

        Instantiate(randomTemplate, spawnPoint, Quaternion.identity);

        if(randomTemplate.TryGetComponent<Coin>(out Coin coin))
        {
            int randomNumberOfCoins = Random.Range(1, 5);
            for (int i = 1; i < randomNumberOfCoins; i++)
            {
                _occupiedPositions.Add(WorldToGrid(spawnPoint + new Vector3(i, 0, 0)));
                Instantiate(randomTemplate, spawnPoint + new Vector3(i, 0, 0), Quaternion.identity);
            }
        }

    }

    private GridObject GetRandomTemplate()
    {
        if (_barrierTemplate.Chance >= Random.Range(1, 101))
            return _barrierTemplate;
        else if (_coinTemplate.Chance >= Random.Range(1, 101))
            return _coinTemplate;
        return null;
    }

    private Vector2Int WorldToGrid(Vector3 vector)
    {
        return new Vector2Int((int)vector.x, (int)vector.y);
    }
}
