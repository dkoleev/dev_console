using Avocado.DeveloperCheatConsole.Scripts.Core;
using UnityEngine;

namespace Avocado.DeveloperCheatConsole.Scripts.Visual {
    public abstract class DevConsoleGUI : MonoBehaviour {
        protected DeveloperConsole _console;
        
        protected string _input;
        protected Vector2 _scroll;
        protected bool _inputFocus;
        
        private void Awake() {
#if UNITY_EDITOR
            _console = new DeveloperConsole();
            GenerateCommands();
#endif
        }
        
        private void GenerateCommands() {
            
        }
        
        private void OnGUI() {
#if UNITY_EDITOR
            DrawGUI();
#endif
        }
        
        protected void OnReturn() {
            if (_console.ShowConsole) {
                _console.InvokeCommand(_input);
                _input = "";
            }
        }

        protected void HandleEscape() {
            if (!_console.ShowHelp) {
                _console.ShowConsole = false;
                _inputFocus = false;
            } else {
                _console.ShowHelp = false;
                GUI.FocusControl("inputField");
            }
        }

        protected virtual void HandleShowConsole() {
        }
        
        protected virtual void HadnleKeyboardInGUI() {
        }
        
        private void DrawGUI() {
            if (!_console.ShowConsole) {
                HandleShowConsole();
                return;
            }
            
            HadnleKeyboardInGUI();

            var inputHeight = 100;
            var y = Screen.height - inputHeight;

            if (_console.ShowHelp) {
                ShowHelp(y);
            }

            GUI.Box(new Rect(0, y, Screen.width, 100), "");
            GUI.backgroundColor = Color.black;
            
            var labelStyle = new GUIStyle("TextField");
            labelStyle.fontStyle = FontStyle.Normal;
            labelStyle.fontSize = 40;
            labelStyle.alignment = TextAnchor.MiddleLeft;
            GUI.SetNextControlName("inputField");
            _input = GUI.TextField(new Rect(10f, y + 10f, Screen.width - 20, 70f), _input, labelStyle);

            SetFocusTextField();
        }

        protected virtual void SetFocusTextField() {
            if (!_inputFocus) {
                _inputFocus = true;
                GUI.FocusControl("inputField");
                _input = string.Empty;
            }
        }

        private void ShowHelp(float y) {
            GUI.Box(new Rect(0, y - 500, Screen.width, 500), "");
            var viewPort = new Rect(0, 0, Screen.width - 30, 80 * _console.Commands.Count);
            _scroll = GUI.BeginScrollView(new Rect(0, y - 480f, Screen.width, 480), _scroll, viewPort);
             
            for (int i = 0; i < _console.Commands.Count; i++) {
                var command = _console.Commands[i];
                var label = $"{command.Id} - {command.Description}";
                var labelRect = new Rect(10, 50*i, viewPort.width-100, 50);
                    
                var labelStyleHelp = new GUIStyle("label");
                labelStyleHelp.fontStyle = FontStyle.Normal;
                labelStyleHelp.fontSize = 30;
                    
                GUI.Label(labelRect, label, labelStyleHelp);
            }
                
            GUI.EndScrollView();
        }
    }
}
