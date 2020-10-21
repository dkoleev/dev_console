using System;
using System.Collections.Generic;

namespace Avocado.DeveloperCheatConsole.Scripts.Core.Commands {
    public class DevCommand {
        public string Id { get; }
        public string Description { get; }
        
        public Action _command;
        public Action<string> _commandStr;
        public Action<List<string>> _commandListStr;
        public Action<List<int>> _commandListInt;
        public Action<int> _commandInt;

        private DevCommand(string commandId, string commandDescription) {
            Id = commandId;
            Description = commandDescription;
        }

        public DevCommand(string commandId, string commandDescription, Action command) : this(commandId, commandDescription) {
            _command = command;
        }
        
        public DevCommand(string commandId, string commandDescription, Action<string> command) : this(commandId, commandDescription) {
            _commandStr = command;
        }
        
        public DevCommand(string commandId, string commandDescription, Action<int> command) : this(commandId, commandDescription) {
            _commandInt = command;
        }
        
        public DevCommand(string commandId, string commandDescription, Action<List<string>> command) : this(commandId, commandDescription) {
            _commandListStr = command;
        }
        
        public DevCommand(string commandId, string commandDescription, Action<List<int>> command) : this(commandId, commandDescription) {
            _commandListInt = command;
        }

        public void Invoke() {
            _command.Invoke();
        }
        
        public void Invoke(string parameter) {
            _commandStr.Invoke(parameter);
        }
        
        public void Invoke(int parameter) {
            _commandInt.Invoke(parameter);
        }
        
        public void Invoke(List<string> parameters) {
            _commandListStr.Invoke(parameters);
        }
        
        public void Invoke(List<int> parameters) {
            _commandListInt.Invoke(parameters);
        }
    }
}