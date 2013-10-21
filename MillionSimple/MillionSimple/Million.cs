using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using MillionSimple.Common;
using MillionSimple.Model;
using System.Threading;
using MillionSimple.Data;

namespace MillionSimple
{
    public partial class Million : Form
    {
        private static string Path = @"\MillionSimple\MillionSimple\Million.cs";


        public Million()
        {
            InitializeComponent();
        }

        #region 属性

        private List<AccountModel> _accountList;

        public List<AccountModel> AccountList
        {
            get { return _accountList; }
            set { _accountList = value; }
        }

        /// <summary>
        /// 当前运行中窗体列表
        /// </summary>
        List<RunningForm> runningList = null;

        /// <summary>
        /// 标识是否继续添加好友
        /// </summary>
        private bool continueAddFriend = false;

        /// <summary>
        /// 标示是否继续领取礼包
        /// </summary>
        private bool continueGetGits = false;


        #endregion

        #region 事件

        /// <summary>
        /// 小号管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAccountManage_Click(object sender, EventArgs e)
        {
            AccountManage manage = AccountManage.Instance(this);
            manage.Show();
            this.Hide();
        }

        /// <summary>
        /// 窗体载入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Million_Load(object sender, EventArgs e)
        {
            GetAccount();
            this.Text = Des.APPName;
        }

        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            Login();
        }

        /// <summary>
        /// 一键领取礼包
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAllLogin_Click(object sender, EventArgs e)
        {

            if (btnAllLogin.Text == "全部登陆并领取礼包")
            {
                txtLoginMessage.Text = "";
                continueGetGits = true;
                Thread t = new Thread(new ThreadStart(OneKeyGetGift));
                t.Start();
                btnAllLogin.Text = "取消全部登陆并领取礼包";
            }
            else
            {
                continueGetGits = false;
                btnAllLogin.Text = "全部登陆并领取礼包";
            }
        }

        /// <summary>
        /// 一键循环加好友
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOneKeyAddFriend_Click(object sender, EventArgs e)
        {

            if (btnOneKeyAddFriend.Text == "一键循环加好友")
            {
                txtLoginMessage.Text = "开始一键循环加好友\r\n";
                continueAddFriend = true;
                Thread t = new Thread(new ThreadStart(OneKeyAddFriend));
                t.Start();
                btnOneKeyAddFriend.Text = "取消一键循环加好友";
            }
            else
            {
                continueAddFriend = false;
                btnOneKeyAddFriend.Text = "一键循环加好友";
                txtLoginMessage.Text = "停止一键循环加好友\r\n";

            }

        }

        #endregion

        #region 方法

        /// <summary>
        /// 用以标记账号信息是否完整
        /// </summary>
        private bool IsMessageFull = true;

        /// <summary>
        /// 标记需要更新信息的账号集合
        /// </summary>
        private List<AccountModel> NeedUpdateList = new List<AccountModel>();

        /// <summary>
        /// 获取小号
        /// </summary>
        public void GetAccount()
        {
            XmlDocument xmlDoc = XmlHelper.GetConfig();
            XmlNodeList list = xmlDoc.SelectNodes("/root/Data/Character");
            if (list == null && list.Count == 0)
            {
                txtLoginMessage.Text = "亲，你还没配置小号哦！";
                return;
            }
            AccountList = new List<AccountModel>();
            AccountModel model = null;
            bool isAccountFull = false;
            foreach (XmlNode node in list)
            {
                model = new AccountModel();
                PlayerModel player = new PlayerModel();
                model.Account = node.ChildNodes[0].InnerText;
                model.Password = node.ChildNodes[1].InnerText;
                string name = string.Empty;
                if (node.ChildNodes.Count == 4)
                {
                    isAccountFull = true;
                    player.Name = node.ChildNodes[2].InnerText;
                    player.UserId = node.ChildNodes[3].InnerText;
                    name = player.Name;
                }
                model.PlayerModel = player;
                string plus = name == "" ? "" : string.Format("{0}|", name);
                string showAccount = plus + node.ChildNodes[0].InnerText;
                this.lbx.Items.Add(showAccount);
                if (!isAccountFull)
                {
                    IsMessageFull = false;
                    NeedUpdateList.Add(model);
                }
                AccountList.Add(model);
            }
        }

        /// <summary>
        /// 登陆
        /// </summary>
        private void Login()
        {
            if (lbx.SelectedItem == null)
            {
                txtLoginMessage.Text = "亲，选择你要登陆的小号";
                return;
            }

            AccountModel account = new AccountModel();
            string accountMessage = lbx.SelectedItem.ToString();
            string[] str = accountMessage.Split('|');
            account.Account = str[str.Length - 1];
            foreach (AccountModel item in AccountList)
            {
                if (item.Account == account.Account)
                {
                    account.Password = item.Password;
                    break;
                }
            }
            RunningForm newForm = RunningForm.Instance(account, this);
            if (cbx.Checked)
            {
                newForm.Job.IsEncrypt = true;
            }
            newForm.Show();
            this.Hide();
        }


        #region 一键循环加好友
        /// <summary>
        /// 一键循环加好友
        /// </summary>
        private void OneKeyAddFriend()
        {
            try
            {
                #region 循环登陆获取角色名称和ID
                if (!IsMessageFull)
                {
                    if (!GetLoginAccountMessage(1))
                    {
                        return;
                    }
                }
                #endregion
                if (runningList == null || runningList.Count == 0)
                {
                    RunningForm childRunning = null;
                    runningList = new List<RunningForm>();
                    foreach (AccountModel account in AccountList)
                    {
                        if (!continueAddFriend)
                        {
                            break;
                        }
                        childRunning = new RunningForm(account, this);
                        childRunning.Start();
                        runningList.Add(childRunning);
                        AddFriend(childRunning);
                        ApproveFriend(childRunning);
                    }
                }
                else
                {
                    foreach (RunningForm childRunning in runningList)
                    {
                        if (!continueAddFriend)
                        {
                            break;
                        }
                        AddFriend(childRunning);
                    }

                    foreach (RunningForm childRunning in runningList)
                    {
                        if (!continueAddFriend)
                        {
                            break;
                        }
                        ApproveFriend(childRunning);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.ErrorLog(Path, "OneKeyAddFriend", ex.Message);
            }
            finally
            {
                continueAddFriend = false;
                Invoke(new RefreshDelegate(delegate()
                {
                    btnOneKeyAddFriend.Text = "一键循环加好友";
                }));
                ShowMessage("一键循环加好友已结束");
            }
        }

        private bool GetLoginAccountMessage(int type)
        {
            RunningForm running = null;
            XmlDocument xmlDoc = XmlHelper.GetConfig();
            XmlNodeList list = xmlDoc.GetElementsByTagName("Account");
            if (list == null || list.Count == 0)
            {
                return false;
            }
            foreach (AccountModel account in NeedUpdateList)
            {
                XmlNode currentNode = null;
                if (type == 1)
                {
                    if (!continueAddFriend)
                    {
                        break;
                    }
                }
                else if (type == 2)
                {
                    if (!continueGetGits)
                    {
                        break;
                    }
                }

                //更新每一个账号的信息
                running = new RunningForm(account, this);
                running.Start();
                #region 新增账号
                bool exist = false;
                foreach (XmlNode node in list)
                {
                    if (node.InnerText == account.Account)
                    {
                        exist = true;
                        currentNode = node;
                        break;
                    }
                }
                if (!exist)
                {
                    //如当前账号不存在，则添加节点
                    XmlElement Character = xmlDoc.CreateElement("Character");
                    XmlElement accountNode = xmlDoc.CreateElement("Account");
                    accountNode.InnerText = account.Account;
                    XmlElement passwordNode = xmlDoc.CreateElement("Password");
                    passwordNode.InnerText = account.Password;
                    XmlElement nameNode = xmlDoc.CreateElement("Name");
                    nameNode.InnerText = account.PlayerModel.Name;
                    XmlElement idNode = xmlDoc.CreateElement("Id");
                    idNode.InnerText = account.PlayerModel.UserId;
                    Character.AppendChild(accountNode);
                    Character.AppendChild(passwordNode);
                    Character.AppendChild(nameNode);
                    Character.AppendChild(idNode);
                    xmlDoc.ChildNodes[1].ChildNodes[0].AppendChild(Character);
                    continue;
                }
                #endregion
                #region 更新账号
                //当前账号存在，则更新信息，并添加缺少的子节点
                XmlNode parentNode = currentNode.ParentNode;
                #region  密码节点
                AddOrEditNode(xmlDoc, parentNode, "Password", account.Password);
                #endregion
                #region 名称节点
                AddOrEditNode(xmlDoc, parentNode, "Name", account.PlayerModel.Name);
                #endregion
                #region ID节点
                AddOrEditNode(xmlDoc, parentNode, "Id", account.PlayerModel.UserId);
                #endregion

                #endregion

                xmlDoc.Save(XmlHelper.ConfigPath);
                string message = string.Format("更新了账号:{0}", account.Account);
                ShowMessage(message);
            }
            return true;
        }

        #region 新增或修改子节点
        /// <summary>
        /// 新增或修改子节点
        /// </summary>
        private void AddOrEditNode(XmlDocument xmlDoc, XmlNode parentNode, string name, string value)
        {
            bool isExist = false;
            foreach (XmlNode child in parentNode.ChildNodes)
            {
                if (child.Name == name)
                {
                    child.InnerText = value;
                    isExist = true;
                    break;
                }
            }
            if (!isExist)
            {
                XmlElement passwordNode = xmlDoc.CreateElement(name);
                passwordNode.InnerText = value;
                parentNode.AppendChild(passwordNode);
            }
        }
        #endregion



        #region 发出加好友申请
        /// <summary>
        /// 发出加好友申请
        /// </summary>
        /// <param name="running"></param>
        /// <param name="id"></param>
        private void AddFriend(RunningForm running, string id)
        {
            try
            {


                if (string.IsNullOrEmpty(id))
                {
                    return;
                }
                running.Job.Account.PlayerModel.Running.FriendId = id;
                string message = running.Job.AddFriend();
                string content = string.Format("当前账号{0}/{1},{2}", running.Account.PlayerModel.Name, running.Account.PlayerModel.UserId, message);
                ShowMessage(content);
            }
            catch (Exception)
            {

                throw;
            }

        }

        private void AddFriend(RunningForm childRunning)
        {
            try
            {
                childRunning.Job.LookFriend();
                childRunning.Job.FriendApplyState();
                childRunning.Job.FriendApplyList();
                RunningModel runningModel = childRunning.Job.Account.PlayerModel.Running;
                //空余好友位置不足0位时，不增加好友
                if (ToolKit.StringToInt(runningModel.MaxFriend) - ToolKit.StringToInt(runningModel.CurrentFriend) <= 0)
                {
                    return;
                }

                foreach (AccountModel waitAccount in AccountList)
                {
                    if (!continueAddFriend)
                    {
                        break;
                    }
                    //自己不添加
                    if (childRunning.Job.Account.PlayerModel.UserId == waitAccount.PlayerModel.UserId)
                    {
                        continue;
                    }
                    //当断当前欲添加的账号，是否已经是好友，
                    bool needContinue = false;
                    foreach (OtherUserModel model in runningModel.FriendList)
                    {
                        if (waitAccount.PlayerModel.UserId == model.id)
                        {
                            needContinue = true;
                            break;
                        }
                    }
                    if (needContinue)
                    {
                        continue;
                    }
                    //是否在申请情况列表中
                    needContinue = false;
                    foreach (OtherUserModel model in runningModel.FriendApplyStateList)
                    {
                        if (waitAccount.PlayerModel.UserId == model.id)
                        {
                            needContinue = true;
                            break;
                        }
                    }
                    if (needContinue)
                    {
                        continue;
                    }
                    //是否在好友通知中
                    foreach (OtherUserModel model in runningModel.ApplyFriendList)
                    {
                        if (waitAccount.PlayerModel.UserId == model.id)
                        {
                            needContinue = true;
                            break;
                        }
                    }
                    if (needContinue)
                    {
                        continue;
                    }


                    AddFriend(childRunning, waitAccount.PlayerModel.UserId);
                }
            }
            catch (Exception ex)
            {
                LogHelper.ErrorLog(Path, "AddFriend", ex.Message);
            }
        }
        #endregion

        #region 接受好友
        /// <summary>
        /// 接受好友
        /// </summary>
        /// <param name="running"></param>
        /// <param name="id"></param>
        private void ApproveFriend(RunningForm running, string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return;
                }
                running.Job.Account.PlayerModel.Running.FriendId = id;
                string message = running.Job.ApproveFriend();
                string content = string.Format("当前账号{0}/{1},{2}", running.Account.PlayerModel.Name, running.Account.PlayerModel.UserId, message);
                ShowMessage(content);

            }
            catch (Exception ex)
            {
                LogHelper.ErrorLog(Path, "AddFriend", ex.Message);
            }
        }

        private void ApproveFriend(RunningForm childRunning)
        {
            try
            {
                RunningModel runningModel = childRunning.Job.Account.PlayerModel.Running;
                foreach (AccountModel waitAccount in AccountList)
                {
                    if (!continueAddFriend)
                    {
                        break;
                    }
                    //空余好友位置不足2位时，不增加好友
                    if (ToolKit.StringToInt(runningModel.MaxFriend) - ToolKit.StringToInt(runningModel.CurrentFriend) <= 2)
                    {
                        break;
                    }
                    //自己账号跳过
                    if (waitAccount.PlayerModel.UserId == childRunning.Account.PlayerModel.UserId)
                    {
                        continue;
                    }
                    //当断当前欲添加的账号 是否在向我好友通知中,若在，则接受
                    foreach (OtherUserModel model in runningModel.ApplyFriendList)
                    {
                        if (waitAccount.PlayerModel.UserId == model.id)
                        {
                            ApproveFriend(childRunning, waitAccount.PlayerModel.UserId);
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.ErrorLog(Path, "ApproveFriend", ex.Message);
            }
        }

        #endregion
        #endregion


        #region 一键登录领取礼包
        /// <summary>
        /// 一键登录领取礼包
        /// </summary>
        private void OneKeyGetGift()
        {
            try
            {
                #region 循环登陆获取角色名称和ID
                if (!IsMessageFull)
                {
                    if (!GetLoginAccountMessage(2))
                    {
                        return;
                    }
                }
                #endregion
                continueGetGits = true;
                if (runningList == null || runningList.Count == 0)
                {
                    runningList = new List<RunningForm>();
                    foreach (AccountModel account in AccountList)
                    {
                        if (!continueGetGits)
                        {
                            break;
                        }
                        RunningForm runningForm = new RunningForm(account, this);
                        LoginAndGetGift(runningForm);
                        runningList.Add(runningForm);
                    }
                }
                else
                {
                    foreach (RunningForm runningForm in runningList)
                    {
                        if (!continueGetGits)
                        {
                            break;
                        }
                        LoginAndGetGift(runningForm);
                    }
                }

            }
            catch (Exception ex)
            {
                LogHelper.ErrorLog(Path, "OneKeyGetGift", ex.Message);
            }
            finally
            {
                continueGetGits = false;
                Invoke(new RefreshDelegate(delegate()
                {
                    btnAllLogin.Text = "全部登陆并领取礼包";
                }));
                ShowMessage("全部登陆并领取礼包结束");
            }
        }


        private void LoginAndGetGift(RunningForm runningForm)
        {
            runningForm.Start();
            string message = string.Format("登录了账号{0}/{1}", runningForm.Account.Account, runningForm.Account.PlayerModel.Name);
            ShowMessage(message);
            runningForm.Job.LookGift();
            string ret = runningForm.Job.GetGifts();
            ShowMessage(ret);
            runningList.Add(runningForm);
        }
        #endregion

        #region
        private void ShowMessage(string message)
        {
            Invoke(new RefreshDelegate(delegate()
            {
                txtLoginMessage.Text += Des.GetTimeDes();
                txtLoginMessage.Text += CommonString.HX;
                //  txtLoginMessage.Text += string.Format("当前账号{0}/{1},{2}", running.Account.PlayerModel.Name, running.Account.PlayerModel.UserId, message);
                txtLoginMessage.Text += message;
                txtLoginMessage.Text += CommonString.HX;
                txtLoginMessage.Text += CommonString.HX;
                this.txtLoginMessage.SelectionStart = this.txtLoginMessage.TextLength;
                this.txtLoginMessage.ScrollToCaret();
            }));
        }
        #endregion

        private void Million_FormClosing(object sender, FormClosingEventArgs e)
        {
            AccountList = null;
            runningList = null;
            Application.Exit();
        }


        #endregion
    }
}
