using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "GameParametersSO", menuName = "Game/GameParametersSO", order = 0)]
    public class GameParametersSO : ScriptableObject
    {
        public int maxScore = 100;
        public float timeTakeZone = 7;
        public float gameTimer = 120f;
    }
}