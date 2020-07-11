using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ViewItem : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Image _image;
    [SerializeField] private Button _button;

    private Item _playerItem;

    private void OnEnable()
    {
        _button.onClick.AddListener(AddItem);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(AddItem);
    }

    private void AddItem()
    {
        _playerItem.AddToPlayer();
    }

    public void Initialize(Item item)
    {
        _text.text = item.Description;
        _image.sprite = item.SpriteImage;
        _playerItem = item;
    }
}
