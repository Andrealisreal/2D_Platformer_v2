using System;
using UnityEngine;

namespace Coins
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(CircleCollider2D))]
    public class Coin : MonoBehaviour
    {
        public event Action<Coin> Collected;
        
        public void Collect()
        {
            Collected?.Invoke(this);
        }
    }
}