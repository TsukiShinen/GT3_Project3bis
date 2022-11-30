using System.Collections.Generic;
using ScriptableObjects.Team;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "GameManagerSO", menuName = "Game/GameManagerSO", order = 0)]
    public class GameManagerSO : ScriptableObject
    {
        public GameParametersSO gameParametersSo;
        public Dictionary<TeamSO, int> Scores;
        public float timer;

        public Tank tankPlayer;

        public List<Tank> tankToDeSpawn;

        public TeamSO team1;
        public TeamSO team2;

        [SerializeField] private GameObject tankPrefab;
        public Vector3 CamPos;
        public Vector3 Tilt;

        public void Init()
        {
            timer = gameParametersSo.gameTimer;
            Scores = new Dictionary<TeamSO, int>
            {
                { team1, 0 },
                { team2, 0 }
            };

            var grid = GameObject.FindGameObjectWithTag("Grid").GetComponent<Grid>();
            var spawnTeam1 = GameObject.FindGameObjectWithTag("SpawnTeam1");
            var spawnTeam2 = GameObject.FindGameObjectWithTag("SpawnTeam2");

            for (var i = 0; i < team1.TankList.Count; i++)
            {
                team1.TankList[i].PathFinding.grid = grid;
                var tankObject = Instantiate(tankPrefab, spawnTeam1.transform.position + spawnTeam1.transform.right * 3 * i, spawnTeam1.transform.rotation);
                tankObject.GetComponent<Tank>().InitialLoad(team1.TankList[i], team1, this);

                Debug.Log(tankObject.name);

                if (i > 0) continue;
                tankPlayer = tankObject.GetComponent<Tank>();
                tankPlayer.tankParametersSO.PathFinding = null;
                var camera = Camera.main;
                if (camera == null) continue;
                var cameraTransform = camera.transform;
                cameraTransform.SetParent(tankObject.transform);
                cameraTransform.localPosition = CamPos;
                cameraTransform.localRotation = Quaternion.Euler(Tilt);

            }

            for (var i = 0; i < team2.TankList.Count; i++)
            {
                team2.TankList[i].PathFinding.grid = grid;
                var tankObject = Instantiate(tankPrefab, spawnTeam2.transform.position + spawnTeam2.transform.right * 3 * i, spawnTeam2.transform.rotation);
                tankObject.GetComponent<Tank>().InitialLoad(team2.TankList[i], team2, this);
            }
        }

        public void TankDeath(Tank tank)
        {
            tankToDeSpawn.Add(tank);
        }
    }
}