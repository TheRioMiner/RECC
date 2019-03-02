using System;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.InteropServices;

using Rio_External_Csgo_Cheat.CSGO;
using static Rio_External_Csgo_Cheat.CSGO.Main;

namespace Rio_External_Csgo_Cheat.Cheats
{
    public class TriggerBot
    {
        [DllImport("user32.dll")]
        private static extern bool GetAsyncKeyState(Keys vKey);

        public enum TriggerDelayType : byte
        {
            NO_DELAY,
            HOLDING,
            DELAY,
        };

        public enum TriggerActivateMethod : byte
        {
            ALWAYS,
            ALT,
            SHIFT,
            WINKEY,
            MOUSE4,
            MOUSE5,
            E,
            X,
            Z,
        };

        public enum TriggerTeamType : byte
        {
            ENEMY,
            TEAMMATES,
            ALL,
        };



        public static bool Enabled = false;

        
        public static byte Delay = 20;
        public static byte DelaySpreadProcents = 30;
        public static TriggerDelayType DelayType = TriggerDelayType.DELAY;
        public static TriggerTeamType TeamActivate = TriggerTeamType.ENEMY;
        public static TriggerActivateMethod ActivateMethod = TriggerActivateMethod.E;
        private static Random _Random = new Random();



        public static void MainThread()
        {
            while (!Program.IsClosing)
            {
                if (!Enabled || !InGame)
                {
                    Thread.Sleep(500);
                    continue;
                }

                if (IsActivated()) //Можно триггерится?
                {
                    //Метод без задержки или с простой задержкой
                    if (DelayType == TriggerDelayType.NO_DELAY || DelayType == TriggerDelayType.DELAY)
                    {
                        uint localPlayerPtr = LocalPlayerPtr;
                        if (localPlayerPtr != 0)
                        {
                            int CrossHairID = (KernelBlackMagic.Read<int>(localPlayerPtr + offsets["m_iCrosshairId"]) - 1);
                            if (CrossHairID >= 0 && CrossHairID <= 63)
                            {
                                PlayerEntity player;
                                if (PlayersList.TryGetValue((uint)CrossHairID, out player))
                                {
                                    //Проверяем валидность тимы
                                    if (TeamActivate == TriggerTeamType.ENEMY && !player.IsEnemy)
                                        goto Exit;
                                    else if (TeamActivate == TriggerTeamType.TEAMMATES && player.IsEnemy)
                                        goto Exit;

                                    //Игрок не в неуязвимости
                                    if (!player.IsUmmunity) 
                                    {
                                        //Если метод простой задержки перед выстрелом, то ждем необходимую задержку!
                                        if (DelayType == TriggerDelayType.DELAY)
                                            Thread.Sleep(Delay + ((Delay * _Random.Next(-DelaySpreadProcents, DelaySpreadProcents)) / 100));

                                        //Атакуем!
                                        LocalPlayer.Attack((byte)_Random.Next(8, 16));

                                        //После клика немного поспим
                                        Thread.Sleep(_Random.Next(22, 48));
                                    }
                                }
                            }
                        }
                    }

                    //МЕТОД УДЕРЖАНИЯ
                    else if (DelayType == TriggerDelayType.HOLDING)
                    {
                        MessageBox.Show("Сука, кулхацкер взломал)0");
                        Thread.Sleep(5000);
                    }
                }
                Exit:
                Thread.Sleep(2); //2мс период
            }
        }



        private static bool IsActivated()
        {
            switch (ActivateMethod)
            {
                case TriggerActivateMethod.ALWAYS:
                    return true;
                case TriggerActivateMethod.ALT:
                    return GetAsyncKeyState(Keys.Menu);
                case TriggerActivateMethod.SHIFT:
                    return GetAsyncKeyState(Keys.ShiftKey);
                case TriggerActivateMethod.WINKEY:
                    return GetAsyncKeyState(Keys.LWin);
                case TriggerActivateMethod.MOUSE4:
                    return GetAsyncKeyState(Keys.XButton1);
                case TriggerActivateMethod.MOUSE5:
                    return GetAsyncKeyState(Keys.XButton2);
                case TriggerActivateMethod.E:
                    return GetAsyncKeyState(Keys.E);
                case TriggerActivateMethod.X:
                    return GetAsyncKeyState(Keys.X);
                case TriggerActivateMethod.Z:
                    return GetAsyncKeyState(Keys.Z);

                default:
                    return false;
            }
        }
    }
}
