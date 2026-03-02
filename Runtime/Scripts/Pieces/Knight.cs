using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UdonSharp;
namespace Andrey04o.Chess {
    public class Knight : Piece
    {
        public override void CalcAttack(Piece piece, bool isRemove = false, bool isVisualMoving = false)
        {
            base.CalcAttack(piece);
            piece.AddCellAttack(new Vector2Int(1,2), isRemove, isVisualMoving);
            piece.AddCellAttack(new Vector2Int(2,1), isRemove, isVisualMoving);
            piece.AddCellAttack(new Vector2Int(-1,2), isRemove, isVisualMoving);
            piece.AddCellAttack(new Vector2Int(-2,1), isRemove, isVisualMoving);
            piece.AddCellAttack(new Vector2Int(1,-2), isRemove, isVisualMoving);
            piece.AddCellAttack(new Vector2Int(2,-1), isRemove, isVisualMoving);
            piece.AddCellAttack(new Vector2Int(-1,-2), isRemove, isVisualMoving);
            piece.AddCellAttack(new Vector2Int(-2,-1), isRemove, isVisualMoving);
        }
    }
}