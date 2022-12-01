using Library.Command;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnToCommand : ICommand
{
    private readonly Tank _tank;
    private readonly Transform _target;

    public TurnToCommand(Tank tank, Transform target)
    {
        _tank = tank;
        _target = target;
    }

    public void Execute()
    {
        _tank.tankMovement.BeginTurnTo(_target);
    }
}
