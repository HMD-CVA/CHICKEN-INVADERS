namespace ChikenFIGHT
{
    partial class Huong_Dan_Choi
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Huong_Dan_Choi));
            this.ruleGame = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ruleGame
            // 
            resources.ApplyResources(this.ruleGame, "ruleGame");
            this.ruleGame.Name = "ruleGame";
            this.ruleGame.Click += new System.EventHandler(this.ruleGame_Click);
            // 
            // Huong_Dan_Choi
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ruleGame);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Huong_Dan_Choi";
            this.Load += new System.EventHandler(this.Huong_Dan_Choi_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label ruleGame;
    }
}