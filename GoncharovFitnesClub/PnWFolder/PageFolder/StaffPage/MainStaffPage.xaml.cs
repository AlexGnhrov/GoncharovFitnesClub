using GoncharovFitnesClub.ClassFolder;
using GoncharovFitnesClub.DataFolder;
using GoncharovFitnesClub.PnWFolder.WindoFolder.StaffWindow;
using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace GoncharovFitnesClub.PnWFolder.PageFolder.StaffPage
{
    /// <summary>
    /// Логика взаимодействия для MainStaffPage.xaml
    /// </summary>
    public partial class MainStaffPage : Page
    {
        public MainStaffPage()
        {
            InitializeComponent();


            ListClientDG.ItemsSource = DBEntities.GetContext().Client.ToList()
                                        .OrderBy(u => u.Surname);

        }

        private void SearchTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ClientTI.IsSelected)
            {
                ListClientDG.ItemsSource = DBEntities.GetContext().
                                           Client.Where(u => u.Surname.StartsWith(SearchTB.Text)
                                        || u.Name.StartsWith(SearchTB.Text)
                                        || u.Patronymic.StartsWith(SearchTB.Text)
                                        || u.Subscription.NameSubscription.StartsWith(SearchTB.Text))
                                          .ToList().OrderBy(u => u.ClientID);
            }
            else if (SubscriptionTI.IsSelected)
            {
                ListSubscriptionDG.ItemsSource = DBEntities.GetContext().
                                                 Subscription.Where(u => u.NameSubscription.StartsWith(SearchTB.Text)
                                                 || u.Speciality.NameSpeciality.StartsWith(SearchTB.Text)
                                                 || u.TimeOfVisit.StartsWith(SearchTB.Text))
                                                 .ToList().OrderBy(u => u.SubscriptionID);
            }
            else if (CoachTI.IsSelected)
            {
              ListCoachDG.ItemsSource = DBEntities.GetContext().Coach.Where(u => u.Surname.StartsWith(SearchTB.Text)
                        || u.Name.StartsWith(SearchTB.Text)
                        || u.Patronymic.StartsWith(SearchTB.Text)
                        || u.Speciality.NameSpeciality.StartsWith(SearchTB.Text))
                        .ToList().OrderBy(u => u.CoachID);
            }
        }

        private void AddBT_Click(object sender, RoutedEventArgs e)
        {
            if (ClientTI.IsSelected)
            {

            }
            else if (SubscriptionTI.IsSelected)
            {

            }
            else if (CoachTI.IsSelected)
            {

                if (!VarriableClass.AddUserWinisUsing)
                {
                    new AddCoachWindow(ListCoachDG, AddBT, SearchTB).Show();

                    VarriableClass.AddUserWinisUsing = true;

                    AddBT.IsEnabled = false;
                }
            }
        }

        private void EditUser_Click(object sender, RoutedEventArgs e)
        {
            if (ClientTI.IsSelected)
            {

            }
            else if (SubscriptionTI.IsSelected)
            {

            }
            else if (CoachTI.IsSelected)
            {

            }
        }

        private void DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            if (ClientTI.IsSelected)
            {

            }
            else if (SubscriptionTI.IsSelected)
            {

            }
            else if (CoachTI.IsSelected)
            {

            }
        }



        private void ListUserDG_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ClientTI.IsSelected)
            {
                
            }
            else if (SubscriptionTI.IsSelected)
            {

            }
            else if (CoachTI.IsSelected)
            {
    
            }
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ClientTI.IsSelected)
            {
                ListClientDG.ItemsSource = DBEntities.GetContext().Client.ToList()
                                .OrderBy(u => u.Surname);

                PageLabel.Content = "Список клиентов";
                AddBT.Content = "Добавить клиента";

                ListSubscriptionDG.ItemsSource = null;
                ListCoachDG.ItemsSource = null;
            }
            else if (SubscriptionTI.IsSelected)
            {
                ListSubscriptionDG.ItemsSource = DBEntities.GetContext().Subscription.ToList()
                                     .OrderBy(u => u.SubscriptionID);


                PageLabel.Content = "Список абонементов";
                AddBT.Content = "Добавить абонемент";

                ListClientDG.ItemsSource = null;
                ListCoachDG.ItemsSource = null;
            }
            else if (CoachTI.IsSelected)
            {

                ListCoachDG.ItemsSource = DBEntities.GetContext().Coach.ToList()
                                            .OrderBy(u => u.CoachID);


                PageLabel.Content = "Список тренеров";
                AddBT.Content = "Добавить тренера";

                ListClientDG.ItemsSource = null;
                ListSubscriptionDG.ItemsSource = null;
            }

        }



    }
}
