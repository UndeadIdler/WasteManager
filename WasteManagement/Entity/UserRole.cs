using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class UserRole
    {
        public User user = new User();
        public List<Role> role = new List<Role>();
        public List<int> Add = new List<int>();
        public List<int> Delete = new List<int>();
    }
}
