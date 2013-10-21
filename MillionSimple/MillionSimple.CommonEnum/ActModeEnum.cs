using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace MillionSimple.CommonEnum
{
    public enum ActModeEnum
    {
        /// <summary>
        /// 无
        /// </summary>
        [Description("无")]
        Default = 0,
        /// <summary>
        /// 验证
        /// </summary>
        [Description("验证")]
        check_inspection = 1,
        /// <summary>
        /// 发送参数
        /// </summary>
        [Description("发送参数")]
        post_devicetoken = 2,
        /// <summary>
        /// 登陆
        /// </summary>
        [Description("登陆")]
        login = 3,
        /// <summary>
        /// 链接主界面
        /// </summary>
        [Description("链接主界面")]
        Menu = 4,
        /// <summary>
        /// 探索
        /// </summary>
        [Description("探索")]
        Explore = 5,
        /// <summary>
        /// 区域选择
        /// </summary>
        [Description("区域选择")]
        SelectArea = 6,
        /// <summary>
        /// 楼层选择
        /// </summary>
        [Description("楼层选择")]
        SelectFloor = 7,
        /// <summary>
        /// 因子战选择
        /// </summary>
        [Description("因子战选择")]
        SelectBattle = 8,
        /// <summary>
        /// 舔怪
        /// </summary>
        [Description("舔怪")]
        Lick = 9,
        /// <summary>
        /// 因子战战斗
        /// </summary>
        [Description("因子战战斗")]
        UserFight = 10,
        /// <summary>
        /// 妖精选择
        /// </summary>
        [Description("妖精选择")]
        FairySelect = 11,
        /// <summary>
        /// 进入地图
        /// </summary>
        InMap = 12,
        /// <summary>
        /// 增加好友
        /// </summary>
        AddFriend = 13,
        /// <summary>
        /// 加点
        /// </summary>
        AddPoint = 14,
        /// <summary>
        /// 喝茶
        /// </summary>
        Drink = 15,
        /// <summary>
        /// 领取所有礼包
        /// </summary>
        GetPresent = 16,
        /// <summary>
        /// 查看礼包
        /// </summary>
        LookPresent = 17,
        /// <summary>
        /// 查找好友
        /// </summary>
        SearchPlayer = 18,
        /// <summary>
        /// 使用卡组
        /// </summary>
        ApplyCard = 19,

        /// <summary>
        /// 保存卡组
        /// </summary>
        SaveCard = 20,

        /// <summary>
        /// 删除好友
        /// </summary>
        RemoveFriend = 21,

        /// <summary>
        /// 查看好友
        /// </summary>
        FriendList = 22,

        /// <summary>
        /// 妖精列表
        /// </summary>
        FairyList = 23,

        /// <summary>
        /// 好友通知2
        /// </summary>
        FriendNotice = 24,

        /// <summary>
        /// 接受好友申请
        /// </summary>
        ApproveFriend = 25,

        /// <summary>
        /// 拒绝好友申请
        /// </summary>
        RejectFriend = 26,

        /// <summary>
        /// 申请情况3
        /// </summary>
        FriendApply = 27,
    }
}
