using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class Area
    {
        /// <param name="ID">    </param>
        private int iD;
        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }

        /// <param name="AreaCode">    </param>
        private int areaCode;
        public int AreaCode
        {
            get { return areaCode; }
            set { areaCode = value; }
        }

        /// <param name="FullName">    </param>
        private string fullName;
        public string FullName
        {
            get { return fullName; }
            set { fullName = value; }
        }

        /// <param name="LetterCode">    </param>
        private string letterCode;
        public string LetterCode
        {
            get { return letterCode; }
            set { letterCode = value; }
        }

        /// <param name="ShortName">    </param>
        private string shortName;
        public string ShortName
        {
            get { return shortName; }
            set { shortName = value; }
        }

        /// <param name="IsDelete">    </param>
        private int isDelete;
        public int IsDelete
        {
            get { return isDelete; }
            set { isDelete = value; }
        }

        /// <param name="OrderID">    </param>
        private int orderID;
        public int OrderID
        {
            get { return orderID; }
            set { orderID = value; }
        }

    }

}

