using ScriptableObjects.PathFinding;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName ="New Tank Settings",menuName ="Others/TankParametersSO")]
    public class TankParametersSO : ScriptableObject
    {
        [SerializeField] private float maxLife;
        public float MaxLife { get => maxLife; set => maxLife = value; }

        [SerializeField] private float speed;
        public float Speed { get => speed; set => speed = value; }

        [SerializeField] private float rotationSpeed;
        public float RotationSpeed { get => rotationSpeed; set => rotationSpeed = value; }

        [SerializeField] private float projectileSpeed;
        public float ProjectileSpeed => projectileSpeed;

        [SerializeField] private float shootCooldown;
        public float ShootCooldown { get => shootCooldown; set => shootCooldown = value; }

        [SerializeField] private float respawnTime;
        public float RespawnTime => respawnTime;

        [SerializeField] private PathFindingSO pathFinding;
        public PathFindingSO PathFinding { get => pathFinding; set => pathFinding = value; }

        [SerializeField] private AnimationCurve specialJump;
        public AnimationCurve SpecialJump => specialJump;

        [SerializeField] private float specialJumpCooldown;
        public float SpecialJumpCooldown => specialJumpCooldown;

        [SerializeField] private float detectionRadius;
        public float DetectionRadius => detectionRadius;

        [SerializeField] private float minSize;
        public float MinSize => minSize;

        [SerializeField] private float maxSize;
        public float MaxSize => maxSize;

        /*
    Scale

    ProjectileType

    Methode de d�placement
    */
    }
}
