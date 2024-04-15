using Scorewarrior.Test.Models;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Scorewarrior.Test.Views
{
    public class HUDWindow : Window
    {
        [SerializeField]
        private CharacterHUDPanel _characterHUDPrefab;

        [SerializeField]
        private int _hudStartPoolSize = 6;

        [SerializeField]
        private int _maxPoolSize = 24;

        [SerializeField]
        private Camera _mainCamera;

        [SerializeField]
        private Vector2 _characterHUDOffset;

        private IObjectPool<CharacterHUDPanel> _hudPool;

        protected override void OnInit()
        {
            _eventBus.StartBattle += () => SetActive(true);
            _eventBus.SpawnCharacters += OnSpawnCharacters;
            _hudPool = new ObjectPool<CharacterHUDPanel>(CreatePoolItem, OnTakeFromPool, OnReturnToPool, OnDestroyPoolObject, false, _hudStartPoolSize, _maxPoolSize);
        }

        private void OnDestroyPoolObject(CharacterHUDPanel panel)
        {
            Destroy(panel.gameObject);
        }

        private void OnReturnToPool(CharacterHUDPanel panel)
        {
            panel.gameObject.SetActive(false);
            panel.OnUnsubscribe -= OnReturnToPool;
        }

        private void OnTakeFromPool(CharacterHUDPanel panel)
        {
            panel.gameObject.SetActive(true);
        }

        private CharacterHUDPanel CreatePoolItem()
        {
            return Instantiate(_characterHUDPrefab, transform);
        }

        private void OnSpawnCharacters(List<Character> characters)
        {
            foreach (var character in characters)
            {
                CharacterHUDPanel hud = _hudPool.Get();
                hud.Subscribe(character);
                hud.transform.position = (Vector3)_characterHUDOffset + _mainCamera.WorldToScreenPoint(character.Prefab.transform.position);
                hud.OnUnsubscribe += OnReturnToPool;
            }
        }
    }
}
