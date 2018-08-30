using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class Status
    {
        /// <param name="StatusID">    </param>
        private int statusID;
        public int StatusID
        {
            get { return statusID; }
            set { statusID = value; }
        }

        /// <param name="Name">    </param>
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <param name="Code">    </param>
        private int code;
        public int Code
        {
            get { return code; }
            set { code = value; }
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
