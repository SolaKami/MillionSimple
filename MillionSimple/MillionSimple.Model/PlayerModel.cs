using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MillionSimple.Model
{
    /// <summary>
    /// 玩家类
    /// </summary>
    public class PlayerModel
    {
        private string _userId = string.Empty;
        /// <summary>
        /// 玩家ID
        /// </summary>
        public string UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }

        private string _name = string.Empty;
        /// <summary>
        /// 玩家名称
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private RunningModel _runningModel;
        /// <summary>
        /// 运行类，标明一些会改变的参数
        /// </summary>
        public RunningModel Running
        {
            get { return _runningModel; }
            set { _runningModel = value; }
        }



    }
}
