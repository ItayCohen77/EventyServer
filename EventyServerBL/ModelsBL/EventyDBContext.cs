using EventyServerBL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventyServerBL.Models
{
    partial class EventyDBContext
    {
        public User Login(string email, string pswd)
        {
            User u = this.Users.Where(u => u.Email == email && u.Pass == pswd).FirstOrDefault();
            return u;
        }
    }
}
