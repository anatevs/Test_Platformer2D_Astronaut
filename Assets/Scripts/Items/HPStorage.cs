using System;
using UnityEngine;

namespace Gameplay
{
    public class HPStorage
    {
        public event Action<int> OnHPChanged;

        public int HP => _hp;

        public int MaxHP => _maxHP;

        private int _hp;

        private readonly int _maxHP;

        public HPStorage(int initHP, int maxHP)
        {
            _hp = initHP;
            _maxHP = maxHP;
        }

        public void AddHP(int addHP)
        {
            var newHP = Mathf.Min(_hp + addHP, _maxHP);

            _hp = newHP;

            OnHPChanged?.Invoke(_hp);
        }
    }
}