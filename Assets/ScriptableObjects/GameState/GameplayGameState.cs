using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using State_Machines;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ScriptableObjects.GameState
{
    [CreateAssetMenu(fileName = "GameState", menuName = "GameState/Gameplay")]
    public class GameplayGameState : BaseGameState
    {
        #region Fields
        [SerializeField] private GameManagerSO gameManagerSo;
        [SerializeField] private GameParametersSO gameParametersSo;


        private Camera _mainCamera;
        #endregion

        #region Properties

        #endregion

        #region Methods   
        public override void StartState()
        {
            Machine.StartCoroutine(Initialise());
            Machine.StartCoroutine(Timer());
        }

        private IEnumerator Initialise()
        {
            yield return SceneManager.LoadSceneAsync("ScenePropreWola");
            
            _mainCamera = Camera.main;
            gameManagerSo.Init();
        }

        private IEnumerator Timer()
        {
            yield return new WaitForSecondsRealtime(gameParametersSo.gameTimer);
            
            Machine.ChangeState(EGameState.END);
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
            if(tank == gameManagerSo.tankCamera)
            {
                if (_mainCamera) 
                    _mainCamera.transform.SetParent(null);
            }
            tank.isDead = true;

            var explosion = Instantiate(tank.tankExplosion, tank.transform);
            explosion.transform.SetParent(null);
            Destroy(explosion, 2);

            tank.gameObject.SetActive(false);

            yield return new WaitForSeconds(tank.tankParametersSO.RespawnTime);

            tank.gameObject.SetActive(true);
            var tankTransform = tank.transform;
            tankTransform.position = tank.spawnPosition;
            tankTransform.rotation = tank.spawnRotation;

            tank.life = tank.tankParametersSO.MaxLife;
            tank.tankMesh.transform.rotation = Quaternion.Euler(0, 0, 0);
            tank.SetHealthUI();

            tank.canShoot = true;
            tank.isDead = false;
            if (tank != gameManagerSo.tankCamera) yield break;
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