using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyController : PathFinder
{
    [SerializeField] private Player _player;

    private Enemy _enemy;

    private void Start()
    {
        _movePointsQuery = new List<Vector2>();
        _poolToReset = new List<Tile>();
        _enemy = GetComponent<Enemy>();
    }

    public void RandomMove()
    {
        List<Tile> tiles = new List<Tile>();

        Tile tile;

        tile = GetTile(transform.position.x + 1, transform.position.y);
        if (tile)
        {
            tiles.Add(tile);
        }
        tile = GetTile(transform.position.x - 1, transform.position.y);
        if (tile)
        {
            tiles.Add(tile);
        }
        tile = GetTile(transform.position.x + 1, transform.position.y + 1);
        if (tile)
        {
            tiles.Add(tile);
        }
        tile = GetTile(transform.position.x + 1, transform.position.y - 1);
        if (tile)
        {
            tiles.Add(tile);
        }
        tile = GetTile(transform.position.x, transform.position.y + 1);
        if (tile)
        {
            tiles.Add(tile);
        }
        tile = GetTile(transform.position.x, transform.position.y - 1);
        if (tile)
        {
            tiles.Add(tile);
        }
        tile = GetTile(transform.position.x - 1, transform.position.y + 1);
        if (tile)
        {
            tiles.Add(tile);
        }
        tile = GetTile(transform.position.x - 1, transform.position.y - 1);
        if (tile)
        {
            tiles.Add(tile);
        }

        Move(tiles[Random.Range(0, tiles.Count)].transform.position);
    }

    private void Move(Vector2 where)
    {
        transform.position = Vector2.MoveTowards(transform.position, where, 5);
    }

    public void MoveToPlayer()
    {
        
        FindWay(_player.transform.position.x, _player.transform.position.y);

        if(_movePointsQuery[0].x == _player.transform.position.x && _movePointsQuery[0].y == _player.transform.position.y)
        {
            _enemy.AttackPlayer();
            ResetTilesPool();
            return;
        }

        Move(_movePointsQuery[0]);
        
        ResetTilesPool();
    }
}
