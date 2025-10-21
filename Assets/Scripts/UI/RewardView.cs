using TMPro;
using UnityEngine;

namespace UI
{
    public class RewardView : MonoBehaviour,
        IRewardView
    {
        [SerializeField]
        private TMP_Text _valueText;

        public void ChangeValue(float prevValue, float newValue)
        {
            _valueText.text = newValue.ToString();
        }
    }
}