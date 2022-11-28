using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Tilemaps;
using UnityEngine;

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

    private TankParametersSO _tankParametersSO;
    private Transform _transform;
    private int _life;
    private Queue<Vector3> _waypoints;
    private Vector3 _positionToGo;

    public void InitialLoad(TankParametersSO pTankParametersSO)
    {
        _tankParametersSO = pTankParametersSO;
        _life = pTankParametersSO.MaxLife;

        /* test pathfinding so */
        _tankParametersSO.PathFinding.grid = grid;
        /* ------------------- */
    }

    private void Awake()
    {
        _transform = gameObject.transform;
    }

    private void Start()
    {
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Shoot();
        }
        /* test pathfinding so */
        SetPath(new Queue<Vector3>(_tankParametersSO.PathFinding.FindPath(_transform.position, targetPosition.position)));
        /* ------------------- */
    }

    public void SetPath(Queue<Vector3> lstWaypoint)
    {
    }

    private void Move(Vector3 target)
    {
    }

    private void MoveForward()
    {
    }

    private void Turn(float angle, Vector2 targetDir)
    {
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
        yield return new WaitForSeconds(_tankParametersSO.RespawnTime);
    }
}
