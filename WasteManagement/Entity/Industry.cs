using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class Industry
    {
        /// <param name="ID">    </param>
        private int iD;
        public int ID
        {
            get { return iD; }
            set { iD = value; }
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
