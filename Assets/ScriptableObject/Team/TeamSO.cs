using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="TeamSO", menuName ="Others/TeamSO")]
public class TeamSO : ScriptableObject
{
    [SerializeField] private string teamName;
    public string TeamName { get => teamName;}

    [SerializeField] private Color teamColor;
    public Color TeamColor { get => teamColor; }
}
