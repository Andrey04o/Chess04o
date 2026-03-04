using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UdonSharp;
namespace Andrey04o.Chess {
    public class Promotion : UdonSharpBehaviour
    {
        public Piece piece;
        public Quaternion rotation;
        public void SetPromotion(byte id) {
            piece.ConfirmPromotion(id);
        }
        public void CancelPromotion() {
            piece.CancelPromotion();
        }
        public void ChangeRotation(Quaternion rotation) {
            this.rotation = transform.rotation;
            transform.rotation = rotation;
        }
        public void ResetRotation() {
            transform.rotation = rotation;
        }
    }
}