using System.Collections.Generic;
using System.Linq;

namespace University.Models
{
    public class UserDAO
    {
        private readonly UniversityManagementContext _context;

        public UserDAO(UniversityManagementContext context)
        {
            _context = context;
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public User GetUserByUsername(string username)
        {
            return _context.Users.FirstOrDefault(u => u.Username == username);
        }
        public User GetUserById(int userId)
        {
            return _context.Users.FirstOrDefault(u => u.UserId == userId);
        }
    }
}
