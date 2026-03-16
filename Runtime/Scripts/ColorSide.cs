using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UdonSharp;
namespace Andrey04o.Chess {
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]

    public class ColorSide : UdonSharpBehaviour
    {
        public Material material;
        public Sprite[] sprites;
        public Color color;
    }
}
