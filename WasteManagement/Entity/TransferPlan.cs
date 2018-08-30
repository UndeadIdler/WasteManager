using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class TransferPlan
    {
        /// <param name="PlanID">    </param>
        private int planID;
        public int PlanID
        {
            get { return planID; }
            set { planID = value; }
        }

        /// <param name="ContractID">    </param>
        private int contractID;
        public int ContractID
        {
            get { return contractID; }
            set { contractID = value; }
        }

        /// <param name="PlanID">    </param>
        private int produceID;
        public int ProduceID
        {
            get { return produceID; }
            set { produceID = value; }
        }


        /// <param name="PlanNumber">    </param>
        private string produceName;
        public string ProduceName
        {
            get { return produceName; }
            set { produceName = value; }
        }


        /// <param name="PlanNumber">    </param>
        private string planNumber;
        public string PlanNumber
        {
            get { return planNumber; }
            set { planNumber = value; }
        }

        /// <param name="ApprovalDate">    </param>
        private DateTime? approvalDate;
        public DateTime? ApprovalDate
        {
            get { return approvalDate; }
            set { approvalDate = value; }
        }

        /// <param name="StartDate">    </param>
        private DateTime? startDate;
        public DateTime? StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }

        /// <param name="EndDate">    </param>
        private DateTime? endDate;
        public DateTime? EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }

        /// <param name="PlanAmount">    </param>
        private decimal planAmount;
        public decimal PlanAmount
        {
            get { return planAmount; }
            set { planAmount = value; }
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

        /// <param name="Remark">    </param>
        private string remark;
        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }

        /// <param name="CreateUser">    </param>
        private string createUser;
        public string CreateUser
        {
            get { return createUser; }
            set { createUser = value; }
        }

        /// <param name="CreateDate">    </param>
        private DateTime? createDate;
        public DateTime? CreateDate
        {
            get { return createDate; }
            set { createDate = value; }
        }

        /// <param name="UpdateUser">    </param>
        private string updateUser;
        public string UpdateUser
        {
            get { return updateUser; }
            set { updateUser = value; }
        }

        /// <param name="UpdateDate">    </param>
        private DateTime? updateDate;
        public DateTime? UpdateDate
        {
            get { return updateDate; }
            set { updateDate = value; }
        }

        /// <param name="StatusID">    </param>
        private int statusID;
        public int StatusID
        {
            get { return statusID; }
            set { statusID = value; }
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

    }
}
