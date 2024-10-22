﻿using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using ScriptableObjects.PathFinding;
using ScriptableObjects.Team;
using State_Machines;
using UnityEngine;
using UnityEngine.UI;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "GameManagerSO", menuName = "Game/GameManagerSO", order = 0)]
    public class GameManagerSO : ScriptableObject
    {
        public GameParametersSO gameParametersSo;
        public Dictionary<TeamSO, int> Scores;
        public float timer;

        public Tank tankPlayer;
        public Tank tankCamera;

        public List<Tank> tankToDeSpawn;

        public TeamSO team1;
        public TeamSO team2;

        [SerializeField] private GameObject tankPrefab;

        public Image shootCooldownImage;
        public Image specialJumpCooldownImage;

        public Vector3 CamPos;
        public Vector3 Tilt;

        public bool isPlayer;
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

            var shootImage = Instantiate(shootCooldownImage);
            var jumpImage = Instantiate(specialJumpCooldownImage);
            for (var i = 0; i < team1.TankList.Count; i++)
            {
                
                team1.TankList[i].PathFinding.grid = grid;
                var tankObject = Instantiate(tankPrefab, spawnTeam1.transform.position + spawnTeam1.transform.right * 3 * i, spawnTeam1.transform.rotation);
                tankObject.GetComponent<Tank>().InitialLoad(team1.TankList[i], team1, this);
                var camera = Camera.main;
                if (camera == null) continue;
                var cameraTransform = camera.transform;

                if (i == 0 )
                {
                    if (isPlayer)
                    {
                        tankPlayer = tankObject.GetComponent<Tank>();
                        tankPlayer.tankParametersSO.PathFinding = null;
                        tankPlayer.GetComponent<BehaviorTree>().enabled = false;
                    }
                    tankCamera = tankObject.GetComponent<Tank>();
                    cameraTransform.SetParent(tankObject.transform);
                    cameraTransform.localPosition = CamPos;
                    cameraTransform.localRotation = Quaternion.Euler(Tilt);

                }
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

        public void AddScore(TeamSO team, int amount)
        {
            Scores[team] += amount;
            if (Scores[team] >= gameParametersSo.maxScore)
                GameStateMachine.Instance.ChangeState(EGameState.END);
        }
    }
}