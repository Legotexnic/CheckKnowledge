using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CheckKnowledge.ModelDb;

namespace CheckKnowledge.Managers
{
    public class UserManager
    {
        private TestknowledgeEntities context = new TestknowledgeEntities();

        public UserManager() { }

        public void Save(User user)
        {
            try
            {
                context.Users.Add(user);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public User Login(User user)
        {
            return context.Users.FirstOrDefault(u => u.Login == user.Login && u.Password == user.Password);
        }

        public IEnumerable<User> GetUsers()
        {
            return context.Users;
        }

        public User GetUserByName(string name)
        {
            return context.Users.FirstOrDefault(user => (user.FirstName + " " + user.LastName) == name);
        }

        public User GetUserByLogin(string login)
        {
            return context.Users.FirstOrDefault(user => user.Login == login);
        }
    }
}
