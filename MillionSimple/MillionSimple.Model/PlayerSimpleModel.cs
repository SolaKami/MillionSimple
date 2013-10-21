using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MillionSimple.Model
{
    /// <summary>
    /// 简单玩家类
    /// </summary>
    public class SimplePlayerModel
    {
        private string _name = string.Empty;
        /// <summary>
        /// 角色名
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _id = string.Empty;
        /// <summary>
        /// 角色ID
        /// </summary>
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// 队长卡ID
        /// </summary>
        public string LeaderId { get; set; }

        /// <summary>
        /// 队长卡名称
        /// </summary>
        public string LeaderName { get; set; }
    }
}
