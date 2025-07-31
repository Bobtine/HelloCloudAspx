using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System.Threading.Tasks;
using Microsoft.Owin.Security.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;


namespace HelloCloudAspx
{
    public partial class Startup {

        // Pour plus d'informations sur la configuration de l'authentification, visitez https://go.microsoft.com/fwlink/?LinkId=301883
        public void ConfigureAuth(IAppBuilder app)
        {
            // Laisser l'application utiliser un cookie pour stocker les informations de l'utilisateur connecté
            // et stocke aussi les informations sur la connexion d’un utilisateur avec un fournisseur de connexion tiers.
            // Ceci est obligatoire si votre application autorise les utilisateurs à se connecter
            //app.UseCookieAuthentication(new CookieAuthenticationOptions
            //{
            //    AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
            //    LoginPath = new PathString("/Account/Login")
            //});
            //app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);


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
        // Décommenter les lignes suivantes pour activer la connexion avec des fournisseurs de connexion tiers
        //app.UseMicrosoftAccountAuthentication(
        //    clientId: "",
        //    clientSecret: "");

        //app.UseTwitterAuthentication(
        //   consumerKey: "",
        //   consumerSecret: "");

        //app.UseFacebookAuthentication(
        //   appId: "",
        //   appSecret: "");

        //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
        //{
        //    ClientId = "",
        //    ClientSecret = ""
        //});
    }
    }
}
