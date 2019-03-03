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
    /// Interaction logic for MyStatisticPage.xaml
    /// </summary>
    public partial class MyStatisticPage : Page
    {
        MainWindow wnd;
        private ThemeManager themeManager = new ThemeManager();
        private StatisticManager statisticManager = new StatisticManager();
        public MyStatisticPage(MainWindow wnd)
        {
            this.wnd = wnd;
            InitializeComponent();
            UpdateStatusThemesCombobox();
            UserInfo.Content = "Студент " + ((User)Application.Current.Properties["User"]).FirstName
                + " " + ((User)Application.Current.Properties["User"]).LastName;
        }

        private void UpdateStatusThemesCombobox()
        {
            NameTest.Items.Clear();
            var themes = themeManager.GetThemes();
            NameTest.Items.Add("ВСЕ");
            NameTest.SelectedIndex = 0;
            foreach (var theme in themes)
            {
                NameTest.Items.Add(theme.Name);
            }
        }

        private void ChooseButton_Click(object sender, RoutedEventArgs e)
        {
            Theme theme = null;
            if (NameTest.SelectedItem.ToString() != "ВСЕ")
                theme = themeManager.GetThemeByName(NameTest.SelectedItem.ToString());
            IEnumerable<Statistic> stat = statisticManager.GetStatus(theme, (User)Application.Current.Properties["User"]);
            var dg = stat.Select(st => new { st.Theme.Name, st.User.LastName, st.User.FirstName, st.RightAnswer, st.Theme.Questions.Count });
            DataGrid.ItemsSource = dg.ToList();
        }

        private void AddTestButton_Click(object sender, RoutedEventArgs e)
        {
            wnd.Content = new Views.TestPage(wnd);
        }

        private void ChangeUser_Button_Click(object sender, RoutedEventArgs e)
        {
            wnd.Content = new Views.AuthorizationPage(wnd);
        }
    }
}
