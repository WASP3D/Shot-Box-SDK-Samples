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
using System.Security.Policy;
using BeeSys.Wasp.KernelController;
using BeeSys.Wasp.Communicator;
using System.Security.AccessControl;

namespace GettingStarted
{
    public partial class GettingStart : Form
    {
        #region Variables
        private ShotBox m_objShotBox = null;
        private Link m_objLink = null;
        private LinkManager m_objLinkManager = null;
        private string m_sServerIp = string.Empty;
        //server ip used to connect shot box
        private string m_sShotBoxServerIp = string.Empty;
        private string m_sUrl = "net.tcp://{0}:{1}/TcpBinding/WcfTcpLink";
        bool m_isPause = false;
        private FileInfo m_fileInfo = null;
        private string m_sLinkType = string.Empty;

        DataEntryControl _dataEntryControl = null;
        Form _frmTemplatePool = new Form();
        #endregion

        public GettingStart()
        {
            InitializeComponent();
        }

        #region Events
        /// <summary>
        /// used to connect with the server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConnect_Click(object sender, EventArgs e)
        {
            IPlayoutServerX playoutServer = null;
            try
            {
                if (cmbxServers.SelectedItem != null && cmbxServers.SelectedItem is IPlayoutServerX)
                    playoutServer = cmbxServers.SelectedItem as IPlayoutServerX;

                if (playoutServer != null && (Equals(m_objShotBox, null)))
                {
                    m_sServerIp = ((IPlayoutServer)playoutServer).GetUrl(m_sLinkType.ToLower());
                    m_sShotBoxServerIp = playoutServer.GetPrepareUrl(m_sLinkType.ToLower());
                    if (string.IsNullOrEmpty(m_sServerIp))
                        m_sServerIp = playoutServer.GetPrepareUrl(m_sLinkType.ToLower());
                    m_objLink.Connect(m_sServerIp, (playoutServer as CPlayoutServer).ChannelName);
                }
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("connecting", ex.Message);
            }
        }

        /// <summary>
        /// fires when engine is connected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void objLink_OnEngineConnected(object sender, EngineArgs e)
        {
            btnConnect.BackColor = Color.DarkGreen;
        }

        /// <summary>
        /// this event fires when user wants to close the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void GettingStart_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (!Equals(m_objShotBox, null))
                {
                    m_objShotBox.DeleteSg();
                }
                if (!Equals(m_objLink, null))
                    m_objLink.DisconnectAll();

              
                BeeSys.Wasp.Communicator.CRemoteHelper.AppClosing();
                // Environment.Exit(0) use because Exe stuck in task manager on closing the app
                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("error in form closing", ex.Message);
            }
            finally
            {
                Environment.Exit(0);
            }
        }
        
        /// <summary>
        /// used to selecting the scenegraph
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFileDialog_Click(object sender, EventArgs e)
        {
            try
            {
                if (rdbLocalSG.Checked)
                {
                    openFileDialog1.Filter = "wspx files|*.wspx";
                    openFileDialog1.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
                    if (Equals(openFileDialog1.ShowDialog(), DialogResult.OK))
                    {
                        txtSceneName.Text = string.Empty;
                        txtSceneName.Tag = string.Empty;
                        m_fileInfo = new FileInfo(openFileDialog1.FileName);
                        txtSceneName.Text = m_fileInfo.Name;
                        txtSceneName.Tag = m_fileInfo.FullName;
                    }
                }
                else
                {
                    if (_frmTemplatePool != null)
                    {
                        //Load template pool and Instance pool
                        if (_dataEntryControl == null)
                        {
                            _dataEntryControl = new DataEntryControl();
                            _dataEntryControl.InitialiseObject("", "", "");
                            _dataEntryControl.HandleInstanceDoubleClick = true;
                            _dataEntryControl.Dock = DockStyle.Fill;

                            _dataEntryControl.OnTemplateSelection += DataEntryControl_OnTemplateSelection;
                            _dataEntryControl.OnDataInstancePostUpdate += DataEntryControl_OnDataInstancePostUpdate;
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
        /// Handle the event to set the selected template name and ID
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
        /// Handle the event to set the selected Instance name and ID, and to update scene graph on instance update
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
                        if(!string.IsNullOrEmpty(sInstanceDataXml)
                            && m_objShotBox!=null)
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

        /// <summary>
        /// used to setting the mode as program
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnProgram_Click(object sender, EventArgs e)
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
        private void btnPreview_Click(object sender, EventArgs e)
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
        /// used to pause the scenegraph
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPause_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Equals(m_objShotBox, null))
                {
                    btnPause.BackColor = Color.DarkGray;
                    m_objShotBox.Pause();
                    m_isPause = true;
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
        private void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Equals(m_objShotBox, null))
                {
                    btnStop.BackColor = Color.DarkGray;
                    m_objShotBox.Stop();
                    m_isPause = false;
                }
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("error in stoping the scenegraph", ex.Message);
            }
        }

        /// <summary>
        /// used to play the scenegraph
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPlay_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Equals(m_objShotBox, null))
                {
                    btnPlay.BackColor = Color.DarkGray;
                    if (!m_isPause)
                    {
                        m_objShotBox.Play(true, true);
                    }
                    else
                        m_objShotBox.Play(false, false);
                    m_isPause = false;
                }
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("error in playing the scenegraph", ex.Message);
            }
        }

        /// <summary>
        /// used for preparing the scenegraph
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLoadScene_Click(object sender, EventArgs e)
        {
            try
            {
                #region sceneone
                string xml = string.Empty;
                string shotBoxID = null;
                bool isTicker;
                string templateID = null;
                bool bValid = false;
                string instanceID = null;
                string templatePath = null;
                string _sInstanceDataXml = string.Empty;

                if (txtSceneName.Tag == null)
                    return;

                //get the scene xml on the basis of selection
                if (rdbLocalSG.Checked)
                {
                    templatePath = txtSceneName.Tag.ToString();
                    xml = Util.getSGFromWSL(templatePath);
                }
                else if (rdbTemplate.Checked)
                {
                    templateID = txtSceneName.Tag.ToString();
                    bValid = Util.getSgXml(templateID, "default", out isTicker, out xml);
                }
                else if (rdbInstance.Checked)
                {
                    instanceID = txtSceneName.Tag.ToString();
                    xml = Util.getSgFromInstanceID(instanceID, out _sInstanceDataXml, out templateID);
                }

                if (!string.IsNullOrEmpty(xml))
                {

                    //unload previous scene
                    if (m_objShotBox != null)
                        m_objShotBox.DeleteSg();

                    m_objShotBox = m_objLink.GetShotBox(xml, out shotBoxID, out isTicker) as ShotBox;
                    if (!Equals(m_objShotBox, null))
                    {
                        m_objShotBox.SetEngineUrl(m_sShotBoxServerIp);
                        m_objShotBox.SetOutputChannel(m_objLink.OutputChannel);
                        if (m_objShotBox is IAddinInfo)
                        {
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
                        m_objShotBox.Prepare(m_sShotBoxServerIp, 0, string.Empty, RENDERMODE.PROGRAM);
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("Error in preparing the scenegraph", ex.Message);
            }
        }

        /// <summary>
        /// this event fires when scenegraph is loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void m_objShotBox_OnShotBoxStatus(object sender, SHOTBOXARGS e)
        {
            if (Equals(e.SHOTBOXRESPONSE, SHOTBOXMSG.PREPARED))
            {
                btnPlay.BackColor = Color.DarkGreen;
                btnProgram.BackColor = Color.DarkGreen;
            }
        }

        /// <summary>
        /// used to getting the link
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GettingStart_Load(object sender, EventArgs e)
        {
            try
            {
                string sLinkID = null;
                m_sLinkType = ConfigurationManager.AppSettings["linktype"].ToString();
                string kcurl = ConfigurationManager.AppSettings["REMOTEMANAGERURL"].ToString();
                string appName= ConfigurationManager.AppSettings["appName"].ToString() ;
                int appPort = Convert.ToInt32(ConfigurationManager.AppSettings["appPort"]);

                //create remoteHelper to register Application in KC for response
                BeeSys.Wasp.Communicator.CRemoteHelper cRemoteHelper = new BeeSys.Wasp.Communicator.CRemoteHelper(kcurl, appName, appPort);
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
                    m_objLink.OnEngineConnected += new EventHandler<EngineArgs>(objLink_OnEngineConnected);
                this.FormClosing += new FormClosingEventHandler(GettingStart_FormClosing);
                RefreshServersList();
                rdbLocalSG.Checked = true;
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("loading", ex.Message);
            }
        }
        #endregion

        #region Methods

        private void RemoveUDTResponseInstance()
        {
            try
            {
                string kcUrl = ConfigurationManager.AppSettings["REMOTEMANAGERURL"].ToString();
                //remove client response proxy from UDTDataManager Helper
                if (!string.IsNullOrEmpty(kcUrl)
                    && CRemoteHelper.CurrentConnectionStatus.Status)
                {
                    ServiceUrl serviceUrl = CRemoteHelper.GetDisconnectedUrl("UDTDataManager");
                    UDTDataManagerHelper uDTDataManager = new UDTDataManagerHelper(serviceUrl.sEndpointAddress);
                    if (uDTDataManager != null)
                    {
                        ConnectArgs connectArgs = uDTDataManager.ConnectUdt(CRemoteHelper.WModuleName.ToString());
                        if (!string.IsNullOrEmpty(uDTDataManager.Sessionid))
                        {
                            uDTDataManager.RemoveSessionOnClose(uDTDataManager.Sessionid);
                            uDTDataManager.Dispose();
                            uDTDataManager = null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog(ex);
            }
        }
       
        /// <summary>
        /// Load the connected servers in ComboBox Servers
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
