using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using issueTrack.Models;
using issueTrack.Models.AuthenticationAndAuthorization;
//System.Web.Mail use com , out of date
using System.Net.Mail;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace issueTrack.Controllers
{
    public class UserController : Controller
    {
        private testdbEntities db = new testdbEntities();


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLogginViewModel p)
        {
            if (ModelState.IsValid)
            {
                if (p.EmailOrPhone.Equals(Session["userid"]))
                {
                    //hacker attack
                    return Redirect("../");
                }

                SqlParameter emailParameter = new SqlParameter("FDEmailOrPhone", SqlDbType.VarChar, Common.Const.EmailOrPhoneLength);
                emailParameter.Value = p.EmailOrPhone;

                SqlParameter passwordParameter = new SqlParameter("FDPassword", SqlDbType.VarChar, Common.Const.PasswordLength_Max);
                passwordParameter.Value = p.Password;

                //int count = db.Database.ExecuteSqlCommand("select count(*) from TBUsers");
                TBUser user = db.Database.SqlQuery<TBUser>("select * from TBUsers where FDEmailOrPhone = @FDEmailOrPhone and FDPassword = HASHBYTES('SHA2_256',@FDPassword)", emailParameter, passwordParameter).FirstOrDefault();

                if (user == null)
                {
                    //Verification Code Error
                    ModelState.AddModelError("Password", "User ID or Password not correct");
                    return View(p);
                }
                else
                {
                    Session.Add("userid",p.EmailOrPhone);
                    Session.Add("nickname", user.FDNickname);
                }
            }
            return Redirect("../");
        }

        //// GET: User/Create
        //public ActionResult Completion()
        //{
        //    return View();
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Completion(UserInfoCompletionViewModel p)
        {
            if (ModelState.IsValid)
            {
                if (!p.EmailOrPhone.Equals(Session["userid"]))
                {
                    //hacker attack
                }

                SqlParameter emailParameter = new SqlParameter("FDEmailOrPhone", SqlDbType.VarChar, Common.Const.EmailOrPhoneLength);
                emailParameter.Value = p.EmailOrPhone;

                SqlParameter passwordParameter = new SqlParameter("FDPassword", SqlDbType.VarChar, Common.Const.PasswordLength_Max);
                passwordParameter.Value = p.Password;

                SqlParameter nicknameParameter = new SqlParameter("FDNickname", SqlDbType.NVarChar, Common.Const.NicknameLength_Max);
                nicknameParameter.Value = p.Nickname;

                int res = db.Database.ExecuteSqlCommand("update TBUsers set FDNickname = @FDNickname , FDPassword = HASHBYTES('SHA2_256',@FDPassword) ,FDUpdateTimestamp = SYSDATETIME() where FDEmailOrPhone = @FDEmailOrPhone", emailParameter, passwordParameter, nicknameParameter);
                if (res == 1)
                {
                    Session.Add("nickname", p.Nickname);
                }
                else
                {
                    //update database with 0 row affected , something get wrong
                }
                return Redirect("../");
            }
            return View(p);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Validate(UserRegistrationCodeVerificationViewModel validateForm)
        {
            if (ModelState.IsValid)
            {
                if (!validateForm.EmailOrPhone.Equals(Session["userid"]))
                {
                    //hacker attack
                }

                SqlParameter emailParameter = new SqlParameter("FDEmailOrPhone", SqlDbType.VarChar, Common.Const.EmailOrPhoneLength);
                emailParameter.Value = validateForm.EmailOrPhone;

                SqlParameter passwordParameter = new SqlParameter("FDPassword", SqlDbType.VarChar, Common.Const.VerificationCodeLength);
                passwordParameter.Value = validateForm.VerificationCode;

                //int count = db.Database.ExecuteSqlCommand("select count(*) from TBUsers");
                TBUser user = db.Database.SqlQuery<TBUser>("select * from TBUsers where FDEmailOrPhone = @FDEmailOrPhone and FDPassword = HASHBYTES('SHA2_256',@FDPassword)", emailParameter, passwordParameter).FirstOrDefault();

                if (user == null)
                {
                    //Verification Code Error
                    ModelState.AddModelError("VerificationCode", "Verification Code is wrong ");
                }
                else
                {
                    if (user.FDNickname.StartsWith(" "))
                    {
                        UserInfoCompletionViewModel p = new UserInfoCompletionViewModel();
                        p.EmailOrPhone = validateForm.EmailOrPhone;
                        return View("Completion", p);
                    }
                    else
                    {
                        //hacker attack or wrong user request
                        return Redirect("../");
                    }
                }
            }
            return View("RegistrationCodeVerification", validateForm);
        }




        //Request register page
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register([Bind(Include = "EmailOrPhone")] UserRegistrationViewModel registerForm)
        {
            if (ModelState.IsValid)
            {
                if (db.TBUsers.Any(r => r.FDEmailOrPhone == registerForm.EmailOrPhone))
                {
                    ModelState.AddModelError("EmailOrPhone", "Email address has been used");
                }
                else
                {
                    //db.TBUsers.Add(new TBUser { FDEmailOrPhone = registerForm.EmailOrPhone, FDNickname = " " });
                    //db.SaveChanges();
                    SqlParameter emailParameter = new SqlParameter("FDEmailOrPhone", SqlDbType.VarChar, Common.Const.EmailOrPhoneLength);
                    emailParameter.Value = registerForm.EmailOrPhone;

                    string verifyCode = Common.RandomGenerator.GenerateRandomString(Common.Const.VerificationCodeLength);
                    SqlParameter passwordParameter = new SqlParameter("FDPassword", SqlDbType.VarChar, Common.Const.VerificationCodeLength);
                    passwordParameter.Value = verifyCode;

                    db.Database.ExecuteSqlCommand("insert into TBUsers (FDEmailOrPhone,FDNickname,FDPassword) values (@FDEmailOrPhone,' ',HASHBYTES('SHA2_256',@FDPassword))", emailParameter, passwordParameter);
                    
                    string body = "<h2>{0}</h2>";
                    var message = new MailMessage();
                    message.To.Add(new MailAddress(registerForm.EmailOrPhone));  // replace with valid value 
                    message.From = new MailAddress("hujind@outlook.com");  // replace with valid value
                    message.Subject = "Registration Validation Code";
                    message.Body = String.Format(body, verifyCode);
                    message.IsBodyHtml = true;

                    using (var smtp = new System.Net.Mail.SmtpClient())
                    {
                        var credential = new NetworkCredential
                        {
                            UserName = "hujind@outlook.com",  // replace with valid value
                            Password = "csd584@ee"  // replace with valid value
                        };
                        smtp.Credentials = credential;
                        smtp.Host = "smtp-mail.outlook.com";
                        smtp.Port = 587;
                        smtp.EnableSsl = true;
                        //smtp.Send(message);
                        await smtp.SendMailAsync(message);
                        //return RedirectToAction("Sent");
                    }

                    Session.Add("userid", registerForm.EmailOrPhone);
                    UserRegistrationCodeVerificationViewModel p = new UserRegistrationCodeVerificationViewModel();
                    p.EmailOrPhone = Session["userid"].ToString();
                    return View("RegistrationCodeVerification", p);
                }
            }

            return View(registerForm);
        }

        //error handle
        protected override void OnException(ExceptionContext filterContext)
        {
            System.Diagnostics.Debug.WriteLine("UserController OnException executed ...");
            filterContext.ExceptionHandled = true;

            //Log the error!!
            //_Logger.Error(filterContext.Exception);

            //Redirect or return a view, but not both.
            //filterContext.Result = RedirectToAction("Index", "ErrorHandler");
            // OR 
            filterContext.Result = new ViewResult
            {
                ViewName = "~/Views/Error/BadError.cshtml"
            };
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
