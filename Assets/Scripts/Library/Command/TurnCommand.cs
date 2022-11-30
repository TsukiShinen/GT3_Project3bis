using UnityEngine;

namespace Library.Command
{
    public class TurnCommand : ICommand
    {
        private readonly Tank _tank;
        private readonly float _horizontalDirection;

        public TurnCommand(Tank tank, float horizontalDirection)
        {
            _tank = tank;
            _horizontalDirection = horizontalDirection;
        }

        public void Execute()
        {
            _tank.Turn(90 * Mathf.Sign(_horizontalDirection));
        }
    }
}
