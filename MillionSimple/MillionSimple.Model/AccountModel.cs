using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MillionSimple.Model
{
    /// <summary>
    /// 账号类
    /// </summary>
    public class AccountModel
    {
        private string _account = string.Empty;
        /// <summary>
        /// 账号
        /// </summary>
        public string Account
        {
            get { return _account; }
            set { _account = value; }
        }

        private string _password = string.Empty;
        /// <summary>
        /// 密码
        /// </summary>
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        private string _isLoginSuccess = string.Empty;
        /// <summary>
        /// 是否登陆成功
        /// </summary>
        [Obsolete("无效属性")]
        public string IsLoginSuccess
        {
            get { return _isLoginSuccess; }
            set { _isLoginSuccess = value; }
        }


        private PlayerModel _playerModel;
        [Obsolete("无效属性，请直接使用RunningModel")]
        public PlayerModel PlayerModel
        {
            get { return _playerModel; }
            set { _playerModel = value; }
        }

        /// <summary>
        /// 角色名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 当前连接COOKIE
        /// </summary>
        public string Cookie { get; set; }


        public RunningModel Running { get; set; }

    }
}
