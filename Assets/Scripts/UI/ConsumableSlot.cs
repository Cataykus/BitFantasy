using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsumableSlot : MonoBehaviour
{
    [SerializeField] private Player _player;

    [SerializeField] Button _removeButton;
    [SerializeField] Button _useButton;

    [SerializeField] Sprite _defaultSprite;
    [SerializeField] Image _useButtonImage;
    [SerializeField] Image _removeButtonImage;

    public ConsumableItem Consumable { get; private set; }

    private void OnEnable()
    {
        _useButton.onClick.AddListener(UseConsumable);
        _removeButton.onClick.AddListener(RemoveConsumable);
    }

    private void OnDisable()
    {
        _useButton.onClick.RemoveListener(UseConsumable);
        _removeButton.onClick.RemoveListener(RemoveConsumable);
    }

    private void RemoveConsumable()
    {
        if (Consumable)
        {
            _player.RemoveConsumable(Consumable);
            Consumable = null;
            _useButtonImage.sprite = _defaultSprite;
            _removeButtonImage.sprite = _defaultSprite;
        }
    }

    private void UseConsumable()
    {
        if (Consumable)
        {
            Consumable.Use();   

            Consumable = null;
            _useButtonImage.sprite = _defaultSprite;
            _removeButtonImage.sprite = _defaultSprite;
        }
    }

    public void AddConsumable(ConsumableItem consumable)
    {
        Consumable = consumable;
        _useButtonImage.sprite = consumable.SpriteImage;
        _removeButtonImage.sprite = consumable.SpriteImage;
    }
}
