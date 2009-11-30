#region

using System;
using System.Runtime.Serialization;
using TopCalendar.Server.DataLayer.Entities;

#endregion

namespace TopCalendar.Server.ServiceLibrary.ServiceContract.DataContract
{
    [DataContract]
    public abstract class RequestWithCredentials
    {
        [DataMember]
        public UserCredentials UserCredentials { get; set; }

        public User CurrentUser
        {
            get; set;
        }
    }
}