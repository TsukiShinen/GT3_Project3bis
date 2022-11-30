using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

[CreateAssetMenu(fileName = "GameManagerSO", menuName = "Game/GameManagerSO", order = 0)]
public class GameManagerSO : ScriptableObject
{
    public GameParametersSO gameParametersSO;
    public Dictionary<TeamSO, int> Scores;
    public float timer;

    public Tank tankPlayer;

    public List<Tank> tankToDespawn;

    public TeamSO team1;
    public TeamSO team2;

    [SerializeField] private GameObject tankPrefab;
    public Vector3 CamPos;
    public Vector3 Tilt;

    public bool TimerFished => timer <= 0;

    public void Init()
    {
        timer = gameParametersSO.gameTimer;
        Scores = new Dictionary<TeamSO, int>
        {
            { team1, 0 },
            { team2, 0 }
        };

        Grid grid = GameObject.FindGameObjectWithTag("Grid").GetComponent<Grid>();
        GameObject spawnTeam1 = GameObject.FindGameObjectWithTag("SpawnTeam1");
        GameObject spawnTeam2 = GameObject.FindGameObjectWithTag("SpawnTeam2");

        for (int i = 0; i < team1.TankList.Count; i++)
        {
            team1.TankList[i].PathFinding.grid = grid;
            var tankObject = Instantiate(tankPrefab, spawnTeam1.transform.position + spawnTeam1.transform.right * 3 * i, spawnTeam1.transform.rotation);
            tankObject.GetComponent<Tank>().InitialLoad(team1.TankList[i], team1, this);

            if(i > 0) continue;
            tankPlayer = tankObject.GetComponent<Tank>();
            tankPlayer.TankParametersSO.PathFinding = null;
            var _cam = Camera.main;
            _cam.transform.SetParent(tankObject.transform);
            _cam.transform.localPosition = CamPos;
            _cam.transform.localRotation = Quaternion.Euler(Tilt);
        }

        for (int i = 0; i < team2.TankList.Count; i++)
        {
            team2.TankList[i].PathFinding.grid = grid;
            var tankObject = Instantiate(tankPrefab, spawnTeam2.transform.position + spawnTeam2.transform.right * 3 * i, spawnTeam2.transform.rotation);
            tankObject.GetComponent<Tank>().InitialLoad(team2.TankList[i], team2, this);
        }
    }

    public void TankDeath(Tank tank)
    {
        tankToDespawn.Add(tank);
    }
}