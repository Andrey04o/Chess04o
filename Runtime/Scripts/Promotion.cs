using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UdonSharp;
namespace Andrey04o.Chess {
    public class Promotion : UdonSharpBehaviour
    {
        public Piece piece;
        public void SetPromotion(byte id) {
            piece.ConfirmPromotion(id);
        }
        public void CancelPromotion() {
            piece.CancelPromotion();
        }
    }
}