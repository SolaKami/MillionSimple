using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace MillionSimple.CommonEnum
{
    public enum CountryEnum
    {
        /// <summary>
        /// 魔法
        /// </summary>
        [Description("魔法")]
        Magic = 3,
        /// <summary>
        /// 剑城
        /// </summary>
        [Description("剑城")]
        Sword = 1,
        /// <summary>
        /// 技巧
        /// </summary>
        [Description("技巧")]
        Skill = 2,
    }
}
