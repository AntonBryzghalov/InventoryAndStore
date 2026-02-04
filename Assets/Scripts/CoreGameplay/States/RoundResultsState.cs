using System.Collections;
using System.Text;
using InventoryGame.GameLoop;
using InventoryGame.UI;
using UnityEngine;

namespace InventoryGame.CoreGameplay.States
{
    public class RoundResultsState : CoreGameplayState
    {
        [Header("FSM")]
        [SerializeField] private CoreGameplayFiniteStateMachine fsm;
        [SerializeField] private CoreGameplayStateId itemsSelectionsStateId;
        [SerializeField] private CoreGameplayStateId gameResultStateId;

        [Header("Scriptable References")]
        [SerializeField] private GameContext gameContext;
        [SerializeField] private GameLoopConfig config;

        [Header("UI References")]
        [SerializeField] private FullscreenMessage message;
        [SerializeField] private FormattedIntTextSetter yourScoreText;
        [SerializeField] private FormattedIntTextSetter aiScoreText;

        [Header("Configuration")]
        [SerializeField] private float waitAfterMessageDuration = 1f;

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
            var outcome = CalculateMatchResults();
            ApplyScore(outcome);
            var messageText = BuildOutcomeMessage(outcome);
            message.Show(messageText);
            while (message.IsDisplayed)
            {
                yield return null;
            }

            yield return new WaitForSecondsRealtime(waitAfterMessageDuration);

            MoveToNextState();
        }

        private void MoveToNextState()
        {
            var maxScoreToWinGame = config.RoundsPerGame / 2;
            if (config.RoundsPerGame % 2 > 0) maxScoreToWinGame++;

            var isGameOver = gameContext.RealPlayer.GameState.Score == maxScoreToWinGame ||
                             gameContext.AIPlayer.GameState.Score == maxScoreToWinGame ||
                             gameContext.Round == config.RoundsPerGame;
            fsm.SwitchTo(isGameOver ? gameResultStateId : itemsSelectionsStateId);
        }

        private Outcome CalculateMatchResults()
        {
            return MoveResolver.Compare(
                gameContext.RealPlayer.GameState.SelectedBasicItem.ItemType,
                gameContext.AIPlayer.GameState.SelectedBasicItem.ItemType);
        }

        private void ApplyScore(Outcome outcome)
        {
            if (outcome == Outcome.Win)
            {
                gameContext.RealPlayer.GameState.Score++;
            }
            else if (outcome == Outcome.Lose)
            {
                gameContext.AIPlayer.GameState.Score++;
            }

            yourScoreText.SetValue(gameContext.RealPlayer.GameState.Score);
            aiScoreText.SetValue(gameContext.AIPlayer.GameState.Score);
        }

        private string BuildOutcomeMessage(Outcome outcome)
        {
            _stringBuilder.Clear();
            switch (outcome)
            {
                case Outcome.Win:
                    _stringBuilder.AppendLine("You Win this Round! :D");
                    break;
                case Outcome.Lose:
                    _stringBuilder.AppendLine("You Lose this Round! :(");
                    break;
                case Outcome.Draw:
                    _stringBuilder.AppendLine("Draw! :)");
                    break;
            }

            return _stringBuilder.ToString();
        }
    }
}
