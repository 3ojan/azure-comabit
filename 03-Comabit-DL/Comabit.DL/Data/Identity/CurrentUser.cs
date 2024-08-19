using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace Users.Infrastructure
{
    public class CurrentUser
    {
        ClaimsPrincipal Ident { get; set; }

        public CurrentUser(ClaimsPrincipal ident)
        {
            Ident = ident;
        }
    }
}