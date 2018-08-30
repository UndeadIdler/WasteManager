using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class User
    {
        /// <param name="ID">    </param>
        private int iD;
        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }

        /// <param name="UserName">    </param>
        private string userName;
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        /// <param name="PassWord">    </param>
        private string passWord;
        public string PassWord
        {
            get { return passWord; }
            set { passWord = value; }
        }

        /// <param name="GUID">    </param>
        private string gUID;
        public string GUID
        {
            get { return gUID; }
            set { gUID = value; }
        }

        /// <param name="Department">    </param>
        private string department;
        public string Department
        {
            get { return department; }
            set { department = value; }
        }

        /// <param name="RealName">    </param>
        private string realName;
        public string RealName
        {
            get { return realName; }
            set { realName = value; }
        }

        /// <param name="PwdChgDate">    </param>
        private DateTime? pwdChgDate;
        public DateTime? PwdChgDate
        {
            get { return pwdChgDate; }
            set { pwdChgDate = value; }
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
