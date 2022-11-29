using System.Collections.Generic;
using DefaultNamespace;
using Library.Command;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

[CreateAssetMenu(fileName = "GameManagerSO", menuName = "Game/GameManagerSO", order = 0)]
public class GameManagerSO : ScriptableObject
{
    public GameParametersSO gameParametersSO;
    public Dictionary<TeamSO, int> Scores;
    public float timer;

    [SerializeField] private TeamSO team1;
    [SerializeField] private TeamSO team2;

    public bool TimerFished => timer <= 0;

    public void Init()
    {
        timer = gameParametersSO.gameTimer;
        Scores = new Dictionary<TeamSO, int>
        {
            { team1, 0 },
            { team2, 0 }
        };
    }
}