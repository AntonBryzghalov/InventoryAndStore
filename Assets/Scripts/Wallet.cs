using System;
using UnityEngine;

namespace InventoryGame
{
    [CreateAssetMenu(fileName = "Wallet", menuName = "Inventory Game/Wallet")]
    public class Wallet : ScriptableObject
    {
        [SerializeField] private int goldAmount;
        public event Action CurrencyAmountChanged;

        public int GoldAmount
        {
            get => goldAmount;
            set
            {
                goldAmount = value;
                CurrencyAmountChanged?.Invoke();
            }
        }
    }
}
