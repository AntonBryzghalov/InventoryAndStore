using System.Collections.Generic;
using InventoryGame.Events;
using InventoryGame.Inventory;
using InventoryGame.Shop;
using UnityEngine;

namespace InventoryGame.UI
{
    public class InventoryView : MonoBehaviour
    {
        [SerializeField] private InventorySO inventory;
        [SerializeField] private InventoryItemView itemViewPrefab;
        [SerializeField] private Transform itemViewsContainer;
        [SerializeField] private InventoryItemEvent itemSelectedEvent;

        private readonly List<InventoryItemView> _views = new();

        private void OnEnable()
        {
            inventory.ItemsUpdated += Rebuild;
            Rebuild();
        }

        private void OnDisable()
        {
            inventory.ItemsUpdated -= Rebuild;
        }

        private void Rebuild()
        {
            var items = inventory.Items;
            int requiredCount = items.Count;

            // Ensure enough views
            for (int i = _views.Count; i < requiredCount; i++)
            {
                var view = Instantiate(itemViewPrefab, itemViewsContainer);
                _views.Add(view);
                view.OnItemSelected += OnItemSelected;
            }

            // Bind data
            for (int i = 0; i < requiredCount; i++)
            {
                _views[i].gameObject.SetActive(true);
                _views[i].Bind(items[i]);
            }

            // Destroy redundant views
            for (int i = _views.Count - 1; i >= requiredCount; i--)
            {
                _views[i].OnItemSelected -= OnItemSelected;
                Destroy(_views[i].gameObject);
                _views.RemoveAt(i);
            }
        }

        private void OnItemSelected(InventoryItem inventoryItem)
        {
            itemSelectedEvent?.Invoke(inventoryItem);
        }
    }
}
