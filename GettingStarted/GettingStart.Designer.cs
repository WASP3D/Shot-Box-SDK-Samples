namespace GettingStarted
{
    partial class GettingStart
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
            this.lblServerIp = new System.Windows.Forms.Label();
            this.lblSceneName = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnLoadScene = new System.Windows.Forms.Button();
            this.btnFileDialog = new System.Windows.Forms.Button();
            this.txtSceneName = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.btnPreview = new System.Windows.Forms.Button();
            this.btnProgram = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbxServers = new System.Windows.Forms.ComboBox();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblServerIp
            // 
            this.lblServerIp.AutoSize = true;
            this.lblServerIp.Location = new System.Drawing.Point(15, 16);
            this.lblServerIp.Name = "lblServerIp";
            this.lblServerIp.Size = new System.Drawing.Size(47, 13);
            this.lblServerIp.TabIndex = 0;
            this.lblServerIp.Text = "ServerIp";
            // 
            // lblSceneName
            // 
            this.lblSceneName.AutoSize = true;
            this.lblSceneName.Location = new System.Drawing.Point(15, 44);
            this.lblSceneName.Name = "lblSceneName";
            this.lblSceneName.Size = new System.Drawing.Size(70, 13);
            this.lblSceneName.TabIndex = 1;
            this.lblSceneName.Text = "Scene Graph";
            // 
            // btnConnect
            // 
            this.btnConnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConnect.Location = new System.Drawing.Point(312, 16);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(72, 25);
            this.btnConnect.TabIndex = 2;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnLoadScene
            // 
            this.btnLoadScene.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoadScene.Location = new System.Drawing.Point(392, 16);
            this.btnLoadScene.Name = "btnLoadScene";
            this.btnLoadScene.Size = new System.Drawing.Size(92, 36);
            this.btnLoadScene.TabIndex = 4;
            this.btnLoadScene.Text = "LoadScene";
            this.btnLoadScene.UseVisualStyleBackColor = true;
            this.btnLoadScene.Click += new System.EventHandler(this.btnLoadScene_Click);
            // 
            // btnFileDialog
            // 
            this.btnFileDialog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFileDialog.Location = new System.Drawing.Point(312, 44);
            this.btnFileDialog.Name = "btnFileDialog";
            this.btnFileDialog.Size = new System.Drawing.Size(72, 26);
            this.btnFileDialog.TabIndex = 6;
            this.btnFileDialog.Text = "...";
            this.btnFileDialog.UseVisualStyleBackColor = true;
            this.btnFileDialog.Click += new System.EventHandler(this.btnFileDialog_Click);
            // 
            // txtSceneName
            // 
            this.txtSceneName.Location = new System.Drawing.Point(106, 45);
            this.txtSceneName.Name = "txtSceneName";
            this.txtSceneName.Size = new System.Drawing.Size(164, 20);
            this.txtSceneName.TabIndex = 5;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnStop);
            this.groupBox2.Controls.Add(this.btnPause);
            this.groupBox2.Controls.Add(this.btnPlay);
            this.groupBox2.Location = new System.Drawing.Point(12, 83);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(173, 150);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Controller";
            // 
            // btnStop
            // 
            this.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStop.Location = new System.Drawing.Point(6, 104);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(161, 27);
            this.btnStop.TabIndex = 5;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnPause
            // 
            this.btnPause.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPause.Location = new System.Drawing.Point(6, 62);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(161, 27);
            this.btnPause.TabIndex = 4;
            this.btnPause.Text = "Pause";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlay.Location = new System.Drawing.Point(6, 19);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(161, 28);
            this.btnPlay.TabIndex = 3;
            this.btnPlay.Text = "Play";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // btnPreview
            // 
            this.btnPreview.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPreview.Location = new System.Drawing.Point(12, 62);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(153, 29);
            this.btnPreview.TabIndex = 9;
            this.btnPreview.Text = "Preview";
            this.btnPreview.UseVisualStyleBackColor = true;
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // btnProgram
            // 
            this.btnProgram.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProgram.Location = new System.Drawing.Point(10, 19);
            this.btnProgram.Name = "btnProgram";
            this.btnProgram.Size = new System.Drawing.Size(155, 28);
            this.btnProgram.TabIndex = 8;
            this.btnProgram.Text = "Program";
            this.btnProgram.UseVisualStyleBackColor = true;
            this.btnProgram.Click += new System.EventHandler(this.btnProgram_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.FileName = "openFileDialog2";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnProgram);
            this.groupBox1.Controls.Add(this.btnPreview);
            this.groupBox1.Location = new System.Drawing.Point(211, 83);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(173, 150);
            this.groupBox1.TabIndex = 83;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Render Mode";
            // 
            // cmbxServers
            // 
            this.cmbxServers.Location = new System.Drawing.Point(106, 12);
            this.cmbxServers.Name = "cmbxServers";
            this.cmbxServers.Size = new System.Drawing.Size(163, 21);
            this.cmbxServers.TabIndex = 84;
            // 
            // GettingStart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(492, 237);
            this.Controls.Add(this.cmbxServers);
            this.Controls.Add(this.txtSceneName);
            this.Controls.Add(this.lblSceneName);
            this.Controls.Add(this.lblServerIp);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnFileDialog);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.btnLoadScene);
            this.Name = "GettingStart";
            this.Text = "GettingStarted";
            this.Load += new System.EventHandler(this.GettingStart_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblServerIp;
        private System.Windows.Forms.Label lblSceneName;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnLoadScene;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.TextBox txtSceneName;
        private System.Windows.Forms.Button btnFileDialog;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.Button btnProgram;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbxServers;
    }
}

