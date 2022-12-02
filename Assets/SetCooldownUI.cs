using ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetCooldownUI : MonoBehaviour
{
    [SerializeField] private GameManagerSO gamemanager;
    [SerializeField] private Image shootTimer;
    [SerializeField] private Image jumpTimer;
    [SerializeField] private Image shootIcon;
    [SerializeField] private Image jumpIcon;

    private void Awake()
    {
        shootTimer.color = gamemanager.team1.TeamColor;
        jumpTimer.color = gamemanager.team1.TeamColor;
        shootIcon.color = gamemanager.team1.TeamColor;
        jumpIcon.color = gamemanager.team1.TeamColor;
        gamemanager.shootCooldownImage = shootTimer;
        gamemanager.specialJumpCooldownImage = jumpTimer;
    }
}
