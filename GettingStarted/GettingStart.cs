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

namespace GettingStarted
{
    public partial class GettingStart : Form
    {

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
        private string m_sPort = string.Empty;
        public GettingStart()
        {
            InitializeComponent();
        }

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

            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("error in form closing", ex.Message);
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
                openFileDialog1.Filter = "wspx files|*.wspx";
                openFileDialog1.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
                if (Equals(openFileDialog1.ShowDialog(), DialogResult.OK))
                {
                    m_fileInfo = new FileInfo(openFileDialog1.FileName);
                    txtSceneName.Text = m_fileInfo.Name;
                }
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("error in selecting the scenegraph", ex.Message);
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
                string xml = Util.getSGFromWSL(openFileDialog1.FileName);
                string shotBoxID = null;
                bool isTicker;
                if (!string.IsNullOrEmpty(xml))
                {
                    m_objShotBox = m_objLink.GetShotBox(xml, out shotBoxID, out isTicker) as ShotBox;
                    if (!Equals(m_objShotBox, null))
                    {
                        m_objShotBox.SetEngineUrl(m_sShotBoxServerIp);
                        m_objShotBox.SetOutputChannel(m_objLink.OutputChannel);
                        if (m_objShotBox is IAddinInfo)
                        {
                            //S.No.			: -	1
                            // (m_objShotBox as IAddinInfo).Init(new InstanceInfo() { Type = "wsl", InstanceId = string.Empty, TemplateId = openFileDialog1.FileName, ThemeId = "default" });
                            (m_objShotBox as IAddinInfo).Init(new InstanceInfo() { Type = "wspx", InstanceId = openFileDialog1.FileName, TemplateId = openFileDialog1.FileName, ThemeId = "default" });
                        }
                        m_objShotBox.OnShotBoxStatus += new EventHandler<SHOTBOXARGS>(m_objShotBox_OnShotBoxStatus);
                        m_objShotBox.Prepare(m_sShotBoxServerIp, 0, string.Empty, RENDERMODE.PROGRAM);
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
                m_sPort = ConfigurationManager.AppSettings["port"].ToString();
                m_sLinkType = ConfigurationManager.AppSettings["linktype"].ToString();
                string kcurl = ConfigurationManager.AppSettings["REMOTEMANAGERURL"].ToString();
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
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("loading", ex.Message);
            }
        }

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
    }
}
