using System.Collections;
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
    }

    public IEnumerator tankToDespawn(Tank tank)
    {
        tank.IsDead = true;
        tank.gameObject.SetActive(false);

        Instantiate(tank.TankExplosion, tank.transform);

        yield return new WaitForSeconds(tank.TankParametersSO.RespawnTime);

        tank.gameObject.SetActive(true);
        tank.transform.position = tank.Spawn;

        tank.Life = tank.TankParametersSO.MaxLife;
        tank.SetHealthUI();

        tank.IsDead = false;
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