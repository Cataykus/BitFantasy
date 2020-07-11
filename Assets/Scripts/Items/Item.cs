using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public abstract class Item : MonoBehaviour
{
    [SerializeField] private string _description;

    public Sprite SpriteImage { get; private set; }
    public string Description => _description;

    private void Start()
    {
        SpriteImage = GetComponent<SpriteRenderer>().sprite;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Player player))
        {
            player.AddItem(this);
            gameObject.SetActive(false);
        }
    }

    public abstract void AddToPlayer();
}
