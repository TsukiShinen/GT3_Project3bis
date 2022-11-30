using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameState", menuName = "GameState/Gameplay")]
public class GameplayGameState : BaseGameState
{
    #region fileds
    [SerializeField] private GameManagerSO gameManagerSO;
    #endregion

    #region Properties

    #endregion

    #region Methods   
    public override void StartState()
    {
        gameManagerSO.Init();
    }

    public override void UpdateState()
    {
        gameManagerSO.timer -= Time.deltaTime;

        foreach(Tank tank in gameManagerSO.tankToDespawn)
        {
            _machine.StartCoroutine(tankToDespawn(tank));
        }
        gameManagerSO.tankToDespawn = new List<Tank>();
    }

    public IEnumerator tankToDespawn(Tank tank)
    {
        if(tank == gameManagerSO.tankPlayer)
        {
            Camera.main.transform.SetParent(null);
        }
        tank.IsDead = true;
        tank.gameObject.SetActive(false);

        Instantiate(tank.TankExplosion, tank.transform);

        yield return new WaitForSeconds(tank.TankParametersSO.RespawnTime);

        tank.gameObject.SetActive(true);
        tank.transform.position = tank.SpawnPosition;
        tank.transform.rotation = tank.SpawnRotation;

        tank.Life = tank.TankParametersSO.MaxLife;
        tank.SetHealthUI();

        tank.CanShoot = true;
        tank.IsDead = false;
        if (tank == gameManagerSO.tankPlayer)
        {
            Camera.main.transform.SetParent(tank.transform);
            Camera.main.transform.localPosition = gameManagerSO.CamPos;
            Camera.main.transform.localRotation = Quaternion.Euler(gameManagerSO.Tilt);
        }
    }

    public override void FixedUpdateState()
    {

    }
    public override void LeaveState()
    {
        _machine.LastGState = EGameState.START;
    }

    #endregion
}