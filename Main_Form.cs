using System;
using System.Drawing;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using MetroFramework.Forms;
using MetroFramework.Controls;

using static Rio_External_Csgo_Cheat.CSGO.Main;

namespace Rio_External_Csgo_Cheat
{
    public partial class Main_Form : MetroForm
    {
        //MAIN ENTITIES UPDATE THREAD
        Thread updateEntities_Thread = new Thread(new ThreadStart(PlayerEntitiesUpdater_Thread));

        //CHEATS THREADS
        Thread triggerBot_Thread = new Thread(new ThreadStart(Cheats.TriggerBot.MainThread));
        Thread glowWh_Thread = new Thread(new ThreadStart(Cheats.GlowWH.MainThread));
        Thread bunnyHop_Thread = new Thread(new ThreadStart(Cheats.Bunnyhop.MainThread));
        Thread antiFlash_Thread = new Thread(new ThreadStart(Cheats.AntiFlash.MainThread));
        //Thread noRecoil_Thread = new Thread(new ThreadStart(Cheats.NoRecoil.MainThread));



        //Форма со списком игроков и званий
        ShowRanks_Form ranks_Form;



        public Main_Form()
        {
            //Инициализируем компоненты формы
            InitializeComponent();
            {
                //LIST's INIT
                triggerBot_ActivateTeam_metroComboBox.SelectedIndex = 0;
                triggerBot_ActivateKey_metroComboBox.SelectedIndex = 1;
                triggerBot_DelayType_metroComboBox.SelectedIndex = 1;
                triggerBot_Delay_metroComboBox.SelectedIndex = 2;
                triggerBot_DelaySpread_metroComboBox.SelectedIndex = 5;
                glowWh_Opacity_metroComboBox.SelectedIndex = 6;
                glowWh_TeammatesOpacity_metroComboBox.SelectedIndex = 3;
                glowWh_DefuserOpacity_metroComboBox.SelectedIndex = 3;
                bunnyHop_Period_metroComboBox.SelectedIndex = 2;
                antiFlash_SupressAmount_metroComboBox.SelectedIndex = 14;

                //GUI UNIT
                reccVersion_metroLabel.Text = $"v{Application.ProductVersion}";
                myId_metroLabel.Text = $"ID: {Program.MyCRC}";
            }


            //Запускаем таймер для обновления инфы
            textUpdater_timer.Enabled = true;


            //ГЛАВНЫЙ ПОТОК ОБНОВЛЕНИЯ ИГРОКОВ
            updateEntities_Thread.Priority = ThreadPriority.Lowest;
            updateEntities_Thread.Start();


            //CHEATS THREADS STARTUP
            triggerBot_Thread.Priority = ThreadPriority.Lowest;
            triggerBot_Thread.Start();

            glowWh_Thread.Priority = ThreadPriority.Lowest;
            glowWh_Thread.Start();

            bunnyHop_Thread.Priority = ThreadPriority.Lowest;
            bunnyHop_Thread.Start();

            antiFlash_Thread.Priority = ThreadPriority.Lowest;
            antiFlash_Thread.Start();

            //noRecoil_Thread.Priority = ThreadPriority.Lowest;
            //noRecoil_Thread.Start();
        }


        private void Main_Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Вы точно уверены? Закрыть RECC?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                Program.IsClosing = true; //Сообщаем всем, что мы закрываем лавочку!
            else
                e.Cancel = true; //Отменяем выход!
        }


        private void textUpdater_timer_Tick(object sender, EventArgs e)
        {
            status_metroLabel.Text = $"Статус: {(InGame ? "В игре" : "Не на карте!")}";
            pulse_metroLabel.Text = $"Пульс: {Math.Round(PlayersListUpdateTakenMs, 3)} мс";
        }




        #region MAIN

        private void mySteamLink_metroLink_Click(object sender, EventArgs e)
        {
            Process.Start("https://steamcommunity.com/id/kernelblackmagic/");
        }

        private void showRanks_metroButton_Click(object sender, EventArgs e)
        {
            if (ranks_Form == null || ranks_Form.IsDisposed)
                ranks_Form = new ShowRanks_Form();

            ranks_Form.Visible = false;
            ranks_Form.Show(this);
        }


        private void ranksCalc_metroButton_Click(object sender, EventArgs e)
        {
            if (InGame)
            {
                int ttAdded = 0;
                int ttRanks = 0;
                int ctAdded = 0;
                int ctRanks = 0;

                foreach (var player in PlayersList.Values)
                {
                    uint rank = player.GetRank();
                    if (player.TeamNum == 2) //TT
                    {
                        ttRanks += (int)rank;
                        ttAdded++;
                    }
                    else if (player.TeamNum == 3) //CT
                    {
                        ctAdded++;
                        ctRanks += (int)rank;
                    }
                }

                //Вычисление общего ранга команд
                int diff = Math.Abs(ttRanks - ctRanks);
                string favorDiff = ttRanks > ctRanks ? "в пользу террористов." : ttRanks < ctRanks ? "в пользу спецназа." : "";
                MessageBox.Show($"Общие вместе звания - ТТ: {ttRanks}, CT: {ctRanks}, Разница: {diff} {favorDiff}");

                //Вычисление среднего арифм. ранга команд
                float ttMedium = ttRanks / (float)ttAdded;
                float ctMedium = ctRanks / (float)ctAdded;
                float mediumDiff = (float)Math.Round(Math.Abs(ttMedium - ctMedium), 2);
                string favorMediumDiff = ttMedium > ctMedium ? "в пользу террористов." : ttMedium < ctMedium ? "в пользу спецназа." : "";
                MessageBox.Show($"Средний арифм. звания - TT: {ttMedium}, CT: {ctMedium}, Разница: {mediumDiff} {favorMediumDiff}");
            }
            else
                MessageBox.Show("Вы не в игре!");
        }

