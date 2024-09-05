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
using BeeSys.Wasp.KernelController;
using System.Xml.Linq;
using System.Xml.XPath;
namespace PagingApp
{
    public partial class Paging : Form
    {
        #region Member variables

        private LinkManager m_objLinkManager = null;
        private Link m_objLink = null;
        private ShotBox m_objShotBox = null;
        private string m_sLinkType = string.Empty;
        private string m_sServerIp = string.Empty;
        //server ip used to connect shot box
        private string m_sShotBoxServerIp = string.Empty;
        private string m_sWslPath = string.Empty;
        private bool m_bIsStop;
        private bool m_bIsPauseInfinite;
        private bool m_bIsPauseShotBox;
        private string m_sSGvariable;
        private ArrayList m_objArrayList = null;
        private string m_sPort = string.Empty;
        private int m_iPlayCount;
        private TagData m_objTagData;
        string _sInstanceDataXml = string.Empty;
        Form _frmTemplatePool = new Form();
        CMosDataEntry _dataEntryControl = null;
        #endregion
        public Paging()
        {
            InitializeComponent();
            m_objArrayList = new ArrayList();
            m_objTagData = new TagData();
        }

        #region Events
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
        /// used to prepare the scenegraph
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLoadScene_Click(object sender, EventArgs e)
        {
            string sSG = string.Empty;
            string sShotBoxID = string.Empty;
            bool bIsTicker;
            string templateID = null;
            string instanceID = null;
            bool bValid = false;
            try
            {

                if (!Equals(txtSceneName.Tag, null))
                {
                    _sInstanceDataXml = string.Empty;
                    if (rdbLocalSG.Checked)
                        sSG = Util.getSGFromWSL(m_sWslPath);
                    else if(rdbTemplate.Checked)
                    {
                        templateID = txtSceneName.Tag.ToString();
                        bValid = Util.getSgXml(templateID, "default", out bIsTicker, out sSG);
                    }
                    else if (rdbInstance.Checked)
                    {
                        instanceID = txtSceneName.Tag.ToString();
                        sSG = Util.getSgFromInstanceID(txtSceneName.Tag.ToString(), out _sInstanceDataXml, out templateID);
                    }
                    if (!Equals(sSG, null))
                    {
                        //unload previous scene
                        if (m_objShotBox != null)
                            m_objShotBox.DeleteSg();
                        //Reset the control for next scene graph
                        cmbUserTag.Items.Clear();
                        cmbUserTag.Text = string.Empty;
                        m_iPlayCount = 0;

                        m_sSGvariable = sSG;
                        m_objShotBox = m_objLink.GetShotBox(sSG, typeof(ShotBox), out sShotBoxID, out bIsTicker) as ShotBox;
                        m_objShotBox.SetEngineUrl(m_sShotBoxServerIp);
                        m_objShotBox.SetOutputChannel(m_objLink.OutputChannel);
                        if (m_objShotBox is IAddinInfo)
                        {
                            if (rdbLocalSG.Checked)
                            {
                                (m_objShotBox as IAddinInfo).Init(new Beesys.Wasp.Workflow.InstanceInfo() { Type = "wspx", InstanceId = m_sWslPath, TemplateId = m_sWslPath, ThemeId = "default" });
                            }
                            else 
                            {
                                (m_objShotBox as IAddinInfo).Init(new Beesys.Wasp.Workflow.InstanceInfo() { Type = "wspx", InstanceId = instanceID, TemplateId = templateID, ThemeId = "default" });
                            }
                        }

                        m_objShotBox.OnShotBoxStatus += new EventHandler<SHOTBOXARGS>(objShotBox_OnShotBoxStatus);
                        m_objShotBox.OnShotBoxControllerStatus += new EventHandler<SHOTBOXARGS>(objShotBox_OnShotBoxControllerStatus);
                        m_objShotBox.Prepare(m_sShotBoxServerIp, 0, string.Empty, RENDERMODE.PROGRAM);
                        m_objShotBox.SceneCue();

                        if (m_objShotBox.UserTags != null)
                        {
                            for (int i = 0; i < m_objShotBox.UserTags.Count; i++)
                            {
                                if (!cmbUserTag.Items.Contains(m_objShotBox.UserTags[i].Name))
                                    cmbUserTag.Items.Add(m_objShotBox.UserTags[i].Name);
                            }
                        }
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
                case SHOTBOXMSG.PAGEOUT:
                    if (this.InvokeRequired)
                    {
                        this.Invoke(new Action(() => { HandlePageOut(); }));
                    }
                    else
                        HandlePageOut();
                    break;
            }
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
                string kcurl = ConfigurationManager.AppSettings["REMOTEMANAGERURL"].ToString();
                string appName = ConfigurationManager.AppSettings["appName"].ToString();
                int appPort = Convert.ToInt32(ConfigurationManager.AppSettings["appPort"]);

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
                {
                    m_objLink.OnEngineConnected += new EventHandler<EngineArgs>(objLink_OnEngineConnected);
                }
                this.FormClosing += new FormClosingEventHandler(Paging_FormClosing);
                RefreshServersList();
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("loading", ex.Message);
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
                //Environment.Exit(0) use becuase Exe stuck in task manager on closing the app
                Environment.Exit(0);

            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("error in form closing", ex.Message);
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
                        //https://waspsource.beesys.com/Products/Common/-/issues/3410
                        if (m_objArrayList.Count ==0 )
                        {
                            ReadFile(txtTextFile.Tag.ToString());
                        }

                        string variableName = cmbUserTag.Text;
                        UserTag selectedTag = null;
                        //get the tag name and dataType
                        if (m_objShotBox.UserTags != null)
                        {
                            selectedTag = m_objShotBox.UserTags.Where(item => string.Compare(item.Name, variableName, StringComparison.OrdinalIgnoreCase) == 0).FirstOrDefault();
                            if (selectedTag == null)
                                return;
                        }

                        m_objTagData.UserTags = new string[] { selectedTag.Name };// m_objShotBox.UserTags[1].Name };
                        m_objTagData.Indexes = new string[] { "-1" };
                        m_objTagData.IsOnAirUpdate = true;
                        m_objTagData.SgXml = m_sSGvariable;

                        DataTargetType dataTargetType = (DataTargetType)Enum.Parse(typeof(DataTargetType), selectedTag.DataType.ToString());
                        m_objTagData.TagType = new DataTargetType[] { dataTargetType };
                        m_objTagData.Values = new string[] { m_objArrayList[m_iPlayCount].ToString().Trim() };
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
                    //play controller from where it left, hence call with false,false
                    m_objShotBox.Controllers[0].Play(false, false);

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
                }
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog("error in stoping", ex.Message);
            }
        }
        /// <summary>
        /// To open File dialog to select template from local disk
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            FileInfo objFile = null;
            try
            {
                if (rdbLocalSG.Checked)
                {
                    openFileDialog.Filter = "wspx files|*.wspx";
                    openFileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings["WSL_PATH"];
                    //if (m_objShotBox == null)
                    {
                        if (Equals(openFileDialog.ShowDialog(), DialogResult.OK))
                        {
                            txtSceneName.Text = string.Empty;
                            txtSceneName.Tag = string.Empty;
                            objFile = new FileInfo(openFileDialog.FileName);
                            txtSceneName.Text = objFile.Name;
                            txtSceneName.Tag = objFile.FullName;
                            m_sWslPath = objFile.FullName;
                        }

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
                            _dataEntryControl.Dock = DockStyle.Fill;
                            _dataEntryControl.HandleInstanceDoubleClick = true;
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
            finally
            {
                objFile = null;
            }

        }
        /// <summary>
        /// Handle event to update the loaded instance with udpate data
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
                        string templateID = string.Empty;
                        string xml = Util.getSgFromInstanceID(instanceID, out _sInstanceDataXml, out templateID);
                        if (!string.IsNullOrEmpty(_sInstanceDataXml)
                            && m_objShotBox != null)
                        {
                            m_iPlayCount = 0;
                            m_objArrayList.Clear();

                            m_objShotBox.Controllers[0].Stop();
                            //update the paging data and udpate scene graph
                            ReadFile(txtTextFile.Tag.ToString());


                            m_objShotBox.UpdateSceneGraph(_sInstanceDataXml, true);

                            m_objShotBox.Controllers[0].Play();
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
        /// Handle the event to set the selected template name 
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
        #endregion

        #region Methods

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
            //get the variable name to update for page data
            string variableName = cmbUserTag.Text;
            UserTag selectedTag = null;
            //get the tag name and dataType
            if (m_objShotBox.UserTags != null)
            {
                selectedTag = m_objShotBox.UserTags.Where(item => string.Compare(item.Name, variableName, StringComparison.OrdinalIgnoreCase) == 0).FirstOrDefault();
                if (selectedTag == null)
                    return;
            }

            //create TagData structure with value and call update scene graph for udpated data 
            m_objTagData.UserTags = new string[] { selectedTag.Name };
            m_objTagData.Indexes = new string[] { "-1" };
            m_objTagData.IsOnAirUpdate = true;
            m_objTagData.SgXml = m_sSGvariable;

            DataTargetType dataTargetType = (DataTargetType)Enum.Parse(typeof(DataTargetType), selectedTag.DataType.ToString());
            m_objTagData.TagType = new DataTargetType[] { dataTargetType };
            m_objTagData.Values = new string[] { m_objArrayList[m_iPlayCount].ToString().Trim() };
            m_objShotBox.Controllers[0].GoTo(0);
            m_objShotBox.UpdateSceneGraph(m_objTagData);
            m_objShotBox.Controllers[0].Play();
            m_iPlayCount++;
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
                if(!string.IsNullOrEmpty(_sInstanceDataXml) && rdbInstance.Checked)
                {
                    XDocument xdInstanceData = XDocument.Parse(_sInstanceDataXml);
                    if(xdInstanceData!=null)
                    {
                        //xdInstanceData.Save("D:instance.xml");
                        IEnumerable<XElement> xePages = xdInstanceData.XPathSelectElements("//data//datauser//object//page");
                        if(xePages!=null && xePages.Count()>0)
                        {
                            string pageData = string.Empty;
                            foreach (XElement page in xePages)
                            {
                                pageData = page.Value;
                                m_objArrayList.Add(pageData);
                            }
                        }
                    }
                }


                if (!string.IsNullOrEmpty(sFile) 
                    && (rdbLocalSG.Checked || rdbTemplate.Checked))
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
            catch (Exception ex)
            {
                LogWriter.WriteLog(ex);
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
