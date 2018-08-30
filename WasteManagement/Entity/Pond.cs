using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class Pond
    {
        /// <param name="PondID">    </param>
        private int pondID;
        public int PondID
        {
            get { return pondID; }
            set { pondID = value; }
        }

        /// <param name="Name">    </param>
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <param name="Capacity">    </param>
        private decimal capacity;
        public decimal Capacity
        {
            get { return capacity; }
            set { capacity = value; }
        }

        /// <param name="Stores">    </param>
        private string stores;
        public string Stores
        {
            get { return stores; }
            set { stores = value; }
        }

        /// <param name="Name">    </param>
        private string wasteName;
        public string WasteName
        {
            get { return wasteName; }
            set { wasteName = value; }
        }

        /// <param name="Number">    </param>
        private int number;
        public int Number
        {
            get { return number; }
            set { number = value; }
        }

        /// <param name="Used">    </param>
        private decimal used;
        public decimal Used
        {
            get { return used; }
            set { used = value; }
        }

        ///// <param name="Remain">    </param>
        //private decimal remain;
        //public decimal Remain
        //{
        //    get { return remain; }
        //    set { remain = value; }
        //}


        /// <param name="IsDelete">    </param>
        private int isDelete;
        public int IsDelete
        {
            get { return isDelete; }
            set { isDelete = value; }
        }

        /// <param name="Name">    </param>
        private string status;
        public string Status
        {
            get { return status; }
            set { status = value; }
        }

    }
}
