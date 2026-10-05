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
            menuStrip1 = new MenuStrip();
            fileStripMenuItem = new ToolStripMenuItem();
            newStripMenuItem = new ToolStripMenuItem();
            openStripMenuItem = new ToolStripMenuItem();
            saveStripMenuItem = new ToolStripMenuItem();
            buttonPanel = new Panel();
            columnPanel = new Panel();
            rowPanel = new Panel();
            label1 = new Label();
            liveCounterLabel = new Label();
            label2 = new Label();
            timeCounterLabel = new Label();
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
            fileStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { newStripMenuItem, openStripMenuItem, saveStripMenuItem });
            fileStripMenuItem.Name = "fileStripMenuItem";
            fileStripMenuItem.Size = new Size(37, 20);
            fileStripMenuItem.Text = "File";
            // 
            // newStripMenuItem
            // 
            newStripMenuItem.Name = "newStripMenuItem";
            newStripMenuItem.Size = new Size(112, 22);
            newStripMenuItem.Text = "New...";
            // 
            // openStripMenuItem
            // 
            openStripMenuItem.Name = "openStripMenuItem";
            openStripMenuItem.Size = new Size(112, 22);
            openStripMenuItem.Text = "Open...";
            // 
            // saveStripMenuItem
            // 
            saveStripMenuItem.Name = "saveStripMenuItem";
            saveStripMenuItem.Size = new Size(112, 22);
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
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(884, 711);
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
    }
}
