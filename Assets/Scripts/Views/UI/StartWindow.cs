using UnityEngine;
using UnityEngine.UI;

namespace Scorewarrior.Test.Views
{
    public class StartWindow : Window
    {
        [SerializeField] private Button _startButton;

        protected override void OnInit()
        {
            _startButton.onClick.AddListener(OnStartButtonClick);
            SetActive(true);
        }

        private void OnStartButtonClick()
        {
            SetActive(false);
            _eventBus.StartBattle.Invoke();
        }
    } 
}
