using NFT_Trade.Models;
using System.Collections.Generic;
using System.Linq;
namespace NFT_Trade.DAL
{
    public class UserDAL
    {
        public List<User> GetActiveUsersList(DatabaseEntities de)
        {
            return de.Users.Where(x => x.IsActive == 1).ToList();
        }
        public User GeteActiveUserById(int _Id, DatabaseEntities de)
        {
            return de.Users.Where(x => x.Id == _Id).FirstOrDefault(x => x.IsActive == 1);
        }
        public bool AddUser(User _user, DatabaseEntities de)
        {
            try
            {
                de.Users.Add(_user);
                de.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool UpdateUser(User _user, DatabaseEntities de)
        {
            try
            {
                de.Entry(_user).State = System.Data.Entity.EntityState.Modified;
                de.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool DeleteUser(int _Id, DatabaseEntities de)
        {
            try
            {
                User user = de.Users.Where(x => x.Id == _Id).FirstOrDefault(/*x => x.IsActive == 1*/);
                user.IsActive = 0;
                de.Entry(user).State = System.Data.Entity.EntityState.Modified;
                de.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}