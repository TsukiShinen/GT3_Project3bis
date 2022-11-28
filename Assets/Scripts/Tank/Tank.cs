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
    [SerializeField] private Transform ShootSocket;

    private TankParametersSO _tankParametersSO;
    private Transform _transform;
    private int _life;
    private Queue<Vector3> _waypoints;
    private Vector3 _positionToGo;

    public void InitialLoad(TankParametersSO pTankParametersSO)
    {
        _tankParametersSO = pTankParametersSO;
        _life = pTankParametersSO.MaxLife;
    }

    private void Awake()
    {
        _transform = gameObject.transform;
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("UwU");
            Shoot();
        }
    }

    public void Move()
    {
        _transform.position += _transform.forward * _tankParametersSO.Speed * Time.deltaTime;
    }

    private void SetPath(Vector3 destination)
    {
        // Queue<Vector3> waypoints = Pathfinding(_transform.position, destination);
        // MoveTo(waypoints.Dequeue());
    }

    public void MoveTo(Vector3 destination)
    {
        // TODO
    }

    public void Rotate(float angle, Vector2 targetDir)
    {
        _transform.Rotate(new Vector3(0, 0, (_tankParametersSO.RotationSpeed * -Mathf.Sign(angle)) * Time.deltaTime));
        if (Mathf.Abs(angle) < Mathf.Abs(Vector2.SignedAngle(targetDir, _transform.up)))
        {
            _transform.up = targetDir;
        }
    }

    public void Shoot()
    {
        var bullet = Instantiate(completeShell, ShootSocket,true);
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
