namespace PagingApp
{
    partial class Paging
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
            this.lblServer = new System.Windows.Forms.Label();
            this.lblTextFile = new System.Windows.Forms.Label();
            this.lblPlayText = new System.Windows.Forms.Label();
            this.cmbxServers = new System.Windows.Forms.ComboBox();
            this.txtTextFile = new System.Windows.Forms.TextBox();
            this.cbPlayText = new System.Windows.Forms.CheckBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnLoadScene = new System.Windows.Forms.Button();
            this.btnPlayDefaultController = new System.Windows.Forms.Button();
            this.btnPauseDefaultController = new System.Windows.Forms.Button();
            this.btnStopDefaultController = new System.Windows.Forms.Button();
            this.btnTextFile = new System.Windows.Forms.Button();
            this.btnProgram = new System.Windows.Forms.Button();
            this.btnPreview = new System.Windows.Forms.Button();
            this.gbRenderMode = new System.Windows.Forms.GroupBox();
            this.gbDefaultController = new System.Windows.Forms.GroupBox();
            this.btnOffAir = new System.Windows.Forms.Button();
            this.btnOnAir = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.btnPlayController = new System.Windows.Forms.Button();
            this.btnPauseController = new System.Windows.Forms.Button();
            this.btnStopController = new System.Windows.Forms.Button();
            this.gbController = new System.Windows.Forms.GroupBox();
            this.pnlServer = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.grpSceneGraph = new System.Windows.Forms.GroupBox();
            this.rdbInstance = new System.Windows.Forms.RadioButton();
            this.rdbTemplate = new System.Windows.Forms.RadioButton();
            this.rdbLocalSG = new System.Windows.Forms.RadioButton();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtSceneName = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cmbUserTag = new System.Windows.Forms.ComboBox();
            this.lblUserTag = new System.Windows.Forms.Label();
            this.gbRenderMode.SuspendLayout();
            this.gbDefaultController.SuspendLayout();
            this.gbController.SuspendLayout();
            this.pnlServer.SuspendLayout();
            this.panel1.SuspendLayout();
            this.grpSceneGraph.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblServer
            // 
            this.lblServer.AutoSize = true;
            this.lblServer.Location = new System.Drawing.Point(4, 3);
            this.lblServer.Name = "lblServer";
            this.lblServer.Size = new System.Drawing.Size(38, 13);
            this.lblServer.TabIndex = 0;
            this.lblServer.Text = "Server";
            // 
            // lblTextFile
            // 
            this.lblTextFile.AutoSize = true;
            this.lblTextFile.Location = new System.Drawing.Point(4, 2);
            this.lblTextFile.Name = "lblTextFile";
            this.lblTextFile.Size = new System.Drawing.Size(47, 13);
            this.lblTextFile.TabIndex = 1;
            this.lblTextFile.Text = "Text File";
            // 
            // lblPlayText
            // 
            this.lblPlayText.AutoSize = true;
            this.lblPlayText.Location = new System.Drawing.Point(32, 160);
            this.lblPlayText.Name = "lblPlayText";
            this.lblPlayText.Size = new System.Drawing.Size(89, 13);
            this.lblPlayText.TabIndex = 3;
            this.lblPlayText.Text = "Play Text in Loop";
            // 
            // cmbxServers
            // 
            this.cmbxServers.Location = new System.Drawing.Point(85, 1);
            this.cmbxServers.Name = "cmbxServers";
            this.cmbxServers.Size = new System.Drawing.Size(163, 21);
            this.cmbxServers.TabIndex = 5;
            // 
            // txtTextFile
            // 
            this.txtTextFile.Location = new System.Drawing.Point(85, -1);
            this.txtTextFile.Name = "txtTextFile";
            this.txtTextFile.Size = new System.Drawing.Size(163, 20);
            this.txtTextFile.TabIndex = 6;
            // 
            // cbPlayText
            // 
            this.cbPlayText.AutoSize = true;
            this.cbPlayText.Location = new System.Drawing.Point(14, 159);
            this.cbPlayText.Name = "cbPlayText";
            this.cbPlayText.Size = new System.Drawing.Size(15, 14);
            this.cbPlayText.TabIndex = 8;
            this.cbPlayText.UseVisualStyleBackColor = true;
            // 
            // btnConnect
            // 
            this.btnConnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConnect.Location = new System.Drawing.Point(268, 13);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(97, 23);
            this.btnConnect.TabIndex = 9;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnLoadScene
            // 
            this.btnLoadScene.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoadScene.Location = new System.Drawing.Point(371, 85);
            this.btnLoadScene.Name = "btnLoadScene";
            this.btnLoadScene.Size = new System.Drawing.Size(79, 62);
            this.btnLoadScene.TabIndex = 10;
            this.btnLoadScene.Text = "Load Scene";
            this.btnLoadScene.UseVisualStyleBackColor = true;
            this.btnLoadScene.Click += new System.EventHandler(this.btnLoadScene_Click);
            // 
            // btnPlayDefaultController
            // 
            this.btnPlayDefaultController.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlayDefaultController.Location = new System.Drawing.Point(8, 21);
            this.btnPlayDefaultController.Name = "btnPlayDefaultController";
            this.btnPlayDefaultController.Size = new System.Drawing.Size(108, 23);
            this.btnPlayDefaultController.TabIndex = 11;
            this.btnPlayDefaultController.Text = "Play";
            this.btnPlayDefaultController.UseVisualStyleBackColor = true;
            this.btnPlayDefaultController.Click += new System.EventHandler(this.btnPlayDefaultController_Click);
            // 
            // btnPauseDefaultController
            // 
            this.btnPauseDefaultController.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPauseDefaultController.Location = new System.Drawing.Point(122, 21);
            this.btnPauseDefaultController.Name = "btnPauseDefaultController";
            this.btnPauseDefaultController.Size = new System.Drawing.Size(112, 23);
            this.btnPauseDefaultController.TabIndex = 12;
            this.btnPauseDefaultController.Text = "Pause";
            this.btnPauseDefaultController.UseVisualStyleBackColor = true;
            this.btnPauseDefaultController.Click += new System.EventHandler(this.btnPauseDefaultController_Click);
            // 
            // btnStopDefaultController
            // 
            this.btnStopDefaultController.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStopDefaultController.Location = new System.Drawing.Point(240, 21);
            this.btnStopDefaultController.Name = "btnStopDefaultController";
            this.btnStopDefaultController.Size = new System.Drawing.Size(97, 23);
            this.btnStopDefaultController.TabIndex = 13;
            this.btnStopDefaultController.Text = "Stop";
            this.btnStopDefaultController.UseVisualStyleBackColor = true;
            this.btnStopDefaultController.Click += new System.EventHandler(this.btnStopDefaultController_Click);
            // 
            // btnTextFile
            // 
            this.btnTextFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTextFile.Location = new System.Drawing.Point(268, 44);
            this.btnTextFile.Name = "btnTextFile";
            this.btnTextFile.Size = new System.Drawing.Size(97, 23);
            this.btnTextFile.TabIndex = 17;
            this.btnTextFile.Text = "...";
            this.btnTextFile.UseVisualStyleBackColor = true;
            this.btnTextFile.Click += new System.EventHandler(this.btnTextFile_Click);
            // 
            // btnProgram
            // 
            this.btnProgram.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProgram.Location = new System.Drawing.Point(15, 21);
            this.btnProgram.Name = "btnProgram";
            this.btnProgram.Size = new System.Drawing.Size(104, 23);
            this.btnProgram.TabIndex = 19;
            this.btnProgram.Text = "Program";
            this.btnProgram.UseVisualStyleBackColor = true;
            this.btnProgram.Click += new System.EventHandler(this.btnProgram_Click);
            // 
            // btnPreview
            // 
            this.btnPreview.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPreview.Location = new System.Drawing.Point(17, 60);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(102, 23);
            this.btnPreview.TabIndex = 20;
            this.btnPreview.Text = "Preview";
            this.btnPreview.UseVisualStyleBackColor = true;
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // gbRenderMode
            // 
            this.gbRenderMode.Controls.Add(this.btnPreview);
            this.gbRenderMode.Controls.Add(this.btnProgram);
            this.gbRenderMode.Location = new System.Drawing.Point(377, 187);
            this.gbRenderMode.Name = "gbRenderMode";
            this.gbRenderMode.Size = new System.Drawing.Size(129, 105);
            this.gbRenderMode.TabIndex = 21;
            this.gbRenderMode.TabStop = false;
            this.gbRenderMode.Text = "Render Mode";
            // 
            // gbDefaultController
            // 
            this.gbDefaultController.Controls.Add(this.btnOffAir);
            this.gbDefaultController.Controls.Add(this.btnOnAir);
            this.gbDefaultController.Controls.Add(this.btnStopDefaultController);
            this.gbDefaultController.Controls.Add(this.btnPauseDefaultController);
            this.gbDefaultController.Controls.Add(this.btnPlayDefaultController);
            this.gbDefaultController.Location = new System.Drawing.Point(11, 187);
            this.gbDefaultController.Name = "gbDefaultController";
            this.gbDefaultController.Size = new System.Drawing.Size(351, 105);
            this.gbDefaultController.TabIndex = 23;
            this.gbDefaultController.TabStop = false;
            this.gbDefaultController.Text = "Default Controller";
            // 
            // btnOffAir
            // 
            this.btnOffAir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOffAir.Location = new System.Drawing.Point(240, 60);
            this.btnOffAir.Name = "btnOffAir";
            this.btnOffAir.Size = new System.Drawing.Size(97, 23);
            this.btnOffAir.TabIndex = 25;
            this.btnOffAir.Text = "Off Air";
            this.btnOffAir.UseVisualStyleBackColor = true;
            this.btnOffAir.Click += new System.EventHandler(this.btnOffAir_Click);
            // 
            // btnOnAir
            // 
            this.btnOnAir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOnAir.Location = new System.Drawing.Point(8, 60);
            this.btnOnAir.Name = "btnOnAir";
            this.btnOnAir.Size = new System.Drawing.Size(108, 23);
            this.btnOnAir.TabIndex = 24;
            this.btnOnAir.Text = "On Air";
            this.btnOnAir.UseVisualStyleBackColor = true;
            this.btnOnAir.Click += new System.EventHandler(this.btnOnAir_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // btnPlayController
            // 
            this.btnPlayController.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlayController.Location = new System.Drawing.Point(8, 22);
            this.btnPlayController.Name = "btnPlayController";
            this.btnPlayController.Size = new System.Drawing.Size(108, 23);
            this.btnPlayController.TabIndex = 14;
            this.btnPlayController.Text = "Play";
            this.btnPlayController.UseVisualStyleBackColor = true;
            this.btnPlayController.Click += new System.EventHandler(this.btnPlayController_Click);
            // 
            // btnPauseController
            // 
            this.btnPauseController.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPauseController.Location = new System.Drawing.Point(122, 22);
            this.btnPauseController.Name = "btnPauseController";
            this.btnPauseController.Size = new System.Drawing.Size(112, 23);
            this.btnPauseController.TabIndex = 15;
            this.btnPauseController.Text = "Pause";
            this.btnPauseController.UseVisualStyleBackColor = true;
            this.btnPauseController.Click += new System.EventHandler(this.btnPauseController_Click);
            // 
            // btnStopController
            // 
            this.btnStopController.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStopController.Location = new System.Drawing.Point(240, 22);
            this.btnStopController.Name = "btnStopController";
            this.btnStopController.Size = new System.Drawing.Size(97, 23);
            this.btnStopController.TabIndex = 16;
            this.btnStopController.Text = "Stop";
            this.btnStopController.UseVisualStyleBackColor = true;
            this.btnStopController.Click += new System.EventHandler(this.btnStopController_Click);
            // 
            // gbController
            // 
            this.gbController.Controls.Add(this.btnStopController);
            this.gbController.Controls.Add(this.btnPauseController);
            this.gbController.Controls.Add(this.btnPlayController);
            this.gbController.Location = new System.Drawing.Point(11, 298);
            this.gbController.Name = "gbController";
            this.gbController.Size = new System.Drawing.Size(351, 55);
            this.gbController.TabIndex = 22;
            this.gbController.TabStop = false;
            this.gbController.Text = "Controller 1";
            // 
            // pnlServer
            // 
            this.pnlServer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlServer.Controls.Add(this.cmbxServers);
            this.pnlServer.Controls.Add(this.lblServer);
            this.pnlServer.Location = new System.Drawing.Point(13, 14);
            this.pnlServer.Name = "pnlServer";
            this.pnlServer.Size = new System.Drawing.Size(249, 24);
            this.pnlServer.TabIndex = 31;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblTextFile);
            this.panel1.Controls.Add(this.txtTextFile);
            this.panel1.Location = new System.Drawing.Point(13, 47);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(249, 20);
            this.panel1.TabIndex = 32;
            // 
            // grpSceneGraph
            // 
            this.grpSceneGraph.Controls.Add(this.rdbInstance);
            this.grpSceneGraph.Controls.Add(this.rdbTemplate);
            this.grpSceneGraph.Controls.Add(this.rdbLocalSG);
            this.grpSceneGraph.Controls.Add(this.btnBrowse);
            this.grpSceneGraph.Controls.Add(this.txtSceneName);
            this.grpSceneGraph.Location = new System.Drawing.Point(11, 78);
            this.grpSceneGraph.Name = "grpSceneGraph";
            this.grpSceneGraph.Size = new System.Drawing.Size(354, 72);
            this.grpSceneGraph.TabIndex = 86;
            this.grpSceneGraph.TabStop = false;
            this.grpSceneGraph.Text = "Scene Graph";
            // 
            // rdbInstance
            // 
            this.rdbInstance.AutoSize = true;
            this.rdbInstance.Location = new System.Drawing.Point(165, 22);
            this.rdbInstance.Name = "rdbInstance";
            this.rdbInstance.Size = new System.Drawing.Size(66, 17);
            this.rdbInstance.TabIndex = 9;
            this.rdbInstance.TabStop = true;
            this.rdbInstance.Text = "Instance";
            this.rdbInstance.UseVisualStyleBackColor = true;
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
            // panel3
            // 
            this.panel3.Controls.Add(this.cmbUserTag);
            this.panel3.Controls.Add(this.lblUserTag);
            this.panel3.Location = new System.Drawing.Point(125, 156);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(238, 28);
            this.panel3.TabIndex = 63;
            // 
            // cmbUserTag
            // 
            this.cmbUserTag.FormattingEnabled = true;
            this.cmbUserTag.Location = new System.Drawing.Point(122, 3);
            this.cmbUserTag.Name = "cmbUserTag";
            this.cmbUserTag.Size = new System.Drawing.Size(110, 21);
            this.cmbUserTag.TabIndex = 0;
            // 
            // lblUserTag
            // 
            this.lblUserTag.AutoSize = true;
            this.lblUserTag.Location = new System.Drawing.Point(7, 7);
            this.lblUserTag.Name = "lblUserTag";
            this.lblUserTag.Size = new System.Drawing.Size(109, 13);
            this.lblUserTag.TabIndex = 3;
            this.lblUserTag.Text = "Page Control Variable";
            // 
            // Paging
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(513, 370);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.grpSceneGraph);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlServer);
            this.Controls.Add(this.gbDefaultController);
            this.Controls.Add(this.gbController);
            this.Controls.Add(this.gbRenderMode);
            this.Controls.Add(this.btnTextFile);
            this.Controls.Add(this.btnLoadScene);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.cbPlayText);
            this.Controls.Add(this.lblPlayText);
            this.MaximumSize = new System.Drawing.Size(529, 409);
            this.Name = "Paging";
            this.ShowIcon = false;
            this.Text = "Paging Demo";
            this.Load += new System.EventHandler(this.Paging_Load);
            this.gbRenderMode.ResumeLayout(false);
            this.gbDefaultController.ResumeLayout(false);
            this.gbController.ResumeLayout(false);
            this.pnlServer.ResumeLayout(false);
            this.pnlServer.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.grpSceneGraph.ResumeLayout(false);
            this.grpSceneGraph.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblServer;
        private System.Windows.Forms.Label lblTextFile;
        private System.Windows.Forms.Label lblPlayText;
        //private System.Windows.Forms.TextBox txtServerIp;
        private System.Windows.Forms.TextBox txtTextFile;
        private System.Windows.Forms.CheckBox cbPlayText;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnLoadScene;
        private System.Windows.Forms.Button btnPlayDefaultController;
        private System.Windows.Forms.Button btnPauseDefaultController;
        private System.Windows.Forms.Button btnStopDefaultController;
        private System.Windows.Forms.Button btnTextFile;
        private System.Windows.Forms.Button btnProgram;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.GroupBox gbRenderMode;
        private System.Windows.Forms.GroupBox gbDefaultController;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button btnOffAir;
        private System.Windows.Forms.Button btnOnAir;
        private System.Windows.Forms.Button btnPlayController;
        private System.Windows.Forms.Button btnPauseController;
        private System.Windows.Forms.Button btnStopController;
        private System.Windows.Forms.GroupBox gbController;
        private System.Windows.Forms.Panel pnlServer;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cmbxServers;
        private System.Windows.Forms.GroupBox grpSceneGraph;
        private System.Windows.Forms.RadioButton rdbTemplate;
        private System.Windows.Forms.RadioButton rdbLocalSG;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txtSceneName;
        private System.Windows.Forms.RadioButton rdbInstance;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ComboBox cmbUserTag;
        private System.Windows.Forms.Label lblUserTag;
    }
}

