using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MillionSimple.Rule;
using MillionSimple.Model;
using MillionSimple.CommonEnum;
using MillionSimple.Data;
using MillionSimple.Common;
using System.Threading;

namespace MillionSimple.Job
{
    [Obsolete("请使用Body")]
    public class AutoJob
    {

        private static string Path = @"\MillionSimple\MillionSimple.Job\AutoJob.cs";

        #region 构造函数

        public AutoJob(AccountModel accountModel)
        {
            this._accountModel = accountModel;
            Initial();
        }

        private void Initial()
        {
            RunningModel running = new RunningModel();
            PlayerModel player = new PlayerModel();
            player.Running = running;
            Account.PlayerModel = player;
        }

        #endregion

        #region 属性
        [Obsolete("请使用 YaSeModel")]
        public AccountModel Player { get; set; }

        private bool _jobMode = false;
        /// <summary>
        /// 指示当前程序是否处于自动模式中
        /// </summary>
        public bool JobMode
        {
            get { return _jobMode; }
            set { _jobMode = value; }
        }

        private bool _isEncrypt = false;
        /// <summary>
        /// 指示一个值，标示当前账号是否使用加密模式登陆
        /// </summary>
        public bool IsEncrypt
        {
            get { return _isEncrypt; }
            set { _isEncrypt = value; }
        }


        public event MessageEventHandler Messaging;
        public event MessageEventHandler AutoMessaging;


        public RunningRule _rule;
        public RunningRule Rule
        {
            get
            {
                if (_rule == null)
                {
                    _rule = new RunningRule();
                }
                return _rule;
            }
        }

        public AccountModel _accountModel;
        [Obsolete("请使用Player")]
        public AccountModel Account
        {
            get { return _accountModel; }
            set { _accountModel = value; }
        }

        /// <summary>
        ///用以标识是否继续进行自动JOB
        /// </summary>
        public bool continueAutoJob = false;


        private PlayerStateEnum _playerState;

        public PlayerStateEnum PlayerState
        {
            get { return _playerState; }
            set { _playerState = value; }
        }

        #endregion

        #region 方法

        #region 登陆
        /// <summary>
        /// 登陆
        /// </summary>
        public void Login()
        {
            Account.PlayerModel.Running.Cookie = string.Empty;
            HttpRequestParameter para = GetReq(ActModeEnum.login);
            HttpResponseParameter res;
            if (IsEncrypt)
            {
                res = Rule.DoAct(para, true);
            }
            else
            {
                res = Rule.DoAct(para);
            }
            string message = string.Empty;
            if (res.IsRequestSuccess)
            {
                if (res.ErrorCode == "0")
                {
                    Account.IsLoginSuccess = "0";
                    Account.PlayerModel = Analyze.GetPlayerModel(res.Content);
                    string cookies = res.WebResponse.Headers["Set-Cookie"].ToString();
                    string[] str1 = cookies.Split(',');
                    string[] str2 = str1[str1.Length - 1].Split(';');
                    Account.PlayerModel.Running.Cookie = str2[0];
                    message = "哎哟，你来了哦。";
                    PlayerState = PlayerStateEnum.Login;
                    SendMessage(message);
                    return;
                }
                if (res.ErrorCode == "1000")
                {
                    message = Analyze.GetXmlMsg(res.Content);
                    SendMessage(message);
                    return;
                }
                if (IsEncrypt)
                {
                    Account.IsLoginSuccess = "0";
                    Account.PlayerModel = Analyze.GetPlayerModel(res.Content);
                    string cookies = res.WebResponse.Headers["Set-Cookie"].ToString();
                    string[] str1 = cookies.Split(',');
                    string[] str2 = str1[str1.Length - 1].Split(';');
                    Account.PlayerModel.Running.Cookie = str2[0];
                    message = "哎哟，你来了哦。";
                    PlayerState = PlayerStateEnum.Login;
                    SendMessage(message);
                    MainMenu();
                    return;
                }
                message = string.Format("未知错误(错误码{0})！！！", res.ErrorCode);
                SendMessage(message);
                return;
            }
            message = "服务器连接失败！";
            PlayerState = PlayerStateEnum.Error;
            SendMessage(message);
        }
        #endregion

        #region 探索
        /// <summary>
        /// 探索
        /// </summary>
        public void Explore()
        {
            Account.PlayerModel.Running.AutoBuild = "1";
            if (string.IsNullOrEmpty(Account.PlayerModel.Running.AreaId))
            {
                InMap("");
            }
            HttpRequestParameter para = GetReq(ActModeEnum.Explore);
            HttpResponseParameter res = Rule.DoAct(para);
            string message = string.Empty;
            if (res.IsRequestSuccess)
            {
                if (res.ErrorCode == "0")
                {
                    Analyze.GetBaseState(res.Content, Account);
                    string content = Analyze.AnalyzeExplore(res.Content, ref _playerState);
                    message = content == "" ? "无" : content;
                    SendMessage(message);
                    return;
                }
                if (res.ErrorCode == "9000")
                {
                    message = Analyze.GetXmlMsg(res.Content);
                    PlayerState = PlayerStateEnum.Leave;
                    SendMessage(message);
                    return;
                }
                if (res.ErrorCode == "10000")
                {
                    message = Analyze.GetXmlMsg(res.Content);
                    PlayerState = PlayerStateEnum.Stop;
                    SendMessage(message);
                    return;
                }
                message = string.Format("发生了错误(错误码{0})！！！", res.ErrorCode);
                SendMessage(message);
                return;
            }
            message = "服务器连接失败！";
            PlayerState = PlayerStateEnum.Error;
            SendMessage(message);
        }
        #endregion

        #region 选择地图区域

        /// <summary>
        /// 选择地图区域
        /// </summary>
        public void SelectArea()
        {
            HttpRequestParameter para = GetReq(ActModeEnum.SelectArea);
            HttpResponseParameter res = Rule.DoAct(para);
            string message = string.Empty;
            if (res.IsRequestSuccess)
            {
                if (res.ErrorCode == "0")
                {

                    string content = Analyze.GetMap(res.Content);
                    message = content == "" ? "无" : content;
                    SendMessage(message);
                    return;
                }
                if (res.ErrorCode == "9000")
                {
                    message = Analyze.GetXmlMsg(res.Content);
                    PlayerState = PlayerStateEnum.Leave;
                    SendMessage(message);
                    return;
                }
                message = string.Format("发生了错误(错误码{0})！！！", res.ErrorCode);
                SendMessage(message);
                return;
            }
            message = "服务器连接失败！";
            PlayerState = PlayerStateEnum.Error;
            SendMessage(message);
        }
        #endregion

        #region 选择楼层
        /// <summary>
        /// 选择楼层
        /// </summary>
        public void SelectFloor()
        {
            HttpRequestParameter para = GetReq(ActModeEnum.SelectFloor);
            HttpResponseParameter res = Rule.DoAct(para);
            if (res.ErrorCode == "0")
            {
            }
        }
        #endregion

        #region 进入地图
        /// <summary>
        /// 进入地图
        /// </summary>
        public void InMap(string id)
        {

            Account.PlayerModel.Running.AreaId = Analyze.GetDefaultMapId();
            if (!string.IsNullOrEmpty(id.Trim()))
            {
                Account.PlayerModel.Running.AreaId = id.Trim();
            }
            Account.PlayerModel.Running.Check = "1";
            Account.PlayerModel.Running.FloorId = "1";

            HttpRequestParameter para = GetReq(ActModeEnum.InMap);
            HttpResponseParameter res = Rule.DoAct(para);
            string message = string.Empty;
            if (res.IsRequestSuccess)
            {
                if (res.ErrorCode == "0")
                {
                    Analyze.GetBaseState(res.Content, Account);
                    message = "进入了地图 : " + Analyze.GetInMapMessage(res.Content);
                    SendMessage(message);
                    return;
                }
                if (res.ErrorCode == "9000")
                {
                    message = Analyze.GetXmlMsg(res.Content);
                    PlayerState = PlayerStateEnum.Leave;
                    SendMessage(message);
                    return;
                }
                if (res.ErrorCode == "10000")
                {
                    message = Analyze.GetXmlMsg(res.Content);
                    PlayerState = PlayerStateEnum.Stop;
                    SendMessage(message);
                    return;
                }
                message = string.Format("发生了错误(错误码{0})！！！", res.ErrorCode);
                SendMessage(message);
                return;
            }
            message = "服务器连接失败！";
            PlayerState = PlayerStateEnum.Error;
            SendMessage(message);
        }
        #endregion

        #region 舔妖
        /// <summary>
        /// 舔妖
        /// </summary>
        public void LickFairy()
        {
            FairySelect();
            HttpRequestParameter para = GetReq(ActModeEnum.Attack);
            HttpResponseParameter res = Rule.DoAct(para);
            string message = string.Empty;
            if (res.IsRequestSuccess)
            {
                if (res.ErrorCode == "0")
                {
                    Analyze.GetBaseState(res.Content, Account);
                    message = "舔啊舔啊.....";
                    PlayerState = PlayerStateEnum.WaitMaster;
                    SendMessage(message);
                    return;
                }
                if (res.ErrorCode == "8000")
                {
                    message = Analyze.GetXmlMsg(res.Content);
                    SendMessage(message);
                    return;
                }
                if (res.ErrorCode == "9000")
                {
                    message = Analyze.GetXmlMsg(res.Content);
                    PlayerState = PlayerStateEnum.Leave;
                    SendMessage(message);
                    return;
                }
                message = string.Format("发生了错误(错误码{0})！！！", res.ErrorCode);
                SendMessage(message);
                return;
            }
            message = "服务器连接失败！";
            PlayerState = PlayerStateEnum.Error;
            SendMessage(message);
        }
        #endregion

        #region 妖精选择
        /// <summary>
        /// 妖精选择
        /// </summary>
        public void FairySelect()
        {
            HttpRequestParameter para = GetReq(ActModeEnum.FairySelect);
            HttpResponseParameter res = Rule.DoAct(para);
            string message = string.Empty;
            if (res.IsRequestSuccess)
            {
                if (res.ErrorCode == "0")
                {

                    string content = Analyze.GetFairyList(res.Content);
                    Account.PlayerModel.Running.SerialId = Analyze.GetSerialId(res.Content, Account.PlayerModel.Name);
                    message = content == "" ? "无" : content;
                    if (!continueAutoJob)
                    {
                        SendMessage(message);
                    }
                    return;
                }
                if (res.ErrorCode == "9000")
                {
                    message = Analyze.GetXmlMsg(res.Content);
                    PlayerState = PlayerStateEnum.Leave;
                    SendMessage(message);
                    return;
                }
                message = string.Format("发生了错误(错误码{0})！！！", res.ErrorCode);
                SendMessage(message);
                return;
            }
            message = "服务器连接失败！";
            PlayerState = PlayerStateEnum.Error;
            SendMessage(message);
        }
        #endregion

        #region 主界面/刷新
        /// <summary>
        /// 主界面/刷新/得到状态数据
        /// </summary>
        public void MainMenu()
        {
            HttpRequestParameter para = GetReq(ActModeEnum.Menu);
            HttpResponseParameter res = Rule.DoAct(para);
            string cookie = Account.PlayerModel.Running.Cookie;
            string message = string.Empty;
            if (res.IsRequestSuccess)
            {
                if (res.ErrorCode == "0")
                {
                    Account.PlayerModel = Analyze.GetPlayerModel(res.Content);
                    Account.PlayerModel.Running.Cookie = cookie;
                    if (IsEncrypt)
                    {
                        message = "刷新了状态..";
                    }
                    else
                    {
                        message = "手动刷新了状态..";
                    }
                    PlayerState = PlayerStateEnum.Menu;
                    SendMessage(message);
                    return;
                }
                if (res.ErrorCode == "9000")
                {
                    message = Analyze.GetXmlMsg(res.Content);
                    PlayerState = PlayerStateEnum.Leave;
                    SendMessage(message);
                    return;
                }
                message = string.Format("服务器傲娇啦，求猛戳(错误码{0})！！！", res.ErrorCode);
                SendMessage(message);
                return;
            }
            message = "服务器连接失败！";
            PlayerState = PlayerStateEnum.Error;
            SendMessage(message);
        }
        #endregion

        #region 查看礼物
        /// <summary>
        /// 查看礼物
        /// </summary>
        public void LookGift()
        {
            HttpRequestParameter para = GetReq(ActModeEnum.LookGifts);
            HttpResponseParameter res = Rule.DoAct(para);
            string message = string.Empty;
            if (res.IsRequestSuccess)
            {
                if (res.ErrorCode == "0")
                {
                    message = Analyze.GetGiftId(res.Content);
                    Account.PlayerModel.Running.GiftId = message;
                    return;
                }
                if (res.ErrorCode == "9000")
                {
                    message = Analyze.GetXmlMsg(res.Content);
                    PlayerState = PlayerStateEnum.Leave;
                    SendMessage(message);
                    return;
                }
                return;
            }
            message = "服务器连接失败！";
            PlayerState = PlayerStateEnum.Error;
            return;
        }
        #endregion

        #region 接受全部礼物
        /// <summary>
        /// 接受全部礼物
        /// </summary>
        public string GetGifts()
        {

            HttpRequestParameter para = GetReq(ActModeEnum.GetGifts);
            HttpResponseParameter res = Rule.DoAct(para);

            string message = string.Empty;
            if (res.IsRequestSuccess)
            {
                if (res.ErrorCode == "8000" || res.ErrorCode == "1010")
                {
                    message = Analyze.GetXmlMsg(res.Content);

                    SendMessage(message);
                    return message;
                }
                if (res.ErrorCode == "9000")
                {
                    message = Analyze.GetXmlMsg(res.Content);
                    PlayerState = PlayerStateEnum.Leave;
                    SendMessage(message);
                    return message;
                }
                message = string.Format("发生了错误(错误码{0})！！！", res.ErrorCode);
                SendMessage(message);
                return "";
            }
            message = "服务器连接失败！";
            PlayerState = PlayerStateEnum.Error;
            SendMessage(message);
            return "";
        }
        #endregion

        #region 查看卡组
        /// <summary>
        /// 查看卡组
        /// </summary>
        public void ApplyCard()
        {
            HttpRequestParameter para = GetReq(ActModeEnum.ApplyCard);
            HttpResponseParameter res = Rule.DoAct(para);
            string message = string.Empty;
            if (res.IsRequestSuccess)
            {
                if (res.ErrorCode == "0")
                {
                    return;
                }
                if (res.ErrorCode == "9000")
                {
                    message = Analyze.GetXmlMsg(res.Content);
                    PlayerState = PlayerStateEnum.Leave;
                    SendMessage(message);
                    return;
                }
                message = string.Format("发生了错误(错误码{0})！！！", res.ErrorCode);
                SendMessage(message);
                return;
            }
            message = "服务器连接失败！";
            PlayerState = PlayerStateEnum.Error;
            SendMessage(message);
        }
        #endregion

        #region 保存卡组
        /// <summary>
        /// 保存卡组
        /// </summary>
        public void SaveCard()
        {
            HttpRequestParameter para = GetReq(ActModeEnum.SaveCardGroup);
            HttpResponseParameter res = Rule.DoAct(para);
            string message = string.Empty;
            if (res.IsRequestSuccess)
            {
                if (res.ErrorCode == "0")
                {
                    message = "使用了3COST的卡！";
                    SendMessage(message);
                    return;
                }
                if (res.ErrorCode == "9000")
                {
                    message = Analyze.GetXmlMsg(res.Content);
                    PlayerState = PlayerStateEnum.Leave;
                    SendMessage(message);
                    return;
                }
                message = string.Format("发生了错误(错误码{0})！！！", res.ErrorCode);
                SendMessage(message);
                return;
            }
            message = "服务器连接失败！";
            PlayerState = PlayerStateEnum.Error;
            SendMessage(message);
        }
        #endregion

        #region 增加好友
        /// <summary>
        /// 增加好友
        /// </summary>
        public string AddFriend()
        {
            string FriendId = Account.PlayerModel.Running.FriendId;
            string message = string.Empty;
            if (string.IsNullOrEmpty(FriendId))
            {
                message = "未能找到玩家ID";
                SendMessage(message);
                return message;
            }
            HttpRequestParameter para = GetReq(ActModeEnum.AddFriend);
            HttpResponseParameter res = Rule.DoAct(para);
            if (res.IsRequestSuccess)
            {
                if (res.ErrorCode == "1010")
                {
                    message = Analyze.GetXmlMsg(res.Content);
                    SendMessage(message);
                    return message;
                }
                if (res.ErrorCode == "9000")
                {
                    message = Analyze.GetXmlMsg(res.Content);
                    PlayerState = PlayerStateEnum.Leave;
                    SendMessage(message);
                    return message;
                }
                message = string.Format("发生了错误(错误码{0})！！！", res.ErrorCode);
                SendMessage(message);
                return message;
            }
            message = "服务器连接失败！";
            PlayerState = PlayerStateEnum.Error;
            SendMessage(message);
            return message;

        }
        #endregion

        #region 删除好友
        /// <summary>
        /// 删除好友
        /// </summary>
        public void RemoveFriend()
        {
            string FriendId = Account.PlayerModel.Running.FriendId;
            if (string.IsNullOrEmpty(FriendId))
            {
                SendMessage("未能找到玩家ID");
                return;
            }
            HttpRequestParameter para = GetReq(ActModeEnum.RemoveFriend);
            HttpResponseParameter res = Rule.DoAct(para);
            string message = string.Empty;
            if (res.IsRequestSuccess)
            {
                if (res.ErrorCode == "1010")
                {
                    message = Analyze.GetXmlMsg(res.Content);
                    SendMessage(message);
                    return;
                }
                if (res.ErrorCode == "9000")
                {
                    message = Analyze.GetXmlMsg(res.Content);
                    PlayerState = PlayerStateEnum.Leave;
                    SendMessage(message);
                    return;
                }
                message = string.Format("发生了错误(错误码{0})！！！", res.ErrorCode);
                SendMessage(message);
                return;
            }
            message = "服务器连接失败！";
            PlayerState = PlayerStateEnum.Error;
            SendMessage(message);
        }
        #endregion

        #region 查看好友
        /// <summary>
        /// 查看好友
        /// </summary>
        public void LookFriend()
        {
            HttpRequestParameter para = GetReq(ActModeEnum.ViewFriendList);
            HttpResponseParameter res = Rule.DoAct(para);
            string message = string.Empty;
            if (res.IsRequestSuccess)
            {
                if (res.ErrorCode == "0")
                {
                    Account.PlayerModel.Running.FriendList = Analyze.GetPlayerList(res.Content);
                    message = Analyze.GetPlayerListDes(Account.PlayerModel.Running.FriendList);
                    if (!continueAutoJob)
                    {
                        SendMessage(message);
                    }
                    return;
                }
                if (res.ErrorCode == "9000")
                {
                    message = Analyze.GetXmlMsg(res.Content);
                    PlayerState = PlayerStateEnum.Leave;
                    SendMessage(message);
                    return;
                }
                message = string.Format("发生了错误(错误码{0})！！！", res.ErrorCode);
                SendMessage(message);
                return;
            }
            message = "服务器连接失败！";
            PlayerState = PlayerStateEnum.Error;
            SendMessage(message);
        }
        #endregion

        #region 分配点数
        /// <summary>
        /// 分配点数
        /// </summary>
        /// <param name="ap"></param>
        /// <param name="bc"></param>
        public void SetPoint(int ap, int bc)
        {
            HttpRequestParameter para = GetReq(ActModeEnum.SetPoint);
            HttpResponseParameter res = Rule.DoAct(para);
            string message = string.Empty;
            if (res.IsRequestSuccess)
            {
                if (res.ErrorCode == "0")
                {
                    Analyze.GetBaseState(res.Content, Account);
                    message = string.Format("你加了{0}点AP,{1}点BC", ap, bc);
                    SendMessage(message);
                    return;
                }
                if (res.ErrorCode == "8000")
                {
                    message = Analyze.GetXmlMsg(res.Content);
                    SendMessage(message);
                    return;
                }
                if (res.ErrorCode == "9000")
                {
                    message = Analyze.GetXmlMsg(res.Content);
                    PlayerState = PlayerStateEnum.Leave;
                    SendMessage(message);
                    return;
                }
                message = string.Format("发生了错误(错误码{0})！！！", res.ErrorCode);
                SendMessage(message);
                return;
            }
            message = "服务器连接失败！";
            PlayerState = PlayerStateEnum.Error;
            SendMessage(message);
        }
        #endregion

        #region 嗑药
        /// <summary>
        /// 嗑药
        /// </summary>
        /// <param name="type"></param>
        public void Drink(DrinkEnum type)
        {
            Account.PlayerModel.Running.DrinkType = type;
            HttpRequestParameter para = GetReq(ActModeEnum.DrinkTea);
            HttpResponseParameter res = Rule.DoAct(para);
            string message = string.Empty;
            if (res.IsRequestSuccess)
            {
                if (res.ErrorCode == "0")
                {
                    Analyze.GetBaseState(res.Content, Account);
                    message = string.Format("你喝了一瓶{0},全身都爽啦！！", type == DrinkEnum.Green ? "绿茶" : "红茶");
                    PlayerState = PlayerStateEnum.NeedExplore;
                    SendMessage(message);
                    return;
                }
                if (res.ErrorCode == "1000")
                {
                    message = Analyze.GetXmlMsg(res.Content);
                    PlayerState = PlayerStateEnum.NeedExplore;
                    SendMessage(message);
                    return;
                }
                if (res.ErrorCode == "9000")
                {
                    message = Analyze.GetXmlMsg(res.Content);
                    PlayerState = PlayerStateEnum.Leave;
                    SendMessage(message);
                    return;
                }
                if (res.ErrorCode == "8000")
                {
                    message = Analyze.GetXmlMsg(res.Content);
                    PlayerState = PlayerStateEnum.Stop;
                    SendMessage(message);
                    return;
                }
                PlayerState = PlayerStateEnum.Stop;
                message = string.Format("发生了错误(错误码{0})！！！", res.ErrorCode);
                SendMessage(message);
                return;
            }
            message = "服务器连接失败！";
            PlayerState = PlayerStateEnum.Error;
            SendMessage(message);
        }
        #endregion

        #region 查找玩家
        /// <summary>
        /// 查找玩家
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string SearchPlayer(string name)
        {
            Account.PlayerModel.Running.FriendName = name;
            HttpRequestParameter para = GetReq(ActModeEnum.SearchPlayer);
            HttpResponseParameter res = Rule.DoAct(para);
            string message = string.Empty;
            if (res.IsRequestSuccess)
            {
                if (res.ErrorCode == "0")
                {
                    Account.PlayerModel.Running.FriendId = Analyze.GetFriendId(res.Content);
                    OtherUserModel model = Analyze.GetOtherUserModel(res.Content);
                    message = Des.GetOtherPlayerDes(model);
                    SendMessage(message);
                    return message;
                }
                if (res.ErrorCode == "9000")
                {
                    message = Analyze.GetXmlMsg(res.Content);
                    PlayerState = PlayerStateEnum.Leave;
                    SendMessage(message);
                    return message;
                }
                return "";
            }
            message = "服务器连接失败！";
            PlayerState = PlayerStateEnum.Error;
            return "";
        }

        #endregion

        /// <summary>
        /// 好友向己方申请列表
        /// </summary>
        public void FriendApplyList()
        {
            HttpRequestParameter para = GetReq(ActModeEnum.FriendNotice);
            HttpResponseParameter res = Rule.DoAct(para);
            string message = string.Empty;
            if (res.IsRequestSuccess)
            {
                if (res.ErrorCode == "0")
                {
                    Account.PlayerModel.Running.ApplyFriendList = Analyze.GetPlayerList(res.Content);
                    message = Analyze.GetPlayerListDes(Account.PlayerModel.Running.ApplyFriendList);
                    SendMessage(message);
                    return;
                }
                if (res.ErrorCode == "9000")
                {
                    message = Analyze.GetXmlMsg(res.Content);
                    PlayerState = PlayerStateEnum.Leave;
                    SendMessage(message);
                    return;
                }
                message = string.Format("发生了错误(错误码{0})！！！", res.ErrorCode);
                SendMessage(message);
                return;
            }
            message = "服务器连接失败！";
            PlayerState = PlayerStateEnum.Error;
            SendMessage(message);
        }

        public string ApproveFriend()
        {
            string message = string.Empty;
            try
            {
                string FriendId = Account.PlayerModel.Running.FriendId;
                string cookie = Account.PlayerModel.Running.Cookie;
                if (string.IsNullOrEmpty(FriendId))
                {
                    SendMessage("未能找到玩家ID");
                    return "未能找到玩家ID";
                }
                HttpRequestParameter para = GetReq(ActModeEnum.ApproveFriend);
                HttpResponseParameter res = Rule.DoAct(para);
                if (res.IsRequestSuccess)
                {
                    if (res.ErrorCode == "1010")
                    {
                        Analyze.GetPlayerState(res.Content, Account);
                        Account.PlayerModel.Running.Cookie = cookie;
                        message = Analyze.GetXmlMsg(res.Content);
                        SendMessage(message);
                        return message;
                    }
                    if (res.ErrorCode == "9000")
                    {
                        message = Analyze.GetXmlMsg(res.Content);
                        PlayerState = PlayerStateEnum.Leave;
                        SendMessage(message);
                        return message;
                    }
                    message = string.Format("发生了错误(错误码{0})！！！", res.ErrorCode);
                    SendMessage(message);
                    return message;
                }
                message = "服务器连接失败！";
                PlayerState = PlayerStateEnum.Error;
                SendMessage(message);
                return "";
            }
            catch (Exception ex)
            {
                message = string.Format("少女娇羞中！{0}", ex.Message);
                SendMessage(message);
                ErrorMessageLog.writeLog(ex.Message);
                return ex.Message;
            }
        }

        /// <summary>
        /// 获取已申请好友列表
        /// </summary>
        public void FriendApplyState()
        {
            HttpRequestParameter para = GetReq(ActModeEnum.FriendApplyList);
            HttpResponseParameter res = Rule.DoAct(para);
            string message = string.Empty;
            if (res.IsRequestSuccess)
            {
                if (res.ErrorCode == "0")
                {
                    Account.PlayerModel.Running.FriendApplyStateList = Analyze.GetPlayerList(res.Content);
                    message = Analyze.GetPlayerListDes(Account.PlayerModel.Running.FriendApplyStateList);
                    SendMessage(message);
                    return;
                }
                if (res.ErrorCode == "9000")
                {
                    message = Analyze.GetXmlMsg(res.Content);
                    PlayerState = PlayerStateEnum.Leave;
                    SendMessage(message);
                    return;
                }
                message = string.Format("发生了错误(错误码{0})！！！", res.ErrorCode);
                SendMessage(message);
                return;
            }
            message = "服务器连接失败！";
            PlayerState = PlayerStateEnum.Error;
            SendMessage(message);
        }


        #region 足球队查询

        /// <summary>
        /// 足球队查询
        /// </summary>
        /// <param name="name"></param>
        public void GetFootBall(string name)
        {
            try
            {
                HttpRequestParameter reqPara = Parameter.GetMAKongParameter(name, Account);
                HttpResponseParameter resPara = Rule.GetFootBall(reqPara);
                List<FootBallResultModel> list = Analyze.GetFootBall(resPara.Content);
                string message = string.Empty;
                foreach (FootBallResultModel user in list)
                {
                    if (user.name == Account.PlayerModel.Name)
                    {
                        continue;
                    }
                    message += SearchPlayer(user.name);
                    message += user.message;
                    message += CommonString.HX;
                    message += CommonString.HX;
                    message += CommonString.HX;
                }
                SendMessage(message);

            }
            catch (Exception)
            {
            }
        }
        #endregion

        /// <summary>
        /// 因子战
        /// </summary>
        /// <param name="id"></param>
        public void Battle(string id)
        {
            Account.PlayerModel.Running.FriendId = id;
            HttpRequestParameter para = GetReq(ActModeEnum.UserFight);
            HttpResponseParameter res = Rule.DoAct(para);
            string message = string.Empty;
            if (res.IsRequestSuccess)
            {
                if (res.ErrorCode == "0")
                {
                    Analyze.GetBaseState(res.Content, Account);
                    message += Analyze.GetBattleResultDes(res.Content);
                    SendMessage(message);
                    return;
                }
                message = Analyze.GetXmlMsg(res.Content);
                PlayerState = PlayerStateEnum.Leave;
                SendMessage(message);
                return;
            }
            message = "服务器连接失败！";
            PlayerState = PlayerStateEnum.Error;
            SendMessage(message);
        }

        /// <summary>
        /// 查看卡组
        /// </summary>
        public void LookCard()
        {
            string message = string.Empty;
            foreach (CardModel model in Account.PlayerModel.Running.CardList)
            {
                message += Des.GetCardModelDes(model);
            }
            SendMessage(message);
        }

        #region 无业务方法
        #region 信息反馈
        /// <summary>
        /// 信息反馈
        /// </summary>
        /// <param name="message"></param>
        public void SendMessage(string message)
        {
            if (continueAutoJob)
            {
                AutoMessage(message);
                return;
            }
            if (Messaging != null)
            {
                Messaging(message);
                return;
            }
        }

        #region 自动JOB信息反馈
        /// <summary>
        /// 自动JOB信息反馈
        /// </summary>
        /// <param name="message"></param>
        private void AutoMessage(string message)
        {
            if (AutoMessaging != null)
            {
                AutoMessaging(message);
                return;
            }
        }
        #endregion

        #endregion

        #region  获取参数
        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="act"></param>
        /// <returns></returns>
        public HttpRequestParameter GetReq(ActModeEnum act)
        {
            return Parameter.GetHttpRequestParameter(act, Account);
        }
        #endregion


        #endregion

        #endregion

        #region JOB

        #region 自动放妖
        /// <summary>
        /// 自动放妖
        /// </summary>
        [Obsolete("请使用 FangYao")]
        public void ReleaseFairyJob()
        {
            #region 获取MASTER
            List<string> masterName = ConfigHelper.GetMasterName();
            string retMessage = string.Empty;
            if (masterName == null || masterName.Count == 0)
            {
                AutoMessage("你还没有Master哦！！！");
                return;
            }
            LookFriend();
            bool exist = false;
            foreach (string master in masterName)
            {
                foreach (OtherUserModel simple in Account.PlayerModel.Running.FriendList)
                {
                    if (simple.name == master)
                    {
                        exist = true;
                        break;
                    }
                }
                if (exist)
                {
                    break;
                }

            }
            if (!exist)
            {
                AutoMessage("你还没有和你的Master缔结契约哦！！！");
                return;
            }
            #endregion
            Thread t = new Thread(new ThreadStart(ReleaseFriay));
            t.Start();
        }

        /// <summary>
        /// 放妖线程
        /// </summary>
        [Obsolete("请使用 FangYaoJob")]
        private void ReleaseFriay()
        {
            try
            {
                RunningModel running = Account.PlayerModel.Running;
                string serialId = string.Empty;
                while (continueAutoJob)
                {
                    switch (PlayerState)
                    {
                        case PlayerStateEnum.Login:
                            FairySelect();
                            serialId = running.SerialId;
                            if (string.IsNullOrEmpty(serialId))
                            {
                                PlayerState = PlayerStateEnum.NeedExplore;
                                Wait(3000);
                            }
                            else
                            {
                                SendMessage("发现了活着的妖精");
                                LickFairy();
                                Wait(5000);
                            }
                            break;
                        case PlayerStateEnum.Leave:
                            SendMessage("账号掉线，重连中。。。");
                            Login();
                            Wait(3000);
                            break;
                        case PlayerStateEnum.NeedExplore:
                            Explore();
                            Wait(2000);
                            break;
                        case PlayerStateEnum.FindFairy:
                            LickFairy();
                            Wait(5000);
                            break;
                        case PlayerStateEnum.LickFairy:
                            PlayerState = PlayerStateEnum.WaitMaster;
                            Wait(10000);
                            break;
                        case PlayerStateEnum.LackAP:
                            Drink(DrinkEnum.Green);
                            Wait(1000);
                            break;
                        case PlayerStateEnum.WaitMaster:
                            FairySelect();
                            string newSerialId = running.SerialId;
                            if (string.IsNullOrEmpty(newSerialId))
                            {
                                PlayerState = PlayerStateEnum.NeedExplore;
                                Wait(2000);
                            }
                            else
                            {
                                SendMessage("妖精还没有被消灭");
                                Wait(10000);
                            }
                            break;
                        case PlayerStateEnum.Stop:
                            continueAutoJob = false;
                            break;
                        case PlayerStateEnum.Error:
                            throw new Exception("发生错误！");
                        default:
                            MainMenu();
                            FairySelect();
                            serialId = running.SerialId;
                            if (string.IsNullOrEmpty(serialId))
                            {
                                PlayerState = PlayerStateEnum.NeedExplore;
                                Wait(2000);
                            }
                            else
                            {
                                LickFairy();
                                Wait(5000);
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.ErrorLog(Path, "ReleaseFriay", ex.Message);
                SendMessage("发生错误，已停止一键放妖");
                continueAutoJob = false;
                PlayerState = PlayerStateEnum.Error;
            }
            finally
            {

            }
        }
        #endregion

        private void Wait(int value)
        {
            Thread.Sleep(value);
        }

        #endregion

        #region JOB入口

        #region 爆肝
        /// <summary>
        /// 爆肝
        /// </summary>
        /// <param name="pro"></param>
        public void BaoGan(Protocol pro)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(BaoGanJob), pro);
        }
        #endregion

        #region 放妖
        /// <summary>
        /// 放妖
        /// </summary>
        /// <param name="pro"></param>
        public void FangYao(Protocol pro)
        {
            #region 获取MASTER
            List<string> masterName = ConfigHelper.GetMasterName();
            string retMessage = string.Empty;
            if (masterName == null || masterName.Count == 0)
            {
                AutoMessage("你还没有Master哦！！！");
                return;
            }
            LookFriend();
            bool exist = false;
            foreach (string master in masterName)
            {
                foreach (OtherUserModel simple in Account.PlayerModel.Running.FriendList)
                {
                    if (simple.name == master)
                    {
                        exist = true;
                        break;
                    }
                }
                if (exist)
                {
                    break;
                }

            }
            if (!exist)
            {
                AutoMessage("你还没有和你的Master缔结契约哦！！！");
                return;
            }
            #endregion
            ThreadPool.QueueUserWorkItem(new WaitCallback(FangYaoJob), pro);
        }
        #endregion

        #region 休闲
        /// <summary>
        /// 休闲
        /// </summary>
        /// <param name="pro"></param>
        public void XiuXian(Protocol pro)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(XiuXianJob), pro);
        }
        #endregion
        #endregion

        #region 自动模式

        #region 放妖JOB
        /// <summary>
        /// 放妖job
        /// </summary>
        /// <param name="obj"></param>
        private void FangYaoJob(object obj)
        {
            Protocol pro = obj as Protocol;
            try
            {
                RunningModel running = Account.PlayerModel.Running;
                string serialId = string.Empty;
                while (continueAutoJob)
                {
                    switch (PlayerState)
                    {
                        case PlayerStateEnum.Login:
                            FairySelect();
                            serialId = running.SerialId;
                            if (string.IsNullOrEmpty(serialId))
                            {
                                PlayerState = PlayerStateEnum.NeedExplore;
                                Wait(3000);
                            }
                            else
                            {
                                SendMessage("发现了活着的妖精");
                                LickFairy();
                                Wait(5000);
                            }
                            break;
                        case PlayerStateEnum.Leave:
                            SendMessage("账号掉线，重连中。。。");
                            Login();
                            Wait(3000);
                            break;
                        case PlayerStateEnum.NeedExplore:
                            Explore();
                            Wait(2000);
                            break;
                        case PlayerStateEnum.FindFairy:
                            LickFairy();
                            Wait(5000);
                            break;
                        case PlayerStateEnum.LickFairy:
                            PlayerState = PlayerStateEnum.WaitMaster;
                            Wait(10000);
                            break;
                        case PlayerStateEnum.LackAP:
                            Drink(DrinkEnum.Green);
                            Wait(1000);
                            break;
                        case PlayerStateEnum.WaitMaster:
                            FairySelect();
                            string newSerialId = running.SerialId;
                            if (string.IsNullOrEmpty(newSerialId))
                            {
                                PlayerState = PlayerStateEnum.NeedExplore;
                                Wait(2000);
                            }
                            else
                            {
                                SendMessage("妖精还没有被消灭");
                                Wait(10000);
                            }
                            break;
                        case PlayerStateEnum.Stop:
                            continueAutoJob = false;
                            break;
                        case PlayerStateEnum.Error:
                            throw new Exception("发生错误！");
                        default:
                            MainMenu();
                            FairySelect();
                            serialId = running.SerialId;
                            if (string.IsNullOrEmpty(serialId))
                            {
                                PlayerState = PlayerStateEnum.NeedExplore;
                                Wait(2000);
                            }
                            else
                            {
                                LickFairy();
                                Wait(5000);
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.ErrorLog(Path, "ReleaseFriay", ex.Message);
                SendMessage("发生错误，已停止一键放妖");
                continueAutoJob = false;
                PlayerState = PlayerStateEnum.Error;
            }
            finally
            {

            }
        }
        #endregion

        #region 爆肝JOB
        /// <summary>
        /// 爆肝job
        /// </summary>
        /// <param name="obj"></param>
        private void BaoGanJob(object obj)
        {

        }
        #endregion

        #region 休闲JOB
        /// <summary>
        /// 休闲job
        /// </summary>
        /// <param name="obj"></param>
        private void XiuXianJob(object obj)
        {
            try
            {
                RunningModel running = Account.PlayerModel.Running;
                string serialId = string.Empty;
                while (continueAutoJob)
                {
                    switch (PlayerState)
                    {
                        case PlayerStateEnum.Login:
                            FairySelect();
                            serialId = running.SerialId;
                            if (string.IsNullOrEmpty(serialId))
                            {
                                PlayerState = PlayerStateEnum.NeedExplore;
                                Wait(3000);
                            }
                            else
                            {
                                SendMessage("发现了活着的妖精");
                                LickFairy();
                                Wait(5000);
                            }
                            break;
                        case PlayerStateEnum.Leave:
                            SendMessage("账号掉线，重连中。。。");
                            Login();
                            Wait(3000);
                            break;
                        case PlayerStateEnum.NeedExplore:
                            Explore();
                            Wait(2000);
                            break;
                        case PlayerStateEnum.FindFairy:
                            LickFairy();
                            Wait(5000);
                            break;
                        case PlayerStateEnum.LickFairy:
                            PlayerState = PlayerStateEnum.WaitMaster;
                            Wait(10000);
                            break;
                        case PlayerStateEnum.LackAP:
                            Drink(DrinkEnum.Green);
                            Wait(1000);
                            break;
                        case PlayerStateEnum.WaitMaster:
                            FairySelect();
                            string newSerialId = running.SerialId;
                            if (string.IsNullOrEmpty(newSerialId))
                            {
                                PlayerState = PlayerStateEnum.NeedExplore;
                                Wait(2000);
                            }
                            else
                            {
                                SendMessage("妖精还没有被消灭");
                                Wait(10000);
                            }
                            break;
                        case PlayerStateEnum.Stop:
                            continueAutoJob = false;
                            break;
                        case PlayerStateEnum.Error:
                            throw new Exception("发生错误！");
                        default:
                            MainMenu();
                            FairySelect();
                            serialId = running.SerialId;
                            if (string.IsNullOrEmpty(serialId))
                            {
                                PlayerState = PlayerStateEnum.NeedExplore;
                                Wait(2000);
                            }
                            else
                            {
                                LickFairy();
                                Wait(5000);
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.ErrorLog(Path, "ReleaseFriay", ex.Message);
                SendMessage("发生错误，已停止一键放妖");
                continueAutoJob = false;
                PlayerState = PlayerStateEnum.Error;
            }
            finally
            {

            }
        }
        #endregion

        #endregion

    }
}
