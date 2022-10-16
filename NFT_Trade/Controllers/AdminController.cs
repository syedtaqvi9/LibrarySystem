using NFT_Trade.BL;
using NFT_Trade.helpingClasses;
using NFT_Trade.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NFT_Trade.Controllers
{
    public class AdminController : Controller
    {

        DatabaseEntities db = new DatabaseEntities();
        GeneralPurpose gp = new GeneralPurpose();
        private bool isLogedIn()
        {
            if (gp.validateUser() != null)
            {
                if (gp.validateUser().Role == 2)
                    return true;
                else
                    return false;
            }
            else
            {
                return false;
            }
        }
        public ActionResult Dashboard()
        {
            if (!isLogedIn())
            {
                return RedirectToAction("Login", "Auth");
            }
            return View();
        }
        #region ManageUsers
        public ActionResult AddUser(string msg = "", string color = "")
        {
            if (!isLogedIn())
            {
                return RedirectToAction("Login", "Auth");
            }
            else
            {
                ViewBag.msg = msg;
                ViewBag.color = color;
                return View();
            }
        }

        [HttpPost]
        public ActionResult PostAddUser(User newUser)
        {
            if ((!isLogedIn()))
            {
                return RedirectToAction("Login", "Auth");
            }
            if (newUser.Name == null || newUser.Email == null || newUser.Password == null || newUser.Address == null || newUser.Contact == null)
            {
                return RedirectToAction("AddUser", new { msg = "Fill all the fieldds first", color = "red" });
            }
            User u = new UserBL().GetActiveUsersList(db).Where(x => x.Email.ToLower() == newUser.Email.ToLower()).FirstOrDefault();
            if (u != null)
            {
                return RedirectToAction("AddUser", new { msg = "Email already exist", color = "red" });
            }
            newUser.Password = StringCypher.Encrypt(newUser.Password);
            newUser.IsActive = 1;
            newUser.Role = 3;
            newUser.CreatedAt = DateTime.Now;

            if (new UserBL().AddUser(newUser, db))
            {
                return RedirectToAction("AddUser", new { msg = "Record Added successfully", color = "green" });
            }
            return RedirectToAction("AddUser", new { msg = "Record connot be added to database", color = "red" });
        }
        public ActionResult ViewUser(string msg = "", string color = "black")
        {
            if (!isLogedIn())
            {
                return RedirectToAction("Login", "Auth");
            }

            ViewBag.msg = msg;
            ViewBag.color = color;
            return View();
        }
        [HttpPost]
        public ActionResult GetUserList(string name = "", string contact = "", string email = "")
        {
            List<User> ulist = new UserBL().GetActiveUsersList(db).Where(x => x.Role == 3).OrderByDescending(x => x.Id).ToList();
            if (name != "")
            {
                ulist = ulist.Where(x => x.Name.ToLower().Contains(name.ToLower())).ToList();
            }
            if (contact != "")
            {
                ulist = ulist.Where(x => x.Contact.ToLower().Contains(contact.ToLower())).ToList();
            }
            if (email != "")
            {
                ulist = ulist.Where(x => x.Email.ToLower().Contains(email.ToLower())).ToList();
            }
            int start = Convert.ToInt32(Request["start"]);
            int length = Convert.ToInt32(Request["length"]);
            string searchValue = Request["search[value]"];
            string sortColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
            string sortDirection = Request["order[0][dir]"];

            if (sortColumnName != "" && sortColumnName != null)
            {
                if (sortDirection == "asc")
                {
                    ulist = ulist.OrderByDescending(x => x.GetType().GetProperty(sortColumnName).GetValue(x)).ToList();
                }
                else
                {
                    ulist = ulist.OrderBy(x => x.GetType().GetProperty(sortColumnName).GetValue(x)).ToList();
                }
            }

            if (!string.IsNullOrEmpty(searchValue))
            {
                ulist = ulist.Where(x => x.Name.ToLower().Contains(searchValue.ToLower()) ||
                        x.Contact.ToLower().Contains(searchValue.ToLower()) ||
                        x.Email.ToLower().Contains(searchValue.ToLower()) ||
                        (x.Address != null && x.Address.ToLower().Contains(searchValue.ToLower()))).ToList();
            }
            int totalrows = ulist.Count();
            int totalrowsafterfilterinig = ulist.Count();
            ulist = ulist.Skip(start).Take(length).ToList();

            List<User> udto = new List<User>();
            foreach (User u in ulist)
            {
                User obj = new User()
                {
                    Id = u.Id,
                    Name = u.Name,
                    Contact = u.Contact,
                    Email = u.Email,
                    Address = u.Address
                };

                udto.Add(obj);
            }
            return Json(new { data = udto, draw = Request["draw"], recordsTotal = totalrows, recordsFiltered = totalrowsafterfilterinig }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult PostUpdateUser(User _user)
        {
            if (!isLogedIn())
            {
                return RedirectToAction("Login", "Auth");
            }
            User u = new UserBL().GetActiveUserById(_user.Id, db);
            u.Name = _user.Name.Trim();
            u.Contact = _user.Contact;
            u.Address = _user.Address;

            bool chkUser = new UserBL().UpdateUser(u, db);

            if (chkUser)
            {
                return RedirectToAction("ViewUser", "Admin", new { msg = "User updated successfully", color = "green" });
            }
            else
            {
                return RedirectToAction("ViewUser", "Admin", new { msg = "Somethings' wrong", color = "red" });
            }
        }
        [HttpPost]
        public ActionResult UserById(int id)
        {
            User user = new UserBL().GetActiveUserById(id, db);
            User obj = new User()
            {
                Id = user.Id,
                Name = user.Name,
                Contact = user.Contact,
                Address = user.Address
            };
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteUser(int id)
        {
            if (!isLogedIn())
            {
                return RedirectToAction("Login", "Auth");
            }
            if (new UserBL().DeleteUser(id, db)
)
            {
                return RedirectToAction("ViewUser", new { msg = "Record deleted successfully", color = "green" });
            }
            else
            {
                return RedirectToAction("ViewUser", new { msg = "Somethings' wrong", color = "red" });
            }
        }
        #endregion
    }
}