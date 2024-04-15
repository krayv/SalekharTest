using System;
using UnityEngine;

namespace Scorewarrior.Test.Views
{
	[Serializable]
	public abstract class HUDBar : MonoBehaviour
	{
		public abstract void SetValue(float MaxValue, float CurrentValue, float Progress);
		public abstract void SetBarColor(Color color);
	}

}