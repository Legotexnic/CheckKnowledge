using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CheckKnowledge.Managers;
using CheckKnowledge.ModelDb;
using CheckKnowledgeLibrary;


namespace CheckKnowledge.GameClasses
{
    public class Game
    {
        private QuestionManager _questionManager;
        private AnswerManager _answerManager;

        public int NuberOfQuestions { get; set; }
        IEnumerable<GameQuestion> Questions { get; set; }
        public int CurrentQuestionNumber { get; set; }
        public Theme Theme { get; set; }
        public int RightAnswer { get; set; }

        public Game(QuestionManager questionManager, AnswerManager answerManager, Theme theme)
        {
            _questionManager = questionManager;
            _answerManager = answerManager;
            Theme = theme;
            CurrentQuestionNumber = 0;
            RightAnswer = 0;
            this.GetQuestions();
        }

        private void GetQuestions()
        {
            Questions = _questionManager.GetQuestionsForGame(this.Theme).ToList();
            NuberOfQuestions = Questions.Count();
            foreach (var question in Questions)
            {
                question.Answers = RandomizeHelper.Shuffle(_answerManager.GetAnswersForQuestion(question.Id));
            }
            Questions = RandomizeHelper.Shuffle(Questions);
        }

        public GameQuestion GetCurrentQuestion()
        {
            return Questions.ToList()[CurrentQuestionNumber];
        }

        public string Result()
        {
            double result = (double)RightAnswer / (double)NuberOfQuestions;
            if (result < 0.5)
                return "2";
            else if (result < 0.7)
                return "3";
            else if (result < 0.9)
                return "4";
            else return "5";
        }
    }
}

