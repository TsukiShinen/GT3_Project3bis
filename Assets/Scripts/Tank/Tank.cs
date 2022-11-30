using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using ScriptableObjects.Team;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Tank : MonoBehaviour
{
    #region Variables

    [SerializeField] private GameObject tankMesh;
    [SerializeField] private GameObject completeShell;
    [SerializeField] private Transform shootSocket;

    [SerializeField] private SpriteRenderer icon;

    public GameObject tankExplosion;

    public TankParametersSO tankParametersSo;

    private Transform _transform;
    public float life;

    private float DistanceFromPositionToGo => Vector2.Distance(new Vector2(_positionToGo.x, _positionToGo.z), new Vector2(transform.position.x, transform.position.z));
    private bool ArrivedAtWaypoint => DistanceFromPositionToGo < 0.5f;
    public bool ArrivedAtDestination => ArrivedAtWaypoint && _waypoints.Count == 0;

    public bool isMoving;

    private Vector3 _positionToGo;
    private Queue<Vector3> _waypoints;

    public TeamSO team;
    public bool canShoot = true;

    public NavMeshAgent navMeshAgent;
    public Rigidbody tankRigidbody;
    public Vector3 target;

    public Slider slider;
    public Image fillImage;
    public Color fullHealthColor = Color.green;       // The color the health bar will be when on full health.
    public Color zeroHealthColor = Color.red;         // The color the health bar will be when on no health.

    public bool isDead;
    public Vector3 spawnPosition;
    public Quaternion spawnRotation;

    private GameManagerSO _gameManager;
    #endregion

    public void InitialLoad(TankParametersSO pTankParametersSo, TeamSO pTeam, GameManagerSO pGameManagerSo)
    {
        tankParametersSo = ScriptableObject.Instantiate(pTankParametersSo);
        life = pTankParametersSo.MaxLife;
        team = pTeam;
        _gameManager = pGameManagerSo;

        var renderers = tankMesh.GetComponentsInChildren<MeshRenderer>();

        foreach (var r in renderers)
        {
            r.material.color = team.TeamColor;
        }

        icon.color = team.TeamColor;

        _positionToGo = transform.position;
        _waypoints = new Queue<Vector3>();
        spawnPosition = _transform.position;
        spawnRotation = _transform.rotation;

        SetHealthUI();

    }

    private void Awake()
    {
        _transform = gameObject.transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        tankRigidbody = gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (isDead) return;

        if (target != Vector3.zero)
        {
            SetPath(new Queue<Vector3>(tankParametersSo.PathFinding.FindPath(_transform.position, target)));
            target = Vector3.zero;
        }

        if (!tankParametersSo.PathFinding) return;
        if (ArrivedAtWaypoint && _waypoints.Count > 0)
        {
            _positionToGo = _waypoints.Dequeue();
        }
        isMoving = false;
        MoveTo(_positionToGo);
    }

    public void SetPath(Queue<Vector3> lstWaypoint)
    {
        if (isDead) return;

        if (lstWaypoint == null) return;
        _waypoints = lstWaypoint;
        _positionToGo = _waypoints.Dequeue();
    }

    public void Move(float verticalDirection)
    {
        if (isDead) return;

        _transform.position += _transform.forward * (tankParametersSo.Speed * verticalDirection * Time.deltaTime);
    }

    public void MoveTo(Vector3 pTarget)
    {
        if (isDead) return;

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
        if (isDead) return;

        var transform1 = transform;
        transform1.position += transform1.forward * (tankParametersSo.Speed * Time.deltaTime);
        isMoving = true;
    }

    public void Turn(float angle)
    {
        if (isDead) return;

        transform.Rotate(new Vector3(0, (tankParametersSo.RotationSpeed * Mathf.Sign(angle)) * Time.deltaTime, 0));
    }

    public void Shoot()
    {
        if (isDead) return;
        if (!canShoot) return;
        StartCoroutine(ShootCoroutine());
    }

    private IEnumerator ShootCoroutine()
    {
        canShoot = false;

        var bullet = Instantiate(completeShell, shootSocket);

        bullet.transform.SetParent(null);

        var rbBullet = bullet.GetComponent<Rigidbody>();

        rbBullet.velocity = tankParametersSo.ProjectileSpeed * shootSocket.forward;

        yield return new WaitForSeconds(tankParametersSo.ShootCooldown);

        canShoot = true;
    }

    public void TakeDamage(float amount)
    {
        if (isDead) return;

        life -= amount;

        SetHealthUI();
    }

    public void SetHealthUI()
    {
        slider.value = life / tankParametersSo.MaxLife * 100;
        fillImage.color = Color.Lerp(zeroHealthColor, fullHealthColor, life / tankParametersSo.MaxLife);

        if (life <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        _gameManager.TankDeath(this);
    }
}
