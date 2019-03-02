namespace Rio_External_Csgo_Cheat
{
    partial class Auth_Form
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Auth_Form));
            this.loading_metroPanel = new MetroFramework.Controls.MetroPanel();
            this.status_metroLabel = new MetroFramework.Controls.MetroLabel();
            this.metroProgressSpinner = new MetroFramework.Controls.MetroProgressSpinner();
            this.auth_metroPanel = new MetroFramework.Controls.MetroPanel();
            this.auth_metroButton = new MetroFramework.Controls.MetroButton();
            this.key_metroTextBox = new MetroFramework.Controls.MetroTextBox();
            this.dbDownloader_backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.formSeperator_metroTile = new MetroFramework.Controls.MetroTile();
            this.authButtonReload_timer = new System.Windows.Forms.Timer(this.components);
            this.loading_metroPanel.SuspendLayout();
            this.auth_metroPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // loading_metroPanel
            // 
            this.loading_metroPanel.Controls.Add(this.status_metroLabel);
            this.loading_metroPanel.Controls.Add(this.metroProgressSpinner);
            this.loading_metroPanel.HorizontalScrollbarBarColor = true;
            this.loading_metroPanel.HorizontalScrollbarHighlightOnWheel = false;
            this.loading_metroPanel.HorizontalScrollbarSize = 10;
            this.loading_metroPanel.Location = new System.Drawing.Point(0, 65);
            this.loading_metroPanel.Name = "loading_metroPanel";
            this.loading_metroPanel.Size = new System.Drawing.Size(380, 125);
            this.loading_metroPanel.TabIndex = 0;
            this.loading_metroPanel.VerticalScrollbarBarColor = true;
            this.loading_metroPanel.VerticalScrollbarHighlightOnWheel = false;
            this.loading_metroPanel.VerticalScrollbarSize = 10;
            // 
            // status_metroLabel
            // 
            this.status_metroLabel.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.status_metroLabel.Location = new System.Drawing.Point(3, 17);
            this.status_metroLabel.Name = "status_metroLabel";
            this.status_metroLabel.Size = new System.Drawing.Size(374, 23);
            this.status_metroLabel.TabIndex = 3;
            this.status_metroLabel.Text = "Идет получение данных из интернета...";
            this.status_metroLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // metroProgressSpinner
            // 
            this.metroProgressSpinner.Location = new System.Drawing.Point(158, 51);
            this.metroProgressSpinner.Maximum = 100;
            this.metroProgressSpinner.Name = "metroProgressSpinner";
            this.metroProgressSpinner.Size = new System.Drawing.Size(64, 64);
            this.metroProgressSpinner.Speed = 2.22F;
            this.metroProgressSpinner.Style = MetroFramework.MetroColorStyle.Orange;
            this.metroProgressSpinner.TabIndex = 2;
            this.metroProgressSpinner.UseSelectable = true;
            this.metroProgressSpinner.Value = 100;
            // 
            // auth_metroPanel
            // 
            this.auth_metroPanel.Controls.Add(this.auth_metroButton);
            this.auth_metroPanel.Controls.Add(this.key_metroTextBox);
            this.auth_metroPanel.HorizontalScrollbarBarColor = true;
            this.auth_metroPanel.HorizontalScrollbarHighlightOnWheel = false;
            this.auth_metroPanel.HorizontalScrollbarSize = 10;
            this.auth_metroPanel.Location = new System.Drawing.Point(0, 65);
            this.auth_metroPanel.Name = "auth_metroPanel";
            this.auth_metroPanel.Size = new System.Drawing.Size(380, 125);
            this.auth_metroPanel.TabIndex = 1;
            this.auth_metroPanel.VerticalScrollbarBarColor = true;
            this.auth_metroPanel.VerticalScrollbarHighlightOnWheel = false;
            this.auth_metroPanel.VerticalScrollbarSize = 10;
            this.auth_metroPanel.Visible = false;
            // 
            // auth_metroButton
            // 
            this.auth_metroButton.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.auth_metroButton.Location = new System.Drawing.Point(52, 64);
            this.auth_metroButton.Name = "auth_metroButton";
            this.auth_metroButton.Size = new System.Drawing.Size(275, 36);
            this.auth_metroButton.TabIndex = 3;
            this.auth_metroButton.Text = "Авторизоваться";
            this.auth_metroButton.UseSelectable = true;
            this.auth_metroButton.Click += new System.EventHandler(this.auth_metroButton_Click);
            // 
            // key_metroTextBox
            // 
            // 
            // 
            // 
            this.key_metroTextBox.CustomButton.Image = null;
            this.key_metroTextBox.CustomButton.Location = new System.Drawing.Point(247, 2);
            this.key_metroTextBox.CustomButton.Name = "";
            this.key_metroTextBox.CustomButton.Size = new System.Drawing.Size(25, 25);
            this.key_metroTextBox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.key_metroTextBox.CustomButton.TabIndex = 1;
            this.key_metroTextBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.key_metroTextBox.CustomButton.UseSelectable = true;
            this.key_metroTextBox.CustomButton.Visible = false;
            this.key_metroTextBox.FontSize = MetroFramework.MetroTextBoxSize.Tall;
            this.key_metroTextBox.FontWeight = MetroFramework.MetroTextBoxWeight.Bold;
            this.key_metroTextBox.Lines = new string[0];
            this.key_metroTextBox.Location = new System.Drawing.Point(52, 26);
            this.key_metroTextBox.MaxLength = 32;
            this.key_metroTextBox.Name = "key_metroTextBox";
            this.key_metroTextBox.PasswordChar = '\0';
            this.key_metroTextBox.PromptText = "Ваш ключ";
            this.key_metroTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.key_metroTextBox.SelectedText = "";
            this.key_metroTextBox.SelectionLength = 0;
            this.key_metroTextBox.SelectionStart = 0;
            this.key_metroTextBox.ShortcutsEnabled = true;
            this.key_metroTextBox.Size = new System.Drawing.Size(275, 30);
            this.key_metroTextBox.TabIndex = 2;
            this.key_metroTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.key_metroTextBox.UseSelectable = true;
            this.key_metroTextBox.WaterMark = "Ваш ключ";
            this.key_metroTextBox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            this.key_metroTextBox.WaterMarkFont = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            // 
            // dbDownloader_backgroundWorker
            // 
            this.dbDownloader_backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.dbDownloader_backgroundWorker_DoWork);
            // 
            // formSeperator_metroTile
            // 
            this.formSeperator_metroTile.ActiveControl = null;
            this.formSeperator_metroTile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.formSeperator_metroTile.BackColor = System.Drawing.SystemColors.ControlLight;
            this.formSeperator_metroTile.Location = new System.Drawing.Point(-2, 62);
            this.formSeperator_metroTile.Name = "formSeperator_metroTile";
            this.formSeperator_metroTile.Size = new System.Drawing.Size(381, 2);
            this.formSeperator_metroTile.TabIndex = 2;
            this.formSeperator_metroTile.UseCustomBackColor = true;
            this.formSeperator_metroTile.UseSelectable = true;
            // 
            // authButtonReload_timer
            // 
            this.authButtonReload_timer.Interval = 1000;
            this.authButtonReload_timer.Tick += new System.EventHandler(this.authButtonReload_timer_Tick);
            // 
            // Auth_Form
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(380, 190);
            this.Controls.Add(this.loading_metroPanel);
            this.Controls.Add(this.formSeperator_metroTile);
            this.Controls.Add(this.auth_metroPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Movable = false;
            this.Name = "Auth_Form";
            this.Resizable = false;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.DropShadow;
            this.Style = MetroFramework.MetroColorStyle.Lime;
            this.Text = "Авторизация - ID: 00000000";
            this.Shown += new System.EventHandler(this.Auth_Form_Shown);
            this.loading_metroPanel.ResumeLayout(false);
            this.auth_metroPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroPanel loading_metroPanel;
        private MetroFramework.Controls.MetroPanel auth_metroPanel;
        public MetroFramework.Controls.MetroTextBox key_metroTextBox;
        private MetroFramework.Controls.MetroProgressSpinner metroProgressSpinner;
        public MetroFramework.Controls.MetroButton auth_metroButton;
        private System.ComponentModel.BackgroundWorker dbDownloader_backgroundWorker;
        private MetroFramework.Controls.MetroLabel status_metroLabel;
        private MetroFramework.Controls.MetroTile formSeperator_metroTile;
        private System.Windows.Forms.Timer authButtonReload_timer;
    }
}