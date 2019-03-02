using System;
using System.Drawing;
using System.Threading;
using static Rio_External_Csgo_Cheat.CSGO.Main;

namespace Rio_External_Csgo_Cheat.Cheats
{
    public class GlowWH
    {
        //Специальная структура для быстрой записи в память в 2 захода
        public class GlowParams
        {
            public byte[] ColorArray;
            public byte[] RenderArray;

            public Color InitColor;


            public bool IsValid
            {
                get
                {
                    if (ColorArray != null && RenderArray != null)
                        return (ColorArray.Length == 16) && (RenderArray.Length == 2);
                    else
                        return false;
                }
            }


            public GlowParams(byte r, byte g, byte b, byte a, bool renderOccluded, bool renderUnocludded)
            {
                InitColor = Color.FromArgb(a, r, g, b);
                float reuslt = (float)Math.Round(r / 255f, 1);
                byte[] rArr = BitConverter.GetBytes(reuslt);
                byte[] gArr = BitConverter.GetBytes((float)Math.Round(g / 255f, 1));
                byte[] bArr = BitConverter.GetBytes((float)Math.Round(b / 255f, 1));
                byte[] aArr = BitConverter.GetBytes((float)Math.Round(a / 255f, 1));
                ColorArray = new byte[]
                {
                    rArr[0], rArr[1], rArr[2], rArr[3],
                    gArr[0], gArr[1], gArr[2], gArr[3],
                    bArr[0], bArr[1], bArr[2], bArr[3],
                    aArr[0], aArr[1], aArr[2], aArr[3],
                };
                RenderArray = new byte[] { (renderOccluded ? (byte)1 : (byte)0), (renderUnocludded ? (byte)1 : (byte)0) };
            }


            public void Glow(uint GlowObjectPtr, uint GlowIndex)
            {
                if (IsValid)
                {
                    KernelBlackMagic.WriteBytes(GlowObjectPtr + (GlowIndex * 0x38) + 0x4, ColorArray);
                    KernelBlackMagic.WriteBytes(GlowObjectPtr + (GlowIndex * 0x38) + 0x24, RenderArray);
                }
            }
        }



        public static bool Enabled = false;


        public static bool GlowEnemiesEnabled = true;
        public static GlowParams EnemiesGlow = new GlowParams(255, 255, 0, 128, true, false);

        public static bool GlowTeammatesEnabled = false;
        public static GlowParams TeammatesGlow = new GlowParams(255, 0, 0, 128, true, false);

        public static bool GlowDefusingEnabled = true;
        public static GlowParams DefusingGlow = new GlowParams(65, 105, 225, 255, true, false);



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
                    uint _glowObjPtr = GlowObjectPtr;
                    if (_glowObjPtr != 0)
                    {
                        //Если хоть что-то подсвечиваем
                        if (GlowEnemiesEnabled || GlowTeammatesEnabled || GlowDefusingEnabled)
                        {
                            foreach (var player in PlayersList.Values) //Проходимся в цикле по игрокам
                            {
                                if (player.IsDefusing && GlowDefusingEnabled)
                                    DefusingGlow.Glow(_glowObjPtr, player.GlowIndex); //Сраный нинзя дефюзер
                                else if (player.IsEnemy && GlowEnemiesEnabled)
                                    EnemiesGlow.Glow(_glowObjPtr, player.GlowIndex); //ВРАГ НАРОДА!!!
                                else if (player.IsTeammate && GlowTeammatesEnabled)
                                    TeammatesGlow.Glow(_glowObjPtr, player.GlowIndex); //Союзник
                                
                            }
                        }
                    }
                }
                Thread.Sleep(2); //2мс период
            }
        }
    }
}
