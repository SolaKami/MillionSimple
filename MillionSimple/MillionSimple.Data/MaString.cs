using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MillionSimple.Data
{
    public static class MaString
    {
        //低COST卡，id

        public const string CardList = "9,124";

        //login

        public const string request_login = "/connect/app/login?cyt=1";
        public const string data_login = "login_id={0}&password={1}";

        //menu
        public const string request_menu = "/connect/app/mainmenu?cyt=1";
        public const string data_menu = "";


        //map

        public const string request_area = "/connect/app/exploration/area?cyt=1";
        public const string data_area = "";

        //floor

        public const string request_floor = "/connect/app/exploration/floor?cyt=1";

        public const string data_floor = "area_id={0}";

        //get_floor


        public const string request_inmap = "/connect/app/exploration/get_floor?cyt=1";

        public const string data_inmap = "area_id={0}&check={1}&floor_id={2}";

        //explore
        public const string request_explore = "/connect/app/exploration/explore?cyt=1";

        public const string data_explore = "area_id={0}&auto_build={1}&floor_id={2}";

        //fairy select

        public const string request_fairySelect = "/connect/app/menu/fairyselect?cyt=1";
        public const string data_fairySelect = "";


        //battle fairy

        public const string request_battle = "/connect/app/exploration/fairybattle?cyt=1";
        public const string data_battle = "serial_id={0}&user_id={1}";

        //add friend
        public const string request_addFriend = "/connect/app/friend/add_friend?cyt=1";
        public const string data_addFriend = "dialog={0}&user_id={1}";

        // drink green

        public const string request_green = "/connect/app/item/use?cyt=1";
        public const string data_green = "item_id=1";

        //level up

        public const string request_levelUp = "/connect/app/town/pointsetting?cyt=1";
        public const string data_levelUp = "ap={0}&bc={1}";


        //GetPresent
        public const string request_present = "/connect/app/menu/get_rewards?cyt=1";
        public const string data_present = "notice_id={0}";


        //look present

        public const string request_lookpresent = "/connect/app/menu/rewardbox?cyt=1";
        public const string data_lookpresent = "";


        //search player

        public const string request_searchPlayer = "/connect/app/menu/player_search?cyt=1";
        public const string data_searchPlayer = "name={0}";

        //look card
        public const string request_applyCard = "/connect/app/roundtable/edit?cyt=1";
        public const string data_applyCard = "move=1";
        //save card
        public const string request_saveCard = "/connect/app/cardselect/savedeckcard?cyt=1";
        public const string data_saveCard = "C={0}&lr={1}";


        //RemoveFriend
        public const string request_removeFriend = "/connect/app/friend/remove_friend?cyt=1";
        public const string data_removeFriend = "dialog=1&user_id={0}";

        //friend list
        public const string request_friendlist = "/connect/app/menu/friendlist?cyt=1";
        public const string data_friendlist = "move=0";

        // friend notice

        public const string request_friend_notice = "/connect/app/menu/friend_notice?cyt=1";
        public const string data_friend_notice = "move=0";


        // approve_friend
        public const string request_approve_friend = "/connect/app/friend/approve_friend?cyt=1";
        public const string data_approve_friend = "dialog=1&user_id={0}";


        //friend_appstate 申请情况3

        public const string request_friend_appstate = "/connect/app/menu/friend_appstate?cyt=1";
        public const string data_friend_appstate = "move=0";


        //战斗
        public const string request_zhandou = "/connect/app/battle/area?cyt=1";
        public const string data_zhandou = "";

        //competition_parts
        public const string request_competition_parts = "/connect/app/battle/competition_parts?redirect_flg=1";
        public const string data_competition_parts = "";

        //  /connect/app/battle/battle?cyt=1
        public const string request_userbattle = "/connect/app/battle/battle?cyt=1";
        /// <summary>
        /// 0,0,id
        /// </summary>
        public const string data_userbattle = "lake_id={0}&parts_id={1}&user_id={2}";

        ///connect/app/battle/battle_userlist?cyt=1
        ///
        public const string request_battle_userlist = "/connect/app/battle/battle_userlist?cyt=1";
        /// <summary>
        /// 0,1,0
        /// </summary>
        public const string data_battle_userlist = "knight_id={0}&move={1}&parts_id={2}";
    }
}
