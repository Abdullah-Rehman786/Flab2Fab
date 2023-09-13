using System;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Flab2Fab.Models;
using System.Linq;

namespace Flab2Fab.Account
{
    public partial class Register : Page
    {
        protected void CreateUser_Click(object sender, EventArgs e)
        {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var signInManager = Context.GetOwinContext().Get<ApplicationSignInManager>();
            var user = new ApplicationUser() { UserName = Email.Text, 
                Email = Email.Text, 
                Paid = Convert.ToBoolean(RadioButtonListOptions.SelectedValue), 
                Name = TxtName .Text, 
                PhoneNumber = TxtPhone.Text,
                Anonymous = Convert.ToBoolean(AnonymousButtonListOptions.SelectedValue),
                StartWeight = Convert.ToDecimal(TxtStartWeight.Text)
            };
            IdentityResult result = manager.Create(user, Password.Text);

            if(result.Succeeded)
            {
                signInManager.SignIn(user, isPersistent: false, rememberBrowser: false);
                IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
            } else { 
                    ErrorMessage.Text = result.Errors.FirstOrDefault();
            }
            //TODO revert changes before deployment
            //if (result.Succeeded)
            //{
            //    // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
            //    string code = manager.GenerateEmailConfirmationToken(user.Id);
            //    string callbackUrl = IdentityHelper.GetUserConfirmationRedirectUrl(code, user.Id, Request);
            //    manager.SendEmail(user.Id, "Confirm your Flab2Fab account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>.");

            //    if (user.EmailConfirmed)
            //    {
            //        signInManager.SignIn(user, isPersistent: false, rememberBrowser: false);
            //        IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
            //    }
            //    else
            //    {
            //        ErrorMessage.Text = "An email has been sent to your account. Please view the email and confirm your account to complete the registration process.";
            //    }

            //}
            //else
            //{
            //    ErrorMessage.Text = result.Errors.FirstOrDefault();
            //}

        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}