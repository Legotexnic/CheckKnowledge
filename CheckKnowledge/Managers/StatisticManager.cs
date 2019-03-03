using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CheckKnowledge.ModelDb;

namespace CheckKnowledge.Managers
{

    public class StatisticManager
    {
        private TestknowledgeEntities context = new TestknowledgeEntities();

        public StatisticManager() { }

        public void Save(Statistic stat)
        {
            try
            {
                context.Statistics.Add(stat);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Statistic> GetStatus(Theme theme, User user)
        {
            if (theme == null && user == null)
                return context.Statistics;
            else
            if (theme == null)
                return context.Statistics.Where(stat => stat.UserId == user.Id);
            else
            if (user == null)
                return context.Statistics.Where(stat => stat.ThemeId == theme.Id);
            else
                return context.Statistics.Where(stat => stat.ThemeId == theme.Id && stat.UserId == user.Id); ;
        }
    }
}
