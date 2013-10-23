using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MillionSimple.Model;
using MillionSimple.Enum;
using MillionSimple.Work;
using MillionSimple.Data;
using MillionSimple.Common;

namespace MillionSimple.Body
{
    public class BodyPartOne
    {
        #region 属性和构造函数

        #region 代表亚瑟的虚拟人物
        private YaSeModel _yaSe;
        /// <summary>
        /// 代表亚瑟的虚拟人物
        /// </summary>
        public YaSeModel YaSe
        {
            get { return _yaSe; }
            set { _yaSe = value; }
        }
        #endregion

        private ConnectWork _connect;

        public ConnectWork Connect
        {
            get { return _connect; }
            set { _connect = value; }
        }


        #region 构造
        public BodyPartOne(YaSeModel model)
        {
            this._yaSe = model;
            this._connect = new ConnectWork();
        }
        #endregion

        #endregion

        #region 方法

        #region 妖精战

        #region 选择地图区域

        /// <summary>
        /// 选择地图区域
        /// </summary>
        public ActResult SelectArea()
        {
            YaSe.Act = ActModeEnum.Explore;
            return Connect.DoAct(YaSe);
        }
        #endregion

        #region 进入地图
        /// <summary>
        /// 进入地图
        /// </summary>
        public ActResult InMap(int floorId = 0)
        {
            YaSe.Act = ActModeEnum.InMap;
            YaSe.FloorId = floorId;
            if (floorId == 0)
            {
                YaSe.FloorId = ToolKit.StringToInt(Analyze.GetDefaultMapId());
            }
            return Connect.DoAct(YaSe);
        }
        #endregion

        #region 探索
        /// <summary>
        /// 探索
        /// </summary>
        public ActResult Explore()
        {
            YaSe.Act = ActModeEnum.Explore;
            return Connect.DoAct(YaSe);
        }
        #endregion

        #region 妖精列表
        /// <summary>
        /// 妖精列表
        /// </summary>
        public ActResult FairyList()
        {
            YaSe.Act = ActModeEnum.FairyList;
            return Connect.DoAct(YaSe);
        }
        #endregion

        #region 攻击妖精
        /// <summary>
        /// 攻击妖精
        /// </summary>
        public ActResult AttackFairy()
        {
            YaSe.Act = ActModeEnum.Attack;
            return Connect.DoAct(YaSe);
        }
        #endregion

        #endregion

        #region 好友

        #region 增加好友
        /// <summary>
        /// 增加好友
        /// </summary>
        public ActResult AddFriend()
        {
            YaSe.Act = ActModeEnum.AddFriend;
            return Connect.DoAct(YaSe);
        }
        #endregion

        #region 删除好友
        /// <summary>
        /// 删除好友
        /// </summary>
        public ActResult RemoveFriend()
        {
            YaSe.Act = ActModeEnum.RemoveFriend;
            return Connect.DoAct(YaSe);
        }
        #endregion

        #region 查看好友
        /// <summary>
        /// 查看好友
        /// </summary>
        public ActResult ViewFriendList()
        {
            YaSe.Act = ActModeEnum.ViewFriendList;
            return Connect.DoAct(YaSe);
        }
        #endregion

        #region 查找玩家
        /// <summary>
        /// 查找玩家
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ActResult SearchPlayer(string name)
        {
            YaSe.Act = ActModeEnum.SearchPlayer;
            return Connect.DoAct(YaSe);
        }

        #endregion

        #region 好友通知2
        /// <summary>
        /// 好友通知2
        /// </summary>
        public ActResult FriendNotice()
        {
            YaSe.Act = ActModeEnum.FriendNotice;
            return Connect.DoAct(YaSe);
        }
        #endregion

        #region 接受好友
        /// <summary>
        /// 接受好友
        /// </summary>
        /// <returns></returns>
        public ActResult ApproveFriend()
        {
            YaSe.Act = ActModeEnum.ApproveFriend;
            return Connect.DoAct(YaSe);
        }
        #endregion

        #region 好友申请列表
        /// <summary>
        /// 好友申请列表3
        /// </summary>
        public ActResult FriendApplyList()
        {
            YaSe.Act = ActModeEnum.FriendApplyList;
            return Connect.DoAct(YaSe);
        }
        #endregion

        #endregion

        #region 因子战

        #region 因子战
        /// <summary>
        /// 因子战
        /// </summary>
        /// <param name="userId"></param>
        public ActResult UserFight(string userId)
        {
            YaSe.Act = ActModeEnum.UserFight;
            return Connect.DoAct(YaSe);
        }
        #endregion

        #endregion

        #region 其他

        #region 登陆
        /// <summary>
        /// 登陆
        /// </summary>
        public ActResult Login()
        {
            YaSe.Act = ActModeEnum.login;
            return Connect.DoAct(YaSe);
        }
        #endregion

        #region 主界面/刷新
        /// <summary>
        /// 主界面/刷新/得到状态数据
        /// </summary>
        public ActResult MainMenu()
        {
            YaSe.Act = ActModeEnum.Menu;
            return Connect.DoAct(YaSe);
        }
        #endregion

        #region 查看礼物
        /// <summary>
        /// 查看礼物
        /// </summary>
        public ActResult LookGifts()
        {
            YaSe.Act = ActModeEnum.LookGifts;
            return Connect.DoAct(YaSe);
        }
        #endregion

        #region 接受全部礼物
        /// <summary>
        /// 接受全部礼物
        /// </summary>
        public ActResult GetGifts()
        {
            YaSe.Act = ActModeEnum.GetGifts;
            return Connect.DoAct(YaSe);
        }
        #endregion

        #region 设定卡组
        /// <summary>
        /// 设定卡组
        /// </summary>
        public ActResult SaveCardGroup()
        {
            YaSe.Act = ActModeEnum.SaveCardGroup;
            return Connect.DoAct(YaSe);
        }
        #endregion

        #region 分配点数
        /// <summary>
        /// 分配点数
        /// </summary>
        /// <param name="ap"></param>
        /// <param name="bc"></param>
        public ActResult SetPoint(int ap, int bc)
        {
            YaSe.Act = ActModeEnum.SetPoint;
            return Connect.DoAct(YaSe);
        }
        #endregion

        #region 嗑药
        /// <summary>
        /// 嗑药
        /// </summary>
        /// <param name="type"></param>
        public ActResult DrinkTea(DrinkEnum type)
        {
            YaSe.Act = ActModeEnum.DrinkTea;
            return Connect.DoAct(YaSe);
        }
        #endregion

        #endregion

        #endregion
    }
}
