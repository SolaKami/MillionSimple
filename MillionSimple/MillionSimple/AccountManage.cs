using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using MillionSimple.Common;
using System.Xml;

namespace MillionSimple
{
    public partial class AccountManage : Form
    {

        public static AccountManage _accountManage;

        public static AccountManage Instance(Million main)
        {
            if (_accountManage == null || _accountManage.IsDisposed)
            {
                _accountManage = new AccountManage(main);
            }
            return _accountManage;
        }

        #region 属性
        private Million _main;

        public Million Main
        {
            get { return _main; }
            set { _main = value; }
        }

        #endregion

        public AccountManage(Million main)
        {
            this._main = main;
            InitializeComponent();
        }

        public AccountManage()
        {
            InitializeComponent();
        }

        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            AddAccount();
        }

        private void AddAccount()
        {
            string content = txtAccount.Text.Trim();
            string password = txtPassword.Text.Trim();
            string account = string.Empty;
            XmlDocument xmlDoc = XmlHelper.GetConfig();
            if (xmlDoc == null)
            {
                lblMessage.Text = "找不到配置文件！";
                return;
            }
            Regex reg = new Regex(@"1[3|4|5|8][0-9]\d{8}");
            MatchCollection collection = reg.Matches(content);
            for (int i = 0; i < collection.Count; i++)
            {
                XmlElement Character = xmlDoc.CreateElement("Character");
                XmlElement accountNode = xmlDoc.CreateElement("Account");
                accountNode.InnerText = collection[i].Value;
                XmlElement passwordNode = xmlDoc.CreateElement("Password");
                passwordNode.InnerText = password;
                Character.AppendChild(accountNode);
                Character.AppendChild(passwordNode);
                if (IsAccountExist(accountNode.InnerText))
                {
                    ModifyAccount(xmlDoc, accountNode.InnerText, passwordNode.InnerText);
                }
                xmlDoc.ChildNodes[1].ChildNodes[0].AppendChild(Character);
            }
            xmlDoc.Save(XmlHelper.ConfigPath);
            lblMessage.Text = "账号添加就绪！";
            return;
        }


        private string GetAccountString()
        {
            XmlDocument xmlDoc = XmlHelper.GetConfig();
            if (xmlDoc == null)
            {
                lblMessage.Text = "找不到配置文件！";
                return "";
            }
            XmlNodeList list = xmlDoc.SelectNodes("/root/Data/Character");

            if (list == null || list.Count == 0)
            {
                return "";
            }
            string ret = string.Empty;
            foreach (XmlNode node in list)
            {
                ret += node.ChildNodes[0].InnerText + ",";
            }
            return ret;

        }

        private void ModifyAccount(XmlDocument xmlDoc, string account, string password)
        {
            XmlNodeList list = xmlDoc.SelectNodes("/root/Data/Character");
            if (list == null || list.Count == 0)
            {
                return;
            }
            foreach (XmlNode node in list)
            {
                if (node.ChildNodes[0].InnerText == account)
                {
                    node.ChildNodes[1].InnerText = password;
                    break;
                }
            }
        }

        private bool IsAccountExist(string account)
        {
            string accountList = GetAccountString();
            if (accountList.Contains(account))
            {
                return true;
            }
            return false;
        }

        private void AccountManage_Load(object sender, EventArgs e)
        {

        }

        private void AccountManage_FormClosing(object sender, FormClosingEventArgs e)
        {
            Main.Show();
            Main.GetAccount();
        }


    }
}
