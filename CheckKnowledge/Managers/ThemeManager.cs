using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CheckKnowledge.ModelDb;

namespace CheckKnowledge.Managers
{
    class ThemeManager
    {
        private TestknowledgeEntities context = new TestknowledgeEntities();

        public ThemeManager() { }

        public void AddTheme(Theme theme)
        {
            try
            {
                context.Themes.Add(theme);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Theme> GetThemes()
        {
            return context.Themes;
        }

        public Theme GetThemeByName(string name)
        {
            return context.Themes.FirstOrDefault(theme => theme.Name == name);
        }
    }
}
