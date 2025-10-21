using System;
using UI;

namespace Gameplay
{
    public class RewardController : IDisposable
    {
        private readonly IRewardStorage _storage;

        private readonly IRewardView _view;

        public RewardController(IRewardStorage storage, IRewardView view)
        {
            _storage = storage;
            _view = view;

            _view.ChangeValue(0, _storage.Value);

            _storage.OnValueChanged += _view.ChangeValue;
        }

        void IDisposable.Dispose()
        {
            _storage.OnValueChanged -= _view.ChangeValue;
        }
    }
}