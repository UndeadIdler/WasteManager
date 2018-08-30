using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class Position
    {
        /// <param name="ID">    </param>
        private int iD;
        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }

        /// <param name="Name">    </param>
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <param name="OrderID">    </param>
        private int orderID;
        public int OrderID
        {
            get { return orderID; }
            set { orderID = value; }
        }

        /// <param name="IsShow">    </param>
        private int isShow;
        public int IsShow
        {
            get { return isShow; }
            set { isShow = value; }
        }

    }
}
