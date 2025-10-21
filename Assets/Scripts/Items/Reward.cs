using UnityEngine;

namespace Gameplay
{
    public class Reward : MonoBehaviour
    {
        public float Cost => _data.Cost;

        [SerializeField]
        private RewardData _data;

        public void Hide()
        {
            transform.gameObject.SetActive(false);
        }
    }
}