using Scorewarrior.Test.Models;
using System;
using UnityEngine;

namespace Scorewarrior.Test.Views
{
    public class CharacterHUDPanel : MonoBehaviour
    {
        public Action<CharacterHUDPanel> OnUnsubscribe;

        private Character _character;

        [SerializeField]
        private HUDBar _healthBar;
        [SerializeField]
        private HUDBar _armorBar;
        [SerializeField]
        private uint _playersTeam = 1;
        [SerializeField]
        private Color _playerHealthBarColor = Color.green;
        [SerializeField]
        private Color _enemyHealthBarColor = Color.red;

        public void Subscribe(Character character)
        {
            _character = character;
            character.OnArmorChanged += OnChangeArmor;
            character.OnHealthChanged += OnChangeHealth;
            character.OnCharacterDeath += Unsubscribe;
            character.OnDestroy += Unsubscribe;
            OnChangeArmor(character.Armor);
            OnChangeHealth(character.Health);
            _healthBar.SetBarColor(character.Team == _playersTeam ? _playerHealthBarColor : _enemyHealthBarColor);
        }

        private void OnChangeHealth(float value)
        {
            _healthBar.SetValue(_character.MaxHealth, value, _character.HealthPercent);
        }

        private void OnChangeArmor(float value)
        {
            _armorBar.SetValue(_character.MaxArmor, value, _character.ArmorPercent);
        }

        private void OnDestroy()
        {
            Unsubscribe();
        }

        private void OnGameRestart()
        {
            Unsubscribe();
        }

        private void Unsubscribe()
        {
            OnUnsubscribe?.Invoke(this);
            _character.OnArmorChanged -= OnChangeArmor;
            _character.OnHealthChanged -= OnChangeHealth;
            _character.OnCharacterDeath -= Unsubscribe;
            _character.OnDestroy -= Unsubscribe;
        }
    } 
}
