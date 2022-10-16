using NFT_Trade.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Web;

namespace NFT_Trade.helpingClasses
{
    public class GeneralPurpose
    {
        DatabaseEntities db = new DatabaseEntities();
        public User validateUser()
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal; // Get the claims values
            var userId = identity.Claims.Where(c => c.Type == ClaimTypes.Sid).Select(c => c.Value).SingleOrDefault();
            int id = Convert.ToInt32(userId);
            User loggedInUser = db.Users.Where(x => x.Id == id).FirstOrDefault();
            return loggedInUser;
        }
    }
}