using Library.Command;
using UnityEngine;

public class TurnCommand : ICommand
{
    private Tank _tank;
    private float _horizontalDirection;

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
