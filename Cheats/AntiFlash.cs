using System.Threading;
using static Rio_External_Csgo_Cheat.CSGO.Main;

namespace Rio_External_Csgo_Cheat.Cheats
{
    public class AntiFlash
    {
        public static bool Enabled = false;

        public static float SuppressAmount = 128f;



        public static void MainThread()
        {
            while (!Program.IsClosing)
            {
                if (!Enabled || !InGame) //Чит не включен или не в игре
                {
                    Thread.Sleep(500);
                    continue;
                }
                {
                    uint _localPlayerPtr = LocalPlayerPtr;
                    if (_localPlayerPtr != 0)
                    {
                        float _alpha = 255f - SuppressAmount;

                        //Чекаем лимиты
                        if (_alpha > 255f)
                            _alpha = 255f;
                        else if (_alpha < 0f)
                            _alpha = 0;

                        //Хакаем
                        KernelBlackMagic.Write(_localPlayerPtr + offsets["m_flFlashMaxAlpha"], _alpha);
                    }
                }
                Thread.Sleep(500);
            }
        }


        public static void Disable()
        {
            uint _localPlayerPtr = LocalPlayerPtr;
            if (InGame && _localPlayerPtr != 0)
                KernelBlackMagic.Write(_localPlayerPtr + offsets["m_flFlashMaxAlpha"], 255f);
        }
    }
}
