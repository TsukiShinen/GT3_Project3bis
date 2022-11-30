using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class Tank : MonoBehaviour
{
    #region Variables

    [SerializeField] private GameObject tankMesh;
    [SerializeField] private GameObject completeShell;
    [SerializeField] private GameObject dustTrail;
    [SerializeField] private GameObject tankExplosion;
    [SerializeField] private Transform shootSocket;

    public TankParametersSO TankParametersSO;

    private Transform _transform;
    private float _life;

    private float DistanceFromPositionToGo => Vector2.Distance(new Vector2(_positionToGo.x, _positionToGo.z), new Vector2(transform.position.x, transform.position.z));
    private bool ArrivedAtWaypoint => DistanceFromPositionToGo < 0.5f;
    public bool ArrivedAtDestination => ArrivedAtWaypoint && _waypoints.Count == 0;

    public bool isMoving = false;

    private Vector3 _positionToGo;
    private Queue<Vector3> _waypoints;

    public TeamSO team;
    private bool _canShoot = true;

    public NavMeshAgent navMeshAgent;

    public Vector3 target;

    public Slider m_Slider;
    public Image m_FillImage;
    public Color m_FullHealthColor = Color.green;       // The color the health bar will be when on full health.
    public Color m_ZeroHealthColor = Color.red;         // The color the health bar will be when on no health.
    #endregion

    public void InitialLoad(TankParametersSO pTankParametersSO, TeamSO pteam)
    {
        TankParametersSO = ScriptableObject.Instantiate(pTankParametersSO);
        _life = pTankParametersSO.MaxLife;
        team = pteam;

        MeshRenderer[] renderers = tankMesh.GetComponentsInChildren<MeshRenderer>();

        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material.color = team.TeamColor;
        }

        _positionToGo = transform.position;
        _waypoints = new Queue<Vector3>();

        SetHealthUI();

    }

    private void Awake()
    {
        _transform = gameObject.transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public void TakeDamage(float amount)
    {
        _life -= amount;

        SetHealthUI();
    }


    private void SetHealthUI()
    {
        m_Slider.value = _life/TankParametersSO.MaxLife * 100;
        m_FillImage.color = Color.Lerp(m_ZeroHealthColor, m_FullHealthColor, _life / TankParametersSO.MaxLife);
    }

    private void Update()
    {
        if(target != Vector3.zero)
        {
            SetPath(new Queue<Vector3>(TankParametersSO.PathFinding.FindPath(_transform.position, target)));
            target = Vector3.zero;
        }

        if(TankParametersSO.PathFinding == null) { return; }
        if (ArrivedAtWaypoint && _waypoints.Count > 0)
        {
            _positionToGo = _waypoints.Dequeue();
        }
        isMoving = false;
        MoveTo(_positionToGo);
    }

    public void SetPath(Queue<Vector3> lstWaypoint)
    {
        if (lstWaypoint == null) return;
        _waypoints = lstWaypoint;
        _positionToGo = _waypoints.Dequeue();
    }

    public void Move(float verticalDirection)
    {
        transform.position += _transform.forward * TankParametersSO.Speed * verticalDirection * Time.deltaTime;    
    }

    public void MoveTo(Vector3 target)
    {
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
        transform.position += transform.forward * TankParametersSO.Speed * Time.deltaTime;
        isMoving = true;
    }

    public void Turn(float angle)
    {
        transform.Rotate(new Vector3(0, (TankParametersSO.RotationSpeed * Mathf.Sign(angle)) * Time.deltaTime, 0));
    }

    public void Shoot()
    {
        if (!_canShoot) return;
        StartCoroutine(ShootCoroutine());
    }

    private IEnumerator ShootCoroutine()
    {
        _canShoot = false;

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
