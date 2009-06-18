using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IdentityModel.Selectors;
using System.ServiceModel;

namespace ServerLib
{
    class TCUserValidator : UserNamePasswordValidator
    {
        public override void Validate(string userName, string password)
        {
            if (userName == null || password == null)
            {
                throw new ArgumentException("UserName or password is null");
            }

            if (!userName.Equals("test") || !password.Equals("test"))
                throw new FaultException("Bad username or password");

        }
    }
}
