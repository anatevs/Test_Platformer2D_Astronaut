using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HPView : MonoBehaviour,
        IHPView
    {
        [SerializeField]
        private Slider _slider;

        [SerializeField]
        private TMP_Text _text;

        public void SetMaxHP(int max)
        {
            _slider.maxValue = max;
        }

        public void SetHP(int hp)
        {
            _slider.value = hp;
            _text.text = hp.ToString();
        }
    }
}