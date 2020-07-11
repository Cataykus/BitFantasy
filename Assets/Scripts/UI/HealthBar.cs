using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Player _player;

    [SerializeField] private TMP_Text _currentHealth;
    [SerializeField] private TMP_Text _maxHealth;

    private void OnEnable()
    {
        _player.HealthChanged += RenderHealthBar;
    }

    private void OnDisable()
    {
        _player.HealthChanged -= RenderHealthBar;
    }

    private void RenderHealthBar(int current, int max)
    {
        _currentHealth.text = current.ToString();
        _maxHealth.text = max.ToString();
    }
}
