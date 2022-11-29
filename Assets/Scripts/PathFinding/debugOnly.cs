using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class debugOnly : MonoBehaviour
{
    public Tank tank;
    public TankParametersSO tankparameter;
    void Awake()
    {
        tank.InitialLoad(tankparameter);
        tank.SetPath(new Queue<Vector3>(tank.TankParametersSO.PathFinding.FindPath(tank.transform.position, tank.targetPosition.position)));
    }
}
