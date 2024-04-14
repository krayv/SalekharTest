using System;
using UnityEngine;

namespace Scorewarrior.Test.Views
{
    [Serializable]
    public abstract class Window : MonoBehaviour
    {
        protected UIController _uiController;
        protected EventBus _eventBus => _uiController.EventBus;
        public void Init(UIController uiController)
        {
            _uiController = uiController;
            OnInit();
        }

        protected abstract void OnInit();

        public virtual void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }
    } 
}
