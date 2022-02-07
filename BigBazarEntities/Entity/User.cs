using System;
using System.Collections.Generic;

#nullable disable

namespace BigBazarEntities.Entity
{
    public partial class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public bool Role { get; set; }
        public string Password { get; set; }
        public bool IsDeleted { get; set; } 
    }
}
