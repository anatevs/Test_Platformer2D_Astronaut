using System;
using UI;
using UnityEngine;

namespace Gameplay
{
    public class PlayerInfoController : MonoBehaviour
    {
        [SerializeField]
        private float _initMoney = 5;

        [SerializeField]
        private int _initHP = 50;

        [SerializeField]
        private int _maxHP = 100;

        [SerializeField]
        private RewardView _moneyView;

        [SerializeField]
        private HPView _hpView;

        private RewardStorage _moneyStorage;

        private RewardController _moneyController;

        private HPStorage _hpStorage;

        private HPController _hpController;

        private void OnEnable()
        {
            _moneyStorage = new RewardStorage(_initMoney);

            _moneyController = new RewardController(_moneyStorage, _moneyView);

            _hpStorage = new HPStorage(_initHP, _maxHP);

            _hpController = new HPController(_hpStorage, _hpView);
        }

        public void AddReward(float cost)
        {
            _moneyStorage.Add(cost);
        }

        public void ChangeHP(int addHP)
        {
            _hpStorage.AddHP(addHP);
        }
    }

    [Serializable]
    public struct RewardData
    {
        public string Name;

        public float Cost;
    }
}