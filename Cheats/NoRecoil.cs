using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using static Rio_External_Csgo_Cheat.CSGO.Main;

namespace Rio_External_Csgo_Cheat.Cheats
{
    //public class NoRecoil
    //{
    //    public static bool Enabled = false; //TODO: по дефолту должно false

    //    public static Vector2 OldAimPunch = new Vector2();
    //    public static uint OldShotsFired = 0;



    //    public static void MainThread()
    //    {
    //        while (!Program.IsClosing)
    //        {
    //            if (!Enabled || !InGame) //Чит не включен или не в игре
    //            {
    //                Thread.Sleep(500);
    //                continue;
    //            }
    //            {
    //                uint _LocalPlayerPtr = LocalPlayerPtr;
    //                uint _ClientStatePtr = ClientStatePtr;
    //                if (_LocalPlayerPtr != 0 && _ClientStatePtr != 0)
    //                {
    //                    bool state = KernelBlackMagic.Read<bool>(_LocalPlayerPtr + offsets["m_bIsDefusing"]);

    //                    if (state)
    //                    { }

    //                    //uint shotsFired = KernelBlackMagic.Read<uint>(LocalPlayerPtr + offsets["m_iShotsFired"]);
    //                    //if (false) //(shotsFired != OldShotsFired)
    //                    //{
    //                    //    if (shotsFired > 0 && shotsFired <= 4)
    //                    //    {
    //                    //        var AimPunch = KernelBlackMagic.Read<Vector2>(LocalPlayerPtr + offsets["m_aimPunchAngle"]);
    //                    //        var viewAngle = KernelBlackMagic.Read<Vector2>(ClientStatePtr + offsets["dwClientState_ViewAngles"]);
    //                    //        var newViewAngle = new Vector2(viewAngle - ((AimPunch * 2.0f) - OldAimPunch));
    //                    //        OldAimPunch = AimPunch;
    //                    //        KernelBlackMagic.Write(ClientStatePtr + offsets["dwClientState_ViewAngles"], newViewAngle);
    //                    //    }
    //                    //    OldShotsFired = shotsFired;
    //                    //}
    //                }
    //            }
    //            Thread.Sleep(4);
    //        }
    //    }



    //    private static Vector2 NormaliseViewAngle(Vector2 angle)
    //    {
    //        while (angle.Y <= -180) angle.Y += 360;
    //        while (angle.Y > 180) angle.Y -= 360;
    //        while (angle.X <= -180) angle.X += 360;
    //        while (angle.X > 180) angle.X -= 360;


    //        if (angle.X > 89) angle.X = 89;
    //        if (angle.X < -89) angle.X = -89;
    //        if (angle.Y < -180) angle.Y = -179.999f;
    //        if (angle.Y > 180) angle.Y = 179.999f;

    //        return angle;
    //    }
    //}
}
