using NFT_Trade.helpingClasses;
using NFT_Trade.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NFT_Trade.Controllers
{
    public class UserController : Controller
    {
        DatabaseEntities db = new DatabaseEntities();
        GeneralPurpose gp = new GeneralPurpose();
        private bool isLogedIn()
        {
            if (gp.validateUser() != null)
            {
                if (gp.validateUser().Role == 3)
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
                return RedirectToAction("Login", "Auth");
            return View();
        }
    }
}