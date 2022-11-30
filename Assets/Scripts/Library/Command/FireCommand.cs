using Library.Command;

public class FireCommand : ICommand
{
    private Tank _tank;

    public FireCommand(Tank tank)
    {
        _tank = tank;
    }

    public void Execute()
    {
        _tank.Shoot();
    }
}
