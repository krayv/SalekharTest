using Scorewarrior.Test.Descriptors;

namespace Scorewarrior.Test.Descriptors
{
    public class WeaponModifiedDescriptor : ModifiedDescriptor
    {
        public float Damage;
        public float Accuracy;
        public float FireRate;
        public uint ClipSize;
        public float ReloadTime;

        public WeaponModifiedDescriptor(Descriptor descriptor, DescriptorModifierWrap wrap) : base(descriptor, wrap)
        {
            ResetValue();
            Modify();
        }

        public override void ResetValue()
        {
            if (_descriptor is WeaponDescriptor weaponDescriptor)
            {
                Damage = weaponDescriptor.Damage;
                FireRate = weaponDescriptor.FireRate;
                ClipSize = weaponDescriptor.ClipSize;
                ReloadTime = weaponDescriptor.ReloadTime;
                Accuracy = weaponDescriptor.Accuracy;
            }
        }
    }
}
