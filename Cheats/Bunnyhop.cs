using System.Threading;
using System.Windows.Forms;
using System.Runtime.InteropServices;

using Rio_External_Csgo_Cheat.CSGO;
using static Rio_External_Csgo_Cheat.CSGO.Main;

namespace Rio_External_Csgo_Cheat.Cheats
{
    public class Bunnyhop
    {
        [DllImport("user32.dll")]
        private static extern bool GetAsyncKeyState(Keys vKey);


        public static bool Enabled = false;

        public static byte Period = 2;


        public static void MainThread()
        {
            while (!Program.IsClosing)
            {
                if (!Enabled || !InGame)
                {
                    Thread.Sleep(500);
                    continue;
                }
                {
                    //Если нажали пробел
                    if (GetAsyncKeyState(Keys.Space))
                    {
                        //Получаем состояние игрока
                        uint fFlags = LocalPlayer.fFlags;

                        //Если клавиша нажата и игрок не в воздухе
                        if (fFlags % 2 == 0)
                        {
                            KernelBlackMagic.Write(Modules.ClientDLL + offsets["dwForceJump"], 4); //"Un"force jump
                            KernelBlackMagic.Write(Modules.ClientDLL + offsets["dwForceJump"], 5); //Force jump
                            KernelBlackMagic.Write(Modules.ClientDLL + offsets["dwForceJump"], 4); //"Un"force jump
                        }
                        else if (!(fFlags % 2 == 0)) 
                            KernelBlackMagic.Write(Modules.ClientDLL + offsets["dwForceJump"], 5); //Force jump
                        else
                            KernelBlackMagic.Write(Modules.ClientDLL + offsets["dwForceJump"], 4); //"Un"force jump
                    }
                }
                Thread.Sleep(Period);
            }
        }
    }
}
