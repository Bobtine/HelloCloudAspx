using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Request.IsAuthenticated)
        {
            HttpContext.Current.GetOwinContext().Authentication.Challenge(
                new Microsoft.Owin.Security.AuthenticationProperties { RedirectUri = "/" },
                Microsoft.Owin.Security.OpenIdConnect.OpenIdConnectAuthenticationDefaults.AuthenticationType);
            Response.StatusCode = 401;
            Response.End();
        }
    }
}