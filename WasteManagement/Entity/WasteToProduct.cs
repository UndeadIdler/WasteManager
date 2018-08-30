using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class WasteToProduct
    {
        /// <param name="DealID">    </param>
        private int dealID;
        public int DealID
        {
            get { return dealID; }
            set { dealID = value; }
        }

        /// <param name="FromPondID">    </param>
        private int fromPondID;
        public int FromPondID
        {
            get { return fromPondID; }
            set { fromPondID = value; }
        }

        /// <param name="FromWasteCode">    </param>
        private string fromWasteCode;
        public string FromWasteCode
        {
            get { return fromWasteCode; }
            set { fromWasteCode = value; }
        }

        /// <param name="DateTime">    </param>
        private DateTime? dateTime;
        public DateTime? DateTime
        {
            get { return dateTime; }
            set { dateTime = value; }
        }

        /// <param name="FromAmount">    </param>
        private decimal fromAmount;
        public decimal FromAmount
        {
            get { return fromAmount; }
            set { fromAmount = value; }
        }

        /// <param name="HanderManID">    </param>
        private int handerManID;
        public int HanderManID
        {
            get { return handerManID; }
            set { handerManID = value; }
        }


        /// <param name="ReceiverID">    </param>
        private int receiverID;
        public int ReceiverID
        {
            get { return receiverID; }
            set { receiverID = value; }
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
