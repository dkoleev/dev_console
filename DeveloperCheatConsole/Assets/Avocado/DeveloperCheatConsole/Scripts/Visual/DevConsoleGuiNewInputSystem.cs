using UnityEngine;
using UnityEngine.InputSystem;

namespace Avocado.DeveloperCheatConsole.Scripts.Visual {
    public class DevConsoleGuiNewInputSystem : DevConsoleGUI {
        private void Update() {
            var keyboard = Keyboard.current;
            if (keyboard.backquoteKey.wasPressedThisFrame) {
                if (!_console.ShowConsole) {
                    _console.ShowConsole = true;
                }
            }else if (keyboard.enterKey.wasPressedThisFrame) {
                OnReturn();
            }else if (keyboard.escapeKey.wasPressedThisFrame) {
                if (_console.ShowConsole) {
                    HandleEscape();
                }
            }else if (keyboard.upArrowKey.wasPressedThisFrame) {
                _input = _console.GetBufferCommand(false);
                GUI.FocusControl("inputField");
            }else if (keyboard.downArrowKey.wasPressedThisFrame) {
                _input = _console.GetBufferCommand(true);
                GUI.FocusControl("inputField");
            }
        }
    }
}
