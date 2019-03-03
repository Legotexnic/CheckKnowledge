using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckKnowledge.GameClasses
{
    public class GameQuestion
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public IEnumerable<GameAnswer> Answers { get; set; }

        public bool RightAnswer(string Answer)
        {
            foreach (var answer in Answers)
            {
                if (answer.Text == Answer)
                    return answer.IsTrue;
            }
            return false;
        }
    }
}
