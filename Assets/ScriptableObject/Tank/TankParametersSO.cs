using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Tank Settings",menuName ="Others/TankParametersSO")]
public class TankParametersSO : ScriptableObject
{
    [SerializeField] private int maxLife;
    public int MaxLife { get => maxLife; }

    [SerializeField] private float speed;
    public float Speed { get => speed; }

    [SerializeField] private float rotationSpeed;
    public float RotationSpeed { get => rotationSpeed; }

    [SerializeField] private float shootCooldown;
    public float ShootCooldown { get => shootCooldown; }

    [SerializeField] private float respawnTime;
    public float RespawnTime { get => respawnTime; }

    [SerializeField] private PathFindingSO pathFinding;
    public PathFindingSO PathFinding { get => pathFinding; }

    /*
    Scale

    ProjectileType

    Methode de déplacement
    */
}
