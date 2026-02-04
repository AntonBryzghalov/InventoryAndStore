using System.Collections;
using System.Text;
using InventoryGame.Events;
using InventoryGame.GameLoop;
using InventoryGame.UI;
using UnityEngine;

namespace InventoryGame.CoreGameplay.States
{
    public class GameResultsState : CoreGameplayState
    {
        [Header("Scriptable References")]
        [SerializeField] private GameContext gameContext;
        [SerializeField] private GameLoopConfig config;
        [SerializeField] private ScriptableEvent gameOverEvent;

        [Header("UI References")]
        [SerializeField] private FullscreenMessage message;

        [Header("Configuration")]
        [SerializeField] private float messageDuration = 3f;
        [SerializeField] private float waitAfterMessageDuration = 1f;
        [SerializeField] private Color playerScoreColor = Color.black;
        [SerializeField] private Color aiScoreColor = Color.black;

        private StringBuilder _stringBuilder = new ();

        public override void OnEnter()
        {
            StartCoroutine(ResultsCoroutine());
        }

        public override void OnExit()
        {
            _stringBuilder.Clear();
        }

        private IEnumerator ResultsCoroutine()
        {
            var outcome = CalculateGameResults();
            GiveRewards(outcome);
            var messageText = BuildOutcomeMessage(outcome);
            message.Show(messageText, messageDuration);
            while (message.IsDisplayed)
            {
                yield return null;
            }

            yield return new WaitForSecondsRealtime(waitAfterMessageDuration);

            gameOverEvent.Invoke();
        }

        private Outcome CalculateGameResults()
        {
            return (Outcome) gameContext.RealPlayer.GameState.Score.CompareTo(gameContext.AIPlayer.GameState.Score);
        }

        private void GiveRewards(Outcome outcome)
        {
            switch (outcome)
            {
                case Outcome.Win:
                    gameContext.RealPlayer.Wallet.GoldAmount += config.WinnerGoldReward;
                    break;
                case Outcome.Lose:
                    gameContext.AIPlayer.Wallet.GoldAmount += config.WinnerGoldReward;
                    break;
                case Outcome.Draw:
                    gameContext.RealPlayer.Wallet.GoldAmount += config.DrawGoldReward;
                    gameContext.AIPlayer.Wallet.GoldAmount += config.DrawGoldReward;
                    break;
            }
        }

        private string BuildOutcomeMessage(Outcome outcome)
        {
            _stringBuilder.Clear();

            // Score line
            _stringBuilder.AppendLine(
                string.Format(
                    "<color=#{0}>{1:D0}</color>:<color=#{2}>{3:D0}</color>",
                    ColorUtility.ToHtmlStringRGB(playerScoreColor),
                    gameContext.RealPlayer.GameState.Score,
                    ColorUtility.ToHtmlStringRGB(aiScoreColor),
                    gameContext.AIPlayer.GameState.Score
                )
            );

            switch (outcome)
            {
                case Outcome.Win:
                    _stringBuilder.AppendLine("You Win this Game! :D");
                    AppendGoldBonusString(config.WinnerGoldReward);
                    break;
                case Outcome.Lose:
                    _stringBuilder.AppendLine("You Lose this Game! :(");
                    AppendGoldBonusString(0);
                    break;
                case Outcome.Draw:
                    _stringBuilder.AppendLine("Draw! :)");
                    AppendGoldBonusString(config.DrawGoldReward);
                    break;
            }

            return _stringBuilder.ToString();
        }

        private void AppendGoldBonusString(int goldAmount)
        {
            if (goldAmount <= 0)
            {
                _stringBuilder.Append("(no Gold bonus)");
                return;
            }

            _stringBuilder.Append("Gold bonus: ").Append(goldAmount);
        }
    }
}
