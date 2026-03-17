using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UdonSharp;
using Andrey04o.Chess.RaycastButton;
namespace Andrey04o.Chess {
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class Player : UdonSharpBehaviour
    {
        public Piece[] pieces;
        public Piece king;
        public byte side = 0;
        public byte pieceAttackKing;
        public LayerMask layerMask;
        public Vector2Int forward;
        public Vector2Int left;
        public Vector3 promotionAngle;
        public Transform desktopPosition;
        public GameField gameField;
        public VRC.SDK3.Components.VRCStation station;
        public StationDesktopView stationDesktopView;
        Cell cellPreviousPositionMoved;
        Cell cellPositionMoved;
        public byte idPreviousPositionMoved;
        public byte idPositionMoved;
        public byte[] attackByCount = new byte[128];
        public Locker locker;
        public bool IsAttackCell(Cell cell) {
            return attackByCount[cell.position] > 0;
        }
        public byte CountAttackCell(Cell cell) {
            return attackByCount[cell.position];
        }
        public void DesktopMode() {
            stationDesktopView.Enter(side);
        }
        public bool IsHisTurn() {
            return gameField.indexSideTurn == side;
        }
        
    }
}
