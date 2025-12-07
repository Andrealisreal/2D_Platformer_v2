using Enemies.Movement;
using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CapsuleCollider2D))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Patroller))]
    public class Enemy : MonoBehaviour
    {
        private Patroller _patroller;
        
        private void  Awake()
        {
            _patroller = GetComponent<Patroller>();
        }

        private void FixedUpdate()
        {
            _patroller.Patrol();
        }
    }
}
