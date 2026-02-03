using UnityEngine;

namespace InventoryGame.GameLoop
{
    [CreateAssetMenu(fileName = "GameRules", menuName = "Inventory Game/Game Loop Config")]
    public class GameLoopConfig : ScriptableObject
    {
        [SerializeField] private int initialGoldAmount = 0;
        [SerializeField] private int goldGivenEachCycle = 1500;
        [SerializeField] private int basicItemsToBuy = 3;
        [SerializeField] private int winnerGoldReward = 500;
        [SerializeField] private int evenGoldReward = 250;

        public int InitialGoldAmount => initialGoldAmount;
        public  int GoldGivenEachCycle => goldGivenEachCycle;
        public int BasicItemsToBuy => basicItemsToBuy;
        public int WinnerGoldReward => winnerGoldReward;
        public int EvenGoldReward => evenGoldReward;
    }
}
