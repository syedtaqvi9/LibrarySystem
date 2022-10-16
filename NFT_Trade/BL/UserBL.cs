using NFT_Trade.DAL;
using NFT_Trade.Models;
using System.Collections.Generic;
namespace NFT_Trade.BL
{
    public class UserBL
    {
        public List<User> GetActiveUsersList(DatabaseEntities de)
        {
            return new UserDAL().GetActiveUsersList(de);
        }

        public User GetActiveUserById(int _Id, DatabaseEntities de)
        {
            return new UserDAL().GeteActiveUserById(_Id, de);
        }

        public bool AddUser(User _user, DatabaseEntities de)
        {
            if (_user.Name == "" || _user.Contact == "" || _user.Email == "" || _user.Password == "" || _user.Name == null || _user.Contact == null || _user.Email == null || _user.Password == null)
            {
                return false;
            }
            else
            {
                return new UserDAL().AddUser(_user, de);
            }
        }

        public bool UpdateUser(User _user, DatabaseEntities de)
        {
            if (_user.Name == "" || _user.Contact == "" || _user.Role == null || _user.Email == null)
            {
                return false;
            }
            else
            {
                return new UserDAL().UpdateUser(_user, de);
            }
        }

        public bool DeleteUser(int _id, DatabaseEntities de)
        {
            return new UserDAL().DeleteUser(_id, de);
        }
    }
}