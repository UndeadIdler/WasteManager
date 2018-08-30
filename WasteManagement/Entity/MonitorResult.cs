using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class MonitorResult
    {
        /// <param name="ResultID">    </param>
        private int resultID;
        public int ResultID
        {
            get { return resultID; }
            set { resultID = value; }
        }

        /// <param name="MonitorID">    </param>
        private int monitorID;
        public int MonitorID
        {
            get { return monitorID; }
            set { monitorID = value; }
        }

        /// <param name="ItemCode">    </param>
        private string itemCode;
        public string ItemCode
        {
            get { return itemCode; }
            set { itemCode = value; }
        }


        /// <param name="ItemCode">    </param>
        private string itemName;
        public string ItemName
        {
            get { return itemName; }
            set { itemName = value; }
        }

        /// <param name="Result">    </param>
        private decimal result;
        public decimal Result
        {
            get { return result; }
            set { result = value; }
        }

    }
}
