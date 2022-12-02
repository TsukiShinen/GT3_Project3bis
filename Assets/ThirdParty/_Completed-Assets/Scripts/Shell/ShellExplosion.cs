using UnityEngine;

namespace Complete
{
    public class ShellExplosion : MonoBehaviour
    {
        public LayerMask m_TankMask;
        public ParticleSystem m_ExplosionParticles;
        public AudioSource m_ExplosionAudio;
        public float m_MaxDamage = 100f;
        public float m_ExplosionForce = 1000f;
        public float m_MaxLifeTime = 2f;
        public float m_ExplosionRadius = 5f;


        private void Start ()
        {
            Destroy (gameObject, m_MaxLifeTime);
        }

        private void OnTriggerEnter (Collider other)
        {
            if (!other.CompareTag("Tank") && !other.CompareTag("Obstacle")) return;
            Collider[] colliders = Physics.OverlapSphere (transform.position, m_ExplosionRadius, m_TankMask);

            for (int i = 0; i < colliders.Length; i++)
            {
                Tank targetHealth = colliders[i].gameObject.GetComponentInParent<Tank>();

                if (!targetHealth)
                    continue;

                float damage = CalculateDamage (colliders[i].gameObject.transform.position);

                targetHealth.TakeDamage(damage);
            }

            m_ExplosionParticles.transform.parent = null;

            m_ExplosionParticles.Play();

            m_ExplosionAudio.Play();

            ParticleSystem.MainModule mainModule = m_ExplosionParticles.main;
            Destroy (m_ExplosionParticles.gameObject, mainModule.duration);

            Destroy (gameObject);
        }


        private float CalculateDamage (Vector3 targetPosition)
        {
            Vector3 explosionToTarget = targetPosition - transform.position;

            float explosionDistance = explosionToTarget.magnitude;

            float relativeDistance = (m_ExplosionRadius - explosionDistance) / m_ExplosionRadius;

            float damage = relativeDistance * m_MaxDamage;

            damage = Mathf.Max (0f, damage);

            return damage;
        }
    }
}