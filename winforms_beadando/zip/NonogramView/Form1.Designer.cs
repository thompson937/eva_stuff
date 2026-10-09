namespace NonogramView
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            menuStrip1 = new MenuStrip();
            fileStripMenuItem = new ToolStripMenuItem();
            newStripMenuItem = new ToolStripMenuItem();
            x5ToolStripMenuItem = new ToolStripMenuItem();
            leafToolStripMenuItem = new ToolStripMenuItem();
            smileyToolStripMenuItem = new ToolStripMenuItem();
            bombToolStripMenuItem = new ToolStripMenuItem();
            randomToolStripMenuItem = new ToolStripMenuItem();
            x10ToolStripMenuItem = new ToolStripMenuItem();
            bookToolStripMenuItem = new ToolStripMenuItem();
            heartToolStripMenuItem = new ToolStripMenuItem();
            donutToolStripMenuItem = new ToolStripMenuItem();
            randomToolStripMenuItem1 = new ToolStripMenuItem();
            x15ToolStripMenuItem = new ToolStripMenuItem();
            goombaToolStripMenuItem = new ToolStripMenuItem();
            starToolStripMenuItem = new ToolStripMenuItem();
            wardrobeToolStripMenuItem = new ToolStripMenuItem();
            randomToolStripMenuItem2 = new ToolStripMenuItem();
            openStripMenuItem = new ToolStripMenuItem();
            saveStripMenuItem = new ToolStripMenuItem();
            buttonPanel = new Panel();
            columnPanel = new Panel();
            rowPanel = new Panel();
            label1 = new Label();
            liveCounterLabel = new Label();
            label2 = new Label();
            timeCounterLabel = new Label();
            gameTimer = new System.Windows.Forms.Timer(components);
            playPauseButton = new Button();
            exitStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(884, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "Strip";
            // 
            // fileStripMenuItem
            // 
            fileStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { newStripMenuItem, openStripMenuItem, saveStripMenuItem, exitStripMenuItem });
            fileStripMenuItem.Name = "fileStripMenuItem";
            fileStripMenuItem.Size = new Size(37, 20);
            fileStripMenuItem.Text = "File";
            // 
            // newStripMenuItem
            // 
            newStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { x5ToolStripMenuItem, x10ToolStripMenuItem, x15ToolStripMenuItem });
            newStripMenuItem.Name = "newStripMenuItem";
            newStripMenuItem.ShortcutKeys = Keys.Control | Keys.N;
            newStripMenuItem.Size = new Size(180, 22);
            newStripMenuItem.Text = "New...";
            // 
            // x5ToolStripMenuItem
            // 
            x5ToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { leafToolStripMenuItem, smileyToolStripMenuItem, bombToolStripMenuItem, randomToolStripMenuItem });
            x5ToolStripMenuItem.Name = "x5ToolStripMenuItem";
            x5ToolStripMenuItem.Size = new Size(103, 22);
            x5ToolStripMenuItem.Text = "5x5";
            // 
            // leafToolStripMenuItem
            // 
            leafToolStripMenuItem.Name = "leafToolStripMenuItem";
            leafToolStripMenuItem.Size = new Size(119, 22);
            leafToolStripMenuItem.Tag = "leaf_5x5";
            leafToolStripMenuItem.Text = "Leaf";
            leafToolStripMenuItem.Click += NewGamePredefined;
            // 
            // smileyToolStripMenuItem
            // 
            smileyToolStripMenuItem.Name = "smileyToolStripMenuItem";
            smileyToolStripMenuItem.Size = new Size(119, 22);
            smileyToolStripMenuItem.Tag = "smiley_5x5";
            smileyToolStripMenuItem.Text = "Smiley";
            smileyToolStripMenuItem.Click += NewGamePredefined;
            // 
            // bombToolStripMenuItem
            // 
            bombToolStripMenuItem.Name = "bombToolStripMenuItem";
            bombToolStripMenuItem.Size = new Size(119, 22);
            bombToolStripMenuItem.Tag = "bomb_5x5";
            bombToolStripMenuItem.Text = "Bomb";
            bombToolStripMenuItem.Click += NewGamePredefined;
            // 
            // randomToolStripMenuItem
            // 
            randomToolStripMenuItem.Name = "randomToolStripMenuItem";
            randomToolStripMenuItem.Size = new Size(119, 22);
            randomToolStripMenuItem.Tag = "5";
            randomToolStripMenuItem.Text = "Random";
            randomToolStripMenuItem.Click += NewGameRandom;
            // 
            // x10ToolStripMenuItem
            // 
            x10ToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { bookToolStripMenuItem, heartToolStripMenuItem, donutToolStripMenuItem, randomToolStripMenuItem1 });
            x10ToolStripMenuItem.Name = "x10ToolStripMenuItem";
            x10ToolStripMenuItem.Size = new Size(103, 22);
            x10ToolStripMenuItem.Text = "10x10";
            // 
            // bookToolStripMenuItem
            // 
            bookToolStripMenuItem.Name = "bookToolStripMenuItem";
            bookToolStripMenuItem.Size = new Size(119, 22);
            bookToolStripMenuItem.Tag = "book_10x10";
            bookToolStripMenuItem.Text = "Book";
            bookToolStripMenuItem.Click += NewGamePredefined;
            // 
            // heartToolStripMenuItem
            // 
            heartToolStripMenuItem.Name = "heartToolStripMenuItem";
            heartToolStripMenuItem.Size = new Size(119, 22);
            heartToolStripMenuItem.Tag = "heart_10x10";
            heartToolStripMenuItem.Text = "Heart";
            heartToolStripMenuItem.Click += NewGamePredefined;
            // 
            // donutToolStripMenuItem
            // 
            donutToolStripMenuItem.Name = "donutToolStripMenuItem";
            donutToolStripMenuItem.Size = new Size(119, 22);
            donutToolStripMenuItem.Tag = "donut_10x10";
            donutToolStripMenuItem.Text = "Donut";
            donutToolStripMenuItem.Click += NewGamePredefined;
            // 
            // randomToolStripMenuItem1
            // 
            randomToolStripMenuItem1.Name = "randomToolStripMenuItem1";
            randomToolStripMenuItem1.Size = new Size(119, 22);
            randomToolStripMenuItem1.Tag = "10";
            randomToolStripMenuItem1.Text = "Random";
            randomToolStripMenuItem1.Click += NewGameRandom;
            // 
            // x15ToolStripMenuItem
            // 
            x15ToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { goombaToolStripMenuItem, starToolStripMenuItem, wardrobeToolStripMenuItem, randomToolStripMenuItem2 });
            x15ToolStripMenuItem.Name = "x15ToolStripMenuItem";
            x15ToolStripMenuItem.Size = new Size(103, 22);
            x15ToolStripMenuItem.Text = "15x15";
            // 
            // goombaToolStripMenuItem
            // 
            goombaToolStripMenuItem.Name = "goombaToolStripMenuItem";
            goombaToolStripMenuItem.Size = new Size(126, 22);
            goombaToolStripMenuItem.Tag = "goomba_15x15";
            goombaToolStripMenuItem.Text = "Goomba";
            goombaToolStripMenuItem.Click += NewGamePredefined;
            // 
            // starToolStripMenuItem
            // 
            starToolStripMenuItem.Name = "starToolStripMenuItem";
            starToolStripMenuItem.Size = new Size(126, 22);
            starToolStripMenuItem.Tag = "star_15x15";
            starToolStripMenuItem.Text = "Star";
            starToolStripMenuItem.Click += NewGamePredefined;
            // 
            // wardrobeToolStripMenuItem
            // 
            wardrobeToolStripMenuItem.Name = "wardrobeToolStripMenuItem";
            wardrobeToolStripMenuItem.Size = new Size(126, 22);
            wardrobeToolStripMenuItem.Tag = "wardrobe_15x15";
            wardrobeToolStripMenuItem.Text = "Wardrobe";
            wardrobeToolStripMenuItem.Click += NewGamePredefined;
            // 
            // randomToolStripMenuItem2
            // 
            randomToolStripMenuItem2.Name = "randomToolStripMenuItem2";
            randomToolStripMenuItem2.Size = new Size(126, 22);
            randomToolStripMenuItem2.Tag = "15";
            randomToolStripMenuItem2.Text = "Random";
            randomToolStripMenuItem2.Click += NewGameRandom;
            // 
            // openStripMenuItem
            // 
            openStripMenuItem.Name = "openStripMenuItem";
            openStripMenuItem.ShortcutKeys = Keys.Control | Keys.O;
            openStripMenuItem.Size = new Size(180, 22);
            openStripMenuItem.Text = "Load...";
            // 
            // saveStripMenuItem
            // 
            saveStripMenuItem.Enabled = false;
            saveStripMenuItem.Name = "saveStripMenuItem";
            saveStripMenuItem.ShortcutKeys = Keys.Control | Keys.S;
            saveStripMenuItem.Size = new Size(180, 22);
            saveStripMenuItem.Text = "Save...";
            // 
            // buttonPanel
            // 
            buttonPanel.Location = new Point(173, 41);
            buttonPanel.Name = "buttonPanel";
            buttonPanel.Size = new Size(400, 400);
            buttonPanel.TabIndex = 1;
            // 
            // columnPanel
            // 
            columnPanel.Location = new Point(173, 447);
            columnPanel.Name = "columnPanel";
            columnPanel.Size = new Size(400, 300);
            columnPanel.TabIndex = 2;
            // 
            // rowPanel
            // 
            rowPanel.Location = new Point(579, 41);
            rowPanel.Name = "rowPanel";
            rowPanel.Size = new Size(300, 400);
            rowPanel.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            label1.ForeColor = SystemColors.ControlText;
            label1.Location = new Point(12, 41);
            label1.Name = "label1";
            label1.Size = new Size(70, 30);
            label1.TabIndex = 4;
            label1.Text = "Lives:";
            // 
            // liveCounterLabel
            // 
            liveCounterLabel.AutoSize = true;
            liveCounterLabel.BorderStyle = BorderStyle.Fixed3D;
            liveCounterLabel.Font = new Font("Segoe UI", 16F);
            liveCounterLabel.Location = new Point(88, 39);
            liveCounterLabel.Name = "liveCounterLabel";
            liveCounterLabel.Size = new Size(27, 32);
            liveCounterLabel.TabIndex = 5;
            liveCounterLabel.Text = "3";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            label2.Location = new Point(12, 75);
            label2.Name = "label2";
            label2.Size = new Size(70, 30);
            label2.TabIndex = 6;
            label2.Text = "Time:";
            // 
            // timeCounterLabel
            // 
            timeCounterLabel.AutoSize = true;
            timeCounterLabel.BorderStyle = BorderStyle.Fixed3D;
            timeCounterLabel.Font = new Font("Segoe UI", 16F);
            timeCounterLabel.Location = new Point(88, 75);
            timeCounterLabel.Name = "timeCounterLabel";
            timeCounterLabel.Size = new Size(68, 32);
            timeCounterLabel.TabIndex = 7;
            timeCounterLabel.Text = "00:00";
            // 
            // gameTimer
            // 
            gameTimer.Interval = 1000;
            gameTimer.Tick += OnGameTimerTick;
            // 
            // playPauseButton
            // 
            playPauseButton.Enabled = false;
            playPauseButton.Font = new Font("Segoe UI", 16F);
            playPauseButton.Location = new Point(12, 123);
            playPauseButton.Name = "playPauseButton";
            playPauseButton.Size = new Size(144, 43);
            playPauseButton.TabIndex = 8;
            playPauseButton.Text = "Pause";
            playPauseButton.UseVisualStyleBackColor = true;
            playPauseButton.Click += OnGamePaused;
            // 
            // exitStripMenuItem
            // 
            exitStripMenuItem.Name = "exitStripMenuItem";
            exitStripMenuItem.ShortcutKeys = Keys.Control | Keys.E;
            exitStripMenuItem.Size = new Size(180, 22);
            exitStripMenuItem.Text = "Exit...";
            exitStripMenuItem.Click += ExitGame;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(884, 711);
            Controls.Add(playPauseButton);
            Controls.Add(timeCounterLabel);
            Controls.Add(label2);
            Controls.Add(liveCounterLabel);
            Controls.Add(label1);
            Controls.Add(rowPanel);
            Controls.Add(columnPanel);
            Controls.Add(buttonPanel);
            Controls.Add(menuStrip1);
            Name = "Form1";
            Text = "Form1";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileStripMenuItem;
        private ToolStripMenuItem newStripMenuItem;
        private ToolStripMenuItem openStripMenuItem;
        private ToolStripMenuItem saveStripMenuItem;
        private Panel buttonPanel;
        private Panel columnPanel;
        private Panel rowPanel;
        private Label label1;
        private Label liveCounterLabel;
        private Label label2;
        private Label timeCounterLabel;
        private System.Windows.Forms.Timer gameTimer;
        private Button playPauseButton;
        private ToolStripMenuItem x5ToolStripMenuItem;
        private ToolStripMenuItem leafToolStripMenuItem;
        private ToolStripMenuItem smileyToolStripMenuItem;
        private ToolStripMenuItem bombToolStripMenuItem;
        private ToolStripMenuItem randomToolStripMenuItem;
        private ToolStripMenuItem x10ToolStripMenuItem;
        private ToolStripMenuItem bookToolStripMenuItem;
        private ToolStripMenuItem heartToolStripMenuItem;
        private ToolStripMenuItem donutToolStripMenuItem;
        private ToolStripMenuItem randomToolStripMenuItem1;
        private ToolStripMenuItem x15ToolStripMenuItem;
        private ToolStripMenuItem goombaToolStripMenuItem;
        private ToolStripMenuItem starToolStripMenuItem;
        private ToolStripMenuItem wardrobeToolStripMenuItem;
        private ToolStripMenuItem randomToolStripMenuItem2;
        private ToolStripMenuItem exitStripMenuItem;
    }
}
