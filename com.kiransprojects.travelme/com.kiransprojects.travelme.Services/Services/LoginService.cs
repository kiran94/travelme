namespace com.kiransprojects.travelme.Services.Services
{
    using com.kiransprojects.travelme.DataAccess.Interfaces;
using com.kiransprojects.travelme.Framework.Entities;
using com.kiransprojects.travelme.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    /// <summary>
    /// Login Service
    /// </summary>
    public class LoginService : ILoginService
    {
        /// <summary>
        /// User Entity Repository
        /// </summary>
        private readonly IRepository<UserEntity> _repository = null; 

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginService"/> class.
        /// </summary>
        public LoginService(IRepository<UserEntity> repository)
        {
            if(repository == null)
            {
                throw new NotImplementedException("Login Repository"); 
            }

            this._repository = repository; 
        }

        public bool RegisterUser(out UserEntity User)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Indicates if a user has provided the correct details for authentication
        /// </summary>
        /// <param name="Email">User input Email</param>
        /// <param name="Password">User input Password</param>
        /// <returns>Flag indicating if user is authenticated</returns>
        public bool SignIn(string Email, string Password)
        {
            throw new NotImplementedException();
        }

        public bool ForgotPassword(string Email)
        {
            throw new NotImplementedException();
        }
    }
}