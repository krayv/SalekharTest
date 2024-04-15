using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scorewarrior.Test.Views
{
    public class DefaultHUDBar : HUDBar
    {
        [SerializeField]
        private Image _bar;
        [SerializeField]
        private TextMeshProUGUI _text;

        public override void SetBarColor(Color color)
        {
            _bar.color = color;
        }

        public override void SetValue(float maxValue, float currentValue, float progress)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(Mathf.Ceil(currentValue).ToString());
            stringBuilder.Append("/");
            stringBuilder.Append(Mathf.Ceil(maxValue).ToString());
            _text.text = stringBuilder.ToString();
            _bar.fillAmount = progress;
        }
    } 
}
