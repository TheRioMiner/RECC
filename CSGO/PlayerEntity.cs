using static Rio_External_Csgo_Cheat.CSGO.Main;

namespace Rio_External_Csgo_Cheat.CSGO
{
    public class PlayerEntity
    {
        public uint Pointer;
        public uint Index;
        public uint GlowIndex;
        public bool IsUmmunity;
        public bool IsDefusing;
        public uint TeamNum;
        public uint Health;

        public bool IsValid => (TeamNum == 2 || TeamNum == 3);
        public bool IsEnemy => (LocalPlayer.TeamNum != TeamNum);
        public bool IsTeammate => (LocalPlayer.TeamNum == TeamNum);
        public bool IsDead => (Health == 0);


        public PlayerEntity(uint Pointer, uint Index)
        {
            this.Pointer = Pointer;
            this.Index = Index;
        }



        /// <summary>
        /// Update the internal player data
        /// </summary>
        public void Update()
        {
            this.Update(Pointer);
        }

        /// <summary>
        /// Update the internal player data
        /// </summary>
        public void Update(uint Pointer)
        {
            this.Pointer = Pointer;
            this.GlowIndex = KernelBlackMagic.Read<uint>(Pointer + offsets["m_iGlowIndex"]);
            this.IsUmmunity = (KernelBlackMagic.Read<byte>(Pointer + offsets["m_bGunGameImmunity"]) == 1);
            this.IsDefusing = KernelBlackMagic.Read<bool>(Pointer + offsets["m_bIsDefusing"]);
            this.TeamNum = KernelBlackMagic.Read<uint>(Pointer + offsets["m_iTeamNum"]);
            this.Health = KernelBlackMagic.Read<uint>(Pointer + offsets["m_iHealth"]);
        }



        /// <summary>
        /// Get MM player ranking
        /// </summary>
        public uint GetRank()
        {
            uint _playerResourcesPtr = PlayerResourcesPtr;
            if (_playerResourcesPtr != 0)
                return KernelBlackMagic.Read<uint>(_playerResourcesPtr + offsets["m_iCompetitiveRanking"] + (Index+1) * 0x4);
            return 0;
        }

        /// <summary>
        /// Get MM wins
        /// </summary>
        public uint GetWins()
        {
            uint _playerResourcesPtr = PlayerResourcesPtr;
            if (_playerResourcesPtr != 0)
                return KernelBlackMagic.Read<uint>(_playerResourcesPtr + offsets["m_iCompetitiveWins"] + (Index + 1) * 0x4);
            return 0;
        }


        /// <summary>
        /// Gets the PlayerInfo, name, steamid64, etc
        /// </summary>
        public PlayerInfo GetInfo() => PlayerInfo.GetByIndex(Index);
    }
}
