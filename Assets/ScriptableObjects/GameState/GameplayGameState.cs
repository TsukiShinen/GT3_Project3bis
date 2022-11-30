using System.Collections;
using System.Collections.Generic;
using State_Machines;
using UnityEngine;

namespace ScriptableObjects.GameState
{
    [CreateAssetMenu(fileName = "GameState", menuName = "GameState/Gameplay")]
    public class GameplayGameState : BaseGameState
    {
        #region Fields
        [SerializeField] private GameManagerSO gameManagerSo;

        private Camera _mainCamera;
        #endregion

        #region Properties

        #endregion

        #region Methods   
        public override void StartState()
        {
            _mainCamera = Camera.main;
            gameManagerSo.Init();
        }

        public override void UpdateState()
        {
            gameManagerSo.timer -= Time.deltaTime;

            foreach(var tank in gameManagerSo.tankToDeSpawn)
            {
                Machine.StartCoroutine(TankToDeSpawn(tank));
            }
            gameManagerSo.tankToDeSpawn = new List<Tank>();
        }

        private IEnumerator TankToDeSpawn(Tank tank)
        {
            if(tank == gameManagerSo.tankPlayer)
            {
                if (_mainCamera) 
                    _mainCamera.transform.SetParent(null);
            }
            tank.isDead = true;
            tank.gameObject.SetActive(false);

            Instantiate(tank.tankExplosion, tank.transform);

            yield return new WaitForSeconds(tank.tankParametersSo.RespawnTime);

            tank.gameObject.SetActive(true);
            var tankTransform = tank.transform;
            tankTransform.position = tank.spawnPosition;
            tankTransform.rotation = tank.spawnRotation;

            tank.life = tank.tankParametersSo.MaxLife;
            tank.SetHealthUI();

            tank.canShoot = true;
            tank.isDead = false;
            if (tank != gameManagerSo.tankPlayer) yield break;
            var cameraTransform = _mainCamera.transform;
            cameraTransform.SetParent(tank.transform);
            cameraTransform.localPosition = gameManagerSo.CamPos;
            _mainCamera.transform.localRotation = Quaternion.Euler(gameManagerSo.Tilt);
        }

        public override void FixedUpdateState()
        {

        }
        public override void LeaveState()
        {
            Machine.LastGState = EGameState.START;
        }

        #endregion
    }
}