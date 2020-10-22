using System.Collections.Generic;
using UnityEngine;

namespace Avocado.DeveloperCheatConsole.Scripts.Core.Commands {
    public class AllCommands {
        public AllCommands() {
            GenerateCommands();
        }

        public IList<DevCommand> GenerateCommands() {
            var commands = new List<DevCommand>();
            commands.Add(new DevCommand("test", "test", () => {
                Debug.LogError("Test command");
            }));
            
            return commands;
        }
    }
}