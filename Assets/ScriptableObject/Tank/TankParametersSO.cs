using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName ="New Tank Settings",menuName ="Others/TankParametersSO")]
public class TankParametersSO : ScriptableObject
{
    [SerializeField] private float maxLife;
    public float MaxLife { get => maxLife; }

    [SerializeField] private float speed;
    public float Speed { get => speed; }

    [SerializeField] private float rotationSpeed;
    public float RotationSpeed { get => rotationSpeed; }

    [SerializeField] private float projectileSpeed;
    public float ProjectileSpeed { get => projectileSpeed; }

    [SerializeField] private float shootCooldown;
    public float ShootCooldown { get => shootCooldown; }

    [SerializeField] private float respawnTime;
    public float RespawnTime { get => respawnTime; }

    [SerializeField] private PathFindingSO pathFinding;
    public PathFindingSO PathFinding { get => pathFinding; set => pathFinding = value; }

    [SerializeField] private GameObject tankWaifu;
    public GameObject TankWaifu { get => tankWaifu; }

    /*
    Scale

    ProjectileType

    Methode de déplacement
    */
}
