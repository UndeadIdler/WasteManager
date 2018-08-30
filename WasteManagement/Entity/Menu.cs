using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class Menu
    {
        /// <param name="ID">    </param>
        private int iD;
        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }

        /// <param name="FatherID">    </param>
        private int fatherID;
        public int FatherID
        {
            get { return fatherID; }
            set { fatherID = value; }
        }

        /// <param name="MenuName">    </param>
        private string menuName;
        public string MenuName
        {
            get { return menuName; }
            set { menuName = value; }
        }

        /// <param name="ImageUrl">    </param>
        private string imageUrl;
        public string ImageUrl
        {
            get { return imageUrl; }
            set { imageUrl = value; }
        }

        /// <param name="MenuUrl">    </param>
        private string menuUrl;
        public string MenuUrl
        {
            get { return menuUrl; }
            set { menuUrl = value; }
        }

        /// <param name="MenuOrder">    </param>
        private int menuOrder;
        public int MenuOrder
        {
            get { return menuOrder; }
            set { menuOrder = value; }
        }

        /// <param name="MenuFile">    </param>
        private string menuFile;
        public string MenuFile
        {
            get { return menuFile; }
            set { menuFile = value; }
        }

    }
}
