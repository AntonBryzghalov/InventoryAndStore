using InventoryGame.Events;
using InventoryGame.Inventory;
using UnityEngine;

namespace InventoryGame.Shop
{
    [CreateAssetMenu(fileName = "ItemPurchasedEvent", menuName = "Inventory Game/Events/Item Purchased")]
    public class InventoryItemEvent : ScriptableEvent<InventoryItem>
    {
    }
}
