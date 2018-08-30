using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class PondUsed
    {
        /// <param name="ID">    </param>
        private int iD;
        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }

        /// <param name="PondID">    </param>
        private int pondID;
        public int PondID
        {
            get { return pondID; }
            set { pondID = value; }
        }

        /// <param name="Used">    </param>
        private decimal used;
        public decimal Used
        {
            get { return used; }
            set { used = value; }
        }

        /// <param name="SourceType">    </param>
        private int sourceType;
        public int SourceType
        {
            get { return sourceType; }
            set { sourceType = value; }
        }

        /// <param name="TypeName">    </param>
        private string typeName;
        public string TypeName
        {
            get { return typeName; }
            set { typeName = value; }
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

    }
}
