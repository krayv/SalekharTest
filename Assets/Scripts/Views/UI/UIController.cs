using Scorewarrior.Test;
using System.Collections.Generic;
using UnityEngine;

namespace Scorewarrior.Test.Views
{
    public class UIController : MonoBehaviour
    {
        private EventBus _eventBus;
        [SerializeField] private List<Window> _windows = new List<Window>();

        public EventBus EventBus => _eventBus;

        public void Init(EventBus eventBus)
        {
            _eventBus = eventBus;
            foreach (Window window in _windows)
            {
                window.Init(this);
            }
        }
    }

}