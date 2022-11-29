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
    private bool _canShoot = true;


    public void InitialLoad(TankParametersSO pTankParametersSO)
    {
        TankParametersSO = pTankParametersSO;
        _life = pTankParametersSO.MaxLife;

        _positionToGo = transform.position;
        _waypoints = new Queue<Vector3>();
    }

    private void Awake()
    {
        _transform = gameObject.transform;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _canShoot)
        {
            _canShoot = false;
            StartCoroutine(Shoot());
        }

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
    }

    public IEnumerator Shoot()
    {
        var bullet = Instantiate(completeShell, shootSocket);

        bullet.transform.SetParent(null);

        var rbBullet = bullet.GetComponent<Rigidbody>();

        rbBullet.velocity = TankParametersSO.ProjectileSpeed * shootSocket.forward;

        yield return new WaitForSeconds(TankParametersSO.ShootCooldown);

        _canShoot = true;
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
