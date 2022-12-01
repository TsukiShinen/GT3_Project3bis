using Complete;
using ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UIElements;
using System;
using System.Runtime.CompilerServices;

public class TankMovement : MonoBehaviour
{
    #region Variables

    [SerializeField] private Tank tank;
    
    private NavMeshAgent _navMeshAgent;
    private Vector3 _positionToGo;
    private Queue<Vector3> _waypoints;

    public bool mustTurn;
    private Transform _targetToAimAt;

    public bool ArrivedAtWaypoint => DistanceFromPositionToGo < 0.5f;

    public bool ArrivedAtDestination => ArrivedAtWaypoint && _waypoints.Count == 0;

    private float DistanceFromPositionToGo => Vector2.Distance(new Vector2(_positionToGo.x, _positionToGo.z), new Vector2(transform.position.x, transform.position.z));

    #endregion

    public void Init()
    {
        _positionToGo = transform.position;
        _waypoints = new Queue<Vector3>();
    }

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (tank.isDead) return;
        if (!tank.tankParametersSO.PathFinding) return;
        if (ArrivedAtWaypoint && _waypoints.Count > 0)
        {
            _positionToGo = _waypoints.Dequeue();
        }
        MoveTo(_positionToGo);

        if (mustTurn)
            TurnTo(_targetToAimAt);
    }

    public void Move(float verticalDirection)
    {
        if (tank.isDead) return;

        transform.position += transform.forward * (tank.tankParametersSO.Speed * verticalDirection * Time.deltaTime);
    }

    #region IA Movement

    public void GeneratePath(Vector3 targetPosition)
    {
        SetPath(new Queue<Vector3>(tank.tankParametersSO.PathFinding.FindPath(transform.position, targetPosition, _navMeshAgent)));
    }

    public void SetPath(Queue<Vector3> lstWaypoint)
    {
        if (tank.isDead) return;
        if (lstWaypoint.Count == 0) return;
        if (lstWaypoint == null) return;
        _waypoints = lstWaypoint;
        _positionToGo = _waypoints.Dequeue();
    }

    public void MoveTo(Vector3 pTarget)
    {
        if (tank.isDead) return;

        if (DistanceFromPositionToGo < 0.1f) { return; }

        var transform1 = transform;
        var position = transform1.position;
        var targetDir = new Vector2(pTarget.x, pTarget.z) - new Vector2(position.x, position.z);

        var forward = transform1.forward;
        var angle = Vector2.SignedAngle(targetDir, new Vector2(forward.x, forward.z));

        if (Mathf.Abs(angle) > 2f)
        {
            Turn(angle);
        }
        else
        {
            MoveForward();
        }
    }

    private void MoveForward()
    {
        if (tank.isDead) return;

        var transform1 = transform;
        transform1.position += transform1.forward * (tank.tankParametersSO.Speed * Time.deltaTime);
    }

    public void Turn(float angle)
    {
        if (tank.isDead) return;

        transform.Rotate(new Vector3(0, (tank.tankParametersSO.RotationSpeed * Mathf.Sign(angle)) * Time.deltaTime, 0));
    }

    public void BeginTurnTo(Transform target)
    {
        mustTurn= true;
        _targetToAimAt= target;
    }

    private void TurnTo(Transform target)
    {
        if (tank.isDead) return;

        var direction = target.position - transform.position;

        var angle = Vector2.SignedAngle(direction, new Vector2(transform.forward.x, transform.forward.z));

        Turn(angle);

        if(Mathf.Approximately(angle, 0))
            mustTurn = false;
    }

    #endregion
}
