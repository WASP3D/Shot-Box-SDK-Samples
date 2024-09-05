using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Beesys.Wasp.Workflow;
using System.Configuration;
using System.IO;
using System.Collections;
using BeeSys.Wasp.Communicator;
using BeeSys.Wasp.KernelController;

namespace OnlineUpdate
{
    public partial class frmOnline : Form
    {
        #region Class variables
        const string m_sUrl = "net.tcp://{0}:{1}/TcpBinding/WcfTcpLink";
        private ShotBox m_objShotBox = null;
        private Link m_objLink = null;
        private LinkManager m_objLinkManager = null;
        private string m_sServerIp = string.Empty;
        //server ip used to connect shot box
        private string m_sShotBoxServerIp = string.Empty;
        private string kcurl;
        private string m_sLinkType = string.Empty;
        private string m_sPort = string.Empty;
        bool m_bIsPause = false;
        private UserTagCollection m_objUserTag;
        private FileInfo m_objFileInfo = null;
        private int m_appPort;
        private string m_appName = string.Empty;
        string _sInstanceDataXml = string.Empty;
        Form _frmTemplatePool = new Form();
        CMosDataEntry _dataEntryControl = null;
        #endregion

        #region Constructor
        public frmOnline()
        {
            InitializeComponent();
        }
        #endregion

        #region Events
        /// <summary>
        /// fires when engine is connected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void objLink_OnEngineConnected(object sender, EngineArgs e)
        {
            btnConnect.BackColor = Color.DarkGreen;
        }

        /// <summary>
        /// used to connect with the server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConnect_Click_1(object sender, EventArgs e)
        {
            IPlayoutServerX playoutServer = null;
            try
            {
                if (cmbxServers.SelectedItem != null && cmbxServers.SelectedItem is IPlayoutServerX)
                    playoutServer = cmbxServers.SelectedItem as IPlayoutServerX;

                if (playoutServer != null && (Equals(m_objShotBox, null)))
                {
                    m_sServerIp = ((IPlayoutServer)playoutServer).GetUrl(m_sLinkType.ToLower());
                    //Get server ip for shotbox
                    m_sShotBoxServerIp = playoutServer.GetPrepareUrl(m_sLinkType.ToLower());
                    if (string.IsNullOrEmpty(m_sServerIp))
                        m_sServerIp = playoutServer.GetPrepareUrl(m_sLinkType.ToLower());
                    //m_objLink.Connect(m_sServerIp, (playoutServer as CPlayoutServer).ChannelName);
                    m_objLink.Connect(m_sServerIp, (playoutServer as CPlayoutServer).ChannelName);
                }
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("connecting", ex.Message);
            }
        }
        
        /// <summary>
        /// used for preparing the scenegraph
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLoadScene_Click_1(object sender, EventArgs e)
        {
            string sXml = string.Empty;
            string sShotBoxID = null;
            bool isTicker;
            string templateID = null;
            bool bValid = false;
            string instanceID = null;
            string templatePath = null;
            try
            {
                #region sceneone

                if (txtSceneName.Tag!=null)
                {
                    //sXml = Util.getSGFromWSL(fileDialog.FileName);

                    if (rdbLocalSG.Checked)
                    {
                        templatePath = txtSceneName.Tag.ToString();
                        sXml = Util.getSGFromWSL(templatePath);
                    }
                    else if (rdbTemplate.Checked)
                    {
                        templateID = txtSceneName.Tag.ToString();
                        bValid = Util.getSgXml(templateID, "default", out isTicker, out sXml);
                    }
                    else if (rdbInstance.Checked)
                    {
                        instanceID = txtSceneName.Tag.ToString();
                        sXml = Util.getSgFromInstanceID(instanceID, out _sInstanceDataXml, out templateID);
                    }
                    if (!string.IsNullOrEmpty(sXml))
                    {
                        //unload previous scene
                        if (m_objShotBox != null)
                            m_objShotBox.DeleteSg();

                        cmbUserTag.Items.Clear();
                        txtUserValue.Text = string.Empty;
                        cmbUserTag.Text = string.Empty;

                        m_objShotBox = m_objLink.GetShotBox(sXml, out sShotBoxID, out isTicker) as ShotBox;
                        if (!Equals(m_objShotBox, null))
                        {
                            m_objShotBox.SetEngineUrl(m_sShotBoxServerIp);
                            if (m_objShotBox is IAddinInfo)
                            {
                                // S.No.			: -	1
                                //(m_objShotBox as IAddinInfo).Init(new InstanceInfo() { Type = "wspx", InstanceId = fileDialog.FileName, TemplateId = fileDialog.FileName, ThemeId = "default" });

                                if (rdbLocalSG.Checked)
                                {
                                    (m_objShotBox as IAddinInfo).Init(new Beesys.Wasp.Workflow.InstanceInfo() { Type = "wspx", InstanceId = templatePath, TemplateId = templatePath, ThemeId = "default" });
                                }
                                else
                                {
                                    (m_objShotBox as IAddinInfo).Init(new Beesys.Wasp.Workflow.InstanceInfo() { Type = "wspx", InstanceId = instanceID, TemplateId = templateID, ThemeId = "default" });
                                }

                            }
                            m_objShotBox.OnShotBoxStatus += new EventHandler<SHOTBOXARGS>(m_objShotBox_OnShotBoxStatus);
                            m_objShotBox.Prepare(m_sShotBoxServerIp, 0, RENDERMODE.PROGRAM);
                            m_objShotBox.SceneCue();

                        }
                        m_objUserTag = m_objShotBox.UserTags;
                        //  if (cmbUserTag.SelectedIndex==-1)
                        // {
                        for (int i = 0; i < m_objUserTag.Count; i++)
                        {
                            if (!cmbUserTag.Items.Contains(m_objUserTag[i].Name))
                                cmbUserTag.Items.Add(m_objUserTag[i].Name);
                        }
                        // }
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("error in preparing the scenegraph", ex.Message);
            }
        }

        /// <summary>
        /// this event fires when scenegraph is loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_objShotBox_OnShotBoxStatus(object sender, SHOTBOXARGS e)
        {
            if (Equals(e.SHOTBOXRESPONSE, SHOTBOXMSG.PREPARED))
            {
                btnPlay.BackColor = Color.DarkGreen;
                btnProgram.BackColor = Color.DarkGreen;
            }
        }
        
        /// <summary>
        /// used to play the scenegraph
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPlay_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (!Equals(m_objShotBox, null))
                {
                    btnPlay.BackColor = Color.DarkGray;
                    if (!m_bIsPause)
                    {
                        m_objShotBox.Play(true, true);
                    }
                    else
                        m_objShotBox.Play(false, false);
                    m_bIsPause = false;
                }
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("error in playing the scenegraph", ex.Message);
            }
        }

