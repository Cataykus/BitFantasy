using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class Player : MonoBehaviour
{
    [SerializeField] private EnemyManager _enemyManager;
    [SerializeField] private SceneLoader _sceneLoader;

    [SerializeField] private ParticleSystem _applyDamageEffect;
    [SerializeField] private ParticleSystem _missDamageEffect;

    [SerializeField] private AudioClip _powerupClip;
    [SerializeField] private AudioClip _hurtClip;
    [SerializeField] private AudioClip _selectClip;

    [SerializeField] private int _basicDamage;
    [SerializeField] private int _basicCritDamage;
    [SerializeField] private int _basicMaxHealth;
    [SerializeField] private int _basicCritChance;
    [SerializeField] private int _basicMissChance;

    [SerializeField] private List<Item> _itemsInventory;
    [SerializeField] private List<ConsumableItem> _consumableInventory;
    [SerializeField] private List<ArtifactItem> _artifactsInventory;

    [SerializeField] private List<ConsumableSlot> _consumableSlots;
    [SerializeField] private List<ArtifactSlot> _artifactSlots;

    private AudioSource _audioSource;

    private int _currentHealth;

    private int _damage;
    private int _critDamage;
    private int _missChance;
    private int _critChance;
    private int _maxHealth;

    public int Damage => _damage;
    public int CritDamage => _critDamage;
    public int MissChance => _missChance;
    public int CritChance => _critChance;
    public int MaxHealth => _maxHealth;

    public event UnityAction PlayerTurnSkipped;

    public event UnityAction RescaledStats;

    public event UnityAction<int, int> HealthChanged;

    public event UnityAction ItemsChanged;

    public bool IsPlayerTurn { get; private set; } = true;
    public List<Item> ItemsInventory => _itemsInventory;

    private void OnEnable()
    {
        _enemyManager.EnemyTurnSkipped += StartTurn;
    }

    private void OnDisable()
    {
        _enemyManager.EnemyTurnSkipped -= StartTurn;
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        RescaleStats();

        _currentHealth = _maxHealth;

        HealthChanged?.Invoke(_currentHealth, _maxHealth);
    }

    private void RescaleStats()
    {
        _damage = _basicDamage;
        _critDamage = _basicCritDamage;
        _missChance = _basicMissChance;
        _critChance = _basicCritChance;
        _maxHealth = _basicMaxHealth;

        if(_artifactsInventory.Count > 0)
        {
            foreach(var item in _artifactsInventory)
            {
                _damage += item.BonusDamage;
                _critDamage += item.BonusCritDamage;
                _missChance += item.BonusMissChance;
                _critChance += item.BonusCritChance;
                _maxHealth += item.BonusMaxHealth;
            }
        }

        RescaledStats?.Invoke();
        HealthChanged?.Invoke(_currentHealth, _maxHealth);
    }

    private void StartTurn()
    {
        IsPlayerTurn = true;
    }

    public void SkipTurn()
    {
        PlayerTurnSkipped?.Invoke();
        IsPlayerTurn = false;
    }

    public void ApplyDamage(int damage)
    {
        _audioSource.PlayOneShot(_hurtClip);
        if (Random.value < (float)_missChance / 100)
        {
            _missDamageEffect.Play();
            return;
        }

        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            _sceneLoader.LoadScene(3);
        }

        _applyDamageEffect.Play();
        HealthChanged?.Invoke(_currentHealth, _maxHealth);
    }

    public void Attack(Enemy enemy)
    {
        enemy.ApplyDamage(_damage, _critDamage, _critChance);
        _audioSource.PlayOneShot(_hurtClip);
        SkipTurn();
    }

    public void AddItem(Item item)
    {
        _itemsInventory.Add(item);

        _audioSource.PlayOneShot(_selectClip);

        ItemsChanged?.Invoke();
    }

    public void RemoveItem(Item item)
    {
        _itemsInventory.Remove(item);

        _audioSource.PlayOneShot(_selectClip);

        ItemsChanged?.Invoke();
    }

    public void AddArtifact(ArtifactItem item)
    {
        foreach(var slot in _artifactSlots)
        {
            if (slot.Artifact == null)
            {
                slot.AddArtifact(item);

                _artifactsInventory.Add(item);
                RemoveItem(item);
                RescaleStats();

                return;
            }
        } 
    }

    public void RemoveArtifact(ArtifactItem item)
    {
        _artifactsInventory.Remove(item);
        AddItem(item);

        RescaleStats();
    }

    public void AddConsumable(ConsumableItem item)
    {
        foreach (var slot in _consumableSlots)
        {
            if (slot.Consumable == null)
            {
                slot.AddConsumable(item);

                _consumableInventory.Add(item);
                RemoveItem(item);

                return;
            }
        }
    }

    public void RemoveConsumable(ConsumableItem item)
    {
        _consumableInventory.Remove(item);
        AddItem(item);
    }

    public void UseConsumable(ConsumableItem consumable)
    {
        _audioSource.PlayOneShot(_powerupClip);

        _currentHealth += consumable.Healing;

        if (_currentHealth >= _maxHealth)
        {
            _currentHealth = _maxHealth;
        }

        HealthChanged?.Invoke(_currentHealth, _maxHealth);
        SkipTurn();
    }
}
