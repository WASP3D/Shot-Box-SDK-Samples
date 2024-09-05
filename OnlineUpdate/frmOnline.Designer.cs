namespace OnlineUpdate
{
    partial class frmOnline
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.txtUserValue = new System.Windows.Forms.TextBox();
            this.lblUserValue = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cmbUserTag = new System.Windows.Forms.ComboBox();
            this.lblUserTag = new System.Windows.Forms.Label();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnOffAir = new System.Windows.Forms.Button();
            this.btnOnAir = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.btnPreview = new System.Windows.Forms.Button();
            this.btnProgram = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.cmbxServers = new System.Windows.Forms.ComboBox();
            this.lblServerIp = new System.Windows.Forms.Label();
            this.btnLoadScene = new System.Windows.Forms.Button();
            this.fileDialog = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.grpSceneGraph = new System.Windows.Forms.GroupBox();
            this.rdbTemplate = new System.Windows.Forms.RadioButton();
            this.rdbLocalSG = new System.Windows.Forms.RadioButton();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtSceneName = new System.Windows.Forms.TextBox();
            this.rdbInstance = new System.Windows.Forms.RadioButton();
            this.groupBox3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.grpSceneGraph.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.panel4);
            this.groupBox3.Controls.Add(this.panel3);
            this.groupBox3.Controls.Add(this.btnUpdate);
            this.groupBox3.Location = new System.Drawing.Point(679, 121);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(251, 146);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Variables";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.txtUserValue);
            this.panel4.Controls.Add(this.lblUserValue);
            this.panel4.Location = new System.Drawing.Point(16, 60);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(223, 24);
            this.panel4.TabIndex = 64;
            // 
            // txtUserValue
            // 
            this.txtUserValue.Location = new System.Drawing.Point(59, 2);
            this.txtUserValue.Name = "txtUserValue";
            this.txtUserValue.Size = new System.Drawing.Size(163, 20);
            this.txtUserValue.TabIndex = 2;
            // 
            // lblUserValue
            // 
            this.lblUserValue.AutoSize = true;
            this.lblUserValue.Location = new System.Drawing.Point(13, 4);
            this.lblUserValue.Name = "lblUserValue";
            this.lblUserValue.Size = new System.Drawing.Size(34, 13);
            this.lblUserValue.TabIndex = 4;
            this.lblUserValue.Text = "Value";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.cmbUserTag);
            this.panel3.Controls.Add(this.lblUserTag);
            this.panel3.Location = new System.Drawing.Point(16, 19);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(223, 24);
            this.panel3.TabIndex = 63;
            // 
            // cmbUserTag
            // 
            this.cmbUserTag.FormattingEnabled = true;
            this.cmbUserTag.Location = new System.Drawing.Point(59, 1);
            this.cmbUserTag.Name = "cmbUserTag";
            this.cmbUserTag.Size = new System.Drawing.Size(163, 21);
            this.cmbUserTag.TabIndex = 0;
            // 
            // lblUserTag
            // 
            this.lblUserTag.AutoSize = true;
            this.lblUserTag.Location = new System.Drawing.Point(13, 6);
            this.lblUserTag.Name = "lblUserTag";
            this.lblUserTag.Size = new System.Drawing.Size(35, 13);
            this.lblUserTag.TabIndex = 3;
            this.lblUserTag.Text = "Name";
            // 
            // btnUpdate
            // 
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdate.Location = new System.Drawing.Point(88, 98);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(151, 28);
            this.btnUpdate.TabIndex = 1;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click_1);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnOffAir);
            this.groupBox2.Controls.Add(this.btnOnAir);
            this.groupBox2.Controls.Add(this.btnStop);
            this.groupBox2.Controls.Add(this.btnPause);
            this.groupBox2.Controls.Add(this.btnPlay);
            this.groupBox2.Location = new System.Drawing.Point(3, 121);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(437, 146);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Controller";
            // 
            // btnOffAir
            // 
            this.btnOffAir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOffAir.Location = new System.Drawing.Point(210, 60);
            this.btnOffAir.Name = "btnOffAir";
            this.btnOffAir.Size = new System.Drawing.Size(218, 32);
            this.btnOffAir.TabIndex = 7;
            this.btnOffAir.Text = "OffAir";
            this.btnOffAir.UseVisualStyleBackColor = true;
            this.btnOffAir.Click += new System.EventHandler(this.btnOffAir_Click_1);
            // 
            // btnOnAir
            // 
            this.btnOnAir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOnAir.Location = new System.Drawing.Point(210, 19);
            this.btnOnAir.Name = "btnOnAir";
            this.btnOnAir.Size = new System.Drawing.Size(218, 31);
            this.btnOnAir.TabIndex = 6;
            this.btnOnAir.Text = "OnAir";
            this.btnOnAir.UseVisualStyleBackColor = true;
            this.btnOnAir.Click += new System.EventHandler(this.btnOnAir_Click_1);
            // 
            // btnStop
            // 
            this.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStop.Location = new System.Drawing.Point(6, 98);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(191, 32);
            this.btnStop.TabIndex = 5;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click_1);
            // 
            // btnPause
            // 
            this.btnPause.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPause.Location = new System.Drawing.Point(6, 60);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(191, 32);
            this.btnPause.TabIndex = 4;
            this.btnPause.Text = "Pause";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click_1);
            // 
            // btnPlay
            // 
            this.btnPlay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlay.Location = new System.Drawing.Point(6, 19);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(191, 31);
            this.btnPlay.TabIndex = 3;
            this.btnPlay.Text = "Play";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click_1);
            // 
            // btnPreview
            // 
            this.btnPreview.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPreview.Location = new System.Drawing.Point(6, 60);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(215, 32);
            this.btnPreview.TabIndex = 9;
            this.btnPreview.Text = "Preview";
            this.btnPreview.UseVisualStyleBackColor = true;
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click_1);
            // 
            // btnProgram
            // 
            this.btnProgram.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProgram.Location = new System.Drawing.Point(6, 19);
            this.btnProgram.Name = "btnProgram";
            this.btnProgram.Size = new System.Drawing.Size(215, 33);
            this.btnProgram.TabIndex = 8;
            this.btnProgram.Text = "Program";
            this.btnProgram.UseVisualStyleBackColor = true;
            this.btnProgram.Click += new System.EventHandler(this.btnProgram_Click_1);
            // 
            // btnConnect
            // 
            this.btnConnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConnect.Location = new System.Drawing.Point(290, 4);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(82, 28);
            this.btnConnect.TabIndex = 2;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click_1);
            // 
            // cmbxServers
            // 
            this.cmbxServers.Location = new System.Drawing.Point(81, 3);
            this.cmbxServers.Name = "cmbxServers";
            this.cmbxServers.Size = new System.Drawing.Size(187, 21);
            this.cmbxServers.TabIndex = 3;
            // 
            // lblServerIp
            // 
            this.lblServerIp.AutoSize = true;
            this.lblServerIp.Location = new System.Drawing.Point(14, 6);
            this.lblServerIp.Name = "lblServerIp";
            this.lblServerIp.Size = new System.Drawing.Size(47, 13);
            this.lblServerIp.TabIndex = 0;
            this.lblServerIp.Text = "ServerIp";
            // 
            // btnLoadScene
            // 
            this.btnLoadScene.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoadScene.Location = new System.Drawing.Point(378, 46);
            this.btnLoadScene.Name = "btnLoadScene";
            this.btnLoadScene.Size = new System.Drawing.Size(92, 69);
            this.btnLoadScene.TabIndex = 4;
            this.btnLoadScene.Text = "LoadScene";
            this.btnLoadScene.UseVisualStyleBackColor = true;
            this.btnLoadScene.Click += new System.EventHandler(this.btnLoadScene_Click_1);
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.FileName = "openFileDialog2";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblServerIp);
            this.panel1.Controls.Add(this.cmbxServers);
            this.panel1.Location = new System.Drawing.Point(3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(271, 28);
            this.panel1.TabIndex = 60;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnProgram);
            this.groupBox1.Controls.Add(this.btnPreview);
            this.groupBox1.Location = new System.Drawing.Point(446, 121);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(227, 146);
            this.groupBox1.TabIndex = 62;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Render Mode";
            // 
            // grpSceneGraph
            // 
            this.grpSceneGraph.Controls.Add(this.rdbInstance);
            this.grpSceneGraph.Controls.Add(this.rdbTemplate);
            this.grpSceneGraph.Controls.Add(this.rdbLocalSG);
            this.grpSceneGraph.Controls.Add(this.btnBrowse);
            this.grpSceneGraph.Controls.Add(this.txtSceneName);
            this.grpSceneGraph.Location = new System.Drawing.Point(3, 38);
            this.grpSceneGraph.Name = "grpSceneGraph";
            this.grpSceneGraph.Size = new System.Drawing.Size(369, 76);
            this.grpSceneGraph.TabIndex = 87;
            this.grpSceneGraph.TabStop = false;
            this.grpSceneGraph.Text = "Scene Graph";
            // 
            // rdbTemplate
            // 
            this.rdbTemplate.AutoSize = true;
            this.rdbTemplate.Location = new System.Drawing.Point(88, 22);
            this.rdbTemplate.Name = "rdbTemplate";
            this.rdbTemplate.Size = new System.Drawing.Size(69, 17);
            this.rdbTemplate.TabIndex = 8;
            this.rdbTemplate.TabStop = true;
            this.rdbTemplate.Text = "Template";
            this.rdbTemplate.UseVisualStyleBackColor = true;
            // 
            // rdbLocalSG
            // 
            this.rdbLocalSG.AutoSize = true;
            this.rdbLocalSG.Location = new System.Drawing.Point(13, 22);
            this.rdbLocalSG.Name = "rdbLocalSG";
            this.rdbLocalSG.Size = new System.Drawing.Size(64, 17);
            this.rdbLocalSG.TabIndex = 7;
            this.rdbLocalSG.TabStop = true;
            this.rdbLocalSG.Text = "LocalSg";
            this.rdbLocalSG.UseVisualStyleBackColor = true;
            // 
            // btnBrowse
            // 
            this.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowse.Location = new System.Drawing.Point(254, 42);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(97, 23);
            this.btnBrowse.TabIndex = 6;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtSceneName
            // 
            this.txtSceneName.Location = new System.Drawing.Point(13, 45);
            this.txtSceneName.Name = "txtSceneName";
            this.txtSceneName.Size = new System.Drawing.Size(238, 20);
            this.txtSceneName.TabIndex = 5;
            // 
            // rdbInstance
            // 
            this.rdbInstance.AutoSize = true;
            this.rdbInstance.Location = new System.Drawing.Point(163, 22);
            this.rdbInstance.Name = "rdbInstance";
            this.rdbInstance.Size = new System.Drawing.Size(66, 17);
            this.rdbInstance.TabIndex = 9;
            this.rdbInstance.TabStop = true;
            this.rdbInstance.Text = "Instance";
            this.rdbInstance.UseVisualStyleBackColor = true;
            // 
            // frmOnline
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(939, 275);
            this.Controls.Add(this.grpSceneGraph);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.btnLoadScene);
            this.MaximumSize = new System.Drawing.Size(955, 314);
            this.Name = "frmOnline";
            this.ShowIcon = false;
            this.Text = "Online Update";
            this.Load += new System.EventHandler(this.frmOnline_Load);
            this.groupBox3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.grpSceneGraph.ResumeLayout(false);
            this.grpSceneGraph.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblUserValue;
        private System.Windows.Forms.Label lblUserTag;
        private System.Windows.Forms.TextBox txtUserValue;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.ComboBox cmbUserTag;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.Button btnProgram;
        private System.Windows.Forms.Button btnOffAir;
        private System.Windows.Forms.Button btnOnAir;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button btnConnect;
        //private System.Windows.Forms.TextBox txtServerIp;
        private System.Windows.Forms.Label lblServerIp;
        private System.Windows.Forms.Button btnLoadScene;
        private System.Windows.Forms.OpenFileDialog fileDialog;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbxServers;
        private System.Windows.Forms.GroupBox grpSceneGraph;
        private System.Windows.Forms.RadioButton rdbTemplate;
        private System.Windows.Forms.RadioButton rdbLocalSG;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txtSceneName;
        private System.Windows.Forms.RadioButton rdbInstance;
    }
}

