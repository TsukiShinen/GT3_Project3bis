using System;
using System.Collections.Generic;
using ScriptableObjects.GameState;
using UnityEngine;

namespace State_Machines
{
    public class GameStateMachine : MonoBehaviour
    {
        #region fields
        [SerializeField] private List<GameState> lstGStates;

        [SerializeField] private EGameState currentGState;
        #endregion

        #region Properties

        private BaseGameState CurrentGState => lstGStates.Find(gs => gs.state == currentGState).machine;
        public EGameState LastGState { get; set; }
        #endregion

        #region Methods

        #region Start And Init
        private void Start()
        {
            SubGStateInit();
            CurrentGState.StartState();
        }

        private void SubGStateInit()
        {
            foreach (var gState in lstGStates)
            {
                gState.machine.Init(this);
            }

        }
        #endregion

        #region RunTime
        private void Update()
        {
            CurrentGState.UpdateState();
        }

        private void FixedUpdate()
        {
            CurrentGState.FixedUpdateState();
        }

        public void ChangeState(EGameState nextState)
        {
            CurrentGState.LeaveState();
            currentGState = nextState;
            CurrentGState.StartState();
        }
        #endregion

        #endregion
    }

    [Serializable]
    public struct GameState
    {
        public EGameState state;
        public BaseGameState machine;
    }

    public enum EGameState
    {
        START,
        MENU,
        GAMEPLAY,
        NONE
    }
}