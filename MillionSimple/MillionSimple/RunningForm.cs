using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MillionSimple.Model;
using MillionSimple.Job;
using MillionSimple.CommonEnum;
using MillionSimple.Data;
using MillionSimple.Common;
using System.Threading;

namespace MillionSimple
{
    public partial class RunningForm : Form
    {

        #region 构造函数
        public static RunningForm _runningForm;

        public static RunningForm Instance(AccountModel model, Million main)
        {
            if (_runningForm == null || _runningForm.IsDisposed)
            {
                _runningForm = new RunningForm(model, main);
            }
            return _runningForm;
        }

        public RunningForm()
        {
            InitializeComponent();

        }
        public RunningForm(AccountModel model, Million main)
        {
            this._account = model;
            this._main = main;
            InitializeComponent();
            this.Job = new AutoJob(Account);
            #region 状态监测
            CheckRunningState();
            #endregion
        }
        #region 状态监测,用以保证程序因错误停止后，自动重新开始
        /// <summary>
        /// 状态监测,用以保证程序因错误停止后，自动重新开始
        /// </summary>
        private void CheckRunningState()
        {
            Thread t = new Thread(new ThreadStart(RestartJobFromError));
            t.Start();
            Thread t2 = new Thread(new ThreadStart(RestartJobThanOneHour));
            t2.Start();
        }


        private void RestartJobFromError()
        {
            while (!this.IsDisposed)
            {
                Thread.Sleep(60 * 1000);
                if (Job.PlayerState == PlayerStateEnum.Stop)
                {
                    //等待放妖10秒间隔结束
                    Thread.Sleep(20 * 1000);
                }
                if (Job.PlayerState == PlayerStateEnum.Error || Job.PlayerState == PlayerStateEnum.Stop)
                {
                    btnStartAuto.Enabled = false;
                    开启一键放妖ToolStripMenuItem.Enabled = false;
                    btnStopAuto.Enabled = true;
                    关闭一键放妖ToolStripMenuItem.Enabled = true;
                    Job.continueAutoJob = true;
                    Job.Login();
                    if (Job.PlayerState == PlayerStateEnum.Login)
                    {
                        Job.ReleaseFairyJob();
                    }
                }
            }
        }

        private void RestartJobThanOneHour()
        {
            while (!this.IsDisposed)
            {
                //每小时重启一次JOB
                Thread.Sleep(60 * 60 * 1000);
                Thread.Sleep(20 * 1000);
                if (Job.JobMode)
                {
                    Job.continueAutoJob = false;
                    Invoke(new RefreshDelegate(delegate()
                    {
                        txtMessage.Text = Des.GetTimeDes();
                        txtMessage.Text += CommonString.HX;
                        txtMessage.Text += "为了程序的稳定性，周期性清除数据，重新开始一键放妖！";
                        txtMessage.Text += CommonString.HX;
                        txtMessage.Text += CommonString.HX;
                    }));
                    //等待放妖模式的10秒间隔结束
                    Thread.Sleep(20 * 1000);
                    btnStartAuto.Enabled = false;
                    开启一键放妖ToolStripMenuItem.Enabled = false;
                    btnStopAuto.Enabled = true;
                    关闭一键放妖ToolStripMenuItem.Enabled = true;
                    Job.continueAutoJob = true;
                    Job.Login();
                    if (Job.PlayerState == PlayerStateEnum.Login)
                    {
                        Job.ReleaseFairyJob();
                    }
                }

            }
        }
        #endregion



        #endregion

        #region 属性

        private AccountModel _account;

        public AccountModel Account
        {
            get { return _account; }
            set { _account = value; }
        }


        private Million _main;

        public Million Main
        {
            get { return _main; }
            set { _main = value; }
        }

        private AutoJob _job;

        public AutoJob Job
        {
            get { return _job; }
            set { _job = value; }
        }

        private static string Path = @"\MillionSimple\MillionSimple\RunningForm.cs";

        private bool _isReturn = false;
        /// <summary>
        /// 指示一个值，来标记是关闭窗体还是最小化
        /// </summary>
        public bool IsClose
        {
            get { return _isReturn; }
            set { _isReturn = value; }
        }




        #endregion

        #region 事件
        private void RunningForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!IsClose)
            {
                AppHide();
                e.Cancel = true;
            }
            IsClose = false;
        }

        private void RunningForm_Load(object sender, EventArgs e)
        {
            Start();
            this.Text = string.Format("{0} {1}", Des.APPName, Job.Account.PlayerModel.Name);
        }

        public void Start()
        {
            this.Text = "";
            this.Text += "";
            Job.Messaging += new Common.MessageEventHandler(Job_Messaging);
            Job.AutoMessaging += new MessageEventHandler(Job_AutoMessaging);
            Job.Login();
        }

        /// <summary>
        /// 自动信息
        /// </summary>
        /// <param name="msg"></param>
        void Job_AutoMessaging(string msg)
        {
            #region 状态信息
            InvokeMessae(Des.GetPlayerDes(Account), txtState, true);
            #endregion

            #region 其余信息
            ShowMessage(msg);
            #endregion
        }

        private void InvokeMessae(string message, TextBox txt, bool clear = false)
        {
            try
            {
                Invoke(new RefreshDelegate(delegate()
                   {
                       if (clear)
                       {
                           txt.Text = "";
                       }
                       txt.Text += message;
                   }));
            }
            catch (Exception ex)
            {
                LogHelper.ErrorLog(Path, "InvokeMessae", ex.Message);
            }
        }

        private void ShowMessage(string message, bool needScroll = true)
        {
            try
            {
                Invoke(new RefreshDelegate(delegate()
                  {
                      txtMessage.Text += Des.GetTimeDes();
                      txtMessage.Text += CommonString.HX;
                      txtMessage.Text += message;
                      txtMessage.Text += CommonString.HX;
                      txtMessage.Text += CommonString.HX;
                      if (needScroll)
                      {
                          this.txtMessage.SelectionStart = this.txtMessage.TextLength;
                          this.txtMessage.ScrollToCaret();
                      }
                  }));
            }
            catch (Exception ex)
            {
                LogHelper.ErrorLog(Path, "ShowMessage", ex.Message);
            }
        }

        /// <summary>
        /// 手动信息
        /// </summary>
        /// <param name="msg"></param>
        void Job_Messaging(string msg)
        {
            string[] str = msg.Split('|');
            #region 状态信息
            InvokeMessae(Des.GetPlayerDes(Account), txtState, true);
            #endregion

            #region 其余信息
            InvokeMessae("", txtMessage, true);
            ShowMessage(str[str.Length - 1], false);
            #endregion

        }
        #endregion

        #region 手动
        private void btnRefreshMap_Click(object sender, EventArgs e)
        {
            Job.SelectArea();
        }

        private void btnInMap_Click(object sender, EventArgs e)
        {
            Job.InMap(txtMapId.Text.Trim());
        }

        private void btnAddAp_Click(object sender, EventArgs e)
        {
            Job.SetPoint(1, 0);
        }

        private void btnDrinkGreen_Click(object sender, EventArgs e)
        {
            Job.Drink(DrinkEnum.Green);
        }

        private void btnExplore_Click(object sender, EventArgs e)
        {
            Job.Explore();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Job.MainMenu();
        }

        private void btnLick_Click(object sender, EventArgs e)
        {
            Job.LickFairy();
        }

        private void btnFairySelect_Click(object sender, EventArgs e)
        {
            Job.FairySelect();
        }

        private void btnAddFriend_Click(object sender, EventArgs e)
        {
            string name = txtFriendName.Text.Trim();
            if (string.IsNullOrEmpty(name))
            {
                txtMessage.Text = "请输入好友名称";
                return;
            }
            Job.SearchPlayer(name);
            Job.AddFriend();
        }

        private void btnRemoveFriend_Click(object sender, EventArgs e)
        {
            string name = txtFriendName.Text.Trim();
            if (string.IsNullOrEmpty(name))
            {
                txtMessage.Text = "请输入好友名称";
                return;
            }
            Job.SearchPlayer(name);
            Job.RemoveFriend();
        }

        private void btnSaveCard_Click(object sender, EventArgs e)
        {
            //Job.LookCard();
            Job.SaveCard();
        }

        private void btnAddBC_Click(object sender, EventArgs e)
        {
            Job.SetPoint(0, 1);
        }

        private void btnDrinkRed_Click(object sender, EventArgs e)
        {
            Job.Drink(DrinkEnum.Red);
        }

        private void btnGetPresent_Click(object sender, EventArgs e)
        {
            Job.LookGift();
            Job.GetGifts();
        }

        private void btnFriendList_Click(object sender, EventArgs e)
        {
            Job.LookFriend();
        }

        private void btnFriendApplyList_Click(object sender, EventArgs e)
        {
            Job.FriendApplyList();
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            string name = txtFriendName.Text.Trim();
            if (string.IsNullOrEmpty(name))
            {
                txtMessage.Text = "请输入好友名称";
                return;
            }
            Job.SearchPlayer(name);
            Job.ApproveFriend();
        }

        private void btnAddAllAp_Click(object sender, EventArgs e)
        {
            Job.SetPoint(3, 0);
        }

        private void btnAddAllBC_Click(object sender, EventArgs e)
        {
            Job.SetPoint(0, 3);
        }



        private void btnExit_Click(object sender, EventArgs e)
        {
            AppExit();
        }

        private void nIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
        }

        private void 显示ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void 开启一键放妖ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnAutoReleaseFairy_Click(sender, e);
        }

        private void 关闭一键放妖ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnStopRelease_Click(sender, e);
        }

        private void 退出程序ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IsClose = true;
            AppExit();
        }

        private void AppExit()
        {
            AppClose();
            Application.Exit();
        }

        private void AppHide()
        {
            this.Hide();
            this.nIcon.Visible = true;
            this.nIcon.ShowBalloonTip(1000, "", "四系乃在这里哦", ToolTipIcon.None);
        }

        private void AppReturn()
        {
            Main.Show();
            AppClose();
        }

        private void AppClose()
        {
            IsClose = true;
            Job.continueAutoJob = false;
            Job.PlayerState = PlayerStateEnum.Default;
            Job.Messaging -= new Common.MessageEventHandler(Job_Messaging);
            Job.AutoMessaging -= new MessageEventHandler(Job_AutoMessaging);
            this.Close();
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            AppReturn();
        }

        private void btnLookPlayer_Click(object sender, EventArgs e)
        {
            string name = txtFriendName.Text;
            if (string.IsNullOrEmpty(name))
            {
                return;
            }
            Job.SearchPlayer(name);
        }

        private void btnLookCard_Click(object sender, EventArgs e)
        {
            Job.LookCard();
        }

        private void btnFootBall_Click(object sender, EventArgs e)
        {
            string name = txtFriendName.Text;
            if (string.IsNullOrEmpty(name))
            {
                return;
            }
            Job.GetFootBall(name);
        }

        private void btnBattle_Click(object sender, EventArgs e)
        {
            string id = txtUserId.Text.Trim();
            if (string.IsNullOrEmpty(id))
            {
                txtMessage.Text += "\r\n请输入玩家ID\r\n";
                return;
            }
            Job.Battle(id);
        }
        #endregion

        #region 自动
        private void btnStartAuto_Click(object sender, EventArgs e)
        {
            Protocol pro = new Protocol();
            if (rbBao.Checked)
            {
                pro.AutoMode = AutoModeEnum.BaoGan;
                Job.BaoGan(pro);
            }
            else if (rbFang.Checked)
            {
                pro.AutoMode = AutoModeEnum.FangYao;
                btnAutoReleaseFairy_Click(sender, e);
            }
            else if (rbXiu.Checked)
            {
                pro.AutoMode = AutoModeEnum.XiuXian;
                Job.XiuXian(pro);

            }
            else
            {
                pro.AutoMode = AutoModeEnum.Manual;
            }
        }

        private void btnStopAuto_Click(object sender, EventArgs e)
        {
            btnStopRelease_Click(sender, e);
        }

        #region 爆肝

        #endregion

        #region 休闲

        #endregion

        #region 放妖
        [Obsolete("请使用控制器")]
        private void btnAutoReleaseFairy_Click(object sender, EventArgs e)
        {
            txtMessage.Text = "开始一键放妖";
            txtMessage.Text += CommonString.HX;
            txtMessage.Text += CommonString.HX;
            Job.JobMode = true;
            btnStartAuto.Enabled = false;
            开启一键放妖ToolStripMenuItem.Enabled = false;
            btnStopAuto.Enabled = true;
            关闭一键放妖ToolStripMenuItem.Enabled = true;
            Job.continueAutoJob = true;
            Job.ReleaseFairyJob();
        }

        [Obsolete("请使用控制器")]
        private void btnStopRelease_Click(object sender, EventArgs e)
        {
            Job.SendMessage("手动停止一键放妖！");
            Job.JobMode = false;
            btnStopAuto.Enabled = false;
            关闭一键放妖ToolStripMenuItem.Enabled = false;
            btnStartAuto.Enabled = true;
            开启一键放妖ToolStripMenuItem.Enabled = true;
            Job.continueAutoJob = false;
            Job.PlayerState = PlayerStateEnum.End;
        }
        #endregion

        #endregion




    }
}
