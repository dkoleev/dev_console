using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Avocado.DeveloperCheatConsole.Scripts.Core.Commands;

namespace Avocado.DeveloperCheatConsole.Scripts.Core {
    public class DeveloperConsole {
        public IList<DevCommand> Commands => _commands;
        public bool ShowConsole { get; set; }
        public bool ShowHelp { get; set; }
        
        private IList<DevCommand> _commands;
        private IList<string> _commandsBuffer = new List<string>();
        private int _currentIndexBufferCommand;
        
        public DeveloperConsole() {
            GenerateBuildInCommands();
        }

        private void GenerateBuildInCommands() {
            var help = new DevCommand("help", "show a list of commands", () => {
                ShowHelp = true;
            });
            
            var exit = new DevCommand("exit", "disable console", () => {
                ShowConsole = false;
            });

            _commands = new Collection<DevCommand> {
                help,
                exit
            };    
        }

        public virtual void InvokeCommand(string commandInput) {
            var success = false;
            var properties = commandInput.Split(' ');
            var parameters = properties.ToList();
            parameters.RemoveAt(0);
            
            foreach (var command in _commands) {
                if (!commandInput.Contains(command.Id)) {
                    continue;
                }

                if (parameters.Count == 0) {
                    command.Invoke();
                    success = true;
                }else if (parameters.Count == 1) {
                    if (int.TryParse(parameters[0], out int resultInt)) {
                        command.Invoke(resultInt);
                    } else {
                        command.Invoke(parameters[0]);
                    }
                    success = true;
                } else {
                    if (parameters.TrueForAll(value => int.TryParse(value, out int intValue))) {
                        command.Invoke(parameters.Select(int.Parse).ToList());
                    } else {
                        command.Invoke(parameters);
                    }
                    success = true;
                }
            }
            
            if (success && !_commandsBuffer.Contains(commandInput)) {
                _commandsBuffer.Add(commandInput);
            }
        }

        public string GetBufferCommand(bool next) {
            if (next) {
                _currentIndexBufferCommand++;
            } else {
                _currentIndexBufferCommand--;
            }

            if (_currentIndexBufferCommand < 0) {
                _currentIndexBufferCommand = _commandsBuffer.Count - 1;
            }else if (_currentIndexBufferCommand >= _commandsBuffer.Count) {
                _currentIndexBufferCommand = 0;
            }

            return _commandsBuffer[_currentIndexBufferCommand];
        }
    }
}