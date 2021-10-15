using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelNow2.Api.Helpers;

namespace BD2.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ExtendedControllerBase : ControllerBase
    {
        public string UserEmail
        {
            get
            {                
                User.TryGetUserEmail(out var email);
                return email;
            }
        }

        public Guid? UserId
        {
            get
            {
                User.TryGetUserId(out var id);
                return id;
            }
        }
    }
}
