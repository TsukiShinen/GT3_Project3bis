namespace Library.Command
{
    public class MoveCommand : ICommand
    {
        private readonly Tank _tank;
        private readonly float _direction;

        public MoveCommand(Tank tank, float verticalDirection)
        {
            _tank = tank;
            _direction = verticalDirection;
        }

        public void Execute()
        {
            _tank.tankMovement.Move(_direction);
        }
    }
}
