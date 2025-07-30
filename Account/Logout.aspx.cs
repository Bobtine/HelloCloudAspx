using System;
using System.Web;
using System.Web.UI;
using Microsoft.Owin.Security;
namespace WebFormsEntraID.Account
{
    public partial class Logout : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Web.HttpContext.Current.GetOwinContext().Authentication.SignOut();
            Response.Redirect("../Pages/Default.aspx");
        }
    }
}
