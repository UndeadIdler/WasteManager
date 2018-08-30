using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class AnalysisResult
    {
        /// <param name="ResultID">    </param>
        private int resultID;
        public int ResultID
        {
            get { return resultID; }
            set { resultID = value; }
        }

        /// <param name="BillNumber">    </param>
        private string billNumber;
        public string BillNumber
        {
            get { return billNumber; }
            set { billNumber = value; }
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
