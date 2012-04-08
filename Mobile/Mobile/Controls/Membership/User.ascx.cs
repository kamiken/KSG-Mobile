using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Mobile.Core.Web;
using Mobile.Business;

namespace Mobile.Controls.Membership
{
    public partial class User : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDataUser();
                BindCompanySearch();
            }
        }


        private void BindDataUser()
        {
            lvUsers.DataSource = AppGlobal.Business.BusinessUser.GetUsers("", null, null, null).ToList();
            lvUsers.DataBind();
        }

        private void BindCompanySearch()
        {
            ddlCompanyListSearch.DataSource = AppGlobal.Business.BusinessCompany.GetCompanies().ToList();
            ddlCompanyListSearch.DataBind();
        }

        private void BindCompanyUserForm()
        {
            ddlCompanyListUserForm.DataSource = AppGlobal.Business.BusinessCompany.GetCompanies().ToList();
            ddlCompanyListUserForm.DataBind();
        }


        #region "Support Function"
        protected void btnShowUserForm_Click(object sender, EventArgs e)
        {
            //if (ddlCompanyListUserForm.DataSource == null)
            //{
            //    BindCompanyUserForm();
            //}
            lblUserFormTitle.Text = ResxManager.GetResx("AddNewUser");
            upnUserForm.Update();
            mpeUserForm.Show();
            
        }

        protected void btnSaveUserForm_OnClick(object sender , EventArgs e)
        {
            
        }
        #endregion
    }
}