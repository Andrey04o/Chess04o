using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UdonSharp;
namespace Andrey04o.Chess {
    public class King : Piece
    {
        public override void CalcAttack(Piece piece)
        {
            base.CalcAttack(piece);
            piece.AddCellAttack(new Vector2Int(1,1));
            piece.AddCellAttack(new Vector2Int(-1,1));
            piece.AddCellAttack(new Vector2Int(1,-1));
            piece.AddCellAttack(new Vector2Int(-1,-1));
            piece.AddCellAttack(new Vector2Int(1,0));
            piece.AddCellAttack(new Vector2Int(-1,0));
            piece.AddCellAttack(new Vector2Int(0,1));
            piece.AddCellAttack(new Vector2Int(0,-1));
        }
        public override void ShowMove(Piece piece)
        {
            Cell cell;
            for (int i = 0; i < piece.dir1Count; i++) {
                cell = piece.gameField.cells[piece.dir1[i]];
                if (piece.isBlack) {
                    if (cell.attackByCount == 0) {
                        piece.gameField.AddMove(cell, piece);
                    }
                } else {
                    if (cell.attackByCountBlack == 0) {
                        piece.gameField.AddMove(cell, piece);
                    }
                }
            }

            // castling
            if (piece.isNotMoved != 0) return;
            if (piece.GetCurrentCell().IsAttacking(piece)) return;
            //piece.gameField.
            CheckCastlingQueenside(piece);
            CheckCastlingKingside(piece);
        }
        bool GetLeftEmpty(ref Cell cell) {
            cell = cell.GetLeft();
            if (cell.pieceCurrent == null) return false;
            return true;
        }
        bool GetRightEmpty(ref Cell cell) {
            cell = cell.GetRight();
            //Debug(cell.name)
            if (cell.pieceCurrent == null) return false;
            return true;
        }
        public void CheckCastlingKingside(Piece piece) {
            Cell cell;
            //Debug.Log("queenside");
            cell = piece.GetCurrentCell();
            if (GetLeftEmpty(ref cell)) return;
            if (cell.IsAttacking(piece)) return;
            if (GetLeftEmpty(ref cell)) return;
            if (cell.IsAttacking(piece)) return;
            cell.castling = 1;
            cell = cell.GetLeft();
            if (cell.pieceCurrent != null && cell.pieceCurrent.isNotMoved == 0) {
                piece.gameField.AddMove(piece.GetCurrentCell().GetLeft().GetLeft()); 
            }
        }
        public void CheckCastlingQueenside(Piece piece) {
            Cell cell;
            cell = piece.GetCurrentCell();
            if (GetRightEmpty(ref cell)) return;
            if (cell.IsAttacking(piece)) return;
            if (GetRightEmpty(ref cell)) return;
            if (cell.IsAttacking(piece)) return;
            cell.castling = 2;
            if (GetRightEmpty(ref cell)) return;
            cell = cell.GetRight();
            if (cell.pieceCurrent != null && cell.pieceCurrent.isNotMoved == 0) {
                piece.gameField.AddMove(piece.GetCurrentCell().GetRight().GetRight()); 
            }
        }
        bool castling = false;
        public override void PerformMove(Cell cell, Piece piece)
        {
            if (isNotMoved == 0) {
                if (cell.castling == 0) return;
                castling = true;
            }
            base.PerformMove(cell, piece);
        }
        public override void AfterMove(Cell cell, Piece piece)
        {
            base.AfterMove(cell, piece);
            if (castling == false) return;
            castling = false;
            Piece piece1;
            if (cell.castling == 1) {
                piece1 = cell.GetLeft().pieceCurrent;
                PlacePiece(cell.GetRight(), piece1);
            }
            if (cell.castling == 2) {
                piece1 = cell.GetRight().GetRight().pieceCurrent;
                PlacePiece(cell.GetLeft(), piece1);
            }
        }
    }
}
