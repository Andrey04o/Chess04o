using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UdonSharp;
using VRC.SDKBase;
using Andrey04o.Chess;
namespace Andrey04o.RaycastButton {
    public class InteractiveButtonChangeCamera : UdonSharpBehaviour
    {
        public VRC.SDK3.Components.VRCStation station;
        public GameObject desktopControl;
        public GameField gameField;
        public override void Interact()
        {
            base.Interact();
            station.UseStation(Networking.LocalPlayer);
            desktopControl.SetActive(true);
            DisableInteractive = true;
            gameField.Set2DView(true, desktopControl.transform.rotation);
        }

        public void Leave() {
            desktopControl.SetActive(false);
            station.ExitStation(Networking.LocalPlayer);
            DisableInteractive = false;
            gameField.Set2DView(false);
        }
    }
}
