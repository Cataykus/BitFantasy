using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(PlayerVision))]
public class PlayerController : PathFinder
{
    [SerializeField] private float _moveSpeed;

    private Player _player;

    private bool _stopMove = false;

    private PlayerVision _playerVision;


    private void Start()
    {
        _player = GetComponent<Player>();
        _playerVision = GetComponent<PlayerVision>();
        _playerVision.MakeVision();

        _poolToReset = new List<Tile>();
        _movePointsQuery = new List<Vector2>();
    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(0) && _player.IsPlayerTurn)
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            if (_movePointsQuery.Count > 0)
            {
                _stopMove = true;
                return;
            }
            Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            float x = Mathf.Round(point.x);
            float y = Mathf.Round(point.y);

            if (GetTile(x, y) && GetTile(x, y).transform.position != transform.position)
            {
                FindWay(x, y);
            }
        }

        if (_movePointsQuery.Count > 0 && Vector2.Distance(_movePointsQuery[0], transform.position) == 0 && _player.IsPlayerTurn)
        {
            _movePointsQuery.RemoveAt(0);

            _playerVision.MakeVision();
            _player.SkipTurn();
            if (_stopMove)
            {
                ResetMovePointsQuery();
                _stopMove = false;
            }
        }
        else if (_movePointsQuery.Count > 0 && _player.IsPlayerTurn)
        {
            if(TryGetEnemy(_movePointsQuery[0].x, _movePointsQuery[0].y, out Enemy enemy))
            {
                ResetMovePointsQuery();
                _player.Attack(enemy);
                return;
            }
            transform.position = Vector2.MoveTowards(transform.position, _movePointsQuery[0], _moveSpeed * Time.deltaTime);
        }
    }
}
