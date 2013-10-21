using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MillionSimple.CommonEnum
{
    public enum PlayerStateEnum
    {
        /// <summary>
        /// 默认
        /// </summary>
        Default = 0,
        /// <summary>
        /// 掉线
        /// </summary>
        Leave = 1,
        /// <summary>
        /// 发现妖精
        /// </summary>
        FindFairy = 2,
        /// <summary>
        /// 放妖
        /// </summary>
        LickFairy = 3,
        /// <summary>
        /// 等待MASTER消灭妖精
        /// </summary>
        WaitMaster = 4,

        /// <summary>
        /// 需要探索
        /// </summary>
        NeedExplore = 5,

        /// <summary>
        /// 升级
        /// </summary>
        LevelUP = 6,

        /// <summary>
        /// 升级且遇到妖精
        /// </summary>
        LevelUPAndFindFairy = 7,

        /// <summary>
        /// 缺少AP
        /// </summary>
        LackAP = 8,

        /// <summary>
        /// 缺少BC
        /// </summary>
        LackBC = 9,

        /// <summary>
        /// 登陆状态
        /// </summary>
        Login = 10,

        /// <summary>
        /// 主界面刷新状态
        /// </summary>
        Menu = 11,

        /// <summary>
        /// 停止
        /// </summary>
        Stop = 12,

        /// <summary>
        /// 错误
        /// </summary>
        Error = 13,

        /// <summary>
        /// 人工结束
        /// </summary>
        End = 14,

    }
}