        #endregion




        #region TriggerBot

        private void triggerBot_metroToggle_CheckedChanged(object sender, EventArgs e)
        {
            triggerBot_metroPanel.Enabled = triggerBot_metroToggle.Checked;
            Cheats.TriggerBot.Enabled = triggerBot_metroToggle.Checked;
        }

        private void triggerBot_ActivateTeam_metroComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = triggerBot_ActivateTeam_metroComboBox.Text;
            if (str == "На врагов")
                Cheats.TriggerBot.TeamActivate = Cheats.TriggerBot.TriggerTeamType.ENEMY;
            else if (str == "На всех")
                Cheats.TriggerBot.TeamActivate = Cheats.TriggerBot.TriggerTeamType.ALL;
            else if (str == "На союзников")
                Cheats.TriggerBot.TeamActivate = Cheats.TriggerBot.TriggerTeamType.TEAMMATES;
        }

        private void triggerBot_ActivateKey_metroComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = triggerBot_ActivateKey_metroComboBox.Text;
            if (str == "Всегда")
                Cheats.TriggerBot.ActivateMethod = Cheats.TriggerBot.TriggerActivateMethod.ALWAYS;
            else if (str == "ALT")
                Cheats.TriggerBot.ActivateMethod = Cheats.TriggerBot.TriggerActivateMethod.ALT;
            else if (str == "SHIFT")
                Cheats.TriggerBot.ActivateMethod = Cheats.TriggerBot.TriggerActivateMethod.SHIFT;
            else if (str == "WINKEY")
                Cheats.TriggerBot.ActivateMethod = Cheats.TriggerBot.TriggerActivateMethod.WINKEY;
            else if (str == "MOUSE4")
                Cheats.TriggerBot.ActivateMethod = Cheats.TriggerBot.TriggerActivateMethod.MOUSE4;
            else if (str == "MOUSE5")
                Cheats.TriggerBot.ActivateMethod = Cheats.TriggerBot.TriggerActivateMethod.MOUSE5;
            else if (str == "E")
                Cheats.TriggerBot.ActivateMethod = Cheats.TriggerBot.TriggerActivateMethod.E;
            else if (str == "X")
                Cheats.TriggerBot.ActivateMethod = Cheats.TriggerBot.TriggerActivateMethod.X;
            else if (str == "Z")
                Cheats.TriggerBot.ActivateMethod = Cheats.TriggerBot.TriggerActivateMethod.Z;
        }

        private void triggerBot_DelayType_metroComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = triggerBot_DelayType_metroComboBox.Text;
            if (str == "Без задержки")
            {
                triggerBot_Delay_metroComboBox.Enabled = false;
                triggerBot_DelaySpread_metroComboBox.Enabled = false;
                Cheats.TriggerBot.DelayType = Cheats.TriggerBot.TriggerDelayType.NO_DELAY;
            }
            else if (str == "Простая")
            {
                triggerBot_Delay_metroComboBox.Enabled = true;
                triggerBot_DelaySpread_metroComboBox.Enabled = true;
                Cheats.TriggerBot.DelayType = Cheats.TriggerBot.TriggerDelayType.DELAY;
            }
            //else if (str == "Удержание")
            //    Cheats.TriggerBot.DelayType = Cheats.TriggerBot.TriggerDelayType.HOLDING;
        }

        private void triggerBot_Delay_metroComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            byte delay = 15;
            byte.TryParse(triggerBot_Delay_metroComboBox.Text.Replace("мс", "").Trim(), out delay);
            Cheats.TriggerBot.Delay = delay;
        }

        private void triggerBot_DelaySpread_metroComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            byte spread = 15;
            byte.TryParse(triggerBot_DelaySpread_metroComboBox.Text.Replace("±", "").Replace("%", "").Trim(), out spread);
            Cheats.TriggerBot.DelaySpreadProcents = spread;
        }



        #endregion




        #region Glow Wallhack

        private void glowWh_metroToggle_CheckedChanged(object sender, EventArgs e)
        {
            glowWh_metroPanel.Enabled = glowWh_metroToggle.Checked;
            Cheats.GlowWH.Enabled = glowWh_metroToggle.Checked;
        }


        private void glowWh_Enemies_metroToggle_CheckedChanged(object sender, EventArgs e)
        {
            Cheats.GlowWH.GlowEnemiesEnabled = (sender as MetroToggle).Checked;
        }

        private void glowWh_Teammates_metroToggle_CheckedChanged(object sender, EventArgs e)
        {
            Cheats.GlowWH.GlowTeammatesEnabled = (sender as MetroToggle).Checked;
        }

        private void glowWh_Defuser_metroToggle_CheckedChanged(object sender, EventArgs e)
        {
            Cheats.GlowWH.GlowDefusingEnabled = (sender as MetroToggle).Checked;
        }


        private void glowWh_Color_metroTile_Click(object sender, EventArgs e)
        {
            using (var colorDialog = new ColorDialog())
            {
                colorDialog.Color = Cheats.GlowWH.EnemiesGlow.InitColor;

                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    Color c = colorDialog.Color;
                    byte alpha = GetGlowWhAlpha(glowWh_Opacity_metroComboBox.Text);
                    Cheats.GlowWH.EnemiesGlow = new Cheats.GlowWH.GlowParams(c.R, c.G, c.B, alpha, true, false);
                    glowWh_Color_metroTile.BackColor = c;
                }
            }
        }

        private void glowWh_TeamColor_metroTile_Click(object sender, EventArgs e)
        {
            using (var colorDialog = new ColorDialog())
            {
                colorDialog.Color = Cheats.GlowWH.TeammatesGlow.InitColor;

                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    Color c = colorDialog.Color;
                    byte alpha = GetGlowWhAlpha(glowWh_TeammatesOpacity_metroComboBox.Text);
                    Cheats.GlowWH.TeammatesGlow = new Cheats.GlowWH.GlowParams(c.R, c.G, c.B, alpha, true, false);
                    glowWh_TeamColor_metroTile.BackColor = c;
                }
            }
        }

        private void glowWh_DefuserColor_metroTile_Click(object sender, EventArgs e)
        {
            using (var colorDialog = new ColorDialog())
            {
                colorDialog.Color = Cheats.GlowWH.DefusingGlow.InitColor;

                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    Color c = colorDialog.Color;
                    byte alpha = GetGlowWhAlpha(glowWh_DefuserOpacity_metroComboBox.Text);
                    Cheats.GlowWH.DefusingGlow = new Cheats.GlowWH.GlowParams(c.R, c.G, c.B, alpha, true, false);
                    glowWh_DefuserColor_metroTile.BackColor = c;
                }
            }
        }


        private void glowWh_Opacity_metroComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Color c = glowWh_Color_metroTile.BackColor;
            byte alpha = GetGlowWhAlpha(glowWh_Opacity_metroComboBox.Text);
            Cheats.GlowWH.EnemiesGlow = new Cheats.GlowWH.GlowParams(c.R, c.G, c.B, alpha, true, false);
        }

        private void glowWh_TeammatesOpacity_metroComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Color c = glowWh_TeamColor_metroTile.BackColor;
            byte alpha = GetGlowWhAlpha(glowWh_TeammatesOpacity_metroComboBox.Text);
            Cheats.GlowWH.TeammatesGlow = new Cheats.GlowWH.GlowParams(c.R, c.G, c.B, alpha, true, false);
        }

        private void glowWh_DefuserOpacity_metroComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Color c = glowWh_DefuserColor_metroTile.BackColor;
            byte alpha = GetGlowWhAlpha(glowWh_DefuserOpacity_metroComboBox.Text);
            Cheats.GlowWH.DefusingGlow = new Cheats.GlowWH.GlowParams(c.R, c.G, c.B, alpha, true, false);
        }


        private byte GetGlowWhAlpha(string str)
        {
            byte alpha = 100;
            byte.TryParse(str.Replace("%", "").Trim(), out alpha);
            return (byte)(alpha * 2.55f); //При неудаче всеравно возвратит 255
        }



        #endregion




        #region Bunnyhop

        private void bunnyHop_metroToggle_CheckedChanged(object sender, EventArgs e)
        {
            bunnyHop_metroPanel.Enabled = bunnyHop_metroToggle.Checked;
            Cheats.Bunnyhop.Enabled = bunnyHop_metroToggle.Checked;
        }

        private void bunnyHop_Period_metroComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = bunnyHop_Period_metroComboBox.Text;
            if (str == "Медленно")
                Cheats.Bunnyhop.Period = 16;
            else if (str == "Нормально")
                Cheats.Bunnyhop.Period = 8;
            else if (str == "Быстро")
                Cheats.Bunnyhop.Period = 2;
        }



        #endregion




        #region Anti Flash

        private void antiFlash_metroToggle_CheckedChanged(object sender, EventArgs e)
        {
            antiFlash_metroPanel.Enabled = antiFlash_metroToggle.Checked;
            Cheats.AntiFlash.Enabled = antiFlash_metroToggle.Checked;

            if (!antiFlash_metroToggle.Checked)
                Cheats.AntiFlash.Disable();
        }

        private void antiFlash_SupressAmount_metroComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            byte supress = 50;
            byte.TryParse(antiFlash_SupressAmount_metroComboBox.Text.Replace("%", "").Trim(), out supress);
            Cheats.AntiFlash.SuppressAmount = (byte)(supress * 2.55f);
        }


        #endregion
    }
}
