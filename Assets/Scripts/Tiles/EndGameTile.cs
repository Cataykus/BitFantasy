using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameTile : MonoBehaviour
{
    [SerializeField] private SceneLoader _sceneLoader;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Player player))
        {
            _sceneLoader.LoadScene(2);
        }
    }
}
