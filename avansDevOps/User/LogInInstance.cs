using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace avansDevOps.Users
{
    public class LogInInstance
    {
        public User? LoggedInUser { get; set; }
        private readonly UserRepo _userRepo;

        public LogInInstance(UserRepo repo)
        {
            _userRepo = repo;
        }

        public bool LogIn(string username, string password)
        {
            User? user = ComfrimCredentials(username, password);
            if (user == null)
                return false;
            LoggedInUser = user;
            return true;
        }

        public void LogOut()
        {
            LoggedInUser = null;
        }

        private User? ComfrimCredentials(string username, string password)
        {
            if (_userRepo.UserIsUnique(username))
                return null;
            User? user = _userRepo.GetUser(username);
            if (user!.Password == password)
                return user;
            return null;
        }

    }
}
