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
using CheckKnowledge.ModelDb;

namespace CheckKnowledge.Views
{
    /// <summary>
    /// Interaction logic for AddTestPage.xaml
    /// </summary>
    public partial class AddTestPage : Page
    {
        MainWindow wnd;
        private ThemeManager themeManager = new ThemeManager();
        private QuestionManager questionManager = new QuestionManager();
        private AnswerManager answerManager = new AnswerManager();
        public AddTestPage(MainWindow wnd)
        {
            this.wnd = wnd;
            InitializeComponent();
            UpdateThemesCombobox();
            UserInfo.Content = "Учитель";
        }

        private void AddTheme_Button_Click(object sender, RoutedEventArgs e)
        {
            if (AddTheme_Name.Text == "")
            {
                AddTheme_Name.Background = Brushes.Red;
                return;
            }
            else
                AddTheme_Name.Background = Brushes.White;
            var theme = new Theme
            {
                Name = AddTheme_Name.Text,
                Desciption = AddTheme_Description.Text
            };
            try
            {
                themeManager.AddTheme(theme);
                UpdateThemesCombobox();
                ClearThemeForm();
            }
            catch (Exception)
            {
                // TODO : add catch
            }
        }

        private void AddQuestion_Button_Click(object sender, RoutedEventArgs e)
        {
            if (AddQuestion_Text.Text == "")
            {
                AddQuestion_Text.Background = Brushes.Red;
                return;
            }
            else
                AddQuestion_Text.Background = Brushes.White;
            if (AddQuestion_Answer_1.Text == "")
            {
                AddQuestion_Answer_1.Background = Brushes.Red;
                return;
            }
            else
                AddQuestion_Answer_1.Background = Brushes.White;
            if (AddQuestion_Answer_2.Text == "")
            {
                AddQuestion_Answer_2.Background = Brushes.Red;
                return;
            }
            else
                AddQuestion_Answer_2.Background = Brushes.White;
            if (AddQuestion_Answer_3.Text == "")
            {
                AddQuestion_Answer_3.Background = Brushes.Red;
                return;
            }
            else
                AddQuestion_Answer_3.Background = Brushes.White;
            if (AddQuestion_Answer_4.Text == "")
            {
                AddQuestion_Answer_4.Background = Brushes.Red;
                return;
            }
            else
                AddQuestion_Answer_4.Background = Brushes.White;
            if (AddQuestion_Theme.SelectedIndex < 0)
            {
                ThemeSelect_Error.Visibility = Visibility.Visible;
                return;
            }
            else
                ThemeSelect_Error.Visibility = Visibility.Collapsed;

            var question = new Question
            {
                Text = AddQuestion_Text.Text,
                ThemeId = themeManager.GetThemeByName(AddQuestion_Theme.SelectedItem.ToString()).Id
            };
            try
            {
                var questionId = questionManager.AddQuestion(question);
                AddAnswers(questionId);
                ClearQuestionForm();
            }
            catch (Exception)
            {
                // TODO : add catch
            }
        }

        private void StatisticButton_Click(object sender, RoutedEventArgs e)
        {
            wnd.Content = new Views.FullStatisticPage(wnd);
        }

        private void ChangeUser_Button_Click(object sender, RoutedEventArgs e)
        {
            wnd.Content = new Views.AuthorizationPage(wnd);
        }

        private void UpdateThemesCombobox()
        {
            AddQuestion_Theme.Items.Clear();
            var themes = themeManager.GetThemes();
            foreach (var theme in themes)
            {
                AddQuestion_Theme.Items.Add(theme.Name);
            }
        }

        private void AddAnswers(int questionId)
        {
            var answerList = new List<Answer>();
            answerList.Add(new Answer
            {
                IsTrue = true,
                QuestionId = questionId,
                Text = AddQuestion_Answer_1.Text,
            });
            answerList.Add(new Answer
            {
                IsTrue = false,
                QuestionId = questionId,
                Text = AddQuestion_Answer_2.Text,
            });
            answerList.Add(new Answer
            {
                IsTrue = false,
                QuestionId = questionId,
                Text = AddQuestion_Answer_3.Text,
            });
            answerList.Add(new Answer
            {
                IsTrue = false,
                QuestionId = questionId,
                Text = AddQuestion_Answer_4.Text,
            });

            try
            {
                answerManager.AddAnswers(answerList);
            }
            catch (Exception)
            {
                // TODO : add catch
            }
        }

        private void ClearThemeForm()
        {
            AddTheme_Name.Text = string.Empty;
            AddTheme_Description.Text = string.Empty;
        }

        private void ClearQuestionForm()
        {
            AddQuestion_Text.Text = string.Empty;
            AddQuestion_Answer_1.Text = string.Empty;
            AddQuestion_Answer_2.Text = string.Empty;
            AddQuestion_Answer_3.Text = string.Empty;
            AddQuestion_Answer_4.Text = string.Empty;
        }
    }
}
