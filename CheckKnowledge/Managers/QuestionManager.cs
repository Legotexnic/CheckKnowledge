using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CheckKnowledge.ModelDb;
using CheckKnowledge.GameClasses;

namespace CheckKnowledge.Managers
{
    public class QuestionManager
    {
        private TestknowledgeEntities context = new TestknowledgeEntities();

        public QuestionManager() { }

        public int AddQuestion(Question question)
        {
            try
            {
                context.Questions.Add(question);
                context.SaveChanges();
                return context.Questions.FirstOrDefault(q => q.Text == question.Text
                && q.ThemeId == question.ThemeId).Id;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<GameQuestion> GetQuestionsForGame(Theme theme)
        {
            return context.Questions.Where(question => question.ThemeId == theme.Id)
                .Select(q => new GameQuestion { Id = q.Id, Text = q.Text, });
        }
    }
}
