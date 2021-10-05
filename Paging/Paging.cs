using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using System.Configuration;
using System.Windows.Forms;

using Beesys.Wasp.Workflow;
namespace PagingApp
{
    public partial class Paging : Form
    {
        #region member variables

        private LinkManager m_objLinkManager = null;
        private Link m_objLink = null;
        private ShotBox m_objShotBox = null;
        private string m_sLinkType = string.Empty;
        private string m_sEngineIP = string.Empty;
        private string m_sEngineUrl = string.Empty;
        private string m_sServerIp = string.Empty;
        private string kcurl;
        private string m_sWslPath = string.Empty;
        private bool m_bIsStop;
        private bool m_bIsPauseInfinite;
        private bool m_bIsPauseShotBox;
        private string m_sSGvariable;
        private ArrayList m_objArrayList = null;
        private string m_sPort = string.Empty;
        private int m_iPlayCount;
        private TagData m_objTagData;

        #endregion





        public Paging()
        {
            InitializeComponent();
            m_objArrayList = new ArrayList();
            m_objTagData = new TagData();
        }


        /// <summary>
        /// fires when engine is connected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void objLink_OnEngineConnected(object sender, EventArgs e)
        {
            btnConnect.BackColor = Color.DarkGreen;
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
        /// used to select and read the text file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTextFile_Click(object sender, EventArgs e)
        {
            FileInfo objFile = null;
            try
            {
                openFileDialog.Filter = "Text Files (*.txt)|*.txt";
                if (Equals(openFileDialog.ShowDialog(), DialogResult.OK))
                {
                    if (!String.IsNullOrEmpty(openFileDialog.FileName))
                    {
                        txtTextFile.Text = String.Empty;
                        objFile = new FileInfo(openFileDialog.FileName);
                        txtTextFile.Text = objFile.Name;
                        txtTextFile.Tag = openFileDialog.FileName;
                        ReadFile(objFile.FullName);
                    }

                }
                btnLoadScene.Enabled = true;
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("error in selecting the text file", ex);
            }
        }
        /// <summary>
        /// used to select the scenegraph
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnScene_Click(object sender, EventArgs e)
        {
            FileInfo objFile = null;
            try
            {
                openFileDialog.Filter = "wsl files (*.wsl)|*.wsl|wspx files|*.wspx";
                openFileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings["WSL_PATH"];
                if (m_objShotBox == null)
                {
                    if (Equals(openFileDialog.ShowDialog(), DialogResult.OK))
                    {
                        txtSceneGraph.Text = string.Empty;
                        txtSceneGraph.Tag = string.Empty;
                        objFile = new FileInfo(openFileDialog.FileName);
                        txtSceneGraph.Text = objFile.Name;
                        txtSceneGraph.Tag = objFile.FullName;
                        m_sWslPath = objFile.FullName;
                    }

                }
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("error in selecting the scenegraph", ex);
            }
            finally
            {
                objFile = null;
            }

        }
        /// <summary>
        /// used to prepare the scenegraph
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLoadScene_Click(object sender, EventArgs e)
        {
            string sSG = string.Empty;
            string sShotBoxID = string.Empty;
            bool bIsTicker;
            try
            {

                if (Equals(m_objShotBox, null) && !Equals(txtTextFile.Tag, null))
                {

                    sSG = Util.getSGFromWSL(m_sWslPath);
                    if (!Equals(sSG, null))
                    {
                        m_sSGvariable = sSG;
                        m_objShotBox = m_objLink.GetShotBox(sSG, typeof(ShotBox), out sShotBoxID, out bIsTicker) as ShotBox;

                        m_objShotBox.SetEngineUrl(m_sEngineUrl);
                        if (m_objShotBox is IAddinInfo)
                        {
                            // S.No.			: -	1
                            //(m_objShotBox as IAddinInfo).Init(new InstanceInfo() { Type = "wsl", InstanceId = string.Empty, TemplateId = m_sWslPath, ThemeId = "default" });
                            (m_objShotBox as IAddinInfo).Init(new InstanceInfo() { Type = "wsl", InstanceId = m_sWslPath, TemplateId = m_sWslPath, ThemeId = "default" });
                        }

                        m_objShotBox.OnShotBoxStatus += new EventHandler<SHOTBOXARGS>(objShotBox_OnShotBoxStatus);
                        m_objShotBox.OnShotBoxControllerStatus += new EventHandler<SHOTBOXARGS>(objShotBox_OnShotBoxControllerStatus);
                        m_objShotBox.Prepare(m_sEngineIP, 0, string.Empty, RENDERMODE.PROGRAM);

                    }
                }


            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("error in preparing the scenegraph", ex);
            }

        }
        /// <summary>
        /// fires when controller send the pageout status
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void objShotBox_OnShotBoxControllerStatus(object sender, SHOTBOXARGS e)
        {
            switch (e.SHOTBOXRESPONSE)
            {
                case SHOTBOXMSG.PAGEOUT: HandlePageOut();
                    break;


            }

        }
        /// <summary>
        /// handle pageout
        /// </summary>
        private void HandlePageOut()
        {
            try
            {
                if (m_objArrayList.Count > m_iPlayCount)
                {
                    UpdatePlayController();
                }
                else if (Equals(m_objArrayList.Count, m_iPlayCount))
                {
                    ContinuePlay();
                }

            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("error in handle pageout", ex);

            }

        }
        private void ContinuePlay()
        {
            try
            {
                if (!Equals(cbPlayText.CheckState, CheckState.Checked))
                    m_objShotBox.Play();
                else if (Equals(cbPlayText.CheckState, CheckState.Checked))
                {
                    m_iPlayCount = 0;
                    m_objArrayList.Clear();
                    ReadFile(txtTextFile.Tag.ToString());
                    UpdatePlayController();

                }
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("error in continue play", ex);
            }
        }
        /// <summary>
        /// update the controller
        /// </summary>
        private void UpdatePlayController()
        {
            m_objTagData.UserTags = new string[] { m_objShotBox.UserTags[1].Name };
            m_objTagData.Indexes = new string[] { "-1" };
            m_objTagData.IsOnAirUpdate = true;
            m_objTagData.SgXml = m_sSGvariable;
            m_objTagData.TagType = new DataTargetType[] { DataTargetType.UserTag };
            m_objTagData.Values = new string[] { m_objArrayList[m_iPlayCount].ToString().Trim() };
            m_objShotBox.Controllers[0].GoTo(0);
            m_objShotBox.UpdateSceneGraph(m_objTagData);
            m_objShotBox.Controllers[0].Play();
            m_iPlayCount++;

        }
        /// <summary>
        /// this event fires when scenegraph is loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void objShotBox_OnShotBoxStatus(object sender, SHOTBOXARGS e)
        {
            if (Equals(e.SHOTBOXRESPONSE, SHOTBOXMSG.PREPARED))
            {
                btnProgram.BackColor = Color.DarkGreen;
                btnPlayDefaultController.BackColor = Color.DarkGreen;
            }

        }
        /// <summary>
        /// used for reading the file
        /// </summary>
        /// <param name="sFile"></param>
        private void ReadFile(string sFile)
        {
            string sLine = null;
            try
            {
                if (!string.IsNullOrEmpty(sFile))
                {
                    TextReader objTextReader = new StreamReader(sFile);
                    while (!Equals(sLine = objTextReader.ReadLine(), null))
                    {
                        m_objArrayList.Add(sLine);
                    }
                    objTextReader.Close();
                }
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("error in reading the file", ex);

            }
        }



        /// <summary>
        /// used to taking the scenegraph on air
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOnAir_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Equals(m_objShotBox, null))
                {
                    m_objShotBox.SetRender(true);
                }

            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("error in taking the scenegrpah on air", ex);
            }
        }
        /// <summary>
        /// used to taking the scenegraph off air
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOffAir_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Equals(m_objShotBox, null))
                    m_objShotBox.SetRender(false);
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("error in taking the scenegrpah off air", ex);
            }
        }
        /// <summary>
        /// used to change the mode as program
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnProgram_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Equals(m_objShotBox, null))
                {
                    btnProgram.BackColor = Color.DimGray;
                    btnPreview.BackColor = Color.DarkGreen;
                    m_objShotBox.SetMode(RENDERMODE.PROGRAM);

                }
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("error in changing the mode:program", ex);
            }

        }
        /// <summary>
        /// used to change the mode as preview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPreview_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Equals(m_objShotBox, null))
                {
                    btnProgram.BackColor = Color.DarkGreen;
                    btnPreview.BackColor = Color.DimGray;
                    m_objShotBox.SetMode(RENDERMODE.PREVIEW);

                }
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("error in changing the mode:preview", ex);
            }
        }

        /// <summary>
        /// used to get the link from the link manager
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Paging_Load(object sender, EventArgs e)
        {
            string sLinkID = string.Empty;
            try
            {
                m_sPort = ConfigurationManager.AppSettings["port"].ToString();
                m_sLinkType = ConfigurationManager.AppSettings["linktype"].ToString();
                kcurl = ConfigurationManager.AppSettings["REMOTEMANAGERURL"].ToString();
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
                this.FormClosing += new FormClosingEventHandler(Paging_FormClosing);
                Init();
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("loading", ex.Message);
            }
        }
        private void Init()
        {
            try
            {
                RefreshServersList();
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog(ex);
            }
        }
        /// <summary>
        /// fires when user wants to close the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Paging_FormClosing(object sender, FormClosingEventArgs e)
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

            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("error in form closing", ex.Message);
            }
        }
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
            catch
            {

            }
        }
        /// <summary>
        /// used to play the default controller
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPlayDefaultController_Click(object sender, EventArgs e)
        {
            try
            {
                btnPlayDefaultController.BackColor = Color.DimGray;
                if (m_objShotBox != null)
                {
                    if (m_bIsPauseShotBox)
                    {
                        m_objShotBox.Play(false, false);
                        m_bIsPauseShotBox = false;
                    }
                    else
                    {
                        if (m_bIsStop)
                        {
                            m_objShotBox.Controllers[0].Stop();
                            m_iPlayCount = 0;
                            m_objArrayList.Clear();
                            ReadFile(txtTextFile.Tag.ToString());
                            m_bIsStop = false;
                        }
                        m_objTagData.UserTags = new string[] { m_objShotBox.UserTags[0].Name, m_objShotBox.UserTags[1].Name };
                        m_objTagData.Indexes = new string[] { "-1", "-1" };
                        m_objTagData.IsOnAirUpdate = true;
                        m_objTagData.SgXml = m_sSGvariable;
                        m_objTagData.TagType = new DataTargetType[] { DataTargetType.UserTag, DataTargetType.UserTag };
                        m_objTagData.Values = new string[] { "Header", m_objArrayList[0].ToString().Trim() };

                        m_objShotBox.UpdateSceneGraph(m_objTagData);
                        m_objShotBox.Play(true, true);
                        m_iPlayCount++;
                    }


                }
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("error in playing controller", ex);
            }
        }
        /// <summary>
        /// used to pause the default controller
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPauseDefaultController_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Equals(m_objShotBox, null))
                {

                    m_objShotBox.Pause();
                    m_objShotBox.Controllers[0].Pause();
                    m_bIsPauseShotBox = true;

                }
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("error in pausing controller", ex);

            }

        }
        /// <summary>
        /// used to stop the default controller
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStopDefaultController_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Equals(m_objShotBox, null))
                {
                    m_bIsPauseShotBox = false;
                    m_bIsStop = true;
                    m_objShotBox.Stop();
                    m_objShotBox.Controllers[0].Stop();
                    m_iPlayCount = 0;
                }
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("error in stoping controller", ex);
            }
        }
        /// <summary>
        /// used to play the controller 1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPlayController_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Equals(m_objShotBox, null))
                {
                    m_objShotBox.Controllers[0].Play(true, true);

                }
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("error in playing", ex);

            }
        }
        /// <summary>
        /// used to pause the controller 1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPauseController_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_objShotBox != null)
                {
                    m_objShotBox.Controllers[0].Pause();
                }
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("error in pausing", ex.Message);
            }
        }
        /// <summary>
        /// used to stop the controller 1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStopController_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_objShotBox != null)
                {
                    m_objShotBox.Controllers[0].Stop();
                    m_iPlayCount = 0;
                }
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("error in stoping", ex.Message);
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
            finally
            {

            }
        }
    }


}
