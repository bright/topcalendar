using System.Runtime.Serialization;

namespace TopCalendar.Server.ServiceLibrary.ServiceContract.DataContract
{
    [DataContract]
    public class DataAccessFault : BaseResponse
    {
        public DataAccessFault()
        {
            Success = false;
        }
    }
}