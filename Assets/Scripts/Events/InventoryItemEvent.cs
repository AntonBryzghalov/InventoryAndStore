using InventoryGame.Inventory;
using UnityEngine;

namespace InventoryGame.Events
{
    [CreateAssetMenu(fileName = "ItemPurchasedEvent", menuName = "Inventory Game/Events/Item Purchased")]
    public class InventoryItemEvent : ScriptableEvent<InventoryItem>
    {
    }
}
