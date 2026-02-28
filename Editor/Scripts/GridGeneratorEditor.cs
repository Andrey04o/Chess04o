#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
namespace Andrey04o.Chess {
    [CustomEditor(typeof(GridGenerator))]
    public class GridGeneratorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            GridGenerator gridGenerator = (GridGenerator)target;

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Grid Generation", EditorStyles.boldLabel);

            if (GUILayout.Button("Generate Grid", GUILayout.Height(40)))
            {
                gridGenerator.GenerateGrid();
            }

            if (GUILayout.Button("Clear Grid", GUILayout.Height(40)))
            {
                gridGenerator.ClearGrid();
            }
        }
    }
}
#endif