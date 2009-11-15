#region

using System;
using TopCalendar.Server.DataLayer.Entities;
using TopCalendar.Server.DataLayer.Repositories;
using TopCalendar.Server.DataLayer.Repositories.Exceptions;
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
            User user = new User
                            {
                                Login = registerUserRequest.Login,
                                Password = registerUserRequest.Password
                            };

            try
            {
                _usersRepository.Add(user);

                return SuccessSituationResponse();
            }
            catch (UserLoginAlreadyTakenException)
            {
                return ErrorSituationResponse(StatusReasonFor.RegisterUser.LOGIN_ALREADY_TAKEN);
            }
        }

        public CheckUserResponse CheckUser(CheckUserRequest checkUserRequest)
        {
            throw new NotImplementedException();
        }
    }
}