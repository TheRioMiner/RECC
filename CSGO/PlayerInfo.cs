using System;

using static Rio_External_Csgo_Cheat.CSGO.Main;

namespace Rio_External_Csgo_Cheat.CSGO
{
    /// <summary>
    /// Класс с информацией о игроке
    /// </summary>
    public class PlayerInfo
    {
        public uint PlayerInfoStructPtr = 0; //Pointer to struct in memory

        public string Name = "";    //0x10, Lenght=128
        public int UserId;          //0x93
        public string SteamId = ""; //0x94, Lenght=36
        public bool FakePlayer;     //0x139
        public bool IsHltv;         //0x13A


        public PlayerInfo()
        { }

        public PlayerInfo(uint PlayerInfoStructPtr)
        {
            this.PlayerInfoStructPtr = PlayerInfoStructPtr;
        }


        public long GetSteamID64()
        {
            if (SteamId.StartsWith("STEAM_"))
            {
                string[] split = SteamId.Replace("STEAM_", "").Split(':');
                return (Convert.ToInt64(split[2]) * 2) + 76561197960265728 + Convert.ToByte(split[1]);
            }
            return 0;
        }


        public bool Fill()
        {
            return this.Fill(PlayerInfoStructPtr);
        }

        public bool Fill(uint PlayerInfoStructPtr)
        {
            this.PlayerInfoStructPtr = PlayerInfoStructPtr;
            if (PlayerInfoStructPtr != 0)
            {
                Name = KernelBlackMagic.ReadString(PlayerInfoStructPtr + 0x10, 128);
                UserId = KernelBlackMagic.Read<byte>(PlayerInfoStructPtr + 0x93);
                SteamId = KernelBlackMagic.ReadString(PlayerInfoStructPtr + 0x94, 36);
                FakePlayer = KernelBlackMagic.Read<bool>(PlayerInfoStructPtr + 0x139);
                IsHltv = KernelBlackMagic.Read<bool>(PlayerInfoStructPtr + 0x13A);
                return true;
            }
            return false;
        }


        /// <summary>
        /// Get PlayerInfo by Index from entities list
        /// </summary>
        public static PlayerInfo GetByIndex(uint Index)
        {
            uint _playerInfoPtr = ClientStatePlayerInfoPtr;
            if (_playerInfoPtr != 0)
            {
                uint someClassOne = KernelBlackMagic.Read<uint>(_playerInfoPtr + 0x40);
                if (someClassOne != 0)
                {
                    uint someClassTwo = KernelBlackMagic.Read<uint>(someClassOne + 0xC);
                    if (someClassTwo != 0)
                    {
                        uint pInfo = KernelBlackMagic.Read<uint>(someClassTwo + 0x28 + (Index * 0x34));
                        PlayerInfo info = new PlayerInfo();
                        info.Fill(pInfo);
                        return info;
                    }
                }
            }
            return null;
        }
    }
}
