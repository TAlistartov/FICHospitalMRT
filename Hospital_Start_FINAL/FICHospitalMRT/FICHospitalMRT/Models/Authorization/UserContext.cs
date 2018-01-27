using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FICHospitalMRT.Models.Authorization
{
    public class UserContext : DbContext
    {
        public UserContext() : base("AuthorisationConnection") { }
        public DbSet<User> Users {get;set;}
          
    }
}