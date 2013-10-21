using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using MillionSimple.Model;
using MillionSimple.Common;
using MillionSimple.CommonEnum;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Text.RegularExpressions;

namespace MillionSimple.Data
{
    /// <summary>
    /// 解析类
    /// </summary>
    public class Analyze
    {

        private static string Path = @"\MillionSimple\MillionSimple.Data\Analyze.cs";

        #region 获取玩家:name 的妖精序列号
        /// <summary>
        /// 获取玩家:name 的妖精序列号
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetSerialId(string xml, string name)
        {
            if (string.IsNullOrEmpty(xml) || string.IsNullOrEmpty(name))
            {
                return "数据错误";
            }
            string ret = string.Empty;
            try
            {
                XmlDocument xmlDoc = XmlHelper.GetXml(xml);
                if (xmlDoc == null)
                {
                    return "数据错误";
                }
                XmlNodeList list = xmlDoc.SelectNodes("/response/body/fairy_select/fairy_event");
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    if (list[i].ChildNodes[0].ChildNodes[1].InnerText == name
                        && list[i].ChildNodes[2].InnerText == "1"
                        )
                    {
                        ret = list[i].ChildNodes[1].ChildNodes[0].InnerText;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.ErrorLog(Path, "GetSerialId", ex.Message);
            }
            return ret;
        }
        #endregion

        #region 获取妖精列表
        /// <summary>
        /// 获取妖精列表
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static string GetFairyList(string xml)
        {
            if (string.IsNullOrEmpty(xml))
            {
                return "数据错误";
            }
            string strRet = string.Empty;
            string ret = string.Empty;
            try
            {
                XmlDocument xmlDoc = XmlHelper.GetXml(xml);
                if (xmlDoc == null)
                {
                    return "数据错误";
                }
                XmlNodeList list = xmlDoc.SelectNodes("/response/body/fairy_select/fairy_event");
                foreach (XmlNode node in list)
                {
                    ret = string.Format("发现者：{0}", node.ChildNodes[0].ChildNodes[1].InnerText);
                    ret += "\r\n";
                    ret += string.Format("发现时间：{0}", ToolKit.TimeStampToTimeString(node.ChildNodes[3].InnerText).AddHours(8).ToString());
                    ret += "\r\n";
                    ret += string.Format("妖精序列号：{0}", node.ChildNodes[1].ChildNodes[0].InnerText);
                    ret += "\r\n";
                    ret += string.Format("妖精名称：{0}", node.ChildNodes[1].ChildNodes[1].InnerText);
                    ret += "\r\n";
                    ret += string.Format("妖精等级：{0}", node.ChildNodes[1].ChildNodes[2].InnerText);
                    ret += "\r\n";
                    ret += string.Format("已推倒：{0}", node.ChildNodes[2].InnerText == "1" ? "否" : "是");
                    ret += "\r\n";
                    ret += "\r\n";
                    strRet = ret + strRet;
                }
                if (!string.IsNullOrEmpty(strRet))
                {
                    strRet = "\r\n" + strRet;
                }
            }
            catch (Exception ex)
            {
                LogHelper.ErrorLog(Path, "GetFairyList", ex.Message);
            }
            return strRet;
        }
        #endregion

        #region  分析探索事件

        /// <summary>
        /// 分析探索事件
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static string AnalyzeExplore(string xml, ref  PlayerStateEnum state)
        {
            if (string.IsNullOrEmpty(xml))
            {
                return "数据错误";
            }
            string ret = string.Empty;
            try
            {
                XmlDocument xmlDoc = XmlHelper.GetXml(xml);
                string eventType = xmlDoc.SelectSingleNode("/response/body/explore/event_type").InnerText;
                if (eventType == "1")
                {
                    ret += EventType1(xmlDoc);
                    state = PlayerStateEnum.FindFairy;
                    return ret;
                }
                if (eventType == "2")
                {
                    ret += EventType2(xmlDoc);
                    return ret;
                }
                if (eventType == "15")
                {
                    ret += "\r\n捡到了压缩狗粮。。。\r\n";
                    return ret;
                }
                if (eventType == "6")
                {
                    ret += "\r\n喂喂，AP见底啦。。。\r\n喝杯茶休息一下吧。。。";
                    state = PlayerStateEnum.LackAP;
                    return ret;
                }
                if (eventType == "19")
                {
                    ret += CommonString.HX;
                    ret += "什么也没发生。。。";
                    return ret;
                }
                ret += CommonString.HX;
                ret += "好像发生了什么。。。";
                return ret;
            }
            catch (Exception ex)
            {
                LogHelper.ErrorLog(Path, "AnalyzeExplore", ex.Message);
            }
            return ret;
        }

        private static string EventType0(XmlDocument xmlDoc)
        {
            string ret = string.Empty;
            ret += CommonString.HX;
            ret += "发生了什么呢。。。";
            return ret;
        }
        /// <summary>
        /// 妖精出现啦
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <returns></returns>
        private static string EventType1(XmlDocument xmlDoc)
        {
            string ret = string.Empty;
            try
            {
                ret += CommonString.HX;
                ret += "妖精出现啦。。。";
                string lvup = xmlDoc.GetElementsByTagName("lvup")[0].InnerText;
                if (lvup == "1")
                {
                    ret += CommonString.HX;
                    ret += CommonString.HX;
                    ret += "升级啦。。。";
                }
                XmlNode fairy = xmlDoc.GetElementsByTagName("fairy")[0];
                string name = fairy.ChildNodes[2].InnerText;
                string lv = fairy.ChildNodes[3].InnerText;
                string hp = fairy.ChildNodes[4].InnerText;
                string hp_max = fairy.ChildNodes[5].InnerText;
                ret += CommonString.HX;
                ret += CommonString.HX;
                ret += "妖精的三围出来啦。。。";
                ret += CommonString.HX;
                ret += CommonString.HX;
                ret += string.Format("名称:{0}", name);
                ret += CommonString.HX;
                ret += string.Format("等级:{0}", lv);
                ret += CommonString.HX;
                ret += string.Format("血量:{0}/{1}", hp, hp_max);
            }
            catch (Exception ex)
            {
                LogHelper.ErrorLog(Path, "EventType1", ex.Message);
            }
            return ret;
        }
        /// <summary>
        /// 遇到了一只
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <returns></returns>
        private static string EventType2(XmlDocument xmlDoc)
        {
            string ret = string.Empty;
            try
            {
                string name = xmlDoc.SelectSingleNode("/response/body/explore/encounter/name").InnerText;
                ret += CommonString.HX;
                ret += string.Format("遇到了一只{0}。。。", name);
            }
            catch (Exception ex)
            {
                LogHelper.ErrorLog(Path, "EventType2", ex.Message);
            }
            return ret;
        }
        #endregion

        #region 获取返回XML错误码

        /// <summary>
        /// 获取返回XML错误码
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static string GetCommandCode(string xml)
        {
            if (string.IsNullOrEmpty(xml))
            {
                return "数据错误";
            }
            string code = string.Empty;
            try
            {
                code = XmlHelper.ReadXml(xml, "/response/header/error/code");
            }
            catch (Exception ex)
            {
                LogHelper.ErrorLog(Path, "GetCommandCode", ex.Message);
            }
            return code;
        }
        #endregion


        public static string GetFriendId(string xml)
        {
            string ret = string.Empty;
            if (string.IsNullOrEmpty(xml))
            {
                return ret;
            }
            List<OtherUserModel> list = GetPlayerList(xml);
            if (list == null || list.Count == 0)
            {
                return ret;
            }
            ret = list[0].id;
            return ret;
        }

        public static string GetPlayerListDes(List<OtherUserModel> list)
        {
            string ret = string.Empty;
            if (list == null || list.Count == 0)
            {
                return ret;
            }
            ret += "\r\n";

            foreach (OtherUserModel model in list)
            {
                ret += Des.GetOtherPlayerDes(model);
            }
            return ret;
        }

        #region 获取当前玩家最小COST卡序列号

        /// <summary>
        /// 获取当前玩家最小COST卡序列号
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private static string GetMiniCostCardSerialId(string xml)
        {
            if (string.IsNullOrEmpty(xml))
            {
                return "";
            }
            string ret = string.Empty;
            try
            {
                XmlDocument xmlDoc = XmlHelper.GetXml(xml);
                XmlNodeList list = xmlDoc.SelectNodes("/response/header/your_data/owner_card_list/user_card");
                foreach (XmlNode node in list)
                {
                    string hp = node.ChildNodes[3].InnerText;
                    string power = node.ChildNodes[4].InnerText;
                    string lv = node.ChildNodes[6].InnerText;
                    string lv_max = node.ChildNodes[7].InnerText;
                    string sale = node.ChildNodes[13].InnerText;
                    string material_price = node.ChildNodes[14].InnerText;
                    string evolution_price = node.ChildNodes[15].InnerText;
                    if (hp == "790" && power == "320" && lv_max == "15" && sale == "80" && material_price == "300")
                    {
                        ret = node.ChildNodes[0].InnerText;
                        break;
                    }
                    if (hp == "660" && power == "450" && lv_max == "14" && sale == "80" && material_price == "300")
                    {
                        ret = node.ChildNodes[0].InnerText;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.ErrorLog(Path, "GetMiniCostCardSerialId", ex.Message);
            }
            return ret;
        }
        #endregion

        public static string GetGiftId(string xml)
        {
            if (string.IsNullOrEmpty(xml))
            {
                return "";
            }
            string ret = string.Empty;
            try
            {
                XmlDocument xmlDoc = XmlHelper.GetXml(xml);
                XmlNodeList list = xmlDoc.SelectNodes("/response/body/rewardbox_list/rewardbox");
                foreach (XmlNode node in list)
                {
                    ret += node.ChildNodes[0].InnerText + ",";
                }
            }
            catch (Exception ex)
            {
                LogHelper.ErrorLog(Path, "GetGiftId", ex.Message);
            }
            return ret;
        }


        public static string GetMap(string xml)
        {
            if (string.IsNullOrEmpty(xml))
            {
                return "";
            }
            string ret = string.Empty;
            try
            {
                ret += "\r\n";
                ret += "\r\n";
                XmlDocument xmlDoc = XmlHelper.GetXml(xml);
                XmlNodeList list = xmlDoc.SelectNodes("/response/body/exploration_area/area_info_list/area_info");
                foreach (XmlNode node in list)
                {
                    ret += string.Format("name:{0}", node.ChildNodes[1].InnerText);
                    ret += "\r\n";
                    ret += string.Format("id:{0}", node.ChildNodes[0].InnerText);
                    ret += "\r\n";
                    ret += "\r\n";
                }
            }
            catch (Exception ex)
            {
                LogHelper.ErrorLog(Path, "GetMap", ex.Message);
            }
            return ret;
        }


        #region 解析玩家类
        /// <summary>
        /// 解析玩家类
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static PlayerModel GetPlayerModel(string xml)
        {
            if (string.IsNullOrEmpty(xml))
            {
                return null;
            }
            PlayerModel player = new PlayerModel();
            RunningModel running = new RunningModel();
            try
            {
                string name = XmlHelper.ReadXml(xml, "/response/header/your_data/name");
                player.Name = name;

                string userId = XmlHelper.ReadXml(xml, "/response/body/login/user_id");
                player.UserId = userId;

                string leaderId = XmlHelper.ReadXml(xml, "/response/header/your_data/leader_serial_id");
                running.LeaderId = leaderId;

                string level = XmlHelper.ReadXml(xml, "/response/header/your_data/town_level");
                running.Level = level;

                string gold = XmlHelper.ReadXml(xml, "/response/header/your_data/gold");
                running.Gold = gold;

                string maxAP = XmlHelper.ReadXml(xml, "/response/header/your_data/ap/max");
                string currentAP = XmlHelper.ReadXml(xml, "/response/header/your_data/ap/current");
                running.MaxAP = maxAP;
                running.CurrentAP = currentAP;

                string currntBC = XmlHelper.ReadXml(xml, "/response/header/your_data/bc/current");
                string maxBC = XmlHelper.ReadXml(xml, "/response/header/your_data/bc/max");
                running.CurrentBC = currntBC;
                running.MaxBC = maxBC;

                string friend = XmlHelper.ReadXml(xml, "/response/header/your_data/friends");
                running.CurrentFriend = friend;

                string friendMax = XmlHelper.ReadXml(xml, "/response/header/your_data/friend_max");
                running.MaxFriend = friendMax;

                string friendApply = XmlHelper.ReadXml(xml, "/response/header/your_data/friends_invitations");
                running.FriendApply = friendApply;

                string freePoint = XmlHelper.ReadXml(xml, "/response/header/your_data/free_ap_bc_point");
                running.FreeapbcPoint = freePoint;

                string friendPoint = XmlHelper.ReadXml(xml, "/response/header/your_data/friendship_point");
                running.FriendShipPoint = friendPoint;

                string gachaTicket = XmlHelper.ReadXml(xml, "/response/header/your_data/gacha_ticket");
                running.GachaTicket = gachaTicket;

                string country_id = XmlHelper.ReadXml(xml, "/response/header/your_data/country_id");
                running.CountryId = country_id;


                XmlNodeList drinkList = XmlHelper.GetXml(xml).GetElementsByTagName("itemlist");
                foreach (XmlNode drinkNode in drinkList)
                {
                    if (drinkNode.ChildNodes[0].InnerText == "1")
                    {
                        running.GreenCount = drinkNode.ChildNodes[1].InnerText;
                        continue;
                    }
                    if (drinkNode.ChildNodes[0].InnerText == "2")
                    {
                        running.RedCount = drinkNode.ChildNodes[1].InnerText;
                        continue;
                    }
                }
                XmlNodeList cardList = XmlHelper.GetXml(xml).GetElementsByTagName("user_card");
                List<CardModel> modelList = new List<CardModel>();
                foreach (XmlNode card in cardList)
                {
                    CardModel model = GetCardModel(card.OuterXml);
                    modelList.Add(model);
                }
                running.CardList = modelList;
                running.MiniCostCardId = GetMiniCostCardSerialId(xml);
                running.CardCount = modelList.Count.ToString();
                player.Running = running;
            }
            catch (Exception)
            {
                return null;
            }
            return player;
        }
        #endregion


        #region 获取当前用户/用来控制是否记录日志
        /// <summary>
        /// 获取当前用户/用来控制是否记录日志
        /// </summary>
        /// <returns></returns>
        public static string GetDefaultUser()
        {
            XmlDocument xmlDoc = XmlHelper.GetConfig();
            string ret = string.Empty;
            try
            {
                XmlNode node = xmlDoc.SelectSingleNode("/root/Setting/User");
                ret = node.InnerText;
            }
            catch (Exception ex)
            {
                LogHelper.ErrorLog(Path, "GetDefaultUser", ex.Message);
            }
            return ret;
        }
        #endregion


        #region 获取当前默认地图ID
        /// <summary>
        /// 获取当前默认地图ID
        /// </summary>
        /// <returns></returns>
        public static string GetDefaultMapId()
        {
            XmlDocument xmlDoc = XmlHelper.GetConfig();
            string ret = string.Empty;
            try
            {
                XmlNode node = xmlDoc.SelectSingleNode("/root/Setting/AreaId");
                ret = node.InnerText;
            }
            catch (Exception ex)
            {
                LogHelper.ErrorLog(Path, "GetDefaultMapId", ex.Message);
            }
            return ret;
        }
        #endregion


        #region 解析查找玩家列表
        /// <summary>
        /// 解析查找玩家列表
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static List<OtherUserModel> GetPlayerList(string xml)
        {
            XmlDocument xmlDoc = XmlHelper.GetXml(xml);
            List<OtherUserModel> list = new List<OtherUserModel>();
            OtherUserModel model = null;
            try
            {
                XmlNodeList nodeList = xmlDoc.GetElementsByTagName("user");
                if (nodeList != null && nodeList.Count > 0)
                {
                    foreach (XmlNode user in nodeList)
                    {
                        model = GetOtherUserModel(user.OuterXml);
                        list.Add(model);
                    }
                }

            }
            catch (Exception ex)
            {
                LogHelper.ErrorLog(Path, "GetPlayerList", ex.Message);
            }
            return list;
        }
        #endregion

        /// <summary>
        /// 获取XML中的message信息
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static string GetXmlMsg(string xml)
        {
            XmlDocument xmlDoc = XmlHelper.GetXml(xml);
            string ret = string.Empty;
            try
            {
                XmlNode node = xmlDoc.SelectSingleNode("/response/header/error/message");
                ret = node.InnerText;
            }
            catch (Exception ex)
            {
                LogHelper.ErrorLog(Path, "GetXmlMsg", ex.Message);
            }
            return ret;
        }

        /// <summary>
        /// 获得基本状态数据
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="account"></param>
        public static void GetBaseState(string xml, AccountModel account)
        {
            XmlDocument xmlDoc = XmlHelper.GetXml(xml);
            try
            {
                XmlNode node = xmlDoc.GetElementsByTagName("your_data")[0];
                foreach (XmlNode child in node.ChildNodes)
                {
                    if (child.Name == "town_level")
                    {
                        account.PlayerModel.Running.Level = child.InnerText;
                        continue;
                    }
                    if (child.Name == "gold")
                    {
                        account.PlayerModel.Running.Gold = child.InnerText;
                        continue;
                    }
                    if (child.Name == "ap")
                    {
                        account.PlayerModel.Running.CurrentAP = child.ChildNodes[0].InnerText;
                        account.PlayerModel.Running.MaxAP = child.ChildNodes[1].InnerText;
                        continue;
                    }
                    if (child.Name == "bc")
                    {
                        account.PlayerModel.Running.CurrentBC = child.ChildNodes[0].InnerText;
                        account.PlayerModel.Running.MaxBC = child.ChildNodes[1].InnerText;
                        continue;
                    }
                    if (child.Name == "free_ap_bc_point")
                    {
                        account.PlayerModel.Running.FreeapbcPoint = child.InnerText;
                        continue;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.ErrorLog(Path, "GetBaseState", ex.Message);
            }
        }

        public static string GetInMapMessage(string xml)
        {
            XmlDocument xmlDoc = XmlHelper.GetXml(xml);
            string ret = string.Empty;
            try
            {
                XmlNode node = xmlDoc.GetElementsByTagName("area_name")[0];
                ret = node.InnerText;
            }
            catch (Exception ex)
            {
                LogHelper.ErrorLog(Path, "GetInMapMessage", ex.Message);
            }
            return ret;
        }


        public static void GetPlayerState(string xml, AccountModel account)
        {
            XmlDocument xmlDoc = XmlHelper.GetXml(xml);
            try
            {
                GetBaseState(xml, account);
                XmlNode node = xmlDoc.GetElementsByTagName("your_data")[0];
                foreach (XmlNode child in node.ChildNodes)
                {
                    if (child.Name == "friends")
                    {
                        account.PlayerModel.Running.CurrentFriend = child.InnerText;
                        continue;
                    }
                    if (child.Name == "friend_max")
                    {
                        account.PlayerModel.Running.MaxFriend = child.InnerText;
                        continue;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.ErrorLog(Path, "GetPlayerState", ex.Message);
            }
        }

        /// <summary>
        /// 获取其他玩家实体
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static OtherUserModel GetOtherUserModel(string xml)
        {
            OtherUserModel user = new OtherUserModel();
            CardModel card = new CardModel();
            try
            {
                XmlDocument xmlDoc = XmlHelper.GetXml(xml);
                user.id = xmlDoc.GetElementsByTagName("id")[0].InnerText;
                user.name = xmlDoc.GetElementsByTagName("name")[0].InnerText;
                user.country_id = xmlDoc.GetElementsByTagName("country_id")[0].InnerText;
                user.cost = xmlDoc.GetElementsByTagName("cost")[0].InnerText;
                user.town_level = xmlDoc.GetElementsByTagName("town_level")[0].InnerText;
                user.friends = xmlDoc.GetElementsByTagName("friends")[0].InnerText;
                user.friend_max = xmlDoc.GetElementsByTagName("friend_max")[0].InnerText;
                user.last_login = xmlDoc.GetElementsByTagName("last_login")[0].InnerText;
                user.leader_card = GetCardModel(xmlDoc.GetElementsByTagName("leader_card")[0].OuterXml);
            }
            catch (Exception ex)
            {
                LogHelper.ErrorLog(Path, "GetOtherUserModel", ex.Message);
            }
            return user;
        }

        /// <summary>
        /// 获取卡实体
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static CardModel GetCardModel(string xml)
        {
            CardModel card = new CardModel();
            try
            {
                XmlDocument xmlDoc = XmlHelper.GetXml(xml);
                card.serial_id = xmlDoc.GetElementsByTagName("serial_id")[0].InnerText;
                card.master_card_id = xmlDoc.GetElementsByTagName("master_card_id")[0].InnerText;
                card.holography = xmlDoc.GetElementsByTagName("holography")[0].InnerText;
                card.hp = xmlDoc.GetElementsByTagName("hp")[0].InnerText;
                card.power = xmlDoc.GetElementsByTagName("power")[0].InnerText;
                card.critical = xmlDoc.GetElementsByTagName("critical")[0].InnerText;
                card.lv = xmlDoc.GetElementsByTagName("lv")[0].InnerText;
                card.lv_max = xmlDoc.GetElementsByTagName("lv_max")[0].InnerText;
                card.exp = xmlDoc.GetElementsByTagName("exp")[0].InnerText;
                card.max_exp = xmlDoc.GetElementsByTagName("max_exp")[0].InnerText;
                card.next_exp = xmlDoc.GetElementsByTagName("next_exp")[0].InnerText;
                card.exp_diff = xmlDoc.GetElementsByTagName("exp_diff")[0].InnerText;
                card.exp_per = xmlDoc.GetElementsByTagName("exp_per")[0].InnerText;
                card.sale_price = xmlDoc.GetElementsByTagName("sale_price")[0].InnerText;
                card.material_price = xmlDoc.GetElementsByTagName("material_price")[0].InnerText;
                card.evolution_price = xmlDoc.GetElementsByTagName("evolution_price")[0].InnerText;
                card.plus_limit_count = xmlDoc.GetElementsByTagName("plus_limit_count")[0].InnerText;
                card.limit_over = xmlDoc.GetElementsByTagName("limit_over")[0].InnerText;
                card.name = AttributeHelper.GetEnumDes(typeof(CardEnum), card.master_card_id);
            }
            catch (Exception ex)
            {
                LogHelper.ErrorLog(Path, "GetCardModel", ex.Message);
            }
            return card;
        }

        [Obsolete("有误")]
        /// <summary>
        /// 解析足球队
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static List<FootBallResultModel> GetFootBall2(string content)
        {
            List<FootBallResultModel> list = new List<FootBallResultModel>();
            content = content.Replace(@"<!doctype html>", "<?xml version='1.0' encoding='UTF-8'?>");
            if (!content.Contains("</html>"))
            {
                content += "</html>";
            }
            content = content.Trim();
            XElement doc = XElement.Parse(content);
            IEnumerable<XElement> player =
                from el in doc.Elements()
                where el.Attribute("class").Value == "msg-info"
                select el;
            foreach (XElement element in player)
            {
                FootBallResultModel model = new FootBallResultModel();
                var id =
                from div in element.Elements()
                where div.Attribute("class").Value == "msg-detail" || div.Attribute("class").Value == "msg-detail"
                select div;
                if (id.Count() > 2)
                {
                    model.name = id.ElementAt(0).Value;
                    model.message = id.ElementAt(1).Value + " | " + id.ElementAt(2).Value;
                    list.Add(model);
                }
            }
            return list;
        }
        /// <summary>
        /// 解析足球队
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static List<FootBallResultModel> GetFootBall(string content)
        {
            string pattern = @"(?<=<div class=""msg-info"">)([\s\S]*?)(?=</div)";
            string childPattern = @"(?<=['""]>)([\s\S]*?)(?=</span)";
            List<FootBallResultModel> list = new List<FootBallResultModel>();
            MatchCollection matchs = Regex.Matches(content, pattern);
            foreach (var item in matchs)
            {
                MatchCollection childMatchs = Regex.Matches(item.ToString(), childPattern);
                FootBallResultModel model = new FootBallResultModel();
                if (childMatchs.Count > 2)
                {
                    model.name = childMatchs[0].ToString();
                    model.message = childMatchs[1].ToString() + " & " + childMatchs[2].ToString();
                    list.Add(model);
                }
            }
            return list;

        }

        /// <summary>
        /// 因子战结果描述
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static string GetBattleResultDes(string xml)
        {
            XmlDocument xmlDoc = XmlHelper.GetXml(xml);
            string ret = string.Empty;
            try
            {
                string winner = xmlDoc.GetElementsByTagName("winner")[0].InnerText;
                if (winner == "0")
                {
                    ret = "输了！";
                }
                else
                {
                    ret = "赢了！";
                }
            }
            catch (Exception ex)
            {
                LogHelper.ErrorLog(Path, "GetBattleResultDes", ex.Message);
            }
            return ret;
        }
    }

}
