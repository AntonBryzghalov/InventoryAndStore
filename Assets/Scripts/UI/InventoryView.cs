using System.Collections.Generic;
using UnityEngine;

namespace InventoryGame.Inventory
{
    public class InventoryView : MonoBehaviour
    {
        [SerializeField] private Inventory inventory;
        [SerializeField] private InventoryItemView itemViewPrefab;
        [SerializeField] private Transform itemViewsContainer;

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
                Destroy(_views[i].gameObject);
                _views.RemoveAt(i);
            }
        }
    }
}
