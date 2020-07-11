using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fog : MonoBehaviour
{
    [SerializeField] private GameObject _fogPrefab;

    private void Awake()
    {
        Tile[] tiles = GameObject.FindObjectsOfType<Tile>();
        foreach (var tile in tiles)
        {
            Instantiate(_fogPrefab, tile.transform.position, Quaternion.identity);
        }
    }
}
