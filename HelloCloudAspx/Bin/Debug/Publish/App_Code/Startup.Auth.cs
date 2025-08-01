using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;
using System;
using System.Configuration;
using System.IdentityModel.Tokens;
using System.Threading.Tasks;

[assembly: OwinStartup(typeof(HelloCloudAspx.Startup))]

namespace HelloCloudAspx
{
    public partial class Startup {

        // Pour plus d'informations sur la configuration de l'authentification, visitez https://go.microsoft.com/fwlink/?LinkId=301883
        public void ConfigureAuth(IAppBuilder app)
        {

            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                CookieSecure = CookieSecureOption.Never,
                CookieHttpOnly = false
            });

            app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
            {
                ClientId = ConfigurationManager.AppSettings["ida:ClientId"],
                Authority = $"https://login.microsoftonline.com/{ConfigurationManager.AppSettings["ida:TenantId"]}/v2.0",
                RedirectUri = ConfigurationManager.AppSettings["ida:RedirectUri"],
                PostLogoutRedirectUri = ConfigurationManager.AppSettings["ida:PostLogoutRedirectUri"],

                ResponseType = "id_token",
                Scope = "openid profile",

                //ProtocolValidator = new OpenIdConnectProtocolValidator
                //{
                //    RequireNonce = false, // ✅ C’est ici qu’on désactive le contrôle du nonce,
                //},
                TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true
                },

                UseTokenLifetime = false,
                
                Notifications = new OpenIdConnectAuthenticationNotifications
                {
                    RedirectToIdentityProvider = context =>
                    {
                        
                        //if (string.IsNullOrEmpty(context.ProtocolMessage.Nonce))
                        //{
                        //    context.ProtocolMessage.Nonce = Guid.NewGuid().ToString("N");
                        //}
                        return Task.CompletedTask;
                    },
                    AuthenticationFailed = context =>
                    {
                        context.HandleResponse();
                        context.Response.Redirect("/Error.aspx?message=" + context.Exception.Message);
                        return Task.CompletedTask;
                    }
                }
            });


        }
    }
}
