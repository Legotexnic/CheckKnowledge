using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CheckKnowledge.ModelDb;

namespace CheckKnowledge.Managers
{
    public class GroupManager
    {
        private TestknowledgeEntities context = new TestknowledgeEntities();

        public GroupManager() { }

        public void Save(Group group)
        {
            try
            {
                context.Groups.Add(group);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Group> GetGroups()
        {
            return context.Groups;
        }

        public Group GetGroupByName(string name)
        {
            return context.Groups.FirstOrDefault(group => group.Name == name);
        }
    }
}
