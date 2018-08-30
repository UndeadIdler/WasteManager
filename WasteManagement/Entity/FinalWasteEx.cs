using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class FinalWasteEx
    {
        /// <param name="FWID">    </param>
        private int fWID;
        public int FWID
        {
            get { return fWID; }
            set { fWID = value; }
        }

        /// <param name="LogID">    </param>
        private int logID;
        public int LogID
        {
            get { return logID; }
            set { logID = value; }
        }

        /// <param name="ItemCode">    </param>
        private string itemCode;
        public string ItemCode
        {
            get { return itemCode; }
            set { itemCode = value; }
        }

        /// <param name="Result">    </param>
        private decimal result;
        public decimal Result
        {
            get { return result; }
            set { result = value; }
        }

        /// <param name="Status">    </param>
        private int status;
        public int Status
        {
            get { return status; }
            set { status = value; }
        }

    }
}
