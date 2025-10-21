using System;
using UI;

namespace Gameplay
{
    public class HPController : IDisposable
    {
        private readonly HPStorage _hpStorage;
        private readonly IHPView _view;

        public HPController(HPStorage hpStorage, IHPView view)
        {
            _hpStorage = hpStorage;
            _view = view;

            _view.SetMaxHP(_hpStorage.MaxHP);
            _view.SetHP(_hpStorage.HP);

            _hpStorage.OnHPChanged += _view.SetHP;
        }

        void IDisposable.Dispose()
        {
            _hpStorage.OnHPChanged -= _view.SetHP;
        }
    }
}