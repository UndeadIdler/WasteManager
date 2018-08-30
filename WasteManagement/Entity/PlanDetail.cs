using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class PlanDetail
    {
        /// <param name="ItemID">    </param>
        private int itemID;
        public int ItemID
        {
            get { return itemID; }
            set { itemID = value; }
        }

        /// <param name="PlanID">    </param>
        private int planID;
        public int PlanID
        {
            get { return planID; }
            set { planID = value; }
        }

        /// <param name="Code">    </param>
        private string code;
        public string Code
        {
            get { return code; }
            set { code = value; }
        }

        /// <param name="Amount">    </param>
        private decimal amount;
        public decimal Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        /// <param name="IsDelete">    </param>
        private int isDelete;
        public int IsDelete
        {
            get { return isDelete; }
            set { isDelete = value; }
        }

    }
}
