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
    }
}
