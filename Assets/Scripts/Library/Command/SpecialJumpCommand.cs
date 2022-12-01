namespace Library.Command
{
    public class SpecialJumpCommand : ICommand
    {
        private readonly Tank _tank;

        public SpecialJumpCommand(Tank tank)
        {
            _tank = tank;
        }

        public void Execute()
        {
            _tank.tankActions.SpecialJump();
        }
    }
}
