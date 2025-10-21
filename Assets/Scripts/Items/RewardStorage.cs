using System;

namespace Gameplay
{
    public class RewardStorage : IRewardStorage
    {
        public float Value => _value;

        public event Action<float, float> OnValueChanged;

        private float _value;

        private float _levelValue = 0;

        public RewardStorage(float initVal)
        {
            _value = initVal;
        }

        public void Add(float addValue)
        {
            var newValue = _value + addValue;

            OnValueChanged?.Invoke(_value, newValue);

            _value = newValue;

            _levelValue += addValue;
        }

        public void Set(float newValue)
        {
            _levelValue += newValue - _value;

            OnValueChanged?.Invoke(_value, newValue);

            _value = newValue;
        }

        public void ResetLevel()
        {
            Add(-_levelValue);
        }
    }
}