using UnityEngine;

namespace Scorewarrior.Test.Descriptors
{
	public abstract class DescriptorModifier : ScriptableObject
	{
		public abstract void ModifyValues(ModifiedDescriptor descriptor);
	}

}