using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Mobile.Core.Web;
using Mobile.Core.Security;

namespace Mobile.Controls.Account
{
    public partial class Login : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadResx();
            }
        }

        protected void btnLogin_Click(object sender , EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();
            Mobile.Core.Security.LoginStatus status;
            var objUser = MobileAuthentication.GetUser(email, password, out status);
            if (status == Core.Security.LoginStatus.LoginSuccess)
            {
                var user = new User() { 
                    CompanyId = objUser.CompanyID,
                    Email = objUser.Email,
                    FullName = objUser.FullName,
                    UserId = objUser.UserID
                };
                MobileAuthentication.SetAuthentication(user);
            }
            
        }

        private void LoadResx()
        {
            btnLogin.Text = ResxManager.GetResx("Login");
        }
    }
}