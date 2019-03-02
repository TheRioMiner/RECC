using System;
using System.Net;
using System.Text;

namespace Rio_External_Csgo_Cheat
{
    public static class Tools
    {
        public static string DownloadHtml(string url)
        {
            using (var client = new WebClient())
            {
                client.Encoding = Encoding.UTF8;
                return client.DownloadString(url);
            }
        }


        public static uint GetClassId(uint EntityPlayer)
        {
            uint one = KernelBlackMagic.Read<uint>(EntityPlayer + 0x8);
            if (one != 0)
            {
                uint two = KernelBlackMagic.Read<uint>(one + 0x8);
                if (two != 0)
                {
                    uint three = KernelBlackMagic.Read<uint>(two + 0x1);
                    if (three != 0)
                        return KernelBlackMagic.Read<uint>(three + 0x14);
                }
            }
            return 0;
        }
    }
}
