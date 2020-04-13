using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace BrickBreaker
{
    public class CloneRotationsWindow : EditorWindow
    {
        GameObject _gameObject;
        float _rotationIncrement = 0;
        int _rotations = 0;

        List<GameObject> _lastGenerated = new List<GameObject>();

        // Add menu named "My Window" to the Window menu
        [MenuItem("Window/BrickBreaker/Clone rotations")]
        static void Init()
        {
            // Get existing open window or if none, make a new one:
            CloneRotationsWindow window = (CloneRotationsWindow)EditorWindow.GetWindow(typeof(CloneRotationsWindow));
            window.Show();
        }

        void OnGUI()
        {
            GUILayout.Label("Settings", EditorStyles.boldLabel);
            _gameObject = (GameObject)EditorGUILayout.ObjectField(_gameObject, typeof(GameObject), true);
            _rotationIncrement = EditorGUILayout.Slider("Rotation Increment", _rotationIncrement, 0, 90);
            _rotations = EditorGUILayout.IntField("Rotations", _rotations);

            if (GUILayout.Button("Generate"))
            {
                _lastGenerated.Clear();
                for (int i = 0; i < _rotations; i++)
                {
                    var clone = Instantiate(_gameObject);
                    clone.name = _gameObject.name + " " + (i + 1);
                    clone.transform.SetParent(_gameObject.transform.parent);
                    clone.transform.Rotate(Vector3.forward, _rotationIncrement * (i + 1));
                    _lastGenerated.Add(clone);
                }
            }

            if (GUILayout.Button("Delete last"))
            {
                for (int i = 0; i < _lastGenerated.Count; i++)
                {
                    DestroyImmediate(_lastGenerated[i]);
                }
                _lastGenerated.Clear();
            }
        }
    }
}