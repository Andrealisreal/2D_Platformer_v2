using System.Collections.Generic;
using UnityEngine;

namespace Medkits
{
    public class MedkitSpawner : MonoBehaviour
    {
        [SerializeField] private Medkit _prefab;
        [SerializeField] private List<Transform> _points;
        [SerializeField] private int _count = 5;

        private void Start()
        {
            Spawn();
        }
        
        private void Spawn()
        {
            for (var i = 0; i < _count; i++)
            {
                var randomIndex = Random.Range(0, _points.Count);
                var medkit = Instantiate(_prefab);
                medkit.transform.position = _points[randomIndex].position;
                _points.RemoveAt(randomIndex);
            }
        }
    }
}
