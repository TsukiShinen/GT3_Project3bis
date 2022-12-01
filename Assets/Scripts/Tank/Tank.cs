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

    [SerializeField] private SpriteRenderer icon;
    [SerializeField] private Slider slider;
    [SerializeField] private Image fillImage;
    [SerializeField] private Color fullHealthColor = Color.green;
    [SerializeField] private Color zeroHealthColor = Color.red;

    public TankMovement tankMovement;
    public TankActions tankActions;
    public TankDetection tankDetection;
    private GameManagerSO _gameManager;
    public TankParametersSO tankParametersSO;
    public AudioSO audioSO;

    public Rigidbody tankRigidbody;
    public GameObject tankMesh;
    public GameObject tankExplosion;
    public TeamSO team;
    public float life;
    public Vector3 spawnPosition;
    public Quaternion spawnRotation;

    public bool canShoot = true;
    public bool canJump = true;
    public bool isJumping = false;
    public bool isDead;

    public delegate void EventDeath(Tank tank);
    public EventDeath OnDeath;

    #endregion

    public void InitialLoad(TankParametersSO pTankParametersSo, TeamSO pTeam, GameManagerSO pGameManagerSo)
    {
        tankParametersSO = Instantiate(pTankParametersSo);
        life = pTankParametersSo.MaxLife;
        team = pTeam;
        _gameManager = pGameManagerSo;

        var renderers = tankMesh.GetComponentsInChildren<MeshRenderer>();
        foreach (var r in renderers) { r.material.color = team.TeamColor; }
        icon.color = team.TeamColor;

        spawnPosition = transform.position;
        spawnRotation = transform.rotation;

        tankMovement.Init();
        tankActions.Init();
        tankDetection.Init();

        SetHealthUI();
    }

    private void Awake()
    {
        tankRigidbody = gameObject.GetComponent<Rigidbody>();
        audioSO.Play("tank_moving");
    }

    public void TakeDamage(float amount)
    {
        if (isDead) return;

        life -= amount;

        SetHealthUI();
    }

    public void SetHealthUI()
    {
        slider.value = life / tankParametersSO.MaxLife * 100;
        fillImage.color = Color.Lerp(zeroHealthColor, fullHealthColor, life / tankParametersSO.MaxLife);

        if (life <= 0)
            Death();
    }

    public void Death()
    {
        OnDeath?.Invoke(this);
        _gameManager.TankDeath(this);
        audioSO.PlaySFX("tank_explode");
    }
}
