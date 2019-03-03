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
using CheckKnowledge.ModelDb;
using CheckKnowledge.Managers;

namespace CheckKnowledge.Views
{
    /// <summary>
    /// Interaction logic for RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        MainWindow wnd;
        private GroupManager groupManager = new GroupManager();
        private UserManager userManager = new UserManager();
        public RegistrationPage(MainWindow wnd)
        {
            this.wnd = wnd;
            InitializeComponent();
        }

        private void Registration_Complete_Click(object sender, RoutedEventArgs e)
        {
           
            if (Registration_FirstName.Text == "")
            {
                FirstName_Error.Visibility = Visibility.Visible;
                return;
            }
            else
                FirstName_Error.Visibility = Visibility.Collapsed;
            if (Registration_LastName.Text == "")
            {
                LastName_Error.Visibility = Visibility.Visible;
                return;
            }
            else
                LastName_Error.Visibility = Visibility.Collapsed;
            if (Registration_Login.Text == "")
            {
                Login_Error.Visibility = Visibility.Visible;
                Login_Error.Content = "Введите Логин";
                return;
            }
            else
                Login_Error.Visibility = Visibility.Collapsed;
            if (userManager.GetUserByLogin(Registration_Login.Text.ToString()) != null)
            {
                Login_Error.Visibility = Visibility.Visible;
                Login_Error.Content = "Логин Занят";
                return;
            }
            else
                Login_Error.Visibility = Visibility.Collapsed;
            if (Registration_Password.Text == "")
            {
                Password_Error.Visibility = Visibility.Visible;
                return;
            }
            else
                Password_Error.Visibility = Visibility.Collapsed;
            User user = new User
            {
                FirstName = Registration_FirstName.Text,
                LastName = Registration_LastName.Text,
                GroupId = 1,
                Login = Registration_Login.Text,
                Password = Registration_Password.Text
            };
            try
            {
                userManager.Save(user);
            }
            catch (Exception)
            {
                // TODO : add catch
            }
            Application.Current.Properties["User"] = user;
            wnd.Content = new Views.TestPage(wnd);
        }

        private void Registration_Cancel_Click(object sender, RoutedEventArgs e)
        {
            wnd.Content = new Views.AuthorizationPage(wnd);
        }
    }
}
