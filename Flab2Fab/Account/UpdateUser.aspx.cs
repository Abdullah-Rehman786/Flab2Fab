using Flab2Fab.App_Data;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Flab2Fab.Account
{
    public partial class UpdateUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                IUserDataAccess _userDataAccess = new UserDataAccess();
                DataTable dt = _userDataAccess.GetUpdateableUserInfo(User.Identity.GetUserId());
                txtName.Text = dt.Rows[0]["Name"].ToString();
                txtPhone.Text = dt.Rows[0]["PhoneNumber"].ToString();
                chkAnonymous.Checked = Convert.ToBoolean(dt.Rows[0]["Anonymous"]);
                chkPaid.Checked = Convert.ToBoolean(dt.Rows[0]["Paid"]);
            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            IUserDataAccess _userDataAccess = new UserDataAccess();
            DataTable dt = _userDataAccess.UpdateUserInfo(User.Identity.GetUserId(), txtName.Text, txtPhone.Text, chkAnonymous.Checked, chkPaid.Checked);
        }
    }
}