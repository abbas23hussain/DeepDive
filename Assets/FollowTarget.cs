using System;
using UnityEngine;

namespace Managers
{
    public class FollowTarget : MonoBehaviour
    {
        [SerializeField] private Transform target;
        private Vector3 followOffset;

        public void Awake()
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
            followOffset = transform.position - target.position;
        }

        private void LateUpdate()
        {
            var position = transform.position;
            transform.position = target.position + followOffset;
        }
    }
}