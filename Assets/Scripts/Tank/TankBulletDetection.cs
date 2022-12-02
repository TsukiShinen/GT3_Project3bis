using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class TankBulletDetection : MonoBehaviour
    {
        [SerializeField] private Tank tank;

        public bool bulletEntered;

        private void OnDisable()
        {
            bulletEntered = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Bullet")) return;

            StartCoroutine(DetectBullet());
        }

        private IEnumerator DetectBullet()
        {
            bulletEntered = true;
            yield return null;
            bulletEntered = false;
        }
    }
}