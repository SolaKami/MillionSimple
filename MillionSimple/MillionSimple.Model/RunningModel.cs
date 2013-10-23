using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MillionSimple.Enum;

namespace MillionSimple.Model
{
    /// <summary>
    /// 运行参数类
    /// </summary>
    public class RunningModel
    {
        private string _level = string.Empty;
        /// <summary>
        /// 等级
        /// </summary>
        public string Level
        {
            get { return _level; }
            set { _level = value; }
        }

        private string _gold = string.Empty;
        /// <summary>
        /// 金币
        /// </summary>
        public string Gold
        {
            get { return _gold; }
            set { _gold = value; }
        }

        private string _currentAP = string.Empty;
        /// <summary>
        /// 当前AP
        /// </summary>
        public string CurrentAP
        {
            get { return _currentAP; }
            set { _currentAP = value; }
        }

        private string _maxAP = string.Empty;
        /// <summary>
        /// 最大AP
        /// </summary>
        public string MaxAP
        {
            get { return _maxAP; }
            set { _maxAP = value; }
        }


        private string _currentBC = string.Empty;
        /// <summary>
        /// 当前BC
        /// </summary>
        public string CurrentBC
        {
            get { return _currentBC; }
            set { _currentBC = value; }
        }

        private string _maxBC = string.Empty;
        /// <summary>
        /// 最大BC
        /// </summary>
        public string MaxBC
        {
            get { return _maxBC; }
            set { _maxBC = value; }
        }


        private string _currentFriend = string.Empty;
        /// <summary>
        /// 当前好友数
        /// </summary>
        public string CurrentFriend
        {
            get { return _currentFriend; }
            set { _currentFriend = value; }
        }


        private string _maxFriend = string.Empty;
        /// <summary>
        /// 最大好友数
        /// </summary>
        public string MaxFriend
        {
            get { return _maxFriend; }
            set { _maxFriend = value; }
        }


        private string _freeapbcPoint = string.Empty;
        /// <summary>
        /// 待分配APBC
        /// </summary>
        public string FreeapbcPoint
        {
            get { return _freeapbcPoint; }
            set { _freeapbcPoint = value; }
        }

        private string _friendShipPoint = string.Empty;
        /// <summary>
        ///  友情点数
        /// </summary>
        public string FriendShipPoint
        {
            get { return _friendShipPoint; }
            set { _friendShipPoint = value; }
        }

        private string _countryId = string.Empty;
        /// <summary>
        /// 势力ID         
        /// </summary>
        public string CountryId
        {
            get { return _countryId; }
            set { _countryId = value; }
        }

        private string _gachaTicket = string.Empty;
        /// <summary>
        /// 扭蛋券
        /// </summary>
        public string GachaTicket
        {
            get { return _gachaTicket; }
            set { _gachaTicket = value; }
        }

        private List<OtherUserModel> _friendList;
        /// <summary>
        /// 好友列表
        /// </summary>
        public List<OtherUserModel> FriendList
        {
            get { return _friendList; }
            set { _friendList = value; }
        }

        private List<OtherUserModel> _applyFriendList;
        /// <summary>
        /// 好友通知2
        /// </summary>
        public List<OtherUserModel> ApplyFriendList
        {
            get { return _applyFriendList; }
            set { _applyFriendList = value; }
        }

        private List<OtherUserModel> _friendApplyStateList;
        /// <summary>
        /// 申请情况3
        /// </summary>
        public List<OtherUserModel> FriendApplyStateList
        {
            get { return _friendApplyStateList; }
            set { _friendApplyStateList = value; }
        }

        /// <summary>
        /// 队长卡
        /// </summary>
        public CardModel LeaderCard { get; set; }

        private string _leaderId = string.Empty;
        /// <summary>
        /// 队长卡ID
        /// </summary>
        [Obsolete("请使用 LeaderCard.master_card_id")]
        public string LeaderId
        {
            get { return _leaderId; }
            set { _leaderId = value; }
        }

        private string _cookie;
        /// <summary>
        /// COOKIE
        /// </summary>
        [Obsolete("请使用AccountModel.Cookie")]
        public string Cookie
        {
            get { return _cookie; }
            set { _cookie = value; }
        }

        private string _areaId;
        /// <summary>
        /// 当前MAP ID
        /// </summary>
        public string AreaId
        {
            get { return _areaId; }
            set { _areaId = value; }
        }

        private string _autoBuild;
        [Obsolete("无效属性")]
        public string AutoBuild
        {
            get { return _autoBuild; }
            set { _autoBuild = value; }
        }

        private string _floorId;
        /// <summary>
        /// 当前楼层
        /// </summary>
        public string FloorId
        {
            get { return _floorId; }
            set { _floorId = value; }
        }

        private string _check;
        [Obsolete("无效属性")]
        public string Check
        {
            get { return _check; }
            set { _check = value; }
        }

        private string _serialId;
        /// <summary>
        /// 当前本人妖精序列号
        /// </summary>
        [Obsolete("请使用FairyList")]
        public string SerialId
        {
            get { return _serialId; }
            set { _serialId = value; }
        }

        /// <summary>
        /// 当前指定玩家
        /// </summary>
        public OtherUserModel OtherPlayer { get; set; }

        private string _friendName;
        /// <summary>
        ///  当前指定玩家姓名
        /// </summary>
        [Obsolete("请使用 OtherPlayer")]
        public string FriendName
        {
            get { return _friendName; }
            set { _friendName = value; }
        }

        private string _friendId;
        /// <summary>
        /// 当前指定玩家ID
        /// </summary>
        [Obsolete("请使用 OtherPlayer")]
        public string FriendId
        {
            get { return _friendId; }
            set { _friendId = value; }
        }

        private string _miniCostCardId;
        /// <summary>
        /// 最小COST卡ID
        /// </summary>
        [Obsolete("请使用 LeaderCard")]
        public string MiniCostCardId
        {
            get { return _miniCostCardId; }
            set { _miniCostCardId = value; }
        }

        private string _giftId;
        /// <summary>
        /// 礼包ID
        /// </summary>
        public string GiftId
        {
            get { return _giftId; }
            set { _giftId = value; }
        }

        private DrinkEnum _drinkType;
        /// <summary>
        /// 当前喝茶的类型
        /// </summary>
        [Obsolete("无效属性")]
        public DrinkEnum DrinkType
        {
            get { return _drinkType; }
            set { _drinkType = value; }
        }

        private string _friendApply;
        /// <summary>
        /// 好友申请数
        /// </summary>
        [Obsolete("无效属性")]
        public string FriendApply
        {
            get { return _friendApply; }
            set { _friendApply = value; }
        }


        private ActModeEnum _act;
        /// <summary>
        /// 当前行动指令
        /// </summary>
        public ActModeEnum Act
        {
            get { return _act; }
            set { _act = value; }
        }

        /// <summary>
        /// 绿茶数量
        /// </summary>
        public string GreenCount { get; set; }

        /// <summary>
        /// 红茶数量
        /// </summary>
        public string RedCount { get; set; }

        /// <summary>
        /// 卡牌列表
        /// </summary>
        public List<CardModel> CardList { get; set; }

        /// <summary>
        /// 卡牌数
        /// </summary>
        public string CardCount { get; set; }

        /// <summary>
        /// 当前妖精列表
        /// </summary>
        public List<FairyModel> FairyList { get; set; }
    }
}
