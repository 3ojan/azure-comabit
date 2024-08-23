using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Users.Infrastructure;

namespace Comabit.UI.SignalR
{
    public class CompanyIdProvider : IUserIdProvider
    {
        public virtual string GetUserId(HubConnectionContext connection)
        {
            return connection.User?.FindFirst(ComabitClaimTypes.CompanyGuid)?.Value;
        }
    }
}
