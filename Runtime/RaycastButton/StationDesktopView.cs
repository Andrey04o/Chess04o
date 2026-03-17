using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UdonSharp;
using VRC.SDKBase;
namespace Andrey04o.Chess.RaycastButton {
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class StationDesktopView : UdonSharpBehaviour
    {
        public VRC.SDK3.Components.VRCStation station;
        public VRC.SDK3.Components.VRCStation stationBlack;
        public GameObject desktopControl;
        public Camera camera;
        public GameField gameField;
        public GameObject blockerInteraction;
        bool isDesktopMode;
        Quaternion rotation;
        Vector3 rotationEuler;
        public DesktopControls desktopControls;
        public Locker lockerWhite;
        public Locker lockerBlack;
        Locker lockerCurrent;
        byte currentSide;
        Player currentPlayer;
        public void Enter(byte side) {
            currentSide = side;
            currentPlayer = gameField.pieces.players[currentSide];
            gameField.touchControls.ChangeMethod(false);
            camera.transform.rotation = currentPlayer.desktopPosition.transform.rotation;
            
            //station.transform.position = Networking.LocalPlayer.GetPosition();
            //station.transform.eulerAngles = new Vector3 (0f, Networking.LocalPlayer.GetRotation().eulerAngles.y, 0f);
            blockerInteraction.gameObject.SetActive(true);
            blockerInteraction.transform.position = Networking.LocalPlayer.GetTrackingData(VRCPlayerApi.TrackingDataType.Head).position;
            Networking.LocalPlayer.Immobilize(true);
            currentPlayer.station.UseStation(Networking.LocalPlayer);
            lockerCurrent =  currentPlayer.locker;

            desktopControls.Show(true, lockerCurrent, side);
            desktopControl.SetActive(true);
            DisableInteractive = true;
            
            gameField.isTouchMode = false;
            gameField.is2DMode = true;
            gameField.ShowPieces(desktopControl.transform.rotation);
            gameField.ShowPromotion();

            camera.enabled = true;
            isDesktopMode = true;
        }
        public void Leave() {
            blockerInteraction.gameObject.SetActive(false);
            desktopControl.SetActive(false);
            Networking.LocalPlayer.Immobilize(false);
            currentPlayer.station.ExitStation(Networking.LocalPlayer);
            desktopControls.Hide();
            DisableInteractive = false;

            gameField.is2DMode = false;
            gameField.ShowPieces(Quaternion.identity);
            gameField.ShowPromotion();

            camera.enabled = false;
            isDesktopMode = false;
        }
        public override void OnPlayerRespawn(VRCPlayerApi player)
        {
            base.OnPlayerRespawn(player);
            if (isDesktopMode == true) {
                Leave();
            }
        }
    }
}
