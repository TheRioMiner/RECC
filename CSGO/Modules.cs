namespace Rio_External_Csgo_Cheat.CSGO
{
    public class Modules
    {
        private static uint _ClientDLL = 0;
        public static uint ClientDLL
        {
            get
            {
                if (_ClientDLL == 0)
                    _ClientDLL = (uint)KernelBlackMagic.Modules.FindAddress("client_panorama.dll");
                return _ClientDLL;
            }
        }

        private static uint _EngineDLL = 0;
        public static uint EngineDLL
        {
            get
            {
                if (_EngineDLL == 0)
                    _EngineDLL = (uint)KernelBlackMagic.Modules.FindAddress("engine.dll");
                return _EngineDLL;
            }
        }



    }
}
