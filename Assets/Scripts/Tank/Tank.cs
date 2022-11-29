using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Tilemaps;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Tank : MonoBehaviour
{
    [SerializeField] private GameObject tankMesh;
    [SerializeField] private GameObject completeShell;
    [SerializeField] private GameObject dustTrail;
    [SerializeField] private GameObject tankExplosion;
    [SerializeField] private Transform shootSocket;

    /* test pathfinding so */
    public Transform targetPosition;
    public Grid grid;
    /* ------------------- */

    public TankParametersSO TankParametersSO;

    private Transform _transform;
    private int _life;

    private float DistanceFromPositionToGo => Vector2.Distance(new Vector2(_positionToGo.x, _positionToGo.z), new Vector2(transform.position.x, transform.position.z));
    private bool ArrivedAtWaypoint => DistanceFromPositionToGo < 0.5f;
    public bool ArrivedAtDestination => ArrivedAtWaypoint && _waypoints.Count == 0;

    public bool isMoving = false;

    private Vector3 _positionToGo;
    private Queue<Vector3> _waypoints;

    
    public TeamSO team;

    public void InitialLoad(TankParametersSO pTankParametersSO)
    {
        TankParametersSO = pTankParametersSO;
        _life = pTankParametersSO.MaxLife;

        /* test pathfinding so */
        TankParametersSO.PathFinding.grid = grid;
        /* ------------------- */

        _positionToGo = transform.position;
        _waypoints = new Queue<Vector3>();
    }

    private void Awake()
    {
        _transform = gameObject.transform;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Shoot();
        }

        /* test pathfinding so */
        //SetPath(new Queue<Vector3>(_tankParametersSO.PathFinding.FindPath(_transform.position, targetPosition.position)));
        /* ------------------- */

        if (ArrivedAtWaypoint && _waypoints.Count > 0)
        {
            Debug.Log("test");
            _positionToGo = _waypoints.Dequeue();
        }
        isMoving = false;
        Move(_positionToGo);
    }

    public void SetPath(Queue<Vector3> lstWaypoint)
    {
        if (lstWaypoint == null) return;
        _waypoints = lstWaypoint;
        _positionToGo = _waypoints.Dequeue();
    }

    private void Move(Vector3 target)
    {
        if (DistanceFromPositionToGo < 0.1f) { return; }

        Vector2 targetDir = new Vector2(target.x, target.z) - new Vector2(transform.position.x, transform.position.z);

        var angle = Vector2.SignedAngle(targetDir, new Vector2(transform.forward.x, transform.forward.z));

        if (Mathf.Abs(angle) > 2f)
        {
            Turn(angle, targetDir);
        }
        else
        {
            MoveForward();
        }
    }

    private void MoveForward()
    {
        transform.position += transform.forward * TankParametersSO.Speed * Time.deltaTime;
        isMoving = true;
    }

    private void Turn(float angle, Vector2 targetDir)
    {
        transform.Rotate(new Vector3(0, (TankParametersSO.Speed * Mathf.Sign(angle)) * Time.deltaTime, 0));
        //if (Mathf.Abs(angle) < Mathf.Abs(Vector2.SignedAngle(targetDir, transform.up)))
        //{
        //    transform.up = targetDir;
        //}
    }

    public void Shoot()
    {
        var bullet = Instantiate(completeShell, shootSocket, true);
        //bullet.transform.position = Vector3.MoveTowards(bullet.transform.position, bullet.transform.position + bullet.transform.forward, 10 * Time.deltaTime);
    }

    public void Death()
    {
        StartCoroutine(DeathCoroutine());
    }

    private IEnumerator DeathCoroutine()
    {
        tankMesh.SetActive(false);
        Instantiate(tankExplosion, _transform);
        yield return new WaitForSeconds(TankParametersSO.RespawnTime);
    }
}
