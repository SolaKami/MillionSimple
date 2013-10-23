using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MillionSimple.Enum;

namespace MillionSimple.Model
{
    /// <summary>
    /// 控制器协议
    /// </summary>
    public class Protocol
    {
        /// <summary>
        /// 协议构造器
        /// </summary>
        public Protocol()
        {

        }

        /// <summary>
        /// 自动模式
        /// </summary>
        public AutoModeEnum AutoMode { get; set; }

        /// <summary>
        ///攻击自己的妖精的最大等级 
        /// </summary>
        public int MaxSelfFairyLevelAttack { get; set; }

        /// <summary>
        /// 攻击他人妖精的最大等级
        /// </summary>
        public int MaxOtherFairyLevelAttack { get; set; }



    }
}
