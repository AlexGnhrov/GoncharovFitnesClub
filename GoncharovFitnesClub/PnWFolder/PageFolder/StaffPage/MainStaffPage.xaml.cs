using GoncharovFitnesClub.ClassFolder;
using GoncharovFitnesClub.DataFolder;
using GoncharovFitnesClub.PnWFolder.WindoFolder.StaffWindow;
using GoncharovFitnesClub.PnWFolder.WindoFolder.StaffWindow.Client;
using GoncharovFitnesClub.PnWFolder.WindoFolder.StaffWindow.ClientWindow;
using GoncharovFitnesClub.PnWFolder.WindoFolder.StaffWindow.Subscription;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
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

        EditCoachWindow editCoachWindow;
        EditClientWindow editClienthWindow;
        EditSubscriptionWindow editSubscriptionWindow;

        string[] SaveSearchText = { "", "", "" };


        public MainStaffPage()
        {
            InitializeComponent();

            ClientTI.IsSelected = true;

        }

        private void SearchTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchTB.CaretIndex = SearchTB.Text.Length;

            if(SearchTB.Text.Length > 0)
            {
                WipeSearchLB.Visibility = Visibility.Visible;
            }
            else
            {
                WipeSearchLB.Visibility = Visibility.Hidden;
            }

            if (ClientTI.IsSelected)
            {
                ListClientDG.ItemsSource = DBEntities.GetContext().
                                           Client.Where(u => u.Surname.StartsWith(SearchTB.Text)
                                        || u.Name.StartsWith(SearchTB.Text)
                                        || u.Patronymic.StartsWith(SearchTB.Text)
                                        || u.Subscription.NameSubscription.StartsWith(SearchTB.Text)
                                        || u.Status.NameStatus.StartsWith(SearchTB.Text))
                                          .ToList().OrderBy(u => u.ClientID);

                SaveSearchText[0] = SearchTB.Text;
            }
            else if (CoachTI.IsSelected)
            {
                ListCoachDG.ItemsSource = DBEntities.GetContext().Coach.Where(u => u.Surname.StartsWith(SearchTB.Text)
                            || u.Name.StartsWith(SearchTB.Text)
                            || u.Patronymic.StartsWith(SearchTB.Text)
                            || u.Speciality.NameSpeciality.StartsWith(SearchTB.Text))
                            .ToList().OrderBy(u => u.CoachID);

                SaveSearchText[1] = SearchTB.Text;
            }
            else if (SubscriptionTI.IsSelected)
            {
                ListSubscriptionDG.ItemsSource = DBEntities.GetContext().
                                                 Subscription.Where(u => u.NameSubscription.StartsWith(SearchTB.Text)
                                                 || u.Speciality.NameSpeciality.StartsWith(SearchTB.Text))
                                                 .ToList().OrderBy(u => u.SubscriptionID);

                SaveSearchText[2] = SearchTB.Text;
            }
        }

        private void AddBT_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ClientTI.IsSelected)
                {


                    new AddClientWindow(ListClientDG, AddBT, SearchTB, ClientTI).Show();

                    VariableClass.ClientWinisUsing = true;

                    AddBT.IsEnabled = false;

                }
                else if (CoachTI.IsSelected)
                {


                    new AddCoachWindow(ListCoachDG, AddBT, SearchTB, CoachTI).Show();

                    VariableClass.CoachWinisUsing = true;

                    AddBT.IsEnabled = false;

                }
                else if (SubscriptionTI.IsSelected)
                {

                    new AddSubscriptionWindow(ListSubscriptionDG, AddBT, SearchTB, SubscriptionTI).Show();

                    VariableClass.SubscriptionWinisUsing = true;

                    AddBT.IsEnabled = false;

                }
            }
            catch (Exception ex)
            {
                MBClass.Error(ex);
            }
        }

        private void ListUserDG_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenEdit();
        }

        private void EditUser_Click(object sender, RoutedEventArgs e)
        {
            OpenEdit();
        }

        private void DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ClientTI.IsSelected)
                {
                    Client client = ListClientDG.SelectedItem as Client;

                    if (editClienthWindow != null && VariableClass.ClientID == client.ClientID)
                    {
                        MBClass.Error("Данный клиент редактируется!\n" +
                                      "Закройте окно редактирования.");
                    }
                    else if (MBClass.Question("Вы действительно хотите удалить этого клиента?"))
                    {

                        DBEntities.GetContext().Client.Remove(ListClientDG.SelectedItem as Client);

                        DBEntities.GetContext().SaveChanges();


                        ListClientDG.ItemsSource = DBEntities.GetContext().
                                                   Client.Where(u => u.Surname.StartsWith(SearchTB.Text)
                                                || u.Name.StartsWith(SearchTB.Text)
                                                || u.Patronymic.StartsWith(SearchTB.Text)
                                                || u.Subscription.NameSubscription.StartsWith(SearchTB.Text)
                                                || u.Status.NameStatus.StartsWith(SearchTB.Text))
                                                  .ToList().OrderBy(u => u.ClientID);
                    }
                }
                else if (CoachTI.IsSelected)
                {
                    Coach coach = ListCoachDG.SelectedItem as Coach;

                    if (editCoachWindow != null && VariableClass.CoachID == coach.CoachID)
                    {
                        MBClass.Error("Данный тренер редактируется!\n" +
                                      "Закройте окно редактирования.");
                    }
                    else if (MBClass.Question("Вы действительно хотите удалить этого тренера?"))
                    {

                        DBEntities.GetContext().Coach.Remove(ListCoachDG.SelectedItem as Coach);

                        DBEntities.GetContext().SaveChanges();

                        ListCoachDG.ItemsSource = DBEntities.GetContext().Coach.Where(u => u.Surname.StartsWith(SearchTB.Text)
                                  || u.Name.StartsWith(SearchTB.Text)
                                  || u.Patronymic.StartsWith(SearchTB.Text)
                                  || u.Speciality.NameSpeciality.StartsWith(SearchTB.Text))
                                  .ToList().OrderBy(u => u.CoachID);
                    }
                }
                else if (SubscriptionTI.IsSelected)
                {
                    Subscription subscription = ListSubscriptionDG.SelectedItem as Subscription;

                    if (editSubscriptionWindow != null && VariableClass.SubscriptionID == subscription.SubscriptionID)
                    {
                        MBClass.Error("Данный абонемент редактируется!\n" +
                                      "Закройте окно редактирования.");
                    }
                    else if (MBClass.Question("Вы действительно хотите удалить этог абонемент?"))
                    {

                        DBEntities.GetContext().Subscription.Remove(ListSubscriptionDG.SelectedItem as Subscription);

                        DBEntities.GetContext().SaveChanges();

                        ListSubscriptionDG.ItemsSource = DBEntities.GetContext().
                                 Subscription.Where(u => u.NameSubscription.StartsWith(SearchTB.Text)
                                 || u.Speciality.NameSpeciality.StartsWith(SearchTB.Text))
                                 .ToList().OrderBy(u => u.SubscriptionID);
                    }
                }
            }
            catch (Exception ex)
            {
                MBClass.Error(ex);
            }
        }


        private void OpenEdit()
        {
            try
            {
                if (ClientTI.IsSelected)
                {
                    if (ListClientDG.SelectedItem != null)
                    {

                        Client client = ListClientDG.SelectedItem as Client;

                        if (editClienthWindow != null)
                        {
                            if (VariableClass.ClientID == client.ClientID)
                            {
                                editClienthWindow.Focus();
                                return;
                            }
                            else
                            {
                                editClienthWindow.Close();
                            }
                        }

                        VariableClass.ClientID = client.ClientID;

                        editClienthWindow = new EditClientWindow(ListClientDG, SearchTB, ClientTI);
                        editClienthWindow.Show();

                    }
                }
                else if (CoachTI.IsSelected)
                {

                    if (ListCoachDG.SelectedItem != null)
                    {

                        Coach coach = ListCoachDG.SelectedItem as Coach;

                        if (editCoachWindow != null)
                        {
                            if (VariableClass.CoachID == coach.CoachID)
                            {
                                editCoachWindow.Focus();
                                return;
                            }
                            else
                            {
                                editCoachWindow.Close();
                            }
                        }

                        VariableClass.CoachID = coach.CoachID;

                        editCoachWindow = new EditCoachWindow(ListCoachDG, SearchTB, CoachTI);
                        editCoachWindow.Show();

                    }
                }
                else if (SubscriptionTI.IsSelected)
                {

                }

            }
            catch (Exception ex)
            {
                MBClass.Error(ex);
            }
        }





        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            try
            {
                if (ClientTI.IsSelected)
                {

                    SearchTB.Text = SaveSearchText[0];

                    EndOfSubStatusUpdate();


                    ListClientDG.ItemsSource = DBEntities.GetContext().
                                               Client.Where(u => u.Surname.StartsWith(SearchTB.Text)
                                            || u.Name.StartsWith(SearchTB.Text)
                                            || u.Patronymic.StartsWith(SearchTB.Text)
                                            || u.Subscription.NameSubscription.StartsWith(SearchTB.Text)
                                            || u.Status.NameStatus.StartsWith(SearchTB.Text))
                                              .ToList().OrderBy(u => u.ClientID);

                    CountUsersLB.Content = "Количество клиентов: " + DBEntities.GetContext().Client.Where(u => u.StatusID != 4).ToArray().Length;

                    PageLabel.Content = "Список клиентов";
                    AddBT.Content = "Добавить клиента";

                    ListSubscriptionDG.ItemsSource = null;
                    ListCoachDG.ItemsSource = null;


                    AddBT.IsEnabled = !VariableClass.ClientWinisUsing;

                }
                else if (CoachTI.IsSelected)
                {
                    SearchTB.Text = SaveSearchText[1];

                    ListCoachDG.ItemsSource = DBEntities.GetContext().Coach.Where(u => u.Surname.StartsWith(SearchTB.Text)
                              || u.Name.StartsWith(SearchTB.Text)
                              || u.Patronymic.StartsWith(SearchTB.Text)
                              || u.Speciality.NameSpeciality.StartsWith(SearchTB.Text))
                              .ToList().OrderBy(u => u.CoachID);

                    CountUsersLB.Content = "Количество тренеров: " + DBEntities.GetContext().Coach.ToArray().Length;

                    PageLabel.Content = "Список тренеров";
                    AddBT.Content = "Добавить тренера";

                    ListClientDG.ItemsSource = null;
                    ListSubscriptionDG.ItemsSource = null;



                    AddBT.IsEnabled = !VariableClass.CoachWinisUsing;

                }
                else if (SubscriptionTI.IsSelected)
                {
                    SearchTB.Text = SaveSearchText[2];

                    ListSubscriptionDG.ItemsSource = DBEntities.GetContext().
                                                     Subscription.Where(u => u.NameSubscription.StartsWith(SearchTB.Text)
                                                     || u.Speciality.NameSpeciality.StartsWith(SearchTB.Text))
                                                     .ToList().OrderBy(u => u.SubscriptionID);

                    CountUsersLB.Content = "Количество абонементов: " + DBEntities.GetContext().Subscription.ToArray().Length;


                    PageLabel.Content = "Список абонементов";
                    AddBT.Content = "Добавить абонемент";

                    ListClientDG.ItemsSource = null;
                    ListCoachDG.ItemsSource = null;


                    AddBT.IsEnabled = !VariableClass.SubscriptionWinisUsing;

                }
            }
            catch (Exception ex)
            {
                MBClass.Error(ex);
            }
        }

        private void EndOfSubStatusUpdate()
        { 
            object[] ClientSelected = DBEntities.GetContext().Client.ToArray();


            for (int i = 0; i < ClientSelected.Length; i++)
            {
                Client client = ClientSelected[i] as Client;


                if (client.DateOfEnd <= DateTime.Now &&
                    client.StatusID != 2 && client.StatusID != 4)
                {
                    client.StatusID = 2;

                    DBEntities.GetContext().SaveChanges();

                    MBClass.Info($"Абонемент у {client.Surname} {client.Name} {client.Patronymic} Закончился!");
                }


            }
        }


        private void Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            SearchTB.Text = "";

        }
    }
}
