using System;
using UI;
using UnityEngine;

namespace Gameplay
{
    public class PlayerInfoController : MonoBehaviour
    {
        public event Action OnHPEmpty;

        [SerializeField]
        private float _initReward = 0;

        [SerializeField]
        private int _initHP = 50;

        [SerializeField]
        private int _maxHP = 100;

        [SerializeField]
        private RewardView _moneyView;

        [SerializeField]
        private HPView _hpView;

        private RewardStorage _rewardStorage;

        private RewardController _moneyController;

        private HPStorage _hpStorage;

        private HPController _hpController;

        private void OnEnable()
        {
            _rewardStorage = new RewardStorage(_initReward);

            _moneyController = new RewardController(_rewardStorage, _moneyView);

            _hpStorage = new HPStorage(_initHP, _maxHP);

            _hpController = new HPController(_hpStorage, _hpView);
        }

        public void AddReward(float cost)
        {
            _rewardStorage.Add(cost);
        }

        public void ChangeHP(int addHP)
        {
            _hpStorage.AddHP(addHP);

            if (_hpStorage.HP == 0)
            {
                OnHPEmpty?.Invoke();
            }
        }

        public void ResetLevel()
        {
            _rewardStorage.ResetLevel();
        }

        public void ResetGame()
        {
            _rewardStorage.Set(_initReward);
            _hpStorage.SetHP(_initHP);
        }
    }

    [Serializable]
    public struct RewardData
    {
        public string Name;

        public float Cost;
    }
}