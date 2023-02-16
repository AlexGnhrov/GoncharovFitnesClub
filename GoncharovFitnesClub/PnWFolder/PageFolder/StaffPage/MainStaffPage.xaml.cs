using GoncharovFitnesClub.DataFolder;
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

            ListSubscriptionDG.ItemsSource = DBEntities.GetContext().Subscription.ToList()
                                        .OrderBy(u => u.SubscriptionID);
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

            }
            else if (CoachTI.IsSelected)
            {

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
            if(ClientTI.IsSelected)
            {
                ListClientDG.ItemsSource = DBEntities.GetContext().Client.ToList()
                                .OrderBy(u => u.Surname);

                PageLabel.Content = "Список клиентов";
                AddBT.Content = "Добавить клиента";

                ListSubscriptionDG.ItemsSource = null;
            }
            else if(SubscriptionTI.IsSelected)
            {
                ListSubscriptionDG.ItemsSource = DBEntities.GetContext().Subscription.ToList()
                                     .OrderBy(u => u.SubscriptionID);


                PageLabel.Content = "Список абонементов";
                AddBT.Content = "Добавить абонемент";

                ListClientDG.ItemsSource = null;
            }
            else if(CoachTI.IsSelected)
            {

               PageLabel.Content = "Список тренеров";
               AddBT.Content = "Добавить тренера";

               ListClientDG.ItemsSource = null;
            }

        }



    }
}
