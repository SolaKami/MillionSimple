using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MillionSimple.Enum;

namespace MillionSimple.Model
{
    /// <summary>
    /// 亚瑟
    /// </summary>
    public class YaSeModel
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <param name="pro"></param>
        public YaSeModel(string account, string password, Protocol pro)
        {
            this._account = account;
            this._password = password;
            this.Pro = pro;
            Initialize();
        }
        /// <summary>
        /// 初始化
        /// </summary>
        private void Initialize()
        {
            this.Act = ActModeEnum.Default;
            this.ApplyFriendList = new List<OtherUserModel>();
            this.Cookie = string.Empty;
            this.CardList = new List<CardModel>();
            this.CountryId = 0;
            this.CurrentAP = 0;
            this.CurrentBC = 0;
            this.CurrentFriend = 0;
            this.FairyList = new List<FairyModel>();
            this.FloorId = 0;
            this.FreeapbcPoint = 0;
            this.FriendApplyStateList = new List<OtherUserModel>();
            this.FriendList = new List<OtherUserModel>();
            this.GachaTicket = 0;
            this.GiftId = new List<string>();
            this.Gold = 0;
            this.GreenCount = 0;
            this.Id = 0;
            this.LeaderCard = new CardModel();
            this.Level = 0;
            this.MapId = 0;
            this.MaxAP = 0;
            this.MaxBC = 0;
            this.MaxFriend = 0;
            this.FriendShipPoint = 0;
            this.Name = string.Empty;
            this.OtherPlayer = new OtherUserModel();
            this.RedCount = 0;
            this.PlayerState = PlayerStateEnum.Default;
            this.CardGroup = new List<CardModel>();
            this.CurrentFairy = new FairyModel();
        }

        private string _account;
        /// <summary>
        /// 账号
        /// </summary>
        public string Account
        {
            get { return _account; }
        }

        private string _password;
        /// <summary>
        /// 密码
        /// </summary>
        public string Password
        {
            get { return _password; }
        }

        /// <summary>
        /// 角色名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 当前连接COOKIE
        /// </summary>
        public string Cookie { get; set; }

        /// <summary>
        /// 控制器协议
        /// </summary>
        public Protocol Pro { get; set; }

        /// <summary>
        /// 等级
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 金币
        /// </summary>
        public int Gold { get; set; }

        /// <summary>
        /// 当前AP
        /// </summary>
        public int CurrentAP { get; set; }

        /// <summary>
        /// 最大AP
        /// </summary>
        public int MaxAP { get; set; }

        /// <summary>
        /// 当前BC
        /// </summary>
        public int CurrentBC { get; set; }

        /// <summary>
        /// 最大BC
        /// </summary>
        public int MaxBC { get; set; }

        /// <summary>
        /// 当前好友数
        /// </summary>
        public int CurrentFriend { get; set; }

        /// <summary>
        /// 最大好友数
        /// </summary>
        public int MaxFriend { get; set; }

        /// <summary>
        /// 待分配APBC
        /// </summary>
        public int FreeapbcPoint { get; set; }

        /// <summary>
        ///  友情点数
        /// </summary>
        public int FriendShipPoint { get; set; }

        /// <summary>
        /// 势力ID         
        /// </summary>
        public int CountryId { get; set; }

        /// <summary>
        /// 扭蛋券
        /// </summary>
        public int GachaTicket { get; set; }

        /// <summary>
        /// 好友列表
        /// </summary>
        public List<OtherUserModel> FriendList { get; set; }

        /// <summary>
        /// 好友通知2
        /// </summary>
        public List<OtherUserModel> ApplyFriendList { get; set; }

        /// <summary>
        /// 申请情况3
        /// </summary>
        public List<OtherUserModel> FriendApplyStateList { get; set; }

        /// <summary>
        /// 队长卡
        /// </summary>
        public CardModel LeaderCard { get; set; }

        /// <summary>
        /// 当前地图ID
        /// </summary>
        public int MapId { get; set; }

        /// <summary>
        /// 当前楼层
        /// </summary>
        public int FloorId { get; set; }

        /// <summary>
        /// 当前指定玩家
        /// </summary>
        public OtherUserModel OtherPlayer { get; set; }

        /// <summary>
        /// 礼包ID
        /// </summary>
        public List<string> GiftId { get; set; }

        /// <summary>
        /// 当前行动指令
        /// </summary>
        public ActModeEnum Act { get; set; }

        /// <summary>
        /// 绿茶数量
        /// </summary>
        public int GreenCount { get; set; }

        /// <summary>
        /// 红茶数量
        /// </summary>
        public int RedCount { get; set; }

        /// <summary>
        /// 卡牌列表
        /// </summary>
        public List<CardModel> CardList { get; set; }

        /// <summary>
        /// 当前妖精列表
        /// </summary>
        public List<FairyModel> FairyList { get; set; }

        /// <summary>
        /// 角色状态 
        /// </summary>
        public PlayerStateEnum PlayerState { get; set; }

        /// <summary>
        /// 12卡组
        /// </summary>
        public List<CardModel> CardGroup { get; set; }

        /// <summary>
        /// 当前指向的妖精
        /// </summary>
        public FairyModel CurrentFairy { get; set; }
    }
}
