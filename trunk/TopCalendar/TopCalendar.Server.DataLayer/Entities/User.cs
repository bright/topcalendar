using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TopCalendar.Server.DataLayer.Entities
{
    public class User
    {
        public virtual int Id { get; private set; }
        public virtual string Login { get; set; }
        public virtual string Password { get; set; }
        
    }
}
