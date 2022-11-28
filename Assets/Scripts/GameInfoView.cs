using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Engine.Utils
{
    public class GameInfoView : MonoBehaviour
    {
        [SerializeField] private GameManagerSO gameManagerSo;
        [SerializeField] private TMP_Text textTimer;
        [SerializeField] private Image progressbarTimer;

        private void Update()
        {
            textTimer.text = Math.Floor(gameManagerSo.timer).ToString();
            progressbarTimer.fillAmount = gameManagerSo.timer / gameManagerSo.gameParametersSO.gameTimer;
        }
    }
}