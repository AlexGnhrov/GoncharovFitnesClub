using GoncharovFitnesClub.ClassFolder;
using GoncharovFitnesClub.DataFolder;
using GoncharovFitnesClub.PageFolder.AdminPage;
using GoncharovFitnesClub.PnWFolder.PageFolder.StaffPage;
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

namespace GoncharovFitnesClub.PageFolder
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationPage.xaml
    /// </summary>
    public partial class AuthorizationPage : Page
    {
        Border ToolMenuB;
        public AuthorizationPage(Border ToolMenuB)
        {
            InitializeComponent();
            this.ToolMenuB = ToolMenuB;
        }

        private void SignInBT_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var user = DBEntities.GetContext().User.FirstOrDefault(u => u.Login == LoginTB.Text);

                if (user == null || user.Password != PasswordPB.Password)
                {
                    MBClass.Error("Неверный логин или пароль");
                }
                else
                {
                    switch (user.RoleID)
                    {
                        case 1:
                            NavigationService.Navigate(new MainAdminPage());
                            ToolMenuB.Visibility = Visibility.Visible;
                            break;
                        case 2:
                            NavigationService.Navigate(new MainStaffPage());
                            ToolMenuB.Visibility = Visibility.Visible;
                            break;
                    }
                }


            }
            catch (Exception ex)
            {

                MBClass.Error(ex);
            }


        }

        private void LoginTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            EnableButt();
        }

        private void PasswordPB_PasswordChanged(object sender, RoutedEventArgs e)
        {
            EnableButt();
        }

        private void EnableButt()
        {
            if (string.IsNullOrWhiteSpace(LoginTB.Text) ||
                string.IsNullOrWhiteSpace(PasswordPB.Password))
            {
                SignInBT.IsEnabled = false;
            }
            else
            {
                SignInBT.IsEnabled = true;
            }
        }

        private void Page_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                if(SignInBT.IsEnabled)
                {
                    SignInBT_Click(sender, e);
                }
                if(LoginTB.IsFocused)
                {
                    PasswordPB.Focus();
                }
                else
                {
                    LoginTB.Focus();
                }
            }
        }
    }
}
