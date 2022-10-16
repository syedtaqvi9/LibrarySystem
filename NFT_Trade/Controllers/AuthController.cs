using NFT_Trade.BL;
using NFT_Trade.helpingClasses;
using NFT_Trade.Models;
using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Web;
using System.Web.Mvc;
namespace NFT_Trade.Controllers
{
    public class AuthController : Controller
    {
        DatabaseEntities db = new DatabaseEntities();
        GeneralPurpose gp = new GeneralPurpose();
        private bool isLogedIn()
        {
            if (gp.validateUser() != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public ActionResult Login(string msg = "", string color = "")
        {
            if (gp.validateUser() != null)
            {
                if (gp.validateUser().Role == 1)
                {
                    ViewBag.noOfUsers = new UserBL().GetActiveUsersList(db).ToList().Count;
                    return RedirectToAction("Dashboard", "SuperAdmin");
                }
                else
                if (gp.validateUser().Role == 2)
                {
                    return RedirectToAction("Dashboard", "Admin");
                }
                else
                {

                    return RedirectToAction("Dashboard", "User");
                }
            }
            int usercount = new UserBL().GetActiveUsersList(db).Where(x => x.Role == 1).Count();
            if (usercount == 0)
            {
                User u = new User()
                {
                    Name = "Uzair Aslam",
                    Email = "uzair.aslam02@gmail.com",
                    Password = StringCypher.Encrypt("123"),
                    Contact = "0000-0000000",
                    Address = "abc",
                    Role = 1,
                    CreatedAt = DateTime.Now,
                    IsActive =1
                };
            }
            ViewBag.message = msg;
            ViewBag.color = color;
            return View();
        }
        public ActionResult PostLogin(string Email = "", string Password = "")
        {
            User user = new UserBL().GetActiveUsersList(db).Where(x => x.Email.Trim().ToLower() == Email.Trim().ToLower() && StringCypher.Decrypt(x.Password).Equals(Password)).FirstOrDefault();
            if (user == null)
            {
                return RedirectToAction("Login", new { msg = "Incorrect Email/Password!", color = "red" });
            }
            var identity = new ClaimsIdentity(new[]
            {
                //new Claim(ClaimTypes.Name,user.FName),
                //new Claim(ClaimTypes.Surname,user.LName),
                new Claim(ClaimTypes.Sid,user.Id.ToString()),
                new Claim("UserName", user.Name),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim("Role", user.Role.ToString())
            }, "ApplicationCookie");
            var claimsPrincipal = new ClaimsPrincipal(identity); // Set current principal
            Thread.CurrentPrincipal = claimsPrincipal;
            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;
            authManager.SignIn(identity);
            if (user.Role == 1)
            {
                return RedirectToAction("Dashboard", "SuperAdmin");
            }
            else if (user.Role == 2)
            {
                return RedirectToAction("Dashboard", "Admin");
            }
            else
            {
                return RedirectToAction("Dashboard", "User");
            }
        }

        #region Signup

        public ActionResult Register(string msg = "", string color = "black")
        {
            ViewBag.Message = msg;
            ViewBag.Color = color;

            return View();
        }

        [HttpPost]
        public ActionResult PostRegister(User _user, string _confirmPassword = "")
        {
            if (_user.Password != _confirmPassword)
            {
                return RedirectToAction("Register", "Auth", new { msg = "Password and confirm password didn't match", color = "red" });
            }

            User user = new UserBL().GetActiveUsersList(db).Where(x => x.Email == _user.Email).FirstOrDefault();
            if (user != null)
            {
                return RedirectToAction("Register", "Auth", new { msg = "Email already exists. Try sign in!", color = "red" });
            }

            User u = new User()
            {
                Name = _user.Name.Trim(),
                Contact = _user.Contact,
                Email = _user.Email.Trim(),
                Password = StringCypher.Encrypt(_user.Password),
                Role = 3,
                IsActive = 1,
                CreatedAt = DateTime.Now
            };

            bool chkUser = new UserBL().AddUser(u, db);

            if (chkUser)
            {
                return RedirectToAction("Login", "Auth", new { msg = "Account created successfully, Please login", color = "green" });
            }
            else
            {
                return RedirectToAction("Register", "Auth", new { msg = "Somethings' wrong", color = "red" });
            }
        }

        #endregion

        public ActionResult UpdatePassword(string msg = "", string color = "")
        {
            if (!isLogedIn())
            {
                return RedirectToAction("Login", "Auth");
            }
            ViewBag.message = msg;
            ViewBag.color = color;
            return View();
        }
        [HttpPost]
        public ActionResult PostUpdatePassword(string oldPassword, string newPassword, string confirmNewPassword)
        {
            if (!isLogedIn())
            {
                return RedirectToAction("Login", "Auth");
            }
            if (newPassword != confirmNewPassword)
            {
                return RedirectToAction("UpdatePassword", new { msg = "Old password and New password are not same", color = "red" });
            }
            User user = new UserBL().GetActiveUsersList(db).Where(x => x.Email == gp.validateUser().Email).FirstOrDefault();
            if (oldPassword != StringCypher.Decrypt(user.Password))
            {
                return RedirectToAction("UpdatePassword", new { msg = "Wrong Old Password", color = "red" });
            }
            user.Password = StringCypher.Encrypt(newPassword);
            if (new UserBL().UpdateUser(user, db))
            {
                return RedirectToAction("UpdatePassword", new { msg = "Password Updated Successfully", color = "green" });
            }
            return RedirectToAction("UpdatePassword", new { msg = "Error while updating password", color = "red" });
        }
        #region ForgotPassword
        public ActionResult ForgotPassword(string msg = "", string color = "black")
        {
            ViewBag.Color = color;
            ViewBag.Message = msg;

            return View();
        }
        [HttpPost]
        public ActionResult PostForgotPassword(string Email = "")
        {
            User user = new UserBL().GetActiveUsersList(db).Where(x => x.Email == Email).FirstOrDefault();

            if (user != null)
            {
                string BaseUrl = string.Format("{0}://{1}{2}", HttpContext.Request.Url.Scheme, HttpContext.Request.Url.Authority, "/");

                bool checkMail = MailSender.SendForgotPasswordEmail(Email, BaseUrl);

                if (checkMail == true)
                {
                    return RedirectToAction("Login", "Auth", new { msg = "Please check your inbox/spam", color = "green" });
                }
                else
                {
                    return RedirectToAction("ForgotPassword", "Auth", new { msg = "Mail sending fail!", color = "red" });
                }
            }
            else
            {
                return RedirectToAction("ForgotPassword", "Auth", new { msg = "Email does not belong to our record!!", color = "red" });
            }
        }
        public ActionResult ResetPassword(string email = "", string time = "", string msg = "", string color = "black")
        {
            var passDate = DateTime.Now;
            var currDate = DateTime.Now;
            if (time.Length > 0)
            {
                passDate = DateTime.ParseExact(time, "dd/M/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                currDate = DateTime.ParseExact(DateTime.Now.Date.ToString(), "dd/M/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
            }
            if (passDate != currDate)
            {
                return RedirectToAction("Login", "Auth", new { msg = "Link expired, Please try again!", color = "red" });
            }
            ViewBag.Time = time;
            ViewBag.Email = email;
            ViewBag.Message = msg;
            ViewBag.Color = color;
            return View();
        }
        [HttpPost]
        public ActionResult PostResetPassword(string Email = "", string Time = "", string NewPassword = "", string ConfirmPassword = "")
        {
            if (NewPassword != ConfirmPassword)
            {
                return RedirectToAction("ResetPassword", "Auth", new { email = Email, time = Time, msg = "Password and confirm password did not match", color = "red" });
            }
            string DecryptEmail = StringCypher.Base64Decode(Email);
            DatabaseEntities de = new DatabaseEntities();
            User user = new UserBL().GetActiveUsersList(db).Where(x => x.Email == StringCypher.Base64Decode(Email)).FirstOrDefault();
            user.Password = StringCypher.Encrypt(NewPassword);
            bool check = false;
            try
            {
                new UserBL().UpdateUser(user, db);
                check = true;
            }
            catch
            {
                check = false;
            }
            if (check == true)
            {
                return RedirectToAction("Login", "Auth", new { msg = "Password reset successful, Try login", color = "green" });
            }
            else
            {
                return RedirectToAction("ResetPassword", "Auth", new { email = Email, time = Time, msg = "Somethings' wrong!", color = "red" });
            }
        }
        #endregion
        public ActionResult Logout()
        {
            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;
            authManager.SignOut("ApplicationCookie");
            return RedirectToAction("Login");
        }
        [HttpGet]
        public ActionResult Signup(string message, string color)
        {
            ViewBag.message = message;
            ViewBag.color = color;
            return View();
        }
        [HttpPost]
        public ActionResult PostSignup(User user)
        {
            if (!isLogedIn())
            {
                return RedirectToAction("Login");
            }
            if (new UserBL().GetActiveUsersList(db).Where(x => x.Email == user.Email).FirstOrDefault() != null)
            {
                ViewBag.user = user;
                return RedirectToAction("Signup", new { message = "Email has already been taken", color = "red" });
            }
            else
            {
                user.Password = StringCypher.Encrypt(user.Password);
                user.IsActive = 1;
                if (new UserBL().AddUser(user, db))
                {
                    return RedirectToAction("Signup", new { message = "Email has already been taken", color = "red" });
                }
                else
                {
                    return RedirectToAction("Signup", new { message = "Internal server error!", color = "red" });
                }
            }
        }
        public ActionResult EditProfile(string message = "", string color = "")
        {
            if (!isLogedIn())
            {
                return RedirectToAction("Login", "Auth");
            }
            User u = new UserBL().GetActiveUsersList(db).Where(x => x.Email.ToLower() == gp.validateUser().Email.ToLower()).FirstOrDefault();
            ViewBag.User = u;
            ViewBag.message = message;
            ViewBag.color = color;
            return View();
        }
        [HttpPost]
        public ActionResult PostEditProfile(User newData)
        {
            if (!isLogedIn())
            {
                return RedirectToAction("Login", "Auth");
            }
            User user = new UserBL().GetActiveUsersList(db).Where(x => x.Id == newData.Id).FirstOrDefault();
            if (newData.Name == user.Name && newData.Email == user.Email && newData.Contact == user.Contact && newData.Address == user.Address)
            {
                return RedirectToAction("EditProfile", new { message = "No change to update!", color = "red" });
            }
            if (new UserBL().GetActiveUsersList(db).Where(x => x.Email == newData.Email && x.Id != newData.Id).FirstOrDefault() != null)
            {
                return RedirectToAction("EditProfile", new { message = "Email has already been taken!", color = "red" });
            }
            user.Email = newData.Email;
            user.Name = newData.Name;
            user.Contact = newData.Contact;
            user.Address = newData.Address;
            if (new UserBL().UpdateUser(user, db))
            {
                return RedirectToAction("EditProfile", new { message = "Profile Updated Successfully!", color = "green" });
            }
            return RedirectToAction("EditProfile", new { message = "Server Error", color = "red" });
        }
    }
}