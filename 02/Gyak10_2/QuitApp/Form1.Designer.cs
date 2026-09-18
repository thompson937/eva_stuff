namespace QuitApp
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
            quitBtn = new Button();
            SuspendLayout();
            // 
            // quitBtn
            // 
            quitBtn.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            quitBtn.Font = new Font("Segoe UI", 32F, FontStyle.Bold);
            quitBtn.Location = new Point(248, 150);
            quitBtn.Name = "quitBtn";
            quitBtn.Size = new Size(265, 115);
            quitBtn.TabIndex = 0;
            quitBtn.Text = "Quit";
            quitBtn.UseVisualStyleBackColor = true;
            quitBtn.Click += quitBtn_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.MediumTurquoise;
            ClientSize = new Size(800, 450);
            Controls.Add(quitBtn);
            Name = "Form1";
            Text = "Quit Application";
            ResumeLayout(false);
        }

        #endregion

        private Button quitBtn;
    }
}
