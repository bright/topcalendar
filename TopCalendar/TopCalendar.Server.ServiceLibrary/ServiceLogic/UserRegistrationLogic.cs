#region

using System;
using TopCalendar.Server.DataLayer.Entities;
using TopCalendar.Server.DataLayer.Exceptions;
using TopCalendar.Server.DataLayer.Repositories;
using TopCalendar.Server.ServiceLibrary.ServiceContract.DataContract;
using TopCalendar.Server.ServiceLibrary.ServiceContract.DataContract.StatusReason;

#endregion

namespace TopCalendar.Server.ServiceLibrary.ServiceLogic
{
    /// <summary>
    /// todo: Zastanawiam sie nad nazwa tej klasy... - Michal
    /// </summary>
    public class UserRegistrationLogic :
        RequestToResponseLogic<RegisterUserRequest, RegisterUserResponse>
    {
        private readonly IUsersRepository _usersRepository;

        public UserRegistrationLogic(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public RegisterUserResponse RegisterUser(RegisterUserRequest registerUserRequest)
        {			
        	return WithinTransactionDo(s => _usersRepository.Add(
        	                                	new User(registerUserRequest.UserCredentials.Login, registerUserRequest.UserCredentials.Password)
        	                                	)).OnErrorSetMessage(StatusReasonFor.RegisterUser.LOGIN_ALREADY_TAKEN);                                    
        }

        public LoginUserResponse CheckUser(LoginUserRequest loginUserRequest)
        {
            throw new NotImplementedException();
        }
    }
}