using UnityEngine;
using UnityEngine.UI;

namespace Scorewarrior.Test.Views
{
    public class ReplayWindow : Window
    {
        [SerializeField] private Button _replayButton;

        protected override void OnInit()
        {
            _replayButton.onClick.AddListener(OnReplayButtonClick);
            _eventBus.EndBattle += () => SetActive(true);
            SetActive(false);
        }
        private void OnReplayButtonClick()
        {
            _eventBus.RestartBattle.Invoke();
            SetActive(false);
        }
    }

}