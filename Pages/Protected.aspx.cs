using System;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.UI;

namespace WebFormsEntraID.Pages
{
    public partial class Protected : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var user = HttpContext.Current.User as ClaimsPrincipal;
            string authorizedGroupId = "<GROUP_OBJECT_ID>";

            bool isAuthorized = user.Claims.Any(c => c.Type == "groups" && c.Value == authorizedGroupId);

            if (!isAuthorized)
            {
                Response.Redirect("AccessDenied.aspx");
            }
            else
            {
                lblMessage.Text = "Bienvenue, vous êtes autorisé à accéder à cette page.";
            }
        }
    }
}
