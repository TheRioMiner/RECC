using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace Rio_External_Csgo_Cheat.CSGO
{
    public class Main
    {
        /// <summary>
        /// CSGO LIST OF OFFSETS
        /// </summary>
        public static Offsets offsets = new Offsets();


        /// <summary>
        /// Pointer to Client State
        /// </summary>
        public static uint ClientStatePtr
        {
            get
            {
                if (Modules.EngineDLL != 0)
                    return KernelBlackMagic.Read<uint>(Modules.EngineDLL + offsets["dwClientState"]);
                return 0;
            }
           
        }

        /// <summary>
        /// Index in entities list of local player
        /// Returns 'uint.MaxValue' when failed!
        /// </summary>
        public static uint LocalPlayerIndex
        {
            get
            {
                uint _ClientStatePtr = ClientStatePtr;
                if (_ClientStatePtr != 0)
                    return KernelBlackMagic.Read<uint>(_ClientStatePtr + offsets["dwClientState_GetLocalPlayer"]);
                return uint.MaxValue;
            }
        }

        /// <summary>
        /// Pointer to ClientState_PlayerInfo
        /// </summary>
        public static uint ClientStatePlayerInfoPtr
        {
            get
            {
                uint _ClientStatePtr = ClientStatePtr;
                if (_ClientStatePtr != 0)
                    return KernelBlackMagic.Read<uint>(_ClientStatePtr + offsets["dwClientState_PlayerInfo"]);
                return 0;
            }
        }

        /// <summary>
        /// Player on map?
        /// </summary>
        public static bool InGame
        {
            get
            {
                if (ClientStatePtr != 0)
                    return (KernelBlackMagic.Read<int>(ClientStatePtr + offsets["dwClientState_State"]) == 6);
                return false;
            }
        }



        /// <summary>
        /// Pointer to LocalPlayer
        /// </summary>
        public static uint LocalPlayerPtr
        {
            get
            {
                if (Modules.ClientDLL != 0)
                    return KernelBlackMagic.Read<uint>(Modules.ClientDLL + offsets["dwLocalPlayer"]);
                return 0;
            }
        }


        /// <summary>
        /// Pointer to GlowManager
        /// </summary>
        public static uint GlowObjectPtr
        {
            get
            {
                if (Modules.ClientDLL != 0)
                    return KernelBlackMagic.Read<uint>(Modules.ClientDLL + offsets["dwGlowObjectManager"]);
                return 0;
            }
        }


        /// <summary>
        /// Pointer to PlayerResources, like ranks and wins
        /// </summary>
        public static uint PlayerResourcesPtr
        {
            get
            {
                if (Modules.ClientDLL != 0)
                    return KernelBlackMagic.Read<uint>(Modules.ClientDLL + offsets["dwPlayerResource"]);
                return 0;
            }
        }




        public static double PlayersListUpdateTakenMs = 1;
        private static Dictionary<uint, PlayerEntity> _PlayersList = new Dictionary<uint, PlayerEntity>();

        /// <summary>
        /// Player list by index
        /// </summary>
        public static Dictionary<uint, PlayerEntity> PlayersList
        {
            get
            {
                lock (_PlayersList)
                    return new Dictionary<uint, PlayerEntity>(_PlayersList);
            }
        }

        

        public static void PlayerEntitiesUpdater_Thread()
        {
            while (!Program.IsClosing)
            {
                Stopwatch timer = Stopwatch.StartNew();
                if (InGame)
                {
                    //Обновляем текущую тиму у локального игрока
                    uint _localPlayerPtr = LocalPlayerPtr;
                    if (_localPlayerPtr != 0)
                        LocalPlayer.TeamNum = KernelBlackMagic.Read<uint>(_localPlayerPtr + offsets["m_iTeamNum"]);

                    //Обновляем всех игроков
                    for (uint Index = 0; Index < 64; Index++)
                    {
                        uint pointer = KernelBlackMagic.Read<uint>(Modules.ClientDLL + offsets["dwEntityList"] + Index * 16);
                        if (pointer != 0)
                        {
                            //Если игрок уже есть в списке
                            if (_PlayersList.ContainsKey(Index))
                                _PlayersList[Index].Update(pointer); //Обновляем его
                            else
                                lock (_PlayersList) //Игрока нету в списке, добавляем!
                                    _PlayersList.Add(Index, new PlayerEntity(pointer, Index));
                        }
                        else
                        {
                            if (_PlayersList.ContainsKey(Index))
                                lock (_PlayersList) //Удаляем устаревшую информацию
                                    _PlayersList.Remove(Index);
                        }
                    }
                }
                else
                {
                    lock (_PlayersList) //Мы не в игре, поэтому очищаем оставшийся мусор
                        _PlayersList.Clear();
                }
                timer.Stop();
                PlayersListUpdateTakenMs = timer.Elapsed.TotalMilliseconds;

                //Спим примерно 1 тик игры
                Thread.Sleep(16); 
            }
        }
    }
}
