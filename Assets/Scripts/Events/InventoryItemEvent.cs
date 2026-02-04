using InventoryGame.Inventory;
using UnityEngine;

namespace InventoryGame.Events
{
    [CreateAssetMenu(fileName = "InventoryItemEvent", menuName = "Inventory Game/Events/Inventory Item Event")]
    public class InventoryItemEvent : ScriptableEvent<InventoryItem>
    {
    }
}
