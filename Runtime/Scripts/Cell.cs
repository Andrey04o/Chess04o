using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UdonSharp;
using System;
using TMPro;
namespace Andrey04o.Chess {
    public class Cell : UdonSharpBehaviour
    {
        public byte position;
        public Line line;
        public GameObject positionPiece;
        public Piece pieceCurrent;
        public Piece pieceEnPassant;
        public Cell cellLeft;
        public GameField gameField;
        public byte index;
        //public byte[] attackBy;
        public byte attackByCount;
        public byte attackByCountBlack;
        public Material materialNormal;
        public Material materialAttack;
        public Material materialCurrent;
        public MeshRenderer meshRenderer;
        [HideInInspector] public bool isCanMoveHere = false;
        [HideInInspector] public byte castling = 0;
        public TextMeshPro text1;
        public TextMeshPro text2;
        public void PlacePiece(Piece piece) {
            //gameField.cells[piece.position].pieceCurrent = null;
            pieceCurrent = piece;
            piece.transform.parent = transform;
            piece.transform.position = positionPiece.transform.position;
            piece.objectSync.transform.localPosition = piece.offset;
            piece.objectSync.TeleportTo(piece.objectSync.transform);
        }

        public Cell GetNeighbour(Vector2Int dir) {
            if (gameField == null) gameField = line.gameField;
                            Debug.Log("dir is" +dir);
            if (dir == Vector2Int.zero) {
                return this;
            }
            if (dir == Vector2Int.down) {
                return GetDown();
            }
            if (dir == Vector2Int.left) {
                return GetLeft();
            }
            if (dir == Vector2Int.right) {
                return GetRight();
            }
            if (dir == Vector2Int.up) {
                return GetUp();
            }
            return null;
        }
        public Cell GetNeighbourByOffset(Vector2Int offset) {
            Vector2Int myMovement = Vector2Int.zero;
            Cell currentNeighbour = this;
            int dir = 0;
            while (myMovement.x != offset.x) {
                dir = Mathf.Clamp(myMovement.x + offset.x, -1, 1);
                myMovement.x += dir;
                currentNeighbour = currentNeighbour.GetNeighbour(new Vector2Int(dir, 0));
                if (currentNeighbour == null) return null;
            }
            while (myMovement.y != offset.y) {
                dir = Mathf.Clamp(myMovement.y + offset.y, -1, 1);
                myMovement.y += dir;
                currentNeighbour = currentNeighbour.GetNeighbour(new Vector2Int(0, dir));
                if (currentNeighbour == null) return null;
            }
            return currentNeighbour;
        }
        public Cell GetLeft() {
            if (index < line.cells.Length - 1) {
                return line.cells[index + 1];
            }
            return null;
        }

        public Cell GetRight() {
            if (index > 0) {
                return line.cells[index - 1];
            }
            return null;
        }
        public Cell GetUp() {
            if (gameField == null) gameField = line.gameField;
            
            if (line.index >= gameField.lines.Length - 1) return null;
            
            Line lineAbove = gameField.lines[line.index + 1];
            if (lineAbove == null) return null;
            Debug.Log(lineAbove.index);
            return lineAbove.cells[index];
        }
        public Cell GetDown() {
            if (gameField == null) gameField = line.gameField;
            
            if (line.index <= 0) return null;
            
            Line lineBelow = gameField.lines[line.index - 1];
            if (lineBelow == null) return null;
            
            return lineBelow.cells[index];
        }
        /*
        void PerformArray() {
            if (attackBy.Length != attackByCount) {
                byte[] newArray = new byte[attackByCount];
                for (int i = 0; i < attackByCount && i < attackBy.Length; i++) {
                    newArray[i] = attackBy[i];
                }
                attackBy = newArray;
                Debug.Log("new array, " + attackBy.Length);
            }
        }
        

        public void SetAttack(Piece piece, bool isAttack) {
            Debug.Log(name);
            if (isAttack) {
                attackByCount++;
                PerformArray();
                attackBy[attackByCount - 1] = piece.id;
            } else {
                int index = Array.IndexOf<byte>(attackBy, piece.id, 0, attackByCount);
                Debug.Log(name + " " + index);
                Debug.Log("Piece id " + piece.id + ", Cell attacked by ");
                //if (index == -1) return;
                foreach(Byte b in attackBy) {
                    Debug.Log(b);
                }
                attackBy[index] = byte.MaxValue;
                Array.Sort((Array)attackBy);
                attackByCount--;
                PerformArray();
            }
        }
        */
        public void SetAttack(Piece piece, bool isAttack) {
            Debug.Log(name);
            if (isAttack) {
                if (piece.isBlack) attackByCountBlack++;
                else attackByCount++;
            } else {
                if (piece.isBlack) attackByCountBlack--;
                else attackByCount--;
            }
            text1.text = attackByCount + "";
            text2.text = attackByCountBlack + "";
        }
        public void RemoveAttack() {
            attackByCount = 0;
            attackByCountBlack = 0;
        }
        public void SetMove(bool isCan) {
            isCanMoveHere = isCan;
            if (isCanMoveHere) {
                SetMaterial(1);
            } else {
                SetMaterial(0);
            }
        }
        public bool SetMove(Piece piece) {
            isCanMoveHere = false;
            if (pieceCurrent == null) {
                isCanMoveHere = true;
            } else if (pieceCurrent.isBlack != piece.isBlack){
                isCanMoveHere = true;
            }
            if (isCanMoveHere) {
                SetMaterial(1);
            } else {
                SetMaterial(0);
            }
            return isCanMoveHere;
        }
        /*
        bool CheckIsSafe(Piece piece) {
            if (gameField.pieces.king.id == piece.id) {
                for(int i = 0; i < attackByCount; i++) {
                    if (gameField.pieces.InTableAll[attackBy[i]].isBlack != piece.isBlack) {
                        return false;
                    }
                }
            }
            return true;
        }
        */
        public void RemoveMove() {
            isCanMoveHere = false;
            SetMaterial(0);
        }
        public void SetMaterial(byte index) {
            materialCurrent = materialNormal;
            if (index == 0) {
                materialCurrent = materialNormal;
            } else {
                materialCurrent = materialAttack;
            }
            meshRenderer.material = materialCurrent;
        }

        public Cell[] GetSlidingCells(Vector2Int direction)
        {
            Cell[] result = new Cell[7]; // Max 7 cells in any direction on 8x8 board
            int count = 0;

            Cell currentCell = this;

            for (int distance = 1; distance <= 7; distance++)
            {
                currentCell = currentCell.GetNeighbourByOffset(direction);

                if (currentCell == null) break; // Hit board edge

                result[count] = currentCell;
                count++;

                if (currentCell.pieceCurrent != null) break; // Hit a piece, stop
            }

            // Resize array to actual count
            if (count < result.Length)
            {
                Cell[] trimmed = new Cell[count];
                for (int i = 0; i < count; i++)
                {
                    trimmed[i] = result[i];
                }
                return trimmed;
            }

            return result;
        }
        public bool IsAttacking(Piece piece) {
            if (piece.isBlack) {
                if (attackByCount != 0) return true;
            } else {
                if (attackByCountBlack != 0) return true;
            }
            return false;
        }

    }
}
