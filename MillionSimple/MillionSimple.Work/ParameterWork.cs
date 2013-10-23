using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MillionSimple.Model;
using MillionSimple.Enum;
using MillionSimple.Data;
using MillionSimple.Common;

namespace MillionSimple.Work
{
    public class ParameterWork
    {
        private static string Path = @"MillionSimple\MillionSimple.Work\ParameterWork.cs";

        #region 获取MA HTTP参数
        /// <summary>
        /// 获取MA HTTP参数
        /// </summary>
        /// <param name="yase"></param>
        /// <returns></returns>
        public static HttpRequestParameter GetMAParameter(YaSeModel yase)
        {
            HttpRequestParameter reqPara = new HttpRequestParameter();
            try
            {
                switch (yase.Act)
                {
                    case ActModeEnum.login:
                        reqPara.RequestUrl = MaString.request_login;
                        reqPara.RequestData = string.Format(MaString.data_login, yase.Account, yase.Password);
                        reqPara.Cookie = yase.Cookie;
                        break;
                    case ActModeEnum.SelectArea:
                        reqPara.RequestUrl = MaString.request_area;
                        reqPara.RequestData = string.Format(MaString.data_area);
                        reqPara.Cookie = yase.Cookie;
                        break;
                    case ActModeEnum.Explore:
                        reqPara.RequestUrl = MaString.request_explore;
                        reqPara.RequestData = string.Format(MaString.data_explore, yase.MapId, "1", yase.FloorId);
                        reqPara.Cookie = yase.Cookie;
                        break;
                    case ActModeEnum.SelectFloor:
                        reqPara.RequestUrl = MaString.request_floor;
                        reqPara.RequestData = string.Format(MaString.data_floor, yase.MapId);
                        reqPara.Cookie = yase.Cookie;
                        break;
                    case ActModeEnum.InMap:
                        reqPara.RequestUrl = MaString.request_inmap;
                        reqPara.RequestData = string.Format(MaString.data_inmap, yase.MapId, "1", yase.FloorId);
                        reqPara.Cookie = yase.Cookie;
                        break;
                    case ActModeEnum.Attack:
                        reqPara.RequestUrl = MaString.request_battle;
                        reqPara.RequestData = string.Format(MaString.data_battle, yase.CurrentFairy.SerialId, yase.Id);
                        reqPara.Cookie = yase.Cookie;
                        break;
                    case ActModeEnum.FairySelect:
                        reqPara.RequestUrl = MaString.request_fairySelect;
                        reqPara.RequestData = string.Format(MaString.data_fairySelect);
                        reqPara.Cookie = yase.Cookie;
                        break;
                    case ActModeEnum.Menu:
                        reqPara.RequestUrl = MaString.request_menu;
                        reqPara.RequestData = string.Format(MaString.data_menu);
                        reqPara.Cookie = yase.Cookie;
                        break;
                    case ActModeEnum.AddFriend:
                        reqPara.RequestUrl = MaString.request_addFriend;
                        reqPara.RequestData = string.Format(MaString.data_addFriend, "1", yase.OtherPlayer.id);
                        reqPara.Cookie = yase.Cookie;
                        break;
                    case ActModeEnum.SetPoint:
                        reqPara.RequestUrl = MaString.request_levelUp;
                        reqPara.RequestData = string.Format(MaString.data_levelUp, "3", "0");
                        reqPara.Cookie = yase.Cookie;
                        break;
                    case ActModeEnum.DrinkTea:
                        reqPara.RequestUrl = MaString.request_green;
                        reqPara.RequestData = string.Format(MaString.data_green);
                        reqPara.Cookie = yase.Cookie;
                        break;
                    case ActModeEnum.GetGifts:
                        reqPara.RequestUrl = MaString.request_present;
                        reqPara.RequestData = string.Format(MaString.data_present, string.Join(",", yase.GiftId.ToArray()));
                        reqPara.Cookie = yase.Cookie;
                        break;
                    case ActModeEnum.LookGifts:
                        reqPara.RequestUrl = MaString.request_lookpresent;
                        reqPara.RequestData = string.Format(MaString.data_lookpresent);
                        reqPara.Cookie = yase.Cookie;
                        break;
                    case ActModeEnum.SearchPlayer:
                        reqPara.RequestUrl = MaString.request_searchPlayer;
                        reqPara.RequestData = string.Format(MaString.data_searchPlayer, yase.OtherPlayer.name);
                        reqPara.Cookie = yase.Cookie;
                        break;
                    case ActModeEnum.ApplyCard:
                        reqPara.RequestUrl = MaString.request_applyCard;
                        reqPara.RequestData = string.Format(MaString.data_applyCard);
                        reqPara.Cookie = yase.Cookie;
                        break;
                    case ActModeEnum.SaveCardGroup:
                        reqPara.RequestUrl = MaString.request_saveCard;
                        List<string> cardList = new List<string>();
                        foreach (CardModel card in yase.CardGroup)
                        {
                            cardList.Add(card.master_card_id);
                        }
                        reqPara.RequestData = string.Format(MaString.data_saveCard, string.Join(",", cardList.ToArray()), yase.LeaderCard.master_card_id);
                        reqPara.Cookie = yase.Cookie;
                        break;
                    case ActModeEnum.RemoveFriend:
                        reqPara.RequestUrl = MaString.request_removeFriend;
                        reqPara.RequestData = string.Format(MaString.data_removeFriend, yase.OtherPlayer.id);
                        reqPara.Cookie = yase.Cookie;
                        break;
                    case ActModeEnum.ViewFriendList:
                        reqPara.RequestUrl = MaString.request_friendlist;
                        reqPara.RequestData = string.Format(MaString.data_friendlist);
                        reqPara.Cookie = yase.Cookie;
                        break;
                    case ActModeEnum.FriendNotice:
                        reqPara.RequestUrl = MaString.request_friend_notice;
                        reqPara.RequestData = string.Format(MaString.data_friend_notice);
                        reqPara.Cookie = yase.Cookie;
                        break;
                    case ActModeEnum.ApproveFriend:
                        reqPara.RequestUrl = MaString.request_approve_friend;
                        reqPara.RequestData = string.Format(MaString.data_approve_friend, yase.OtherPlayer.id);
                        reqPara.Cookie = yase.Cookie;
                        break;
                    case ActModeEnum.FriendApplyList:
                        reqPara.RequestUrl = MaString.request_friend_appstate;
                        reqPara.RequestData = string.Format(MaString.data_friend_appstate);
                        reqPara.Cookie = yase.Cookie;
                        break;
                    case ActModeEnum.UserFight:
                        reqPara.RequestUrl = MaString.request_userbattle;
                        reqPara.RequestData = string.Format(MaString.data_userbattle, 0, 0, yase.OtherPlayer.id);
                        reqPara.Cookie = yase.Cookie;
                        break;
                }
            }
            catch (Exception ex)
            {
                LogHelper.ErrorLog(Path, "GetMAParameter", ex.Message);
            }
            return reqPara;
        }
        #endregion
    }
}
