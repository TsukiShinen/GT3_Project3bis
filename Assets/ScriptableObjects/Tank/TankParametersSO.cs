using ScriptableObjects.PathFinding;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName ="New Tank Settings",menuName ="Others/TankParametersSO")]
    public class TankParametersSO : ScriptableObject
    {
        [SerializeField] private float maxLife;
        public float MaxLife => maxLife;

        [SerializeField] private float speed;
        public float Speed => speed;

        [SerializeField] private float rotationSpeed;
        public float RotationSpeed => rotationSpeed;

        [SerializeField] private float projectileSpeed;
        public float ProjectileSpeed => projectileSpeed;

        [SerializeField] private float shootCooldown;
        public float ShootCooldown => shootCooldown;

        [SerializeField] private float respawnTime;
        public float RespawnTime => respawnTime;

        [SerializeField] private PathFindingSO pathFinding;
        public PathFindingSO PathFinding { get => pathFinding; set => pathFinding = value; }

        [SerializeField] private GameObject tankWaifu;
        public GameObject TankWaifu => tankWaifu;

        /*
    Scale

    ProjectileType

    Methode de d�placement
    */
    }
}
