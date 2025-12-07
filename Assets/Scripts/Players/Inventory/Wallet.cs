using UnityEngine;

namespace Players.Inventory
{
    public class Wallet : MonoBehaviour
    {
        private int _countCoins;

        public void AddCoin()
        {
            _countCoins++;
            Debug.Log($"Монета собрана. Количество монет = {_countCoins}");
        }
    }
}
