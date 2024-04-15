using Scorewarrior.Test.Descriptors;

namespace Scorewarrior.Test.Descriptors
{
    public class CharacterModifiedDescriptor : ModifiedDescriptor
    {
        public float Accuracy;
        public float Dexterity;
        public float MaxHealth;
        public float MaxArmor;
        public float AimTime;

        public CharacterModifiedDescriptor(Descriptor descriptor, DescriptorModifierWrap wrap) : base(descriptor, wrap)
        {
            ResetValue();
            wrap.Modify(this);
        }

        public override void ResetValue()
        {
            if (_descriptor is WeaponDescriptor weaponDescriptor)
            {
                Accuracy = weaponDescriptor.Accuracy;
            }
            if (_descriptor is CharacterDescriptor characterDescriptor)
            {
                Dexterity = characterDescriptor.Dexterity;
                Accuracy = characterDescriptor.Accuracy;
                MaxHealth = characterDescriptor.MaxHealth;
                MaxArmor = characterDescriptor.MaxArmor;
                AimTime = characterDescriptor.AimTime;
            }
        }
    }
}