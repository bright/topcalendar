#region

using System;
using System.ServiceModel;
using System.ServiceModel.Dispatcher;
using TopCalendar.Server.DataLayer.Entities;
using TopCalendar.Server.DataLayer.Repositories;
using TopCalendar.Server.ServiceLibrary.ServiceContract.DataContract;

#endregion

namespace TopCalendar.Server.ServiceLibrary.ServiceBehavior
{
    public class ValidUserParameterInspector : IParameterInspector
    {
        private readonly IUsersRepository _usersRepository;

        public ValidUserParameterInspector(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public void AfterCall(string operationName, object[] outputs, object returnValue, object correlationState)
        {
        }

        public object BeforeCall(string operationName, object[] inputs)
        {
            RequestWithCredentials requestWithCredentials = ObtainRequestWithCredentials(inputs);

            if (requestWithCredentials == null)
            {
                throw FaultException("RequestWithCredentials is missing");
            }

            UserCredentials userCredentials = requestWithCredentials.UserCredentials;

            if (userCredentials == null)
            {
                throw FaultException("UserCredentials are missing");
            }

            User user = _usersRepository.GetByLoginAndPassword(userCredentials.Login, userCredentials.Password);

            if (user == null)
            {
                throw FaultException("UserCredentials are wrong");
            }

            requestWithCredentials.CurrentUser = user;

            return null;
        }

        private Exception FaultException(string message)
        {
            var fc = new DataAccessFault {StatusReason = message};
            return new FaultException<DataAccessFault>(fc, new FaultReason(message));
        }

        private RequestWithCredentials ObtainRequestWithCredentials(object[] inputs)
        {
            foreach (var inputElement in inputs)
            {
                if (inputElement is RequestWithCredentials)
                {
                    return inputElement as RequestWithCredentials;
                }
            }

            return null;
        }
    }
}