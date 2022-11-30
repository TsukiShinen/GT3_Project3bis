using Library.Command;
using UnityEngine;

public class MoveCommand : ICommand
{
    private Tank _tank;
    private float _direction;

    public MoveCommand(Tank tank, float verticalDirection)
    {
        _tank = tank;
        _direction = verticalDirection;
    }

    public void Execute()
    {
        _tank.Move(_direction);
    }
}
