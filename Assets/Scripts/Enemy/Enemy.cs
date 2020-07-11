using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyController))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private int _missChance;
    [SerializeField] private int _damage;
    [SerializeField] private float _aggreRadius;

    [SerializeField] private ParticleSystem _applyDamageEffect;
    [SerializeField] private ParticleSystem _missDamageEffect;
    [SerializeField] private ParticleSystem _criticalDamageEffect;

    [SerializeField] private Player _player;

    private bool _isAggred = false;

    private EnemyController _enemyController;

    private void Start()
    {
        _enemyController = GetComponent<EnemyController>();
    }

    public void MakeTurn()
    {
        TryAggre();

        if (_isAggred)
        {
            _enemyController.MoveToPlayer();
        }
        else
        {
            _enemyController.RandomMove();
        }
    }

    private void TryAggre()
    {
        _isAggred = false;
        Collider2D player = Physics2D.OverlapCircle(transform.position, _aggreRadius, 1 << 10);
        if (player)
        {
            _isAggred = true;
        }
    }

    public void AttackPlayer()
    {
        _player.ApplyDamage(_damage);
    }

    public void ApplyDamage(int damage, int critDamage, int critChance)
    {
        if (Random.value < (float)_missChance / 100)
        {
            _missDamageEffect.Play();
            return;
        }

        if (Random.value < (float)critChance / 100)
        {
            _health -= Mathf.FloorToInt(damage * (float)critDamage / 100);
            _criticalDamageEffect.Play();

            if (_health <= 0)
            {
                Destroy(gameObject);
            }

            return;
        }

        _health -= damage;
        _applyDamageEffect.Play();

        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
