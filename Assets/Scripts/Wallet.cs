using UnityEngine;

namespace InventoryGame
{
    [CreateAssetMenu(fileName = "Wallet", menuName = "Inventory Game/Wallet")]
    public class Wallet : ScriptableObject
    {
        public int GoldAmount;
    }
}
