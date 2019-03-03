using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CheckKnowledge.ModelDb;
using CheckKnowledge.GameClasses;


namespace CheckKnowledge.Managers
{
    public class AnswerManager
    {
        private TestknowledgeEntities context = new TestknowledgeEntities();

        public AnswerManager() { }

        public void AddAnswers(IEnumerable<Answer> answers)
        {
            try
            {
                context.Answers.AddRange(answers);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<GameAnswer> GetAnswersForQuestion(int questionId)
        {
            return context.Answers.Where(answer => answer.QuestionId == questionId)
                .Select(a => new GameAnswer { Text = a.Text, IsTrue = a.IsTrue });
        }
    }
}
