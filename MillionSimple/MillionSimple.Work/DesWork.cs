using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MillionSimple.Model;
using MillionSimple.Enum;
using MillionSimple.Common;

namespace MillionSimple.Work
{
    public static class Des
    {

        public static string APPName = "进击的MA - V2.5";


        #region 获取时间描述
        /// <summary>
        /// 获取时间描述
        /// </summary>
        /// <returns></returns>
        public static string GetTimeDes()
        {
            return string.Format("时间：{0} ", DateTime.Now.ToString());
        }
        #endregion

        #region 获取玩家描述
        /// <summary>
        /// 获取玩家描述
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public static string GetPlayerDes(AccountModel account)
        {
            string ret = string.Empty;
            ret += string.Format("时间：{0}", DateTime.Now.ToString());
            ret += "\r\n";
            ret += string.Format("账号：{0}", account.Account);
            ret += "\r\n";
            ret += string.Format("角色名：{0}", account.PlayerModel.Name);
            ret += "\r\n";
            ret += string.Format("角色ID：{0}", account.PlayerModel.UserId);
            ret += "\r\n";
            ret += string.Format("势力：{0}", GetContryIdDes(account.PlayerModel.Running.CountryId));
            ret += "\r\n";
            ret += string.Format("AP：{0}/{1}", account.PlayerModel.Running.CurrentAP, account.PlayerModel.Running.MaxAP);
            ret += "\r\n";
            ret += string.Format("BC：{0}/{1}", account.PlayerModel.Running.CurrentBC, account.PlayerModel.Running.MaxBC);
            ret += "\r\n";
            ret += string.Format("好友：{0}/{1}", account.PlayerModel.Running.CurrentFriend, account.PlayerModel.Running.MaxFriend);
            ret += "\r\n";
            ret += string.Format("好友通知：{0}", account.PlayerModel.Running.FriendApply);
            ret += "\r\n";
            ret += string.Format("等级：{0}", account.PlayerModel.Running.Level);
            ret += "\r\n";
            ret += string.Format("待分配APBC：{0}", account.PlayerModel.Running.FreeapbcPoint);
            ret += "\r\n";
            ret += string.Format("友情点数：{0}", account.PlayerModel.Running.FriendShipPoint);
            ret += "\r\n";
            ret += string.Format("扭蛋券：{0}", account.PlayerModel.Running.GachaTicket);
            ret += "\r\n";
            ret += string.Format("金币：{0}", account.PlayerModel.Running.Gold);
            ret += "\r\n";
            ret += string.Format("绿茶：{0}", account.PlayerModel.Running.GreenCount);
            ret += "\r\n";
            ret += string.Format("红茶：{0}", account.PlayerModel.Running.RedCount);
            ret += "\r\n";
            ret += string.Format("卡牌数：{0}", account.PlayerModel.Running.CardCount);
            ret += "\r\n";
            ret += "\r\n";
            return ret;
        }
        #endregion

        #region 获取势力描述
        /// <summary>
        /// 获取势力描述
        /// </summary>
        /// <returns></returns>
        public static string GetContryIdDes(int value)
        {
            CountryEnum con = (CountryEnum)value;
            return con.ToString();
        }
        public static string GetContryIdDes(string value)
        {
            int val = ToolKit.StringToInt(value);
            CountryEnum con = (CountryEnum)val;
            return con.ToString();
        }
        #endregion


        public static string GetOtherPlayerDes(OtherUserModel model)
        {
            string ret = string.Empty;
            try
            {
                ret += string.Format("角色名：{0}", model.name);
                ret += "\r\n";
                ret += string.Format("角色ID：{0}", model.id);
                ret += "\r\n";
                ret += string.Format("势力：{0}", GetContryIdDes(model.country_id));
                ret += "\r\n";
                ret += string.Format("好友：{0}/{1}", model.friends, model.friend_max);
                ret += "\r\n";
                ret += string.Format("等级：{0}", model.town_level);
                ret += "\r\n";
                ret += string.Format("BC上限：{0}", model.cost);
                ret += "\r\n";
                ret += string.Format("最后登陆时间：{0}", model.last_login);
                ret += "\r\n";
                ret += string.Format("队长卡ID：{0}", model.leader_card.master_card_id);
                ret += "\r\n";
                ret += string.Format("队长卡名称：{0}", model.leader_card.name);
                ret += "\r\n";
                ret += string.Format("队长卡等级：{0}/{1}", model.leader_card.lv, model.leader_card.lv_max);
                ret += "\r\n";
                ret += string.Format("队长卡攻击：{0}", model.leader_card.power);
                ret += "\r\n";
                ret += string.Format("队长卡血量：{0}", model.leader_card.hp);
                ret += "\r\n";
                ret += "\r\n";
            }
            catch (Exception)
            {
                ret += string.Format("获取失败");
                ret += "\r\n";
                ret += "\r\n";
            }
            return ret;
        }
        public static string GetCardModelDes(CardModel model)
        {
            string ret = string.Empty;
            try
            {
                ret += string.Format("卡ID：{0}", model.master_card_id);
                ret += "\r\n";
                ret += string.Format("卡名称：{0}", model.name);
                ret += "\r\n";
                ret += string.Format("卡等级：{0}/{1}", model.lv, model.lv_max);
                ret += "\r\n";
                ret += string.Format("卡攻击：{0}", model.power);
                ret += "\r\n";
                ret += string.Format("卡血量：{0}", model.hp);
                ret += "\r\n";
                ret += "\r\n";
            }
            catch (Exception)
            {
                ret += string.Format("获取失败");
                ret += "\r\n";
                ret += "\r\n";
            }
            return ret;
        }
    }
}
