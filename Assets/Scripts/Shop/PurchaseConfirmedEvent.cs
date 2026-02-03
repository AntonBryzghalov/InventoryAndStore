using InventoryGame.Events;
using InventoryGame.Inventory;
using UnityEngine;

namespace InventoryGame.Shop
{
    [CreateAssetMenu(fileName = "PurchaseConfirmedEvent", menuName = "Inventory Game/Events/Purchase Confirmed")]
    public class PurchaseConfirmedEvent : ScriptableEvent<InventoryItem>
    {
    }
}
