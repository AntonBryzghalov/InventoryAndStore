using InventoryGame.Items;

namespace InventoryGame.CoreGameplay
{
    public static class MoveResolver
    {
        public static Outcome Compare(BasicItemType a, BasicItemType b)
        {
            if (a == b) return Outcome.Draw;

            if (a == BasicItemType.Dynamite) return Outcome.Win;

            if (b == BasicItemType.Dynamite) return Outcome.Lose;

            return (a, b) switch
            {
                (BasicItemType.Rock,     BasicItemType.Scissors) => Outcome.Win,
                (BasicItemType.Scissors, BasicItemType.Paper)    => Outcome.Win,
                (BasicItemType.Paper,    BasicItemType.Rock)     => Outcome.Win,
                _ => Outcome.Lose
            };
        }
    }
}
