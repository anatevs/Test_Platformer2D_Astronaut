using System;

namespace Gameplay
{
    public interface IRewardStorage
    {
        public event Action<float, float> OnValueChanged;

        public float Value { get; }
    }
}