using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVision : MonoBehaviour
{
    [SerializeField] private float _playerVision;
    private List<Collider2D> _exploredPool = new List<Collider2D>();

    public void MakeVision()
    {
        if (_exploredPool.Count > 0)
        {
            foreach (var tile in _exploredPool)
            {
                tile.gameObject.SetActive(true);
            }
            _exploredPool.Clear();
        }

        Collider2D[] tiles = Physics2D.OverlapCircleAll(transform.position, _playerVision, 1 << 8);

        foreach (var tile in tiles)
        {
            tile.gameObject.SetActive(false);
            _exploredPool.Add(tile);
        }
    }
}
