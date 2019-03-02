using System.Threading;

using static Rio_External_Csgo_Cheat.CSGO.Main;

namespace Rio_External_Csgo_Cheat.CSGO
{
    public class LocalPlayer
    {
        public static uint Ptr => LocalPlayerPtr;
        public static uint TeamNum = 0;

        /// <summary>
        /// LocalPlayer state flags
        /// </summary>
        public static uint fFlags
        {
            get
            {
                uint _localPlayerPtr = Ptr;
                if (_localPlayerPtr != 0)
                    return KernelBlackMagic.Read<uint>(_localPlayerPtr + offsets["m_fFlags"]);
                return 0;
            }
        }


        /// <summary>
        /// LocalPlayer Entity
        /// If failed returns 'null'
        /// </summary>
        public static PlayerEntity Entity
        {
            get
            {
                PlayerEntity player = null;
                PlayersList.TryGetValue(LocalPlayerIndex, out player);
                return player; //When failed returns null
            }
        }



        public static void Attack(byte pressTime)
        {
            KernelBlackMagic.Write(Modules.ClientDLL + offsets["dwForceAttack"], 5);
            Thread.Sleep(pressTime);
            KernelBlackMagic.Write(Modules.ClientDLL + offsets["dwForceAttack"], 4);
        }

        public static void Attack2(byte pressTime)
        {
            KernelBlackMagic.Write(Modules.ClientDLL + offsets["dwForceAttack2"], 5);
            Thread.Sleep(pressTime);
            KernelBlackMagic.Write(Modules.ClientDLL + offsets["dwForceAttack2"], 4);
        }

        public static void Jump(byte pressTime)
        {
            KernelBlackMagic.Write(Modules.ClientDLL + offsets["dwForceJump"], 5);
            Thread.Sleep(pressTime);
            KernelBlackMagic.Write(Modules.ClientDLL + offsets["dwForceJump"], 4);
        }
    }
}
