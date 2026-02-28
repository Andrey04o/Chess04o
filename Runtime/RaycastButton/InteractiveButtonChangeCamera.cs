using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UdonSharp;
using VRC.SDKBase;
namespace Andrey04o.RaycastButton {
    public class InteractiveButtonChangeCamera : UdonSharpBehaviour
    {
        public VRC.SDK3.Components.VRCStation station;
        public GameObject desktopControl;
        public override void Interact()
        {
            base.Interact();
            station.UseStation(Networking.LocalPlayer);
            desktopControl.SetActive(true);
            DisableInteractive = true;
        }

        public void Leave() {
            desktopControl.SetActive(false);
            station.ExitStation(Networking.LocalPlayer);
            DisableInteractive = false;
        }
    }
}
