using UnityEngine;

namespace Scorewarrior.Test.Descriptors
{
    [CreateAssetMenu]
    public class CharacterModifier : DescriptorModifier
    {
        [SerializeField]
        private float _accuracyAddModifier = 0f;
        [SerializeField]
        private float _dexterityAddModifier = 0f;
        [SerializeField]
        private float _maxHealthAddModifier = 0f;
        [SerializeField]
        private float _maxArmorAddModifier = 0f;
        [SerializeField]
        private float _aimTimeAddModifier = 0f;

        public override void ModifyValues(ModifiedDescriptor descriptor)
        {
            if (descriptor is CharacterModifiedDescriptor character)
            {
                character.Accuracy += _accuracyAddModifier;
                character.Dexterity += _dexterityAddModifier;
                character.MaxHealth += _maxHealthAddModifier;
                character.MaxArmor += _maxArmorAddModifier;
                character.AimTime += _aimTimeAddModifier;
            }
            if (descriptor is WeaponModifiedDescriptor weapon)
            {
                weapon.Accuracy += _accuracyAddModifier;
            }
        }
    }

}