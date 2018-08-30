using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class FinalWasteLog
    {
        /// <param name="LogID">    </param>
        private int logID;
        public int LogID
        {
            get { return logID; }
            set { logID = value; }
        }

        /// <param name="LogNumber">    </param>
        private int logNumber;
        public int LogNumber
        {
            get { return logNumber; }
            set { logNumber = value; }
        }

        /// <param name="DateTime">    </param>
        private DateTime? dateTime;
        public DateTime? DateTime
        {
            get { return dateTime; }
            set { dateTime = value; }
        }

        /// <param name="IYear">    </param>
        private int iYear;
        public int IYear
        {
            get { return iYear; }
            set { iYear = value; }
        }

        /// <param name="Number">    </param>
        private int number;
        public int Number
        {
            get { return number; }
            set { number = value; }
        }

        /// <param name="UserID">    </param>
        private int userID;
        public int UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        /// <param name="FromWasteCode">    </param>
        private string realName;
        public string RealName
        {
            get { return realName; }
            set { realName = value; }
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

        /// <param name="UpdateDate">    </param>
        private DateTime? updateDate;
        public DateTime? UpdateDate
        {
            get { return updateDate; }
            set { updateDate = value; }
        }

        /// <param name="UpdateUser">    </param>
        private string updateUser;
        public string UpdateUser
        {
            get { return updateUser; }
            set { updateUser = value; }
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
