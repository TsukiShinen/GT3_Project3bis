using System;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameInfoView : MonoBehaviour
{
    [SerializeField] private GameManagerSO gameManagerSo;
    [SerializeField] private TMP_Text textTimer;
    [SerializeField] private Image progressbarTimer;
    [SerializeField] private Image team1Score;
    [SerializeField] private Image team2Score;


    private void Start()
    {
        team1Score.color = gameManagerSo.team1.TeamColor;
        team2Score.color = gameManagerSo.team2.TeamColor;
    }

    private void Update()
    {
        textTimer.text = Math.Floor(gameManagerSo.timer).ToString();
        progressbarTimer.fillAmount = gameManagerSo.timer / gameManagerSo.gameParametersSo.gameTimer;
        team1Score.fillAmount = gameManagerSo.Scores[gameManagerSo.team1] / 100f;
        team2Score.fillAmount = gameManagerSo.Scores[gameManagerSo.team2] / 100f;
    }
}