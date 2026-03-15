#if !COMPILER_UDONSHARP && UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UdonSharpEditor;
namespace Andrey04o.Chess {
    [CustomEditor(typeof(GameField), true)]
    public class GameFieldEditor : Editor
    {
        Ray ray;
        public override void OnInspectorGUI()
        {
            if (UdonSharpGUI.DrawDefaultUdonSharpBehaviourHeader(target)) return;
            DrawDefaultInspector();

            GameField myTarget = (GameField)target;

            EditorGUILayout.Space();
            if (GUILayout.Button("Recalc attacks"))
            {
                myTarget.RemoveAttack();
                myTarget.CalcAttacks();
                foreach (Cell cell in myTarget.cells) {
                    EditorUtility.SetDirty(cell);
                }
                EditorUtility.SetDirty(myTarget);
            }
        }
    }
}
#endif
