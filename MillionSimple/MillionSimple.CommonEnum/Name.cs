using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MillionSimple.CommonEnum
{
    public class Name : Attribute
    {
        public string Des { get; set; }

        public Name(string des)
        {
            this.Des = des;
        }

        public Name()
        {
        }
    }
}
