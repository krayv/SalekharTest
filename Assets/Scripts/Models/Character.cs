using Scorewarrior.Test.Descriptors;
using Scorewarrior.Test.Views;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Scorewarrior.Test.Models
{
    public class Character
    {
        private enum State
        {
            Idle,
            Aiming,
            Shooting,
            Reloading
        }

        private readonly CharacterPrefab _prefab;
        private readonly Weapon _weapon;
        private readonly Battlefield _battlefield;

        private float _health;
        private float _armor;

        private State _state;
        private Character _currentTarget;
        private float _time;
        private CharacterModifiedDescriptor _descriptor;
        private List<DescriptorModifier> _modifiers;

        public CharacterModifiedDescriptor Descriptor => _descriptor;
        public List<DescriptorModifier> Modifiers => _modifiers;

        public Character(CharacterPrefab prefab, Weapon weapon, Battlefield battlefield)
        {
            _prefab = prefab;
            _weapon = weapon;
            _battlefield = battlefield;
            Descriptor descriptor = _prefab.GetComponent<CharacterDescriptor>();

            _modifiers = DescriptorModifierWrap.LoadModifiers<CharacterModifier>().Select(m=> m as DescriptorModifier).ToList();
            _modifiers = _modifiers.GetRandomElements(DescriptorModifierWrap.LoadModifierSettings().MaxCharacterModifiers);
            DescriptorModifierWrap wrap = DescriptorModifierWrap.Wrap(_modifiers);
            _descriptor = new CharacterModifiedDescriptor(descriptor, wrap);
            weapon.AddModifiers(_modifiers);

            _health = Descriptor.MaxHealth;
            _armor = Descriptor.MaxArmor;          
        }

        public bool IsAlive => _health > 0 || _armor > 0;
        public uint Team
        {
            get
            {
                if (_battlefield.TryGetTeam(this, out uint team))
                {
                    return team;
                }
                return 0;
            }
        }

        public float HealthPercent => Health / Descriptor.MaxHealth;
        public float MaxHealth => Descriptor.MaxHealth;
        public float ArmorPercent => Armor / Descriptor.MaxArmor;
        public float MaxArmor => Descriptor.MaxArmor;

        public float Health
        {
            get
            {
                return _health;
            }
            set
            {
                value = Mathf.Max(value, 0);
                if (_health != value)
                {                    
                    _health = value; 
                    OnHealthChanged?.Invoke(value);
                }

                if (!IsAlive)
                {
                    OnCharacterDeath?.Invoke();
                }
            }
        }

        public float Armor
        {
            get 
            {
                return _armor; 
            }
            set 
            {
                value = Mathf.Max(value, 0);
                if (_armor != value)
                {                   
                    _armor = value;
                    OnArmorChanged?.Invoke(value);
                }

                if (!IsAlive)
                {
                    OnCharacterDeath?.Invoke();
                }
            }
        }

        public Action<float> OnHealthChanged;
        public Action<float> OnArmorChanged;
        public Action OnCharacterDeath;
        public Action OnDestroy;

        public CharacterPrefab Prefab => _prefab;
        public Vector3 Position => _prefab.transform.position;

        public void Update(float deltaTime)
        {
            if (IsAlive)
            {
                switch (_state)
                {
                    case State.Idle:
                        _prefab.Animator.SetBool("aiming", false);
                        _prefab.Animator.SetBool("reloading", false);
                        if (_battlefield.TryGetNearestAliveEnemy(this, out Character target))
                        {
                            _currentTarget = target;
                            _state = State.Aiming;
                            _time = Descriptor.AimTime;
                            _prefab.transform.LookAt(_currentTarget.Position);
                        }
                        break;
                    case State.Aiming:
                        _prefab.Animator.SetBool("aiming", true);
                        _prefab.Animator.SetBool("reloading", false);
                        if (_currentTarget != null && _currentTarget.IsAlive)
                        {
                            if (_time > 0)
                            {
                                _time -= deltaTime;
                            }
                            else
                            {
                                _state = State.Shooting;
                                _time = 0;
                            }
                        }
                        else
                        {
                            _state = State.Idle;
                            _time = 0;
                        }
                        break;
                    case State.Shooting:
                        _prefab.Animator.SetBool("aiming", true);
                        _prefab.Animator.SetBool("reloading", false);
                        if (_currentTarget != null && _currentTarget.IsAlive)
                        {
                            if (_weapon.HasAmmo)
                            {
                                if (_weapon.IsReady)
                                {
                                    float random = UnityEngine.Random.Range(0.0f, 1.0f);
                                    bool hit = random <= Descriptor.Accuracy &&
                                            random <= _weapon.Descriptor.Accuracy &&
                                            random >= _currentTarget.Descriptor.Dexterity;
                                    _weapon.Fire(_currentTarget, hit);
                                    _prefab.Animator.SetTrigger("shoot");
                                }
                                else
                                {
                                    _weapon.Update(deltaTime);
                                }
                            }
                            else
                            {
                                _state = State.Reloading;
                                _time = _weapon.Descriptor.ReloadTime;
                            }
                        }
                        else
                        {
                            _state = State.Idle;
                        }
                        break;
                    case State.Reloading:
                        _prefab.Animator.SetBool("aiming", true);
                        _prefab.Animator.SetBool("reloading", true);
                        _prefab.Animator.SetFloat("reload_time", _weapon.Descriptor.ReloadTime / 3.3f);
                        if (_time > 0)
                        {
                            _time -= deltaTime;
                        }
                        else
                        {
                            if (_currentTarget != null && _currentTarget.IsAlive)
                            {
                                _state = State.Shooting;
                            }
                            else
                            {
                                _state = State.Idle;
                            }
                            _weapon.Reload();
                            _time = 0;
                        }
                        break;
                }
            }
        }

        public void Destroy()
        {
            OnDestroy?.Invoke();
        }
    }
}