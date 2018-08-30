using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class MonitorItem
    {
        /// <param name="ItemID">    </param>
        private int itemID;
        public int ItemID
        {
            get { return itemID; }
            set { itemID = value; }
        }

        /// <param name="ItemCode">    </param>
        private string itemCode;
        public string ItemCode
        {
            get { return itemCode; }
            set { itemCode = value; }
        }

        /// <param name="ItemName">    </param>
        private string itemName;
        public string ItemName
        {
            get { return itemName; }
            set { itemName = value; }
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

        /// <param name="Unit">    </param>
        private string unit;
        public string Unit
        {
            get { return unit; }
            set { unit = value; }
        }
    }
}
