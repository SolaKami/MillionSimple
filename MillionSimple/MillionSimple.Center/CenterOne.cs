using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MillionSimple.Model;
using System.Threading;
using MillionSimple.Common;
using MillionSimple.Enum;

namespace MillionSimple.CenterOne
{
    /// <summary>
    /// 一号处理中心
    /// </summary>
    public class CenterOne
    {
        private const string Path = @"MillionSimple\MillionSimple.Center\CenterOne.cs";

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

        #region 构造
        public CenterOne(YaSeModel model)
        {
            this._yaSe = model;
        }
        #endregion

        #endregion

        #region 手动模式

        #endregion

        #region 自启动入口

        #region 爆肝
        /// <summary>
        /// 爆肝
        /// </summary>
        /// <param name="pro"></param>
        public void BaoGan()
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(BaoGanMode), new object());
        }
        #endregion

        #region 放妖
        /// <summary>
        /// 放妖
        /// </summary>
        /// <param name="pro"></param>
        public void FangYao()
        {

            ThreadPool.QueueUserWorkItem(new WaitCallback(FangYaoMode), new object());
        }
        #endregion

        #region 休闲
        /// <summary>
        /// 休闲
        /// </summary>
        /// <param name="pro"></param>
        public void XiuXian()
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(XiuXianMode), new object());
        }
        #endregion
        #endregion

        #region 自启动Mode

        #region 放妖Mode
        /// <summary>
        /// 放妖Mode
        /// </summary>
        /// <param name="obj"></param>
        private void FangYaoMode(object obj)
        {
        }
        #endregion

        #region 爆肝Mode
        /// <summary>
        /// 爆肝Mode
        /// </summary>
        /// <param name="obj"></param>
        private void BaoGanMode(object obj)
        {

        }
        #endregion

        #region 休闲Mode
        /// <summary>
        /// 休闲Mode
        /// </summary>
        /// <param name="obj"></param>
        private void XiuXianMode(object obj)
        {
            while (YaSe.Pro.AutoMode == AutoModeEnum.XiuXian)
            {
                switch (YaSe.PlayerState)
                {
                    case PlayerStateEnum.Default:
                        break;
                }
            }
        }
        #endregion

        #endregion

    }
}
