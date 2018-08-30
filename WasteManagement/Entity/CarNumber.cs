using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class CarNumber
    {
        /// <param name="ID">    </param>
        private int iD;
        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }

        /// <param name="CarNumber">    </param>
        private string number;
        public string Number
        {
            get { return number; }
            set { number = value; }
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

        /// <param name="IsStop">    </param>
        private bool isStop;
        public bool IsStop
        {
            get { return isStop; }
            set { isStop = value; }
        }

    }
}
