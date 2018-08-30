using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class WasteStorage
    {
        /// <param name="StorageID">    </param>
        private int storageID;
        public int StorageID
        {
            get { return storageID; }
            set { storageID = value; }
        }

        /// <param name="PlanID">    </param>
        private int planID;
        public int PlanID
        {
            get { return planID; }
            set { planID = value; }
        }

        /// <param name="BillNumber">    </param>
        private string billNumber;
        public string BillNumber
        {
            get { return billNumber; }
            set { billNumber = value; }
        }


        /// <param name="BillNumber">    </param>
        private string planNumber;
        public string PlanNumber
        {
            get { return planNumber; }
            set { planNumber = value; }
        }


        /// <param name="BillNumber">    </param>
        private string contractNumber;
        public string ContractNumber
        {
            get { return contractNumber; }
            set { contractNumber = value; }
        }

        /// <param name="BillNumber">    </param>
        private string produceName;
        public string ProduceName
        {
            get { return produceName; }
            set { produceName = value; }
        }

        /// <param name="DateTime">    </param>
        private DateTime? dateTime;
        public DateTime? DateTime
        {
            get { return dateTime; }
            set { dateTime = value; }
        }

        /// <param name="EnterpriseID">    </param>
        private int enterpriseID;
        public int EnterpriseID
        {
            get { return enterpriseID; }
            set { enterpriseID = value; }
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

        /// <param name="Amount">    </param>
        private decimal amount;
        public decimal Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        /// <param name="DriverID">    </param>
        private int driverID;
        public int DriverID
        {
            get { return driverID; }
            set { driverID = value; }
        }

        /// <param name="WasteCode">    </param>
        private string realName;
        public string RealName
        {
            get { return realName; }
            set { realName = value; }
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

        /// <param name="PondID">    </param>
        private int pondID;
        public int PondID
        {
            get { return pondID; }
            set { pondID = value; }
        }

        /// <param name="WasteCode">    </param>
        private string pondName;
        public string PondName
        {
            get { return pondName; }
            set { pondName = value; }
        }

        /// <param name="ReceiverID">    </param>
        private int receiverID;
        public int ReceiverID
        {
            get { return receiverID; }
            set { receiverID = value; }
        }

        /// <param name="WasteCode">    </param>
        private string receiverName;
        public string ReceiverName
        {
            get { return receiverName; }
            set { receiverName = value; }
        }

        /// <param name="WasteCode">    </param>
        private string statusName;
        public string StatusName
        {
            get { return statusName; }
            set { statusName = value; }
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
