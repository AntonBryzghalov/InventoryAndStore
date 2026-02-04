using InventoryGame.UI;
using UnityEngine;

namespace InventoryGame
{
    public class WalletView : MonoBehaviour
    {
        [SerializeField] private FormattedIntTextSetter textSetter;
        [SerializeField] private Wallet targetWallet;

        private void OnEnable()
        {
            OnWalletCurrencyAmountChanged();
            targetWallet.CurrencyAmountChanged += OnWalletCurrencyAmountChanged;
        }

        private void OnDisable()
        {
            targetWallet.CurrencyAmountChanged -= OnWalletCurrencyAmountChanged;
        }

        private void OnWalletCurrencyAmountChanged()
        {
            textSetter.SetValue(targetWallet.GoldAmount);
        }
    }
}