        /// <summary>
        /// used to pause the scenegraph
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPause_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (!Equals(m_objShotBox, null))
                {
                    btnPause.BackColor = Color.DarkGray;
                    m_objShotBox.Pause();
                    m_bIsPause = true;
                }
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("error in pausing the scenegraph", ex.Message);
            }
        }

        /// <summary>
        /// used to stop the scenegraph
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStop_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (!Equals(m_objShotBox, null))
                {
                    btnStop.BackColor = Color.DarkGray;
                    m_objShotBox.Stop();
                    m_bIsPause = false;
                }
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("error in stoping the scenegraph", ex.Message);
            }
        }

        /// <summary>
        /// used to taking the scenegraph on air
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOnAir_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (!Equals(m_objShotBox, null))
                {
                    m_objShotBox.SetRender(true);
                    btnOnAir.BackColor = Color.DarkGray;
                    btnOffAir.BackColor = Color.DarkGreen;
                }
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("error in taking the scenegraph on air", ex.Message);
            }
        }

        /// <summary>
        /// used to taking the scenegraph off air
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOffAir_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (!Equals(m_objShotBox, null))
                {
                    m_objShotBox.SetRender(false);
                    btnOnAir.BackColor = Color.DarkGreen;
                    btnOffAir.BackColor = Color.DarkGray;
                }
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("error in taking the scenegraph off air", ex.Message);
            }
        }

        /// <summary>
        /// used to setting the mode as program
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnProgram_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (!Equals(m_objShotBox, null))
                {
                    m_objShotBox.SetMode(RENDERMODE.PROGRAM);
                    btnProgram.BackColor = Color.DarkGray;
                    btnPreview.BackColor = Color.DarkGreen;
                }
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("error in setting the mode:program", ex.Message);
            }
        }

        /// <summary>
        /// used to setting the mode as preview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPreview_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (!Equals(m_objShotBox, null))
                {
                    m_objShotBox.SetMode(RENDERMODE.PREVIEW);
                    btnPreview.BackColor = Color.DarkGray;
                    btnProgram.BackColor = Color.DarkGreen;
                }
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("error in setting the mode:preview", ex.Message);
            }
        }

        /// <summary>
        /// used to update the scenegraph
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click_1(object sender, EventArgs e)
        {
            TagData tagData;
            try
            {
                if (!Equals(m_objShotBox, null))
                {
                    if (!string.IsNullOrEmpty(cmbUserTag.SelectedItem.ToString()) && !string.IsNullOrEmpty(txtUserValue.Text))
                    {
                        tagData = new TagData();
                        tagData.IsOnAirUpdate = true;

                        string sgXml = string.Empty;

                        if (txtSceneName.Tag != null)
                        {
                            //sXml = Util.getSGFromWSL(fileDialog.FileName);

                            if (rdbLocalSG.Checked)
                                sgXml = Util.getSGFromWSL(txtSceneName.Tag.ToString());
                            else if(rdbTemplate.Checked)
                            {
                                bool isTicker = false;
                                Util.getSgXml(txtSceneName.Tag.ToString(), "default", out isTicker, out sgXml);
                            }
                            else if (rdbInstance.Checked)
                            {
                                //bool isTicker = false;
                                //Util.getSgXml(txtSceneName.Tag.ToString(), "default", out isTicker, out sgXml);

                                string instanceID = txtSceneName.Tag.ToString();
                                string templateID = string.Empty;
                                sgXml = Util.getSgFromInstanceID(txtSceneName.Tag.ToString(), out _sInstanceDataXml, out templateID);
                            }
                            tagData.SgXml = sgXml;// Util.getSGFromWSL(fileDialog.FileName);
                        }
                        
                        tagData.UserTags = new string[] { cmbUserTag.SelectedItem.ToString() };
                        tagData.Values = new string[] { txtUserValue.Text };
                        tagData.Indexes = new string[] { "-1" };
                        m_objShotBox.UpdateSceneGraph(tagData);
                    }
                }
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("error in updating the scenegraph", ex.Message);
            }
        }

        /// <summary>
        /// used to getting the link
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmOnline_Load(object sender, EventArgs e)
        {
            string sLinkID = string.Empty;
            try
            {
                m_sPort = ConfigurationManager.AppSettings["port"].ToString();
                m_sLinkType = ConfigurationManager.AppSettings["linktype"].ToString();
                kcurl = ConfigurationManager.AppSettings["REMOTEMANAGERURL"].ToString();
                m_appName = ConfigurationManager.AppSettings["appName"].ToString();
                m_appPort = Convert.ToInt32(ConfigurationManager.AppSettings["appPort"]);

                BeeSys.Wasp.Communicator.CRemoteHelper cRemoteHelper = new BeeSys.Wasp.Communicator.CRemoteHelper(kcurl, m_appName, m_appPort);
                cRemoteHelper.InitRemoteHelper();

                m_objLinkManager = new LinkManager(kcurl);
                if (!Equals(m_objLinkManager, null))
                {
                    if (string.Compare(m_sLinkType, "TCP", StringComparison.OrdinalIgnoreCase) == 0)
                        m_objLink = m_objLinkManager.GetLink(LINKTYPE.TCP, out sLinkID);
                    if (string.Compare(m_sLinkType, "NAMEDPIPE", StringComparison.OrdinalIgnoreCase) == 0)
                        m_objLink = m_objLinkManager.GetLink(LINKTYPE.NAMEDPIPE, out sLinkID);
                }
                if (!Equals(m_objLink, null))
                {
                    m_objLink.OnEngineConnected += new EventHandler<EngineArgs>(objLink_OnEngineConnected);
                }
                this.FormClosing += new FormClosingEventHandler(frmOnline_FormClosing);
                RefreshServersList();
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("loading", ex.Message);
            }
        }
       
        /// <summary>
        /// handle the event to release resources
        /// </summary>
        private void frmOnline_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (!Equals(m_objShotBox, null))
                {
                    //Removing the loaded scene from server
                    m_objShotBox.DeleteSg();
                }
                if (!Equals(m_objLink, null))
                {
                    //Remove the old scenes which were in use a long time ago.                   
                    DeleteUnusedSG();

                    //Disconnect and Remove the communication channel with all Engines connected using this link.
                    m_objLink.DisconnectAll();
                }
                // Environment.Exit(0) use because Exe stuck in task manager on closing the app
                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("error in form closing", ex.Message);
            }
        }
        
        /// <summary>
        /// Handle the event to browse the scene from local path
        /// </summary>
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {

                if (rdbLocalSG.Checked)
                {
                    fileDialog.Filter = "wspx files|*.wspx";
                    fileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
                    if (Equals(fileDialog.ShowDialog(), DialogResult.OK))
                    {
                        txtSceneName.Text = string.Empty;
                        txtSceneName.Tag = string.Empty;
                        m_objFileInfo = new FileInfo(fileDialog.FileName);
                        txtSceneName.Text = m_objFileInfo.Name;
                        txtSceneName.Tag = m_objFileInfo.FullName;
                    }
                }
                else
                {
                    if (_frmTemplatePool != null)
                    {
                        //Load template pool and Instance pool
                        if (_dataEntryControl == null)
                        {
                            _dataEntryControl = new CMosDataEntry();
                            _dataEntryControl.InitialiseObject("", "", "");
                            _dataEntryControl.HandleInstanceDoubleClick = true;
                            _dataEntryControl.Dock = DockStyle.Fill;

                            _dataEntryControl.OnTemplateSelection += DataEntryControl_OnTemplateSelection;
                            _dataEntryControl.OnDataInstancePostUpdate += DataEntryControl_OnDataInstancePostUpdate;
                            _frmTemplatePool.Controls.Add(_dataEntryControl);
                            _frmTemplatePool.Controls.Add(_dataEntryControl);
                        }


                        _frmTemplatePool.ShowInTaskbar = false;
                        _frmTemplatePool.ShowIcon = false;
                        _frmTemplatePool.Size = new Size(1000, 600);
                        _frmTemplatePool.StartPosition = FormStartPosition.CenterScreen;
                        _frmTemplatePool.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("error in selecting the scenegraph", ex.Message);
            }
        }

        /// <summary>
        /// Handle the event to set the selected template name and id
        /// </summary>
        private void DataEntryControl_OnTemplateSelection(string TemplateName, string TemplateID)
        {
            try
            {
                if (!string.IsNullOrEmpty(TemplateName)
                    && !string.IsNullOrEmpty(TemplateID)
                    && rdbTemplate.Checked)
                {
                    txtSceneName.Text = TemplateName;
                    txtSceneName.Tag = TemplateID;

                    if (_frmTemplatePool != null)
                        _frmTemplatePool.Close();
                }
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog(ex);
            }
        }

        /// <summary>
        /// Handle the event to udpate the loaded instance data / post new instance
        /// </summary>
        private void DataEntryControl_OnDataInstancePostUpdate(string sID, string sSlug, string sTemplatename)
        {
            try
            {
                if (!string.IsNullOrEmpty(sID)
                  && !string.IsNullOrEmpty(sSlug)
                  && rdbInstance.Checked)
                {
                    if (txtSceneName.Tag != null
                        && string.Compare(sID, txtSceneName.Tag.ToString(), true) == 0)
                    {
                        //get the updated instance data xml
                        string instanceID = txtSceneName.Tag.ToString();
                        string sInstanceDataXml = string.Empty;
                        string templateID = string.Empty;
                        string xml = Util.getSgFromInstanceID(instanceID, out sInstanceDataXml, out templateID);
                        if (!string.IsNullOrEmpty(sInstanceDataXml)
                            && m_objShotBox != null)
                        {
                            m_objShotBox.UpdateSceneGraph(sInstanceDataXml, true);
                        }
                    }
                    else
                    {
                        txtSceneName.Text = sSlug;
                        txtSceneName.Tag = sID;

                        //update the loaded scene
                        if (_frmTemplatePool != null)
                            _frmTemplatePool.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog(ex);
            }
        }

        #endregion

        #region Methods
        /// <summary>
        /// Remove the old scenes which were in use a long time ago.        
        /// </summary>
        private void DeleteUnusedSG()
        {
            try
            {
                //Generally removes scenes which are unused and were last in use 60 seconds ago.
                //This can be configured in ChannelInfo.xml.
                //<timer del_unused_duration = "60" />
                //present in \\WASP3D\Common\HostedAssemblies\LinkCommandManager
                m_objLink.RemoveUnusedSG(m_sServerIp);

            }
            catch(Exception ex)
            {
                LogWriter.WriteLog("error in DeleteUnusedSG", ex.Message);
            }
        }
        /// <summary>
        /// Refresh list of sting servers
        /// </summary>
        private void RefreshServersList()
        {
            try
            {
                //Gets the list of engines i.e, Sting servers available on KC
                List<CPlayoutServer> lstmodules = Util.GetActiveServers();

                cmbxServers.DataSource = null;
                cmbxServers.DataSource = lstmodules;
                cmbxServers.DisplayMember = "EngineName";
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog(ex);
            }
        }

        #endregion
        
    }
}
