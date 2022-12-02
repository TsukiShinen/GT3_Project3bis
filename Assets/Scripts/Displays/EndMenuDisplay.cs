using Complete;
using ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndMenuDisplay : MonoBehaviour
{
    [SerializeField] private GameManagerSO gameManagerSO;
    [SerializeField] private AudioSO audioSO;
    [SerializeField] private TMP_Text result;
    [SerializeField] private TMP_Text team1Name;
    [SerializeField] private TMP_Text team2Name;
    [SerializeField] private TMP_Text team1Score;
    [SerializeField] private TMP_Text team2Score;

    private void Start()
    {
        audioSO.StopAll();

        var tmpResult = gameManagerSO.Scores[gameManagerSO.team1] - gameManagerSO.Scores[gameManagerSO.team2];

        if (tmpResult > 0)
        {
            result.text = "VICTORY";
            audioSO.Play("score_board");
        }
        else if (tmpResult < 0)
        {
            result.text = "DEFEAT";
            audioSO.Play("main_menu");
        }
        else
        {
            result.text = "DRAW";
            audioSO.Play("battle_theme");
        }
            
        team1Name.text = gameManagerSO.team1.TeamName;
        team2Name.text = gameManagerSO.team2.TeamName;
        team1Score.text = gameManagerSO.Scores[gameManagerSO.team1].ToString();
        team2Score.text = gameManagerSO.Scores[gameManagerSO.team2].ToString();
    }
}
