using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactItem : Item
{
    [SerializeField] private int _bonusDamage;
    [SerializeField] private int _bonusCritDamage;
    [SerializeField] private int _bonusMaxHealth;
    [SerializeField] private int _bonusCritChance;
    [SerializeField] private int _bonusMissChance;

    [SerializeField] private Player _player;

    public int BonusDamage => _bonusDamage;
    public int BonusCritDamage => _bonusCritDamage;
    public int BonusMaxHealth => _bonusMaxHealth;
    public int BonusCritChance => _bonusCritChance;
    public int BonusMissChance => _bonusMissChance;

    public override void AddToPlayer()
    {
        _player.AddArtifact(this);
    }
}
