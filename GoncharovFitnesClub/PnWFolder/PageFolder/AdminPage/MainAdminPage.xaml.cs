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

            CountUsersLB.Content = "Количество пользователей: " + DBEntities.GetContext().User.ToArray().Length;
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
                User user = ListUserDG.SelectedItem as User;

                for (int i = 0; i < 3; i++)
                {
                    if (user.UserID == VariableClass.SelectedUserID[i])
                    {
                        MBClass.Error("Данный пользователь уже редактируется!");
                        return;
                    }
                }
                if (VariableClass.CountEditWindowUser > 2)
                {
                    MBClass.Error("Превышен лимит окон!");
                }
                else
                {
                    VariableClass.UserID = user.UserID;

                    new AdminEditUserWindow(ListUserDG, SearchTB).Show();

                    ++VariableClass.CountEditWindowUser;
                }
            }
                
        }


        private void DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            User user = ListUserDG.SelectedItem as User;

            if (ListUserDG.SelectedItem != null)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (user.UserID == VariableClass.SelectedUserID[i])
                    {
                        MBClass.Error("Данный пользователь редактируется!\n" +
                                      "Закройте окно редактирования");
                        return;
                    }
                }
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


            if (!VariableClass.AddUserWinisUsing)
            {
                new AdminAddUserWindow(ListUserDG, AddUserBT, SearchTB).Show();

                VariableClass.AddUserWinisUsing = true;

                AddUserBT.IsEnabled = false;
            }

        }


        private void SearchTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {

                if (SearchTB.Text.Length > 0)
                {
                    WipeSearchLB.Visibility = Visibility.Visible;
                }
                else
                {
                    WipeSearchLB.Visibility = Visibility.Hidden;
                }



                ListUserDG.ItemsSource = DBEntities.GetContext().
                                        User.Where(u => u.Login.StartsWith(SearchTB.Text)
                                        || u.Role.NameRole.StartsWith(SearchTB.Text)).ToList().OrderBy(u=>u.UserID);

                CountUsersLB.Content = "Количество пользователей: "+ DBEntities.GetContext().User.ToArray().Length;
            }
            catch (Exception ex)
            {

                MBClass.Error(ex);
            }
        }

        private void Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SearchTB.Text = "";
        }
    }
}
