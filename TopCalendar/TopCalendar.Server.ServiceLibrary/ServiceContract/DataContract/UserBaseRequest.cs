using System.Runtime.Serialization;
using System.Text;

namespace TopCalendar.Server.ServiceLibrary.ServiceContract.DataContract
{
    [DataContract]
    public abstract class UserBaseRequest
    {
        [DataMember]
        public string Login { get; set; }

        [DataMember]
        public string Password { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("{UserBaseRequest ");
            sb.Append("\tLogin: ").AppendLine(Login);
            sb.Append("\tPassword: ").AppendLine(Password);
            sb.AppendLine("}");
            return sb.ToString();
        }
    }
}