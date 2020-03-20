using UnityEngine;
using UnityEngine.UI;

namespace BrickBreaker
{
    public class CustomDebugOutput : MonoBehaviour
    {
        public Text output;
        private string _consoleString;

        void OnEnable()
        {
            Application.logMessageReceived += HandleLog;
        }

        void OnDisable()
        {
            Application.logMessageReceived -= HandleLog;
        }

        void HandleLog(string message, string stackTrace, LogType type)
        {
            _consoleString = ">>> " + message + "\n" + _consoleString;
            output.text = _consoleString;
        }
    }
}