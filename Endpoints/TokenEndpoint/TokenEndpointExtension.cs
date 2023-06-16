using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SimpleLibraryApi.Models.Context;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SimpleLibraryApi.Endpoints.TokenEndpoint
{
    public static class TokenEndpointExtension
    {
        public static WebApplication MapTokenEndpoints(this WebApplication app)
        {
            app.MapPost("security/getToken", async (UserLoginData  userLoginData, [FromServices] ApiDbContext dbContext, [FromServices] IConfiguration configuration, CancellationToken cancellationToken) =>
            {
                var user = await dbContext.User.FirstOrDefaultAsync(x => x.Email == userLoginData.Email && x.Password == x.Password, cancellationToken);

                if (user is null)
                    return Results.Unauthorized();

                var issuer = configuration["Jwt:Issuer"];
                var audience = configuration["Jwt:Audience"];
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var key = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim("Id", user.UserId.ToString()),
                        new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    }),
                    Expires = DateTime.UtcNow.AddHours(6),
                    Audience = audience,
                    Issuer = issuer,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
                };

                if (user.IsAdmin)
                    tokenDescriptor.Subject.AddClaim(new Claim(ClaimTypes.Role, "admin"));

                var jwtTokenHandler = new JwtSecurityTokenHandler();
                var token = jwtTokenHandler.CreateToken(tokenDescriptor);
                var jwtToken = jwtTokenHandler.WriteToken(token);

                return Results.Ok(jwtToken);

            }).AllowAnonymous()
            .WithTags("Token");

            return app;
        }

        private record UserLoginData(string Email, string Password) { }
    }
}
