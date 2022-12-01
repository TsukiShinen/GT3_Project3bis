using UnityEngine;

namespace Library.Command
{
    public class MoveToCommand : ICommand
    {
        private Tank _tank;
        private Vector3 _target;

        public MoveToCommand(Tank tank, Vector3 target)
        {
            _tank = tank;
            _target = target;
        }
        
        public void Execute()
        {
            _tank.GeneratePath(_target);
        }
    }
}