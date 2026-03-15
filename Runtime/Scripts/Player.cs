using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UdonSharp;
namespace Andrey04o.Chess {
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class Player : UdonSharpBehaviour
    {
        public Piece[] pieces;
        public Piece king;
        public byte side = 0;
        public LayerMask layerMask;
        public Vector2Int forward;
        public Vector2Int left;
        public Vector3 promotionAngle;
    }
}
