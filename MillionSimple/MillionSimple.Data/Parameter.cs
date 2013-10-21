using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MillionSimple.Model;
using MillionSimple.Common;
using MillionSimple.CommonEnum;
using System.Net;

namespace MillionSimple.Data
{
    public static class Parameter
    {
        private static string Path = @"\MillionSimple\MillionSimple.Data\Parameter.cs";

        #region 解析动作，获取具体的HTTP参数
        /// <summary>
        /// 解析动作，获取具体的HTTP参数
        /// </summary>
        /// <param name="actModeEnum"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static HttpRequestParameter GetHttpRequestParameter(ActModeEnum actModeEnum, AccountModel account)
        {
            HttpRequestParameter para = new HttpRequestParameter();
            try
            {
                RunningModel running = account.PlayerModel.Running;
                para.Act = actModeEnum;
                account.PlayerModel.Running.Act = actModeEnum;
                switch (actModeEnum)
                {
                    case ActModeEnum.login:
                        para.InterfaceUrl = CommonString.request_login;
                        para.Para = string.Format(CommonString.data_login, account.Account, account.Password);
                        para.Cookie = running.Cookie;
                        break;
                    case ActModeEnum.SelectArea:
                        para.InterfaceUrl = CommonString.request_area;
                        para.Para = string.Format(CommonString.data_area);
                        para.Cookie = running.Cookie;
                        break;
                    case ActModeEnum.Explore:
                        para.InterfaceUrl = CommonString.request_explore;
                        para.Para = string.Format(CommonString.data_explore, running.AreaId, running.AutoBuild, running.FloorId);
                        para.Cookie = running.Cookie;
                        break;
                    case ActModeEnum.SelectFloor:
                        para.InterfaceUrl = CommonString.request_floor;
                        para.Para = string.Format(CommonString.data_floor, running.AreaId);
                        para.Cookie = running.Cookie;
                        break;
                    case ActModeEnum.InMap:
                        para.InterfaceUrl = CommonString.request_inmap;
                        para.Para = string.Format(CommonString.data_inmap, running.AreaId, running.Check, running.FloorId);
                        para.Cookie = running.Cookie;
                        break;
                    case ActModeEnum.Lick:
                        para.InterfaceUrl = CommonString.request_battle;
                        para.Para = string.Format(CommonString.data_battle, running.SerialId, account.PlayerModel.UserId);
                        para.Cookie = running.Cookie;
                        break;
                    case ActModeEnum.FairySelect:
                        para.InterfaceUrl = CommonString.request_fairySelect;
                        para.Para = string.Format(CommonString.data_fairySelect);
                        para.Cookie = running.Cookie;
                        break;
                    case ActModeEnum.Menu:
                        para.InterfaceUrl = CommonString.request_menu;
                        para.Para = string.Format(CommonString.data_menu);
                        para.Cookie = running.Cookie;
                        break;
                    case ActModeEnum.AddFriend:
                        para.InterfaceUrl = CommonString.request_addFriend;
                        para.Para = string.Format(CommonString.data_addFriend, "1", running.FriendId);
                        para.Cookie = running.Cookie;
                        break;
                    case ActModeEnum.AddPoint:
                        para.InterfaceUrl = CommonString.request_levelUp;
                        para.Para = string.Format(CommonString.data_levelUp, "3", "0");
                        para.Cookie = running.Cookie;
                        break;
                    case ActModeEnum.Drink:
                        para.InterfaceUrl = CommonString.request_green;
                        para.Para = string.Format(CommonString.data_green);
                        para.Cookie = running.Cookie;
                        break;
                    case ActModeEnum.GetPresent:
                        para.InterfaceUrl = CommonString.request_present;
                        para.Para = string.Format(CommonString.data_present, running.GiftId);
                        para.Cookie = running.Cookie;
                        break;
                    case ActModeEnum.LookPresent:
                        para.InterfaceUrl = CommonString.request_lookpresent;
                        para.Para = string.Format(CommonString.data_lookpresent);
                        para.Cookie = running.Cookie;
                        break;
                    case ActModeEnum.SearchPlayer:
                        para.InterfaceUrl = CommonString.request_searchPlayer;
                        para.Para = string.Format(CommonString.data_searchPlayer, running.FriendName);
                        para.Cookie = running.Cookie;
                        break;
                    case ActModeEnum.ApplyCard:
                        para.InterfaceUrl = CommonString.request_applyCard;
                        para.Para = string.Format(CommonString.data_applyCard);
                        para.Cookie = running.Cookie;
                        break;
                    case ActModeEnum.SaveCard:
                        para.InterfaceUrl = CommonString.request_saveCard;
                        para.Para = string.Format(CommonString.data_saveCard, running.MiniCostCardId, running.MiniCostCardId);
                        para.Cookie = running.Cookie;
                        break;
                    case ActModeEnum.RemoveFriend:
                        para.InterfaceUrl = CommonString.request_removeFriend;
                        para.Para = string.Format(CommonString.data_removeFriend, running.FriendId);
                        para.Cookie = running.Cookie;
                        break;
                    case ActModeEnum.FriendList:
                        para.InterfaceUrl = CommonString.request_friendlist;
                        para.Para = string.Format(CommonString.data_friendlist);
                        para.Cookie = running.Cookie;
                        break;
                    case ActModeEnum.FriendNotice:
                        para.InterfaceUrl = CommonString.request_friend_notice;
                        para.Para = string.Format(CommonString.data_friend_notice);
                        para.Cookie = running.Cookie;
                        break;
                    case ActModeEnum.ApproveFriend:
                        para.InterfaceUrl = CommonString.request_approve_friend;
                        para.Para = string.Format(CommonString.data_approve_friend, running.FriendId);
                        para.Cookie = running.Cookie;
                        break;
                    case ActModeEnum.FriendApply:
                        para.InterfaceUrl = CommonString.request_friend_appstate;
                        para.Para = string.Format(CommonString.data_friend_appstate);
                        para.Cookie = running.Cookie;
                        break;
                    case ActModeEnum.UserFight:
                        para.InterfaceUrl = CommonString.request_userbattle;
                        para.Para = string.Format(CommonString.data_userbattle, 0, 0, running.FriendId);
                        para.Cookie = running.Cookie;
                        break;
                }
            }
            catch (Exception ex)
            {
                LogHelper.ErrorLog(Path, "GetHttpRequestParameter", ex.Message);
            }
            return para;
        }
        #endregion

        /// <summary>
        /// MA控参数获取函数
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static HttpRequestParameter GetMAKongParameter(string name, AccountModel account)
        {
            HttpRequestParameter reqPara = new HttpRequestParameter();
            reqPara.Para = string.Format("keyword={0}", name);
            string host = "http://www.niuxba.com";
            reqPara.InterfaceUrl = string.Format("/ma/backend/cgi-bin/getFootball2.php?phone={0}&groupId=1&serverNo=0&userId={1}", account.Account, account.PlayerModel.UserId);
            string url = host + reqPara.InterfaceUrl;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "post";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Host = "www.niuxba.com";
            request.Accept = "image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/x-shockwave-flash, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, */*";
            request.Headers.Set("Accept-Language", "zh-cn");
            request.KeepAlive = true;
            request.Proxy = null;
            request.Timeout = 20 * 1000;
            reqPara.req = request;
            return reqPara;
        }
    }
}
