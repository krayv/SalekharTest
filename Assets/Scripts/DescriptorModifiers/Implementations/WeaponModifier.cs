using UnityEngine;

namespace Scorewarrior.Test.Descriptors
{
    [CreateAssetMenu]
    public class WeaponModifier : DescriptorModifier
    {
        [SerializeField]
        private float _damageAddModifier = 0f;
        [SerializeField]
        private float _accuracyAddModifier = 0f;
        [SerializeField]
        private float _fireRateAddModifier = 0f;
        [SerializeField]
        private uint _clipSizeAddModifier = 0;
        [SerializeField]
        private float _reloadAddTime = 0f;
        public override void ModifyValues(ModifiedDescriptor descriptor)
        {
            if (descriptor is WeaponModifiedDescriptor weapon)
            {
                weapon.Damage += _damageAddModifier;
                weapon.Accuracy += _accuracyAddModifier;
                weapon.FireRate += _fireRateAddModifier;
                weapon.ClipSize += _clipSizeAddModifier;
                weapon.ReloadTime += _reloadAddTime;
            }
        }
    } 
}
