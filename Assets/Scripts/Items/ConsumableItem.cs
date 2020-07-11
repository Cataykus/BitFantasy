using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableItem : Item
{
    [SerializeField] private int _healing;
    [SerializeField] private Player _player;

    public int Healing => _healing;

    public void Use()
    {
        _player.UseConsumable(this);
    }

    public override void AddToPlayer()
    {
        _player.AddConsumable(this);
    }
}
