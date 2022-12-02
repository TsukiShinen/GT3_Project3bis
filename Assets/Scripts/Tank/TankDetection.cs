using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankDetection : MonoBehaviour
{
    [SerializeField] private Tank tank;
    [SerializeField] private SphereCollider detectionZone;

    public List<Tank> tanksInRange;

    public void Init()
    {
        detectionZone.radius = tank.tankParametersSO.DetectionRadius;
    }

    private void OnDisable()
    {
        tanksInRange.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Tank")) return;

        var t = other.GetComponentInParent<Tank>();

        if (t.team == tank.team) return;

        tanksInRange.Add(t);

        t.OnDeath += RemoveTankFromList;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Tank")) return;

        var t = other.GetComponentInParent<Tank>();

        RemoveTankFromList(t);

        t.OnDeath -= RemoveTankFromList;
    }

    private void RemoveTankFromList(Tank pTank)
    {
        if (pTank.team == tank.team) return;

        tanksInRange.Remove(pTank);
    }
}
