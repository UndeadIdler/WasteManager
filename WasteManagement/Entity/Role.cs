using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class Role
    {
        /// <param name="ID">    </param>
        private int iD;
        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }

        /// <param name="RoleName">    </param>
        private string roleName;
        public string RoleName
        {
            get { return roleName; }
            set { roleName = value; }
        }

        /// <param name="Description">    </param>
        private string description;
        public string Description
        {
            get { return description; }
            set { description = value; }
        }


    }
}
