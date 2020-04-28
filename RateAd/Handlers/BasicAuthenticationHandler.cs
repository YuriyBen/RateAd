using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RateAd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace RateAd.Handlers
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly RateAdContext _context;
        public BasicAuthenticationHandler(
            IOptionsMonitor< AuthenticationSchemeOptions>options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            RateAdContext context   ):  base(options,logger,encoder,clock)
        {
            _context = context;
        }

        
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if(!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("Authorization head was not found..");

            try
            {
                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
                string[] credentials = Encoding.UTF8.GetString(credentialBytes).Split(':');
                string UserName = credentials[0];
                string PasswordHash = credentials[1];

                User user = _context.Users.Where(u => u.UserName == UserName 
                                                   && u.PasswordHash == PasswordHash)
                                          .FirstOrDefault();
                if(user == null)
                {
                    return AuthenticateResult.Fail("Invalid credentials..");
                }
                else
                {
                    var claims = new[] { new Claim(ClaimTypes.Name, user.UserName) };
                    var identity = new ClaimsIdentity(claims, Scheme.Name);
                    var principal = new ClaimsPrincipal(identity);
                    var ticket = new AuthenticationTicket(principal, Scheme.Name);

                    return AuthenticateResult.Success(ticket);
                }

            }
            catch (Exception)
            {
                AuthenticateResult.Fail("Error has occured..");
            }
            return AuthenticateResult.Fail("");
        }
    }
}
