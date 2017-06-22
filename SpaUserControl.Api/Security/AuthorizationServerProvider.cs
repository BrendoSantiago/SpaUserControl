using Microsoft.Owin.Security.OAuth;
using SpaUserControl.Common.Resources;
using SpaUserControl.Domain.Contracts.Services;
using System;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;

namespace SpaUserControl.Api.Security
{
    public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        private readonly IUserService _service;

        public AuthorizationServerProvider(IUserService service)
        {
            _service = service;
        }

        //Valida um token já existente
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        //Caso não exista um token
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            try
            {
                var user = _service.Authenticate(context.UserName, context.Password);

                if (user == null)
                {
                    context.SetError("invalid_grant", Errors.InvalidCredentials);
                    return;
                }

                //Se o usuário for válido, vou iniciar a criação de um Claim para identidade dele
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);

                identity.AddClaim(new Claim(ClaimTypes.Name, user.Email));
                identity.AddClaim(new Claim(ClaimTypes.GivenName, user.Name));

                //Vai passar o usuário autenticado para a thread atual
                GenericPrincipal principal = new GenericPrincipal(identity, null);
                Thread.CurrentPrincipal = principal;

                context.Validated(identity);
            }
            catch (Exception)
            {
                context.SetError("invalid_grant", Errors.InvalidCredentials);
            }
        }
    }
}