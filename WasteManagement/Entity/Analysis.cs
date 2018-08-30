using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class Analysis
    {
        /// <param name="AnalysisID">    </param>
        private int analysisID;
        public int AnalysisID
        {
            get { return analysisID; }
            set { analysisID = value; }
        }

        /// <param name="BillNumber">    </param>
        private string billNumber;
        public string BillNumber
        {
            get { return billNumber; }
            set { billNumber = value; }
        }

        /// <param name="DateTime">    </param>
        private DateTime? dateTime;
        public DateTime? DateTime
        {
            get { return dateTime; }
            set { dateTime = value; }
        }

        /// <param name="AnalysisManID">    </param>
        private int analysisManID;
        public int AnalysisManID
        {
            get { return analysisManID; }
            set { analysisManID = value; }
        }

        /// <param name="BillNumber">    </param>
        private string realName;
        public string RealName
        {
            get { return realName; }
            set { realName = value; }
        }

        /// <param name="Status">    </param>
        private int status;
        public int Status
        {
            get { return status; }
            set { status = value; }
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

    }
}
