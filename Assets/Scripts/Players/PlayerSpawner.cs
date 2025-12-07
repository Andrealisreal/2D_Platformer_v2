using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

namespace Players
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private Player _prefab;
        [SerializeField] private Transform _point;
        [SerializeField] private float _delay = 4f;
        [SerializeField] private CinemachineCamera _virtualCamera;
        
        private WaitForSeconds _wait;

        private void Awake()
        {
            _wait = new WaitForSeconds(_delay);
        }
        
        private void Start()
        {
            StartCoroutine(IntervalSpawning());
        }

        private IEnumerator IntervalSpawning()
        {
            yield return _wait;

            var player = Instantiate(_prefab);
            player.transform.position = _point.position;
            _virtualCamera.Follow = player.transform;
        }
    }
}
