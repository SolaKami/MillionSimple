using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MillionSimple.Model
{
    public class FairyModel
    {
        private string _masterName = string.Empty;
        /// <summary>
        /// 发现者
        /// </summary>
        public string MasterName
        {
            get { return _masterName; }
            set { _masterName = value; }
        }

        private string _serialId = string.Empty;
        /// <summary>
        /// 妖精序列号
        /// </summary>
        public string SerialId
        {
            get { return _serialId; }
            set { _serialId = value; }
        }

        private string _name = string.Empty;
        /// <summary>
        /// 妖精名称
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _level = string.Empty;
        /// <summary>
        /// 妖精等级
        /// </summary>
        public string Level
        {
            get { return _level; }
            set { _level = value; }
        }

        private string _findTime = string.Empty;
        /// <summary>
        /// 发现时间
        /// </summary>
        public string FindTime
        {
            get { return _findTime; }
            set { _findTime = value; }
        }

        private string _putDown;
        /// <summary>
        /// 已推倒
        /// </summary>
        public string PutDown
        {
            get { return _putDown; }
            set { _putDown = value; }
        }


        private string _lickCount = string.Empty;
        /// <summary>
        /// 已攻击次数
        /// </summary>
        public string LickCount
        {
            get { return _lickCount; }
            set { _lickCount = value; }
        }

    }
}
