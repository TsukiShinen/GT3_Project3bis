using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects.Team
{
    [CreateAssetMenu(fileName ="TeamSO", menuName ="Others/TeamSO")]
    public class TeamSO : ScriptableObject
    {
        [SerializeField] private string teamName;
        public string TeamName => teamName;

        [SerializeField] private Color teamColor;
        public Color TeamColor => teamColor;

        [SerializeField] private List<TankParametersSO> tankList;
        public List<TankParametersSO> TankList => tankList;
    }
}
