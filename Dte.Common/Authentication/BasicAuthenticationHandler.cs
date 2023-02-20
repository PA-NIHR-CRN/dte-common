using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Dte.Common.Authentication
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly AuthenticationSettings _authenticationSettings;

        public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            AuthenticationSettings authenticationSettings) : base(options, logger, encoder, clock)
        {
            _authenticationSettings = authenticationSettings;
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var authenticationHeader = BasicAuthorizationHeaderParser.GetHeader(Request.Headers["Authorization"]);
            if (authenticationHeader == null)
            {
                return Task.FromResult(AuthenticateResult.NoResult());
            }

            if (_authenticationSettings.BasicAuthClients == null)
            {
                return Task.FromResult(AuthenticateResult.Fail("Please provide in basic authentication credentials - user and password"));
            }
            
            if (!_authenticationSettings.BasicAuthClients.Any(x => x.ClientName == authenticationHeader.Username && x.ClientPassword == authenticationHeader.Password))
            {
                return Task.FromResult(AuthenticateResult.Fail("Invalid user and/or password"));
            }
            
            var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, authenticationHeader.Username), }, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return Task.FromResult(AuthenticateResult.Success(ticket));

        }
    }
}