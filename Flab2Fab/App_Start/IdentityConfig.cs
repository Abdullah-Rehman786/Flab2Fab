using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Flab2Fab.Models;
using SendGrid;
using System.Configuration;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using Newtonsoft.Json.Linq;
using static System.Net.Mime.MediaTypeNames;
using SendGrid.Helpers.Mail;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using System.Drawing;

namespace Flab2Fab
{
    public class EmailService : IIdentityMessageService
    {

        public async Task SendAsync(IdentityMessage message)
        {
            await configSendGridasync(message);
        }

        private async Task configSendGridasync(IdentityMessage message)
        {
            var apiKey = ConfigurationManager.AppSettings.Get("EmailApi");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("abdullah@izazsolutions.com", "Flab2Fab");
            var subject = message.Subject;
            
            var to = new EmailAddress(message.Destination);
            var plainTextContent = message.Body;
            var htmlContent = message.Body;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);

            //var myMessage = new SendGrid.Helpers.Mail.SendGridMessage();
            //myMessage.AddTo(message.Destination);
            //myMessage.From = new SendGrid.Helpers.Mail.EmailAddress(
            //                    "abdullah@izazsolutions.com", "Flab2Fab");
            //myMessage.Subject = message.Subject;
            //myMessage.AddContent("text/html",message.Body);
            ////myMessage.Html = message.Body;

            //var credentials = new NetworkCredential(
            //           ConfigurationManager.AppSettings["emailServiceUserName"],
            //           ConfigurationManager.AppSettings["emailServicePassword"]
            //           );

            //// Create a Web transport for sending email.
            //var transportWeb = new Web(credentials);

            //// Send the email.
            //if (transportWeb != null)
            //{
            //    await transportWeb.DeliverAsync(myMessage);
            //}
            //else
            //{
            //    Trace.TraceError("Failed to create Web transport.");
            //    await Task.FromResult(0);
            //}
        }
    }

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            string accountSid = Environment.GetEnvironmentVariable("TWILIO_ACCOUNT_SID");
            string authToken = Environment.GetEnvironmentVariable("TWILIO_AUTH_TOKEN");
            string twilioNumber = Environment.GetEnvironmentVariable("TWILIO_NUMBER");

            TwilioClient.Init(accountSid, authToken);

            if (!message.Destination.StartsWith("+") && message.Destination.Length == 10)
            {
                message.Destination = "+1" + message.Destination;
            } else if (message.Destination.Length > 10)
            {
                message.Destination = "+1" + message.Destination.Substring(message.Destination.Length - 10);
            }

            var smsConfirmation = MessageResource.Create(
                body: message.Body,
                from: new Twilio.Types.PhoneNumber(twilioNumber),
                to: new Twilio.Types.PhoneNumber(message.Destination)
            );

            Console.WriteLine(smsConfirmation.Sid);

            // var Twilio = new TwilioRestClient(
            //ConfigurationManager.AppSettings["SMSSID"],
            //ConfigurationManager.AppSettings["SMSAuthToken"]
            //);

            // var result = Twilio.SendMessage(
            //     ConfigurationManager.AppSettings["SMSPhoneNumber"],
            //    message.Destination, message.Body);

            // // Status is one of Queued, Sending, Sent, Failed or null if the number is not valid
            // Trace.TraceInformation(result.Status);

            // Twilio doesn't currently have an async API, so return success.
            return Task.FromResult(0);
        }
    }

    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<ApplicationUser>
            {
                MessageFormat = "Your security code is {0}"
            });
            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<ApplicationUser>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }

    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager) :
            base(userManager, authenticationManager) { }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }
}
