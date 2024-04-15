using Scorewarrior.Test.Descriptors;
using Scorewarrior.Test.Views;
using System.Collections.Generic;
using System.Linq;

namespace Scorewarrior.Test.Models
{
	public class Weapon
	{
		private readonly WeaponPrefab _prefab;

		private uint _ammo;

		private bool _ready;
		private float _time;

        private WeaponModifiedDescriptor _descriptor;
        private List<DescriptorModifier> _modifiers;
        public WeaponModifiedDescriptor Descriptor => _descriptor;

        public Weapon(WeaponPrefab prefab)
		{
			_prefab = prefab;
			
            _modifiers = DescriptorModifierWrap.LoadModifiers<WeaponModifier>().Select(m => m as DescriptorModifier).ToList();
            _modifiers = _modifiers.GetRandomElements(DescriptorModifierWrap.LoadModifierSettings().MaxWeaponModifiers);
			ApplyModifiers();           
		}

		public bool IsReady => _ready;
		public bool HasAmmo => _ammo > 0;

		public WeaponPrefab Prefab => _prefab;

		public void ApplyModifiers()
		{
            Descriptor descriptor = _prefab.GetComponent<WeaponDescriptor>();
            DescriptorModifierWrap wrap = DescriptorModifierWrap.Wrap(_modifiers);
            _descriptor = new WeaponModifiedDescriptor(descriptor, wrap);
            _ammo = _descriptor.ClipSize;
        }

		public void AddModifiers(List<DescriptorModifier> modifiers)
		{
			foreach (var modifier in modifiers)
			{
				_modifiers.Add(modifier);
			}
			ApplyModifiers();
		}

		public void Reload()
		{
			_ammo = _descriptor.ClipSize;
		}

		public void Fire(Character character, bool hit)
		{
			if (_ammo > 0)
			{
				_ammo -= 1;
				_prefab.Fire(this, character, hit);
				_time = 1.0f / _descriptor.FireRate;
				_ready = false;
			}
		}

		public void Update(float deltaTime)
		{
			if (!_ready)
			{
				if (_time > 0)
				{
					_time -= deltaTime;
				}
				else
				{
					_ready = true;
				}
			}
		}
	}
}