namespace Game_Snake
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
            lblScore = new Label();
            lblFood = new Label();
            SuspendLayout();
            // 
            // lblScore
            // 
            lblScore.AutoSize = true;
            lblScore.Font = new Font("Scream Up Down", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lblScore.Location = new Point(23, 28);
            lblScore.Name = "lblScore";
            lblScore.Size = new Size(84, 18);
            lblScore.TabIndex = 0;
            lblScore.Text = "Рахунок: 0";
            // 
            // lblFood
            // 
            lblFood.BackColor = Color.Salmon;
            lblFood.ForeColor = Color.IndianRed;
            lblFood.Location = new Point(374, 215);
            lblFood.Name = "lblFood";
            lblFood.Size = new Size(21, 21);
            lblFood.TabIndex = 1;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(984, 461);
            Controls.Add(lblFood);
            Controls.Add(lblScore);
            KeyPreview = true;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            Load += Form1_Load;
            KeyDown += Snake_KeyDown;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblScore;
        private Label lblFood;
    }
}