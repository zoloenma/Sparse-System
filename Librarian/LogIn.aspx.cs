using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sparse.Librarian
{
    public partial class LogIn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void SubmitBtn_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) { return; }
            bool result = AuthenticateUser(EmailTxt.Text, PasswordTxt.Text);
            if (result == true) Response.Redirect("~/Librarian/Librarian.aspx");
            else loginMessageDiv.Visible = true;
        }
        public bool AuthenticateUser(string email, string password)
        {
            if (email == "lib@email.com" & password == "asd") return true;//dummy
            else return false;
        }
    }
}