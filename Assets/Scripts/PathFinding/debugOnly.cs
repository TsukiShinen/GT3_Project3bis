using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class debugOnly : MonoBehaviour
{
    public Tank tank;
    public TankParametersSO tankparameter;
    void Awake()
    {
        tank.InitialLoad(tankparameter);
    }
}
