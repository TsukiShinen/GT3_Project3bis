using System.Collections.Generic;

namespace Library.Command
{
    public class CommandManager : Singleton<CommandManager>
    {
        private Stack<ICommand> _commandsBuffer;

        protected override void Start()
        {
            _commandsBuffer = new Stack<ICommand>();
        }

        public void AddCommand(ICommand command)
        {
            command.Execute();
            _commandsBuffer.Push(command);
        }
    }
}