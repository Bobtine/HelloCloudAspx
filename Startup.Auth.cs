using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Owin;
using Microsoft.Owin.Security;

[assembly: OwinStartup(typeof(WebFormsEntraID.Startup))]

namespace WebFormsEntraID
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.SetDefaultSignInAsAuthenticationType("Cookies");

            app.UseCookieAuthentication(new CookieAuthenticationOptions());
             
            app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
            {
                ClientId = "8218f4dc-8583-45b0-a3aa-8750a9f555d9",
                Authority = "https://login.microsoftonline.com/8d768217-7ee4-4314-802d-3d66f76194db/v2.0",
                RedirectUri = "https://webforms-entraid-demo.azurewebsites.net/signin-oidc",
                ResponseType = "id_token",
                Scope = "openid profile email",

                TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    RoleClaimType = "groups",
                    NameClaimType = "name"
                },

                Notifications = new OpenIdConnectAuthenticationNotifications
                {
                    SecurityTokenValidated = context => Task.CompletedTask,
                    AuthenticationFailed = context =>
                    {
                        context.HandleResponse();
                        context.Response.Redirect("/Pages/AccessDenied.aspx");
                        return Task.CompletedTask;
                    }
                }
            });
        }
    }
}
