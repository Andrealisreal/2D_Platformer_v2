using System.Collections;
using UnityEngine;

namespace Coins
{
    public class CoinSpawner : MonoBehaviour
    {
        [SerializeField] private Coin _prefab;
        [SerializeField] private Transform[] _points;
        [SerializeField] private float _delaySpawn = 2f;
        
        private WaitForSeconds _wait;

        private void Awake()
        {
            _wait = new WaitForSeconds(_delaySpawn);
        }
        
        private void Start()
        {
            StartCoroutine(IntervalSpawning());
        }

        private IEnumerator IntervalSpawning()
        {
            yield return _wait;

            var coin = Instantiate(_prefab);
            coin.transform.position = _points[Random.Range(0, _points.Length)].position;
            coin.Collected += OnCoinCollected;
        }

        private void OnCoinCollected(Coin coin)
        {
            StartCoroutine(IntervalSpawning());
            coin.Collected -= OnCoinCollected;
            Destroy(coin.gameObject);
        }
    }
}
