using GTA;
using GTA.Native;
using GTA.Math;
using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Media;

namespace GoToDlcMapSpa
{
    public class GoToDlcMapSpa : Script
    {
        private string modName = "Teleporting to Spa Francorchamps Circuit";
        // Airport location
        private Vector3 teleportMarkerPos = new Vector3(-1042.0f, -2532.0f, 12.7f);
        // Triple map location
        private Vector3 teleportToPos = new Vector3(482.0f, 9883.0f, 218.40f);
        private Blip teleportBlip;
        private bool atTeleportMarker = false;

        public GoToDlcMapSpa()
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
                UI.ShowSubtitle("Press T for flying to Spa Francorchamps Circuit", 125);
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
            if (e.KeyCode == Keys.T && (World.GetDistance(Game.Player.Character.Position, teleportMarkerPos) < 5))
            {
                if (Game.Player.Character.IsInVehicle())
                {
                    Game.FadeScreenOut(200);
                    Script.Wait(500);
                    Game.Player.Character.CurrentVehicle.Position = teleportToPos;
                    Game.Player.Character.Heading = 135.28f;
                    Script.Wait(500);
                    Game.FadeScreenIn(200);
                }
                else
                {
                    Game.Player.Character.FreezePosition = true;
                    Game.FadeScreenOut(200);
                    Script.Wait(500);
                    Game.Player.Character.Position = teleportToPos;
                    Game.Player.Character.FreezePosition = false;
                    Script.Wait(500);
                    Game.FadeScreenIn(200);
                }
            }
            else if (e.KeyCode == Keys.T && (World.GetDistance(Game.Player.Character.Position, teleportToPos) < 5))
            {
                if (Game.Player.Character.IsInVehicle())
                {
                    Game.FadeScreenOut(200);
                    Script.Wait(500);
                    Game.Player.Character.CurrentVehicle.Position = teleportMarkerPos;
                    Game.Player.Character.Heading = 148.89f;
                    Script.Wait(500);
                    Game.FadeScreenIn(200);
                }
                else
                {
                    Game.Player.Character.FreezePosition = true;
                    Game.FadeScreenOut(200);
                    Script.Wait(500);
                    Game.Player.Character.Position = teleportMarkerPos;
                    Game.Player.Character.FreezePosition = false;
                    Script.Wait(500);
                    Game.FadeScreenIn(200);
                    Game.Player.WantedLevel = 0;
                }
            }
        }
    }
}