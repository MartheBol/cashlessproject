using Microsoft.Owin.Security.OAuth;
using nmct.ba.cashlessproject.model;
using nmct.ba.cashlessproject.model.IT_Bedrijf;
using nmct.ssa.cashlessproject.webapp.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace nmct.ssa.cashlessproject.webapp.Providers
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            Organisations o =  VerenigingenDA.CheckCredentials(context.UserName, context.Password);
            
            if (o == null)
            {
                context.Rejected();
                return Task.FromResult(0);
            }

            var id = new ClaimsIdentity(context.Options.AuthenticationType);
            id.AddClaim(new Claim("username", context.UserName));;
            id.AddClaim(new Claim("dbpass", context.Password));

            id.AddClaim(new Claim(ClaimTypes.Role, "OrganisationManager"));

            context.Validated(id);
            return Task.FromResult(0);
        }
    }
}