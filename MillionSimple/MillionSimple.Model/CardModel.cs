using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MillionSimple.Model
{
    /// <summary>
    /// 卡类
    /// </summary>
    public class CardModel
    {

        public CardModel()
        {
            this.master_card_id = "empty";
        }

        public string serial_id { get; set; }

        public string master_card_id { get; set; }

        public string name { get; set; }

        public string holography { get; set; }

        public string hp { get; set; }

        public string power { get; set; }

        public string critical { get; set; }

        public string lv { get; set; }

        public string lv_max { get; set; }

        public string exp { get; set; }

        public string max_exp { get; set; }

        public string next_exp { get; set; }

        public string exp_diff { get; set; }

        public string exp_per { get; set; }

        public string sale_price { get; set; }

        public string material_price { get; set; }

        public string evolution_price { get; set; }

        public string plus_limit_count { get; set; }

        public string limit_over { get; set; }

    }
}
