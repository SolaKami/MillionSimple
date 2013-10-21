using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MillionSimple.Model
{
    /// <summary>
    /// 区域类
    /// </summary>
    public class AreaModel
    {
        private string _areaName = string.Empty;
        /// <summary>
        /// 区域名称
        /// </summary>
        public string AreaName
        {
            get { return _areaName; }
            set { _areaName = value; }
        }

        private string _areaId = string.Empty;
        /// <summary>
        /// 区域ID
        /// </summary>
        public string AreaId
        {
            get { return _areaId; }
            set { _areaId = value; }
        }

    }
}
