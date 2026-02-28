using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UdonSharp;
namespace Andrey04o.Chess {
    public class Rook : Piece
    {
        public override void CalcAttack(Piece piece)
        {
            base.CalcAttack(piece);
            piece.AddSlidingCellAttack(new Vector2Int(1,0));
            piece.AddSlidingCellAttack(new Vector2Int(-1,0));
            piece.AddSlidingCellAttack(new Vector2Int(0,1));
            piece.AddSlidingCellAttack(new Vector2Int(0,-1));
        }
        public override void RemoveAttack(Piece piece)
        {
            for(int i = 0; i < piece.dir1Count; i++) {
                piece.gameField.cells[piece.dir1[i]].SetAttack(piece, false);
                piece.gameField.cells[piece.dir1[i]].SetAttackVector(piece.dir2[i], false);
            }
            
            piece.dir1Count = 0;
        }
    }
}