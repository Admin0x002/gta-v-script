using GTA;
using GTA.Native;
using GTA.Math;
using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Media;

namespace GoToDlcMapBarcelona
{
    public class GoToDlcMapBarcelona : Script
    {
        private string modName = "Teleporting to Barcelona Catalunya Circuit";
        // Airport location
        private Vector3 teleportMarkerPos = new Vector3(-1042.0f, -2532.0f, 13.0f);
        // Triple map location
        private Vector3 teleportToPos = new Vector3(7347.0f, 2432.0f, 82.0f);
        private Blip teleportBlip;
        private bool atTeleportMarker = false;

        public GoToDlcMapBarcelona()
        {
            UI.Notify(modName);
            Interval = 0;
            Tick += OnTick;
            KeyUp += OnKeyUp;

            teleportBlip = World.CreateBlip(teleportMarkerPos);
            teleportBlip.Sprite = BlipSprite.Helicopter;
            teleportBlip.Name = modName;
            teleportBlip.Color = BlipColor.Yellow;
            teleportBlip.IsShortRange = true;
        }

        private void OnTick(object sender, EventArgs e)
        {
            Ped playerPed = Game.Player.Character;
            World.DrawMarker(MarkerType.VerticalCylinder, teleportMarkerPos, Vector3.Zero, Vector3.Zero, new Vector3(0.65f, 0.65f, 0.25f), Color.Yellow);
            World.DrawMarker(MarkerType.VerticalCylinder, teleportToPos, Vector3.Zero, Vector3.Zero, new Vector3(0.65f, 0.65f, 0.25f), Color.Yellow);

            if (playerPed.Position.DistanceTo(teleportMarkerPos) < 5f)
            {
                atTeleportMarker = true;
                UI.ShowSubtitle("Press T for flying to Barcelona Catalunya Circuit", 125);
            }
            else if (playerPed.Position.DistanceTo(teleportToPos) < 5f)
            {
                atTeleportMarker = true;
                UI.ShowSubtitle("Press T for flying back to LSIA", 125);
            }
            else
            {
                atTeleportMarker = false;
            }
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.T && atTeleportMarker)
            {
                Ped playerPed = Game.Player.Character;
                if (playerPed.IsInVehicle())
                {
                    Game.FadeScreenOut(200);
                    Script.Wait(500);
                    playerPed.CurrentVehicle.Position = teleportToPos;
                    playerPed.Heading = 135.28f;
                    Script.Wait(500);
                    Game.FadeScreenIn(200);
                }
                else
                {
                    playerPed.FreezePosition = true;
                    Game.FadeScreenOut(200);
                    Script.Wait(500);
                    playerPed.Position = teleportToPos;
                    playerPed.FreezePosition = false;
                    Script.Wait(500);
                    Game.FadeScreenIn(200);
                }
            }
        }
    }
}