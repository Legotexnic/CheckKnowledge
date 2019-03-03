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
    /// Interaction logic for AuthorizationPage.xaml
    /// </summary>
    public partial class AuthorizationPage : Page
    {
        MainWindow wnd;
        private UserManager userManager = new UserManager();
        public AuthorizationPage(MainWindow wnd)
        {
            this.wnd = wnd;
            InitializeComponent();
        }

        private void Sign_SignInButton_Click(object sender, RoutedEventArgs e)
        {
            if (Sign_Login.Text == "")
            {
                SignLogin_Error.Content = "Введите Логин";
                SignLogin_Error.Visibility = Visibility.Visible;
                return;
            }
            else
            {
                SignLogin_Error.Visibility = Visibility.Collapsed;
            }
            if (Sign_Password.Password == "")
            {
                SignPassword_Error.Content = "Введите Пароль";
                SignPassword_Error.Visibility = Visibility.Visible;
                return;
            }
            else
            {
                SignPassword_Error.Visibility = Visibility.Collapsed;
            }

            if (Sign_Login.Text.ToString() == "teacher" && Sign_Password.Password == "123456")
            {
                wnd.Content = new Views.AddTestPage(wnd);
                return;
            }
            if (userManager.GetUserByLogin(Sign_Login.Text.ToString()) == null)
            {
                SignLogin_Error.Content = "Пользователь с таким логином\n не найден.";
                SignLogin_Error.Visibility = Visibility.Visible;
                return;
            }
            else
            {
                SignLogin_Error.Visibility = Visibility.Collapsed;
            }
            User loggedUser = userManager.Login(new User{ Login = Sign_Login.Text, Password = Sign_Password.Password });
            if (loggedUser == null)
            {
                SignPassword_Error.Content = "Пароль введён неправильно.";
                SignPassword_Error.Visibility = Visibility.Visible;
                return;
            }
            else
            {
                SignPassword_Error.Visibility = Visibility.Collapsed;
                Application.Current.Properties["User"] = loggedUser;
                wnd.Content = new Views.TestPage(wnd);
            }
        }

        private void Sign_RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            wnd.Content = new Views.RegistrationPage(wnd);
        }
    }
}
