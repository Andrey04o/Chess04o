#if !COMPILER_UDONSHARP && UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UdonSharpEditor;
namespace Andrey04o.Chess {
    [CustomEditor(typeof(Player), true)]
    public class PlayerEditor : Editor
    {
        Ray ray;
        public override void OnInspectorGUI()
        {
            if (UdonSharpGUI.DrawDefaultUdonSharpBehaviourHeader(target)) return;
            DrawDefaultInspector();

            Player myTarget = (Player)target;

            EditorGUILayout.Space();
            if (GUILayout.Button("Place")) {
                foreach (Piece piece in myTarget.pieces) {
                    ray = new Ray(piece.transform.position, Vector3.down);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, 10f, myTarget.layerMask)) {
                        RaycastButton.Cell cell = hit.collider.GetComponent<RaycastButton.Cell>();
                        if (cell != null) {
                            piece.GetCurrentCell().pieceCurrent = null;
                            cell.cell.PlacePieceLocal(piece);
                            piece.position = cell.cell.position;
                            piece.originalPosition = piece.position;
                            EditorUtility.SetDirty(cell.cell);  
                        }
                    }
                    EditorUtility.SetDirty(piece);  
                }
                EditorUtility.SetDirty(myTarget);
            }
            if (GUILayout.Button("Change color")) {
                foreach (Piece piece in myTarget.pieces) {
                    piece.meshRenderer.sharedMaterial = piece.gameField.pieces.colorSides[myTarget.side].material;
                    piece.spriteRenderer.sprite = piece.gameField.pieces.colorSides[myTarget.side].sprites[piece.indexType - 1];
                    EditorUtility.SetDirty(piece.meshRenderer);
                    EditorUtility.SetDirty(piece.spriteRenderer);  
                }
                EditorUtility.SetDirty(myTarget);
            }
            if (GUILayout.Button("Change forward")) {
                foreach (Piece piece in myTarget.pieces) {
                    piece.forward = myTarget.forward;
                    piece.left = myTarget.left;
                    piece.promotion.rotation.eulerAngles = myTarget.promotionAngle;
                    EditorUtility.SetDirty(piece);
                    EditorUtility.SetDirty(piece.promotion);  
                }
                EditorUtility.SetDirty(myTarget);
            }
        }
    }
}
#endif