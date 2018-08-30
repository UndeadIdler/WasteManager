using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class PondChange
    {
        /// <param name="ChangeID">    </param>
        private int changeID;
        public int ChangeID
        {
            get { return changeID; }
            set { changeID = value; }
        }

        /// <param name="PondID">    </param>
        private int pondID;
        public int PondID
        {
            get { return pondID; }
            set { pondID = value; }
        }

        /// <param name="OldAmount">    </param>
        private decimal oldAmount;
        public decimal OldAmount
        {
            get { return oldAmount; }
            set { oldAmount = value; }
        }

        /// <param name="NewAmount">    </param>
        private decimal newAmount;
        public decimal NewAmount
        {
            get { return newAmount; }
            set { newAmount = value; }
        }

        /// <param name="Remark">    </param>
        private string remark;
        public string Remark
        {
            get { return remark; }
            set { remark = value; }
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

    }
}
