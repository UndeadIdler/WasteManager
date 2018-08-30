using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class Enterprise
    {
        /// <param name="EnterpriseID">    </param>
        private int enterpriseID;
        public int EnterpriseID
        {
            get { return enterpriseID; }
            set { enterpriseID = value; }
        }

        /// <param name="Name">    </param>
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <param name="LawManCode">    </param>
        private string lawManCode;
        public string LawManCode
        {
            get { return lawManCode; }
            set { lawManCode = value; }
        }

        /// <param name="OrganizationCode">    </param>
        private string organizationCode;
        public string OrganizationCode
        {
            get { return organizationCode; }
            set { organizationCode = value; }
        }

        /// <param name="PastName">    </param>
        private string pastName;
        public string PastName
        {
            get { return pastName; }
            set { pastName = value; }
        }

        /// <param name="SetUpDate">    </param>
        private DateTime? setUpDate;
        public DateTime? SetUpDate
        {
            get { return setUpDate; }
            set { setUpDate = value; }
        }

        /// <param name="Type">    </param>
        private int type;
        public int Type
        {
            get { return type; }
            set { type = value; }
        }

        /// <param name="FaxNumber">    </param>
        private string faxNumber;
        public string FaxNumber
        {
            get { return faxNumber; }
            set { faxNumber = value; }
        }

        /// <param name="Industry">    </param>
        private int industry;
        public int Industry
        {
            get { return industry; }
            set { industry = value; }
        }

        /// <param name="AreaCode">    </param>
        private string areaCode;
        public string AreaCode
        {
            get { return areaCode; }
            set { areaCode = value; }
        }

        /// <param name="PostCode">    </param>
        private string postCode;
        public string PostCode
        {
            get { return postCode; }
            set { postCode = value; }
        }

        /// <param name="Address">    </param>
        private string address;
        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        /// <param name="LawMan">    </param>
        private string lawMan;
        public string LawMan
        {
            get { return lawMan; }
            set { lawMan = value; }
        }

        /// <param name="Email">    </param>
        private string email;
        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        /// <param name="Phone1">    </param>
        private string phone1;
        public string Phone1
        {
            get { return phone1; }
            set { phone1 = value; }
        }

        /// <param name="Telphone1">    </param>
        private string telphone1;
        public string Telphone1
        {
            get { return telphone1; }
            set { telphone1 = value; }
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
