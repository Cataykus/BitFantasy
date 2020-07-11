using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScrollView : MonoBehaviour
{
    [SerializeField] private GameObject _itemTemplate;

    [SerializeField] private Player _player;

    [SerializeField] private Transform _container;

    private void OnEnable()
    {
        _player.ItemsChanged += RefreshView;

        RefreshView();
    }

    private void OnDisable()
    {
        _player.ItemsChanged -= RefreshView;
    }

    private void RefreshView()
    {
        ViewItem[] viewItems = _container.GetComponentsInChildren<ViewItem>();
        foreach (var item in viewItems)
        {
            Destroy(item.gameObject);
        }

        foreach (var item in _player.ItemsInventory)
        {
            var instantiated = Instantiate(_itemTemplate, _container);
            ViewItem view = instantiated.GetComponent<ViewItem>();
            view.Initialize(item);
        }
    }
}
