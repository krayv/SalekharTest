using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scorewarrior.Test.Descriptors
{
    public class DescriptorModifierWrap
    {
        private DescriptorModifierWrap _innerModifier;
        private DescriptorModifier _modifier;

        public DescriptorModifierWrap(DescriptorModifier modifier, DescriptorModifierWrap innerModifier)
        {
            _modifier = modifier;
            _innerModifier = innerModifier;
        }

        public static DescriptorModifierWrap Wrap(List<DescriptorModifier> modifiers)
        {
            DescriptorModifierWrap wrap = null;
            foreach (var modifier in modifiers)
            {
                wrap = new DescriptorModifierWrap(modifier, wrap);
            }
            return wrap;
        }

        public static DescriptorModifierWrap Wrap<TModifier>(List<TModifier> modifiers) where TModifier : DescriptorModifier
        {
            DescriptorModifierWrap wrap = null;
            foreach (var modifier in modifiers)
            {
                wrap = new DescriptorModifierWrap(modifier, wrap);
            }
            return wrap;
        }

        public void Modify(ModifiedDescriptor _descriptor)
        {
            if (_innerModifier != null)
            {
                _innerModifier.Modify(_descriptor);
            }
            _modifier.ModifyValues(_descriptor);
        }

        public static List<T> LoadModifiers<T>() where T : DescriptorModifier
        {
            return Resources.LoadAll<T>("ScriptableObjects").ToList();
        }

        public static DescriptorModifierSettings LoadModifierSettings()
        {
            return Resources.LoadAll<DescriptorModifierSettings>("ScriptableObjects").FirstOrDefault();
        }
    }
}
