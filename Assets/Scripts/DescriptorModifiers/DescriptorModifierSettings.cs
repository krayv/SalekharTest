using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scorewarrior.Test.Descriptors
{
	[CreateAssetMenu]
	public class DescriptorModifierSettings : ScriptableObject
	{
		public int MaxWeaponModifiers;
		public int MaxCharacterModifiers;
	}

}