namespace Rio_External_Csgo_Cheat
{
    partial class ShowRanks_Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShowRanks_Form));
            this.metroGrid = new MetroFramework.Controls.MetroGrid();
            this.SortRow = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.C_Team = new System.Windows.Forms.DataGridViewImageColumn();
            this.C_Nick = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.C_Rank = new System.Windows.Forms.DataGridViewImageColumn();
            this.C_Wins = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.C_SteamId64 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.C_Index = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.C_UserId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.refresh_metroButton = new MetroFramework.Controls.MetroButton();
            this.status_metroLabel = new MetroFramework.Controls.MetroLabel();
            ((System.ComponentModel.ISupportInitialize)(this.metroGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // metroGrid
            // 
            this.metroGrid.AllowUserToAddRows = false;
            this.metroGrid.AllowUserToDeleteRows = false;
            this.metroGrid.AllowUserToResizeRows = false;
            this.metroGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.metroGrid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.metroGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.metroGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.metroGrid.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.metroGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(177)))), ((int)(((byte)(89)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(208)))), ((int)(((byte)(104)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.metroGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.metroGrid.ColumnHeadersHeight = 32;
            this.metroGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.metroGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SortRow,
            this.C_Team,
            this.C_Nick,
            this.C_Rank,
            this.C_Wins,
            this.C_SteamId64,
            this.C_Index,
            this.C_UserId});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(208)))), ((int)(((byte)(104)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.metroGrid.DefaultCellStyle = dataGridViewCellStyle6;
            this.metroGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.metroGrid.EnableHeadersVisualStyles = false;
            this.metroGrid.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.metroGrid.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.metroGrid.Location = new System.Drawing.Point(20, 60);
            this.metroGrid.MultiSelect = false;
            this.metroGrid.Name = "metroGrid";
            this.metroGrid.ReadOnly = true;
            this.metroGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(177)))), ((int)(((byte)(89)))));
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(208)))), ((int)(((byte)(104)))));
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.metroGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.metroGrid.RowHeadersVisible = false;
            this.metroGrid.RowHeadersWidth = 32;
            this.metroGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.metroGrid.RowTemplate.Height = 32;
            this.metroGrid.RowTemplate.ReadOnly = true;
            this.metroGrid.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.metroGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.metroGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.metroGrid.Size = new System.Drawing.Size(740, 385);
            this.metroGrid.Style = MetroFramework.MetroColorStyle.Green;
            this.metroGrid.TabIndex = 0;
            // 
            // SortRow
            // 
            this.SortRow.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.SortRow.FillWeight = 64F;
            this.SortRow.HeaderText = "_SortRow";
            this.SortRow.MaxInputLength = 64;
            this.SortRow.Name = "SortRow";
            this.SortRow.ReadOnly = true;
            this.SortRow.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.SortRow.Visible = false;
            this.SortRow.Width = 64;
            // 
            // C_Team
            // 
            this.C_Team.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.C_Team.Description = "Сторона игрока";
            this.C_Team.FillWeight = 48F;
            this.C_Team.HeaderText = "";
            this.C_Team.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.C_Team.MinimumWidth = 32;
            this.C_Team.Name = "C_Team";
            this.C_Team.ReadOnly = true;
            this.C_Team.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.C_Team.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.C_Team.Width = 32;
            // 
            // C_Nick
            // 
            this.C_Nick.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.C_Nick.HeaderText = "Ник";
            this.C_Nick.MaxInputLength = 128;
            this.C_Nick.MinimumWidth = 64;
            this.C_Nick.Name = "C_Nick";
            this.C_Nick.ReadOnly = true;
            this.C_Nick.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.C_Nick.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // C_Rank
            // 
            this.C_Rank.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.C_Rank.FillWeight = 80F;
            this.C_Rank.HeaderText = "Звание";
            this.C_Rank.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.C_Rank.MinimumWidth = 80;
            this.C_Rank.Name = "C_Rank";
            this.C_Rank.ReadOnly = true;
            this.C_Rank.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.C_Rank.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.C_Rank.Width = 80;
            // 
            // C_Wins
            // 
            this.C_Wins.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F, System.Drawing.FontStyle.Bold);
            this.C_Wins.DefaultCellStyle = dataGridViewCellStyle2;
            this.C_Wins.FillWeight = 80F;
            this.C_Wins.HeaderText = "Побед";
            this.C_Wins.MaxInputLength = 128;
            this.C_Wins.MinimumWidth = 50;
            this.C_Wins.Name = "C_Wins";
            this.C_Wins.ReadOnly = true;
            this.C_Wins.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.C_Wins.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.C_Wins.ToolTipText = "Количество побед у игрока в сорев. режиме";
            this.C_Wins.Width = 64;
            // 
            // C_SteamId64
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F, System.Drawing.FontStyle.Bold);
            this.C_SteamId64.DefaultCellStyle = dataGridViewCellStyle3;
            this.C_SteamId64.FillWeight = 256F;
            this.C_SteamId64.HeaderText = "SteamID64";
            this.C_SteamId64.MaxInputLength = 128;
            this.C_SteamId64.MinimumWidth = 128;
            this.C_SteamId64.Name = "C_SteamId64";
            this.C_SteamId64.ReadOnly = true;
            this.C_SteamId64.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.C_SteamId64.Width = 200;
            // 
            // C_Index
            // 
            this.C_Index.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.C_Index.DefaultCellStyle = dataGridViewCellStyle4;
            this.C_Index.FillWeight = 64F;
            this.C_Index.HeaderText = "Index";
            this.C_Index.MaxInputLength = 128;
            this.C_Index.MinimumWidth = 48;
            this.C_Index.Name = "C_Index";
            this.C_Index.ReadOnly = true;
            this.C_Index.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.C_Index.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.C_Index.ToolTipText = "Индекс в списке игроков";
            this.C_Index.Width = 48;
            // 
            // C_UserId
            // 
            this.C_UserId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.C_UserId.DefaultCellStyle = dataGridViewCellStyle5;
            this.C_UserId.FillWeight = 64F;
            this.C_UserId.HeaderText = "UserID";
            this.C_UserId.MaxInputLength = 128;
            this.C_UserId.MinimumWidth = 52;
            this.C_UserId.Name = "C_UserId";
            this.C_UserId.ReadOnly = true;
            this.C_UserId.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.C_UserId.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.C_UserId.ToolTipText = "UserID игрока, можно использовать для кика в консоли";
            this.C_UserId.Width = 52;
            // 
            // refresh_metroButton
            // 
            this.refresh_metroButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.refresh_metroButton.Location = new System.Drawing.Point(20, 446);
            this.refresh_metroButton.Name = "refresh_metroButton";
            this.refresh_metroButton.Size = new System.Drawing.Size(740, 30);
            this.refresh_metroButton.TabIndex = 1;
            this.refresh_metroButton.Text = "Обновить";
            this.refresh_metroButton.UseSelectable = true;
            this.refresh_metroButton.Click += new System.EventHandler(this.refresh_metroButton_Click);
            // 
            // status_metroLabel
            // 
            this.status_metroLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.status_metroLabel.FontSize = MetroFramework.MetroLabelSize.Small;
            this.status_metroLabel.Location = new System.Drawing.Point(20, 479);
            this.status_metroLabel.Name = "status_metroLabel";
            this.status_metroLabel.Size = new System.Drawing.Size(740, 18);
            this.status_metroLabel.TabIndex = 2;
            this.status_metroLabel.Text = "Статус: загрузка...";
            this.status_metroLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ShowRanks_Form
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(780, 500);
            this.Controls.Add(this.metroGrid);
            this.Controls.Add(this.refresh_metroButton);
            this.Controls.Add(this.status_metroLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(780, 2000);
            this.MinimumSize = new System.Drawing.Size(780, 500);
            this.Name = "ShowRanks_Form";
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.DropShadow;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Style = MetroFramework.MetroColorStyle.Lime;
            this.Text = "Список игроков";
            this.Load += new System.EventHandler(this.ShowRanks_Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.metroGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroGrid metroGrid;
        private MetroFramework.Controls.MetroButton refresh_metroButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn SortRow;
        private System.Windows.Forms.DataGridViewImageColumn C_Team;
        private System.Windows.Forms.DataGridViewTextBoxColumn C_Nick;
        private System.Windows.Forms.DataGridViewImageColumn C_Rank;
        private System.Windows.Forms.DataGridViewTextBoxColumn C_Wins;
        private System.Windows.Forms.DataGridViewTextBoxColumn C_SteamId64;
        private System.Windows.Forms.DataGridViewTextBoxColumn C_Index;
        private System.Windows.Forms.DataGridViewTextBoxColumn C_UserId;
        private MetroFramework.Controls.MetroLabel status_metroLabel;
    }
}