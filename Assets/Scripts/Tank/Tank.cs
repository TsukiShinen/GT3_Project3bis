using System.Collections;
using System.Collections.Generic;
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

    public GameObject TankExplosion;

    public TankParametersSO TankParametersSO;

    private Transform _transform;
    public float Life;

    private float DistanceFromPositionToGo => Vector2.Distance(new Vector2(_positionToGo.x, _positionToGo.z), new Vector2(transform.position.x, transform.position.z));
    private bool ArrivedAtWaypoint => DistanceFromPositionToGo < 0.5f;
    public bool ArrivedAtDestination => ArrivedAtWaypoint && _waypoints.Count == 0;

    public bool isMoving = false;

    private Vector3 _positionToGo;
    private Queue<Vector3> _waypoints;

    public TeamSO team;
    public bool CanShoot = true;

    public NavMeshAgent navMeshAgent;
    public Rigidbody tankRigidbody;
    public Vector3 target;

    public Slider m_Slider;
    public Image m_FillImage;
    public Color m_FullHealthColor = Color.green;       // The color the health bar will be when on full health.
    public Color m_ZeroHealthColor = Color.red;         // The color the health bar will be when on no health.

    public bool IsDead = false;
    public Vector3 SpawnPosition;
    public Quaternion SpawnRotation;

    GameManagerSO gameManager;
    #endregion

    public void InitialLoad(TankParametersSO pTankParametersSO, TeamSO pteam, GameManagerSO pGameManagerSO)
    {
        TankParametersSO = ScriptableObject.Instantiate(pTankParametersSO);
        Life = pTankParametersSO.MaxLife;
        team = pteam;
        gameManager = pGameManagerSO;

        MeshRenderer[] renderers = tankMesh.GetComponentsInChildren<MeshRenderer>();

        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material.color = team.TeamColor;
        }

        icon.color = team.TeamColor;

        _positionToGo = transform.position;
        _waypoints = new Queue<Vector3>();
        SpawnPosition = _transform.position;
        SpawnRotation = _transform.rotation;

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
        if (IsDead) return;

        if (target != Vector3.zero)
        {
            SetPath(new Queue<Vector3>(TankParametersSO.PathFinding.FindPath(_transform.position, target)));
            target = Vector3.zero;
        }

        if (TankParametersSO.PathFinding == null) { return; }
        if (ArrivedAtWaypoint && _waypoints.Count > 0)
        {
            _positionToGo = _waypoints.Dequeue();
        }
        isMoving = false;
        MoveTo(_positionToGo);
    }

    public void SetPath(Queue<Vector3> lstWaypoint)
    {
        if (IsDead) return;

        if (lstWaypoint == null) return;
        _waypoints = lstWaypoint;
        _positionToGo = _waypoints.Dequeue();
    }

    public void Move(float verticalDirection)
    {
        if (IsDead) return;

        _transform.position += _transform.forward * TankParametersSO.Speed * verticalDirection * Time.deltaTime;
    }

    public void MoveTo(Vector3 target)
    {
        if (IsDead) return;

        if (DistanceFromPositionToGo < 0.1f) { return; }

        Vector2 targetDir = new Vector2(target.x, target.z) - new Vector2(transform.position.x, transform.position.z);

        var angle = Vector2.SignedAngle(targetDir, new Vector2(transform.forward.x, transform.forward.z));

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
        if (IsDead) return;

        transform.position += transform.forward * TankParametersSO.Speed * Time.deltaTime;
        isMoving = true;
    }

    public void Turn(float angle)
    {
        if (IsDead) return;

        transform.Rotate(new Vector3(0, (TankParametersSO.RotationSpeed * Mathf.Sign(angle)) * Time.deltaTime, 0));
    }

    public void Shoot()
    {
        if (IsDead) return;
        if (!CanShoot) return;
        StartCoroutine(ShootCoroutine());
    }

    private IEnumerator ShootCoroutine()
    {
        CanShoot = false;

        var bullet = Instantiate(completeShell, shootSocket);

        bullet.transform.SetParent(null);

        var rbBullet = bullet.GetComponent<Rigidbody>();

        rbBullet.velocity = TankParametersSO.ProjectileSpeed * shootSocket.forward;

        yield return new WaitForSeconds(TankParametersSO.ShootCooldown);

        CanShoot = true;
    }

    public void TakeDamage(float amount)
    {
        if (IsDead) return;

        Life -= amount;

        SetHealthUI();
    }

    public void SetHealthUI()
    {
        m_Slider.value = Life / TankParametersSO.MaxLife * 100;
        m_FillImage.color = Color.Lerp(m_ZeroHealthColor, m_FullHealthColor, Life / TankParametersSO.MaxLife);

        if (Life <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        gameManager.TankDeath(this);
    }
}
