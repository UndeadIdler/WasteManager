using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class Waste
    {

        /// <param name="ID">    </param>
        private int iD;
        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }

        /// <param name="WasteCode">    </param>
        private string wasteCode;
        public string WasteCode
        {
            get { return wasteCode; }
            set { wasteCode = value; }
        }

        /// <param name="WasteName">    </param>
        private string wasteName;
        public string WasteName
        {
            get { return wasteName; }
            set { wasteName = value; }
        }

        /// <param name="List">    </param>
        private string list;
        public string List
        {
            get { return list; }
            set { list = value; }
        }

        /// <param name="Type">    </param>
        private int type;
        public int Type
        {
            get { return type; }
            set { type = value; }
        }

        /// <param name="Unit">    </param>
        private string unit;
        public string Unit
        {
            get { return unit; }
            set { unit = value; }
        }

        /// <param name="OrderID">    </param>
        private int orderID;
        public int OrderID
        {
            get { return orderID; }
            set { orderID = value; }
        }

        /// <param name="IsShow">    </param>
        private int isShow;
        public int IsShow
        {
            get { return isShow; }
            set { isShow = value; }
        }


    }
}
