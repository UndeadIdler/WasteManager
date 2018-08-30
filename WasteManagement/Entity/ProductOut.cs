using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class ProductOut
    {
        /// <param name="OutID">    </param>
        private int outID;
        public int OutID
        {
            get { return outID; }
            set { outID = value; }
        }

        /// <param name="PondID">    </param>
        private int pondID;
        public int PondID
        {
            get { return pondID; }
            set { pondID = value; }
        }

        /// <param name="WasteCode">    </param>
        private string wasteCode;
        public string WasteCode
        {
            get { return wasteCode; }
            set { wasteCode = value; }
        }

        /// <param name="WasteCode">    </param>
        private string wasteName;
        public string WasteName
        {
            get { return wasteName; }
            set { wasteName = value; }
        }

        /// <param name="DateTime">    </param>
        private DateTime? dateTime;
        public DateTime? DateTime
        {
            get { return dateTime; }
            set { dateTime = value; }
        }

        /// <param name="Amount">    </param>
        private decimal amount;
        public decimal Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        /// <param name="ReceiverEnterpriseID">    </param>
        private int receiverEnterpriseID;
        public int ReceiverEnterpriseID
        {
            get { return receiverEnterpriseID; }
            set { receiverEnterpriseID = value; }
        }

        /// <param name="WasteCode">    </param>
        private string enterpriseName;
        public string EnterpriseName
        {
            get { return enterpriseName; }
            set { enterpriseName = value; }
        }

        /// <param name="DriverID">    </param>
        private int driverID;
        public int DriverID
        {
            get { return driverID; }
            set { driverID = value; }
        }

        /// <param name="WasteCode">    </param>
        private string driverName;
        public string DriverName
        {
            get { return driverName; }
            set { driverName = value; }
        }

        /// <param name="PlanID">    </param>
        private int carID;
        public int CarID
        {
            get { return carID; }
            set { carID = value; }
        }

        /// <param name="WasteCode">    </param>
        private string carNumber;
        public string CarNumber
        {
            get { return carNumber; }
            set { carNumber = value; }
        }

        /// <param name="ConsignorID">    </param>
        private int consignorID;
        public int ConsignorID
        {
            get { return consignorID; }
            set { consignorID = value; }
        }

        /// <param name="WasteCode">    </param>
        private string consignorName;
        public string ConsignorName
        {
            get { return consignorName; }
            set { consignorName = value; }
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
