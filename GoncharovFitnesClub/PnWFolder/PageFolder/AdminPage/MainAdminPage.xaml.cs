using GoncharovFitnesClub.ClassFolder;
using GoncharovFitnesClub.DataFolder;
using GoncharovFitnesClub.WindoFolder.AdminWindow;
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

namespace GoncharovFitnesClub.PageFolder.AdminPage
{
    /// <summary>
    /// Логика взаимодействия для MainAdminPage.xaml
    /// </summary>
    public partial class MainAdminPage : Page
    {
        public MainAdminPage()
        {
    

            InitializeComponent();
            ListUserDG.ItemsSource = DBEntities.GetContext().User.ToList()
                .OrderBy(u => u.UserID);
        }



        private void ListUserDG_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenEdit();
        }

        private void EditUser_Click(object sender, RoutedEventArgs e)
        {
            OpenEdit();
        }

        private void OpenEdit()
        {
            if (ListUserDG.SelectedItem != null)
            {
                if (VarriableClass.EditWindowCount > 2)
                {
                    MBClass.Error("Превышен лимит окон!");
                }
                else
                {
                    User user = ListUserDG.SelectedItem as User;

                    VarriableClass.UserID = user.UserID;

                    new AdminEditUserWindow(ListUserDG, SearchTB).Show();

                    ++VarriableClass.EditWindowCount;
                }
                
            }
        }


        private void DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            if (ListUserDG.SelectedItem != null)
            {
                if (MBClass.Question("Вы действительно хотите удалить этого пользователя?"))
                {
                    try
                    {
                        DBEntities.GetContext().User.Remove(ListUserDG.SelectedItem as User);

                        DBEntities.GetContext().SaveChanges();

                        ListUserDG.ItemsSource = DBEntities.GetContext().
                                                User.Where(u => u.Login.StartsWith(SearchTB.Text)
                                                || u.Role.NameRole.StartsWith(SearchTB.Text)).ToList().OrderBy(u => u.UserID);

                    }
                    catch (Exception ex)
                    {

                        MBClass.Error(ex);
                    }
                }
            }
        }

        private void AddUserBT_Click(object sender, RoutedEventArgs e)
        {


            if (!VarriableClass.AddUserWinisUsing)
            {
                new AdminAddUserWindow(ListUserDG, AddUserBT,SearchTB).Show();

                VarriableClass.AddUserWinisUsing = true;

                AddUserBT.IsEnabled = false;
            }

        }


        private void SearchTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                ListUserDG.ItemsSource = DBEntities.GetContext().
                                        User.Where(u => u.Login.StartsWith(SearchTB.Text)
                                        || u.Role.NameRole.StartsWith(SearchTB.Text)).ToList().OrderBy(u=>u.UserID);
            }
            catch (Exception ex)
            {

                MBClass.Error(ex);
            }
        }
    }
}
