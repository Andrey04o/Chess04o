#if !COMPILER_UDONSHARP && UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UdonSharpEditor;
namespace Andrey04o.Chess {
    [CustomEditor(typeof(Piece), true)]
    public class PieceEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            if (UdonSharpGUI.DrawDefaultUdonSharpBehaviourHeader(target)) return;
            DrawDefaultInspector();

            Piece myTarget = (Piece)target;

            EditorGUILayout.Space();
            if (GUILayout.Button("Set Offset"))
            {
                myTarget.offset = myTarget.meshRenderer.gameObject.transform.position;
                EditorUtility.SetDirty(myTarget);
            }
        }
    }
}
#endif