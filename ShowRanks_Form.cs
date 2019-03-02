using System;
using System.Linq;
using System.Drawing;
using System.Diagnostics;
using MetroFramework.Forms;
using System.Collections.Generic;

using static Rio_External_Csgo_Cheat.CSGO.Main;

namespace Rio_External_Csgo_Cheat
{
    public partial class ShowRanks_Form : MetroForm
    {
        public ShowRanks_Form()
        {
            InitializeComponent();
        }


        private void ShowRanks_Form_Load(object sender, EventArgs e)
        {
            refresh_metroButton.PerformClick();
        }


        private void refresh_metroButton_Click(object sender, EventArgs e)
        {
            metroGrid.Rows.Clear();
            if (InGame)
            {
                Stopwatch timer = Stopwatch.StartNew();
                List<object[]> PlayerRowsData = new List<object[]>();
                foreach (var player in PlayersList.Values)
                {
                    var playerInfo = player.GetInfo();
                    if (playerInfo != null)
                    {
                        //Заполняем во временный буфер
                        object[] rowData = new object[]
                        {
                        player.TeamNum, //InvisibleTeamNum for sorting
                        GetTeamBitmap(player.TeamNum, playerInfo.IsHltv),
                        playerInfo.Name,
                        GetRankBitmap(player.GetRank()),
                        player.GetWins(),
                        playerInfo.SteamId == "BOT" ? "BOT" : playerInfo.GetSteamID64().ToString(),
                        player.Index,
                        playerInfo.UserId
                        };
                        PlayerRowsData.Add(rowData);
                    }
                    else
                    {
                        //PLAYERINFO HAS NULL!!!
                        object[] rowData = new object[]
                        {
                        player.TeamNum, //InvisibleTeamNum for sorting
                        GetTeamBitmap(player.TeamNum, false),
                        "<PLAYERINFO IS NULL>",
                        GetRankBitmap(player.GetRank()),
                        player.GetWins(),
                        0,
                        player.Index,
                        -1,
                        };
                        PlayerRowsData.Add(rowData);
                    }
                }
                timer.Stop();

                //Сортируем и выплевываем все из буфера на форму
                foreach (var rowData in PlayerRowsData.OrderBy(o => o[0]).ToList())
                    metroGrid.Rows.Add(rowData);

                //Обновляем текст
                status_metroLabel.Text = $"Статус: {PlayersList.Values.Count} ентити за {Math.Round(timer.Elapsed.TotalMilliseconds, 3)} мс";
            }
            else //Если мы не в игре, пишем об этом
                status_metroLabel.Text = "Статус: Не в игре!";
        }



        public static Bitmap GetTeamBitmap(uint teamNum, bool isHltv)
        {
            if (teamNum == 2)
                return Properties.Resources.team_t;
            else if (teamNum == 3)
                return Properties.Resources.team_ct;
            else if (teamNum == 0 && isHltv)
                return Properties.Resources.team_hltv;
            else
                return Properties.Resources.team_invalid;
        }


        public static Bitmap GetRankBitmap(uint rank)
        {
            switch (rank)
            {
                case 0:
                    return Properties.Resources.rank_unranked;

                case 1:
                    return Properties.Resources.rank_silver1;
                case 2:
                    return Properties.Resources.rank_silver2;
                case 3:
                    return Properties.Resources.rank_silver3;
                case 4:
                    return Properties.Resources.rank_silver4;
                case 5:
                    return Properties.Resources.rank_silver5;
                case 6:
                    return Properties.Resources.rank_silver6;

                case 7:
                    return Properties.Resources.rank_goldnova1;
                case 8:
                    return Properties.Resources.rank_goldnova2;
                case 9:
                    return Properties.Resources.rank_goldnova3;
                case 10:
                    return Properties.Resources.rank_goldnova4;
                case 11:
                    return Properties.Resources.rank_ak1;
                case 12:
                    return Properties.Resources.rank_ak2;

                case 13:
                    return Properties.Resources.rank_twoak;
                case 14:
                    return Properties.Resources.rank_bigstar;
                case 15:
                    return Properties.Resources.rank_eagle1;
                case 16:
                    return Properties.Resources.rank_eagle2;
                case 17:
                    return Properties.Resources.rank_supreme;
                case 18:
                    return Properties.Resources.rank_global;

                default:
                    return null;
            }
        }
    }
}
