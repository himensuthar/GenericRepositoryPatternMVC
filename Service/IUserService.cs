using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Model;

namespace Service
{
    public interface IUserService
    {
        User GetUserDetails(User user);
    }

    public class UserService : IUserService
    {
        List<User> users = new List<User>() {
            new User {username="himen",password="himen@123" },
            new User {username="priha",password="priha@123" },
            new User {username="heer",password="heer@123" },
        };
        public User GetUserDetails(User user) {

            return users.Where(_ => _.username.ToLower() == user.username.ToLower() &&
            _.password.ToLower() == user.password.ToLower()).FirstOrDefault();

        }
    }
}
