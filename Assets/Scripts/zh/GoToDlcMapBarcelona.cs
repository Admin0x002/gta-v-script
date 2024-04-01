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
        private string modName = "飞往 巴塞罗那加泰罗尼亚赛道"; // 模块名称
        private Vector3 teleportMarkerPos = new Vector3(-1042.0f, -2532.0f, 13.0f); // 传送标记位置
        private Vector3 teleportToPos = new Vector3(7347.0f, 2432.0f, 82.0f); // 传送目标位置
        private Blip teleportBlip; // 传送标记
        private bool atTeleportMarker = false; // 是否在传送标记位置

        public GoToDlcMapBarcelona()
        {
            UI.Notify(modName); // 在游戏中显示通知
            Interval = 0; // 设置脚本执行间隔
            Tick += OnTick; // 注册每帧执行的逻辑
            KeyUp += OnKeyUp; // 注册按键松开时的逻辑

            // 创建传送标记
            teleportBlip = World.CreateBlip(teleportMarkerPos);
            teleportBlip.Sprite = BlipSprite.Helicopter;
            teleportBlip.Name = modName;
            teleportBlip.Color = BlipColor.Yellow;
            teleportBlip.IsShortRange = true;
        }

        private void OnTick(object sender, EventArgs e)
        {
            Ped playerPed = Game.Player.Character;
            // 绘制传送标记
            World.DrawMarker(MarkerType.VerticalCylinder, teleportMarkerPos, Vector3.Zero, Vector3.Zero, new Vector3(0.65f, 0.65f, 0.25f), Color.Yellow);
            World.DrawMarker(MarkerType.VerticalCylinder, teleportToPos, Vector3.Zero, Vector3.Zero, new Vector3(0.65f, 0.65f, 0.25f), Color.Yellow);

            // 检测玩家是否接近传送标记位置，并显示相应提示信息
            if (playerPed.Position.DistanceTo(teleportMarkerPos) < 5f)
            {
                atTeleportMarker = true;
                UI.ShowSubtitle("按 T 飞往~y~巴塞罗那加泰罗尼亚赛道", 125);
            }
            else if (playerPed.Position.DistanceTo(teleportToPos) < 5f)
            {
                atTeleportMarker = true;
                UI.ShowSubtitle("按 T 飞往~y~洛圣都", 125);
            }
            else
            {
                atTeleportMarker = false;
            }
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
//            if ((e.KeyCode == Keys.T || Game.IsControlJustPressed(0, GTA.Control.Context)) && atTeleportMarker)
            if (e.KeyCode == Keys.T && atTeleportMarker)
            {
                Ped playerPed = Game.Player.Character;
                if (playerPed.IsInVehicle())
                {
                    // 如果玩家在车辆中，传送车辆到目标位置
                    Game.FadeScreenOut(200);
                    Script.Wait(500);
                    playerPed.CurrentVehicle.Position = teleportToPos;
                    playerPed.Heading = 135.28f;
                    Script.Wait(500);
                    Game.FadeScreenIn(200);
                }
                else
                {
                    // 如果玩家不在车辆中，传送玩家到目标位置
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