using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class ProductDetail
    {
        /// <param name="DetailID">    </param>
        private int detailID;
        public int DetailID
        {
            get { return detailID; }
            set { detailID = value; }
        }

        /// <param name="WTPID">    </param>
        private int wTPID;
        public int WTPID
        {
            get { return wTPID; }
            set { wTPID = value; }
        }

        /// <param name="ItemCode">    </param>
        private string itemCode;
        public string ItemCode
        {
            get { return itemCode; }
            set { itemCode = value; }
        }

        /// <param name="PondID">    </param>
        private int pondID;
        public int PondID
        {
            get { return pondID; }
            set { pondID = value; }
        }

        /// <param name="ItemCode">    </param>
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <param name="Status">    </param>
        private int status;
        public int Status
        {
            get { return status; }
            set { status = value; }
        }

        /// <param name="Amount">    </param>
        private decimal amount;
        public decimal Amount
        {
            get { return amount; }
            set { amount = value; }
        }

    }
}
