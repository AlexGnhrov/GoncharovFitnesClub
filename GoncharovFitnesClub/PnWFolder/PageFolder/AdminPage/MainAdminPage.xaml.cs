using GoncharovFitnesClub.ClassFolder;
using GoncharovFitnesClub.DataFolder;
using GoncharovFitnesClub.PnWFolder.WindoFolder.AdminWin;
using GoncharovFitnesClub.PnWFolder.WindoFolder.AdminWin.UserData;
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

            UpdateData();
        }


        private void ListStaffDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var SelectedStaff = DBEntities.GetContext().Staff.ToArray();

            for (int i = 0; i < SelectedStaff.Length; i++)
            {

                Staff staff = SelectedStaff[i];

                User user = DBEntities.GetContext().User.First(u => u.UserID == staff.UserID);

                user.IsUsing = true;

                DBEntities.GetContext().SaveChanges();

            }

            UpdateData();
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
            if (ListStaffDG.SelectedItem != null)
            {
                Staff staff = ListStaffDG.SelectedItem as Staff;

                if (VariableClass.editStaffWindow != null)
                {
                    if (VariableClass.StaffID == staff.StaffID)
                    {
                        VariableClass.editStaffWindow.Focus();
                        return;
                    }
                    else
                    {
                        VariableClass.editStaffWindow.Close();
                    }
                }

                VariableClass.StaffID = staff.StaffID;

                VariableClass.editStaffWindow = new EditStaffWindow(ListStaffDG, SearchTB, CountUsersLB);
                VariableClass.editStaffWindow.Show();


            }

        }


        private void DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            Staff staff = ListStaffDG.SelectedItem as Staff;

            if (ListStaffDG.SelectedItem != null)
            {
                if (VariableClass.editStaffWindow != null && VariableClass.StaffID == staff.StaffID)
                {
                    MBClass.Error("Данный сотрудник редактируется!\n" +
                                  "Убедитесь, что окно редактирования закрыто! ");
                }
                else if (MBClass.Question("Вы действительно хотите удалить этого сотрудника?"))
                {
                    try
                    {

                        User user = DBEntities.GetContext().User.FirstOrDefault(u => u.UserID == staff.UserID);

                        DBEntities.GetContext().Staff.Remove(ListStaffDG.SelectedItem as Staff);

                        user.IsUsing = false;

                        DBEntities.GetContext().SaveChanges();

                        UpdateData();
                    }
                    catch (Exception ex)
                    {

                        MBClass.Error(ex);
                    }
                }
            }
        }

        private void AddStaffBT_Click(object sender, RoutedEventArgs e)
        {



            new AddStaffWindow(ListStaffDG, AddStaffBT, SearchTB,CountUsersLB).Show();

            VariableClass.AddStaffWinisUsing = true;

            AddStaffBT.IsEnabled = false;


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

                ListStaffDG.ItemsSource = DBEntities.GetContext().
                                Staff.Where(u => u.Surname.StartsWith(SearchTB.Text) ||
                                            u.Name.StartsWith(SearchTB.Text) ||
                                            u.Patronymic.StartsWith(SearchTB.Text) ||
                                            u.User.Login.StartsWith(SearchTB.Text) ||
                                            u.User.Role.NameRole.StartsWith(SearchTB.Text))
                                            .ToList().OrderBy(u => u.StaffID);
            }
            catch (Exception ex)
            {

                MBClass.Error(ex);
            }
        }


        private void WipeSearchLB_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SearchTB.Text = "";
        }

        private void UpdateData()
        {
            ListStaffDG.ItemsSource = DBEntities.GetContext().
                            Staff.Where(u => u.Surname.StartsWith(SearchTB.Text) ||
                                        u.Name.StartsWith(SearchTB.Text) ||
                                        u.Patronymic.StartsWith(SearchTB.Text) ||
                                        u.User.Login.StartsWith(SearchTB.Text) ||
                                        u.User.Role.NameRole.StartsWith(SearchTB.Text))
                                        .ToList().OrderBy(u => u.StaffID);

            CountUsersLB.Content = "Колиество сотрудников: " + ListStaffDG.Items.Count;
        }

    }
}
