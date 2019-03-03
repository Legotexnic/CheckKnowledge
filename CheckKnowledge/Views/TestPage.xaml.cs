using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CheckKnowledge.Managers;
using CheckKnowledge.GameClasses;
using CheckKnowledge.ModelDb;

namespace CheckKnowledge.Views
{
    /// <summary>
    /// Interaction logic for TestPage.xaml
    /// </summary>
    public partial class TestPage : Page
    {
        MainWindow wnd;
        private AnswerManager answerManager = new AnswerManager();
        private QuestionManager questionManager = new QuestionManager();
        private ThemeManager themeManager = new ThemeManager();
        private StatisticManager statisticManager = new StatisticManager();
        private Game game;
        public TestPage(MainWindow wnd)
        {
            this.wnd = wnd;
            InitializeComponent();
            UpdateGameThemesCombobox();
            UserInfo.Content = "Студент " + ((User)Application.Current.Properties["User"]).FirstName 
                + " " + ((User)Application.Current.Properties["User"]).LastName;
        }

        private void UpdateGameThemesCombobox()
        {
            GameSelect_Theme.Items.Clear();
            var themes = themeManager.GetThemes();
            if (themes == null)
            {
                GameSelect_StartButton.IsEnabled = false;
                return;
            }
            else
                GameSelect_StartButton.IsEnabled = true;
            foreach (var theme in themes)
            {
                GameSelect_Theme.Items.Add(theme.Name);
            }
            GameSelect_Theme.SelectedIndex = 0;
        }

        private void GameSelect_StartButton_Click(object sender, RoutedEventArgs e)
        {
            var theme = themeManager.GetThemeByName(GameSelect_Theme.SelectedItem.ToString());
            game = new Game(questionManager, answerManager, theme);
            QuestionWindow.Visibility = Visibility.Visible;
            GameSelectWindow.Visibility = Visibility.Collapsed;
            VisibleQuestion();
        }

        private void VisibleQuestion()
        {
            var CurrentQuestion = game.GetCurrentQuestion();
            TextQuestion.Content = CurrentQuestion.Text;
            Answer0.Content = CurrentQuestion.Answers.ToArray()[0].Text;
            Answer1.Content = CurrentQuestion.Answers.ToArray()[1].Text;
            Answer2.Content = CurrentQuestion.Answers.ToArray()[2].Text;
            Answer3.Content = CurrentQuestion.Answers.ToArray()[3].Text;
        }

        private void AnswerButton_Click(object sender, RoutedEventArgs e)
        {
            string answer = ((Button)sender).Content.ToString();
            if (game.GetCurrentQuestion().RightAnswer(answer))
            {
                game.RightAnswer++;
            }
            game.CurrentQuestionNumber++;
            if (game.CurrentQuestionNumber < game.NuberOfQuestions)
                VisibleQuestion();
            else
                EndGame();
        }

        private void EndGame()
        {
            Statistic stat = new Statistic
            {
                UserId = ((User)Application.Current.Properties["User"]).Id,
                ThemeId = game.Theme.Id,
                RightAnswer = game.RightAnswer
            };
            try
            {
                statisticManager.Save(stat);
            }
            catch (Exception)
            {
                // TODO : add catch
            }
            QuestionWindow.Visibility = Visibility.Collapsed;
            EndWindow.Visibility = Visibility.Visible;
            ResultLabel.Content = "Верных ответов " + game.RightAnswer + " из " + game.NuberOfQuestions + "\n  Ваща оценка " + game.Result();
        }

        private void EndGameButton_Click(object sender, RoutedEventArgs e)
        {
            EndWindow.Visibility = Visibility.Collapsed;
            GameSelectWindow.Visibility = Visibility.Visible;
        }

        private void StatisticButton_Click(object sender, RoutedEventArgs e)
        {
            wnd.Content = new Views.MyStatisticPage(wnd);
        }

        private void ChangeUser_Button_Click(object sender, RoutedEventArgs e)
        {
            wnd.Content = new Views.AuthorizationPage(wnd);
        }
    }
}
