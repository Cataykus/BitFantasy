using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatsBar : MonoBehaviour
{
    [SerializeField] private Player _player;

    [SerializeField] private TMP_Text _damageText;
    [SerializeField] private TMP_Text _critDamageText;
    [SerializeField] private TMP_Text _maxHealthText;
    [SerializeField] private TMP_Text _critChanceText;
    [SerializeField] private TMP_Text _missChanceText;

    private void OnEnable()
    {
        _player.RescaledStats += RenderStatsBar;
    }

    private void OnDisable()
    {
        _player.RescaledStats -= RenderStatsBar;
    }

    private void RenderStatsBar()
    {
        _damageText.text = _player.Damage.ToString(); 
        _critDamageText.text = _player.CritDamage.ToString(); 
        _maxHealthText.text = _player.MaxHealth.ToString(); 
        _critChanceText.text = _player.CritChance.ToString(); 
        _missChanceText.text = _player.MissChance.ToString(); 
    }
}
