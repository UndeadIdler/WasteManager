using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class PondLog
    {
        /// <param name="LogID">    </param>
        private int logID;
        public int LogID
        {
            get { return logID; }
            set { logID = value; }
        }

        /// <param name="SourceID">    </param>
        private int sourceID;
        public int SourceID
        {
            get { return sourceID; }
            set { sourceID = value; }
        }

        /// <param name="ToID">    </param>
        private int toID;
        public int ToID
        {
            get { return toID; }
            set { toID = value; }
        }

        /// <param name="CreateDate">    </param>
        private DateTime? createDate;
        public DateTime? CreateDate
        {
            get { return createDate; }
            set { createDate = value; }
        }

        /// <param name="CreateUser">    </param>
        private string createUser;
        public string CreateUser
        {
            get { return createUser; }
            set { createUser = value; }
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
