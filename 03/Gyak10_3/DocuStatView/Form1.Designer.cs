namespace DocuStatView
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
            FileMenu = new ToolStripMenuItem();
            OpenFileDialogMenuItem = new ToolStripMenuItem();
            ResetFilterMenuItem = new ToolStripMenuItem();
            ExitMenuItem = new ToolStripMenuItem();
            textBox = new TextBox();
            listBoxCounter = new ListBox();
            labelNonWhitespace = new Label();
            labelSentences = new Label();
            labelFlesch = new Label();
            labelColeman = new Label();
            labelProperNouns = new Label();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            labelChar = new Label();
            spinBoxMinLength = new NumericUpDown();
            spinBoxMinOccur = new NumericUpDown();
            textBoxIgnoredWords = new TextBox();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)spinBoxMinLength).BeginInit();
            ((System.ComponentModel.ISupportInitialize)spinBoxMinOccur).BeginInit();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { FileMenu });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // FileMenu
            // 
            FileMenu.DropDownItems.AddRange(new ToolStripItem[] { OpenFileDialogMenuItem, ResetFilterMenuItem, ExitMenuItem });
            FileMenu.Name = "FileMenu";
            FileMenu.Size = new Size(37, 20);
            FileMenu.Text = "File";
            // 
            // OpenFileDialogMenuItem
            // 
            OpenFileDialogMenuItem.Name = "OpenFileDialogMenuItem";
            OpenFileDialogMenuItem.Size = new Size(161, 22);
            OpenFileDialogMenuItem.Text = "Open File Dialog";
            // 
            // ResetFilterMenuItem
            // 
            ResetFilterMenuItem.Name = "ResetFilterMenuItem";
            ResetFilterMenuItem.Size = new Size(161, 22);
            ResetFilterMenuItem.Text = "Reset Filter";
            // 
            // ExitMenuItem
            // 
            ExitMenuItem.Name = "ExitMenuItem";
            ExitMenuItem.Size = new Size(161, 22);
            ExitMenuItem.Text = "Exit";
            // 
            // textBox
            // 
            textBox.Location = new Point(12, 27);
            textBox.Multiline = true;
            textBox.Name = "textBox";
            textBox.ReadOnly = true;
            textBox.ScrollBars = ScrollBars.Vertical;
            textBox.Size = new Size(405, 259);
            textBox.TabIndex = 1;
            // 
            // listBoxCounter
            // 
            listBoxCounter.FormattingEnabled = true;
            listBoxCounter.Location = new Point(423, 27);
            listBoxCounter.Name = "listBoxCounter";
            listBoxCounter.Size = new Size(365, 259);
            listBoxCounter.TabIndex = 2;
            // 
            // labelNonWhitespace
            // 
            labelNonWhitespace.AutoSize = true;
            labelNonWhitespace.Location = new Point(12, 346);
            labelNonWhitespace.Name = "labelNonWhitespace";
            labelNonWhitespace.Size = new Size(158, 15);
            labelNonWhitespace.TabIndex = 4;
            labelNonWhitespace.Text = "Non-Whitespace Characters:";
            // 
            // labelSentences
            // 
            labelSentences.AutoSize = true;
            labelSentences.Location = new Point(12, 387);
            labelSentences.Name = "labelSentences";
            labelSentences.Size = new Size(94, 15);
            labelSentences.TabIndex = 5;
            labelSentences.Text = "Sentence Count:";
            // 
            // labelFlesch
            // 
            labelFlesch.AutoSize = true;
            labelFlesch.Location = new Point(214, 387);
            labelFlesch.Name = "labelFlesch";
            labelFlesch.Size = new Size(115, 15);
            labelFlesch.TabIndex = 8;
            labelFlesch.Text = "Flesch Reading Ease:";
            // 
            // labelColeman
            // 
            labelColeman.AutoSize = true;
            labelColeman.Location = new Point(214, 346);
            labelColeman.Name = "labelColeman";
            labelColeman.Size = new Size(116, 15);
            labelColeman.TabIndex = 7;
            labelColeman.Text = "Coleman-Liau Index:";
            // 
            // labelProperNouns
            // 
            labelProperNouns.AutoSize = true;
            labelProperNouns.Location = new Point(214, 305);
            labelProperNouns.Name = "labelProperNouns";
            labelProperNouns.Size = new Size(114, 15);
            labelProperNouns.TabIndex = 6;
            labelProperNouns.Text = "Proper Noun Count:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(408, 387);
            label7.Name = "label7";
            label7.Size = new Size(88, 15);
            label7.TabIndex = 11;
            label7.Text = "Ignored Words:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(408, 346);
            label8.Name = "label8";
            label8.Size = new Size(155, 15);
            label8.TabIndex = 10;
            label8.Text = "Minimum Word Occurence:";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(408, 305);
            label9.Name = "label9";
            label9.Size = new Size(135, 15);
            label9.TabIndex = 9;
            label9.Text = "Minimum Word Length:";
            // 
            // labelChar
            // 
            labelChar.AutoSize = true;
            labelChar.Location = new Point(12, 305);
            labelChar.Name = "labelChar";
            labelChar.Size = new Size(97, 15);
            labelChar.TabIndex = 12;
            labelChar.Text = "Character Count:";
            // 
            // spinBoxMinLength
            // 
            spinBoxMinLength.Location = new Point(588, 303);
            spinBoxMinLength.Maximum = new decimal(new int[] { 500, 0, 0, 0 });
            spinBoxMinLength.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            spinBoxMinLength.Name = "spinBoxMinLength";
            spinBoxMinLength.Size = new Size(96, 23);
            spinBoxMinLength.TabIndex = 13;
            spinBoxMinLength.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // spinBoxMinOccur
            // 
            spinBoxMinOccur.Location = new Point(588, 344);
            spinBoxMinOccur.Maximum = new decimal(new int[] { 500, 0, 0, 0 });
            spinBoxMinOccur.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            spinBoxMinOccur.Name = "spinBoxMinOccur";
            spinBoxMinOccur.Size = new Size(96, 23);
            spinBoxMinOccur.TabIndex = 14;
            spinBoxMinOccur.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // textBoxIgnoredWords
            // 
            textBoxIgnoredWords.Location = new Point(502, 384);
            textBoxIgnoredWords.Name = "textBoxIgnoredWords";
            textBoxIgnoredWords.Size = new Size(182, 23);
            textBoxIgnoredWords.TabIndex = 15;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(textBoxIgnoredWords);
            Controls.Add(spinBoxMinOccur);
            Controls.Add(spinBoxMinLength);
            Controls.Add(labelChar);
            Controls.Add(label7);
            Controls.Add(label8);
            Controls.Add(label9);
            Controls.Add(labelFlesch);
            Controls.Add(labelColeman);
            Controls.Add(labelProperNouns);
            Controls.Add(labelSentences);
            Controls.Add(labelNonWhitespace);
            Controls.Add(listBoxCounter);
            Controls.Add(textBox);
            Controls.Add(menuStrip1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "Document Statistics";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)spinBoxMinLength).EndInit();
            ((System.ComponentModel.ISupportInitialize)spinBoxMinOccur).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem FileMenu;
        private ToolStripMenuItem OpenFileDialogMenuItem;
        private ToolStripMenuItem ResetFilterMenuItem;
        private ToolStripMenuItem ExitMenuItem;
        private TextBox textBox;
        private ListBox listBoxCounter;
        private Label labelNonWhitespace;
        private Label labelSentences;
        private Label labelFlesch;
        private Label labelColeman;
        private Label labelProperNouns;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label labelChar;
        private NumericUpDown spinBoxMinLength;
        private NumericUpDown spinBoxMinOccur;
        private TextBox textBoxIgnoredWords;
    }
}
