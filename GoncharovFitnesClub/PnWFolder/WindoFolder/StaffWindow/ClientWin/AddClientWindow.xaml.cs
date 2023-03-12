using GoncharovFitnesClub.ClassFolder;
using GoncharovFitnesClub.DataFolder;
using Microsoft.Win32;
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
using System.Windows.Shapes;
using System.ComponentModel;
using GoncharovFitnesClub.PnWFolder.WindoFolder.StaffWindow.AdditionalWIndow.StatusWindow;
using System.Text.RegularExpressions;
using GoncharovFitnesClub.PnWFolder.WindoFolder.StaffWindow.Subscription;

namespace GoncharovFitnesClub.PnWFolder.WindoFolder.StaffWindow.Client
{
    /// <summary>
    /// Логика взаимодействия для AddClientWindow.xaml
    /// </summary>
    public partial class AddClientWindow : Window
    {

        DataGrid MWListClientDG;
        Button MWaddBT;
        TextBox MWSearchTB;
        TabItem MWClientTI;
        Label MWCountLB;


        public AddClientWindow(DataGrid MWListClientDG, Button MWaddBT, TextBox MWSearchTB,
                               TabItem MWClientTI, Label MWCountLB)
        {
            InitializeComponent();

            this.MWListClientDG = MWListClientDG;
            this.MWaddBT = MWaddBT;
            this.MWSearchTB = MWSearchTB;
            this.MWClientTI = MWClientTI;
            this.MWCountLB = MWCountLB;

            PhoneTB.Text = "+7 ";
            PhoneTB.CaretIndex = 4;

            SubscriptionCB.ItemsSource = DBEntities.GetContext().Subscription.ToList()
                                            .OrderBy(u => u.SubscriptionID);
            StatusCB.ItemsSource = DBEntities.GetContext().Status.ToList().
                                            OrderBy(u => u.StatusID);


        }


        bool BlockDragWindow = false;

        bool CloseBIsUsing = false;
        bool HideBIsUsing = false;
        bool ResizeIsUsing = false;

        private void MainB_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!BlockDragWindow && e.LeftButton == MouseButtonState.Pressed &&
                     ToolBarGrid.IsMouseOver)
            {

                DragMove();

            }
        }

        //------------------Кнопка закрытия приложения----------------------


        private async void CloseB_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CloseBIsUsing = true;

            await DisableDrag(e);

            if (!HideBIsUsing && !ResizeIsUsing && CloseB.IsMouseOver)
            {

                Close();
            }

            CloseBIsUsing = false;

        }


        private void Borders_MouseEnter(object sender, MouseEventArgs e)
        {
            ChangeColorButton();
        }




        //------------------Кнопка скрытия приложения----------------------


        private async void HideB_MouseDown(object sender, MouseButtonEventArgs e)
        {
            HideBIsUsing = true;

            await DisableDrag(e);

            if (!CloseBIsUsing && !ResizeIsUsing && HideB.IsMouseOver)
            {
                WindowState = WindowState.Minimized;
            }

            HideBIsUsing = false;
        }


        private void HideB_MouseEnter(object sender, MouseEventArgs e)
        {
            ChangeColorButton();
        }




        //-------Методы для обработки цвета кнопок -------------------------------

        async Task ChangeColorButton()
        {
            if (!HideBIsUsing && !ResizeIsUsing)
            {
                while (CloseB.IsMouseOver)
                {

                    if (BlockDragWindow)
                    {
                        CloseB.Background = new SolidColorBrush(Color.FromRgb(255, 100, 100));
                    }
                    else
                    {
                        CloseB.Background = new SolidColorBrush(Color.FromRgb(205, 50, 50));
                    }

                    CloseLB.Foreground = new SolidColorBrush(Colors.White);

                    await Task.Delay(1);
                }

                CloseB.Background = null;
                CloseLB.Foreground = new SolidColorBrush(Colors.White);

            }

            if (!CloseBIsUsing && !ResizeIsUsing)
            {
                while (HideB.IsMouseOver)
                {
                    if (BlockDragWindow)
                    {
                        HideB.Background = new SolidColorBrush(Color.FromRgb(150, 150, 150));
                    }
                    else
                    {
                        HideB.Background = new SolidColorBrush(Color.FromRgb(211, 211, 211));
                    }
                    await Task.Delay(1);
                }
                HideB.Background = null;

            }

        }



        async Task DisableDrag(MouseButtonEventArgs e)
        {
            BlockDragWindow = true;

            while (e.LeftButton == MouseButtonState.Pressed)
            {
                await Task.Delay(1);
            }

            BlockDragWindow = false;

        }

        private void LoginTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            EnableButt();
        }

        private void PasswordPB_TextChanged(object sender, TextChangedEventArgs e)
        {
            EnableButt();
        }

        private void RoleCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EnableButt();
        }


        private void EnableButt()
        {
            string[] SplitSNP = SNPClientTB.Text.Split(' ');

            if (string.IsNullOrWhiteSpace(SNPClientTB.Text) ||
                string.IsNullOrWhiteSpace(PhoneTB.Text) ||
                StatusCB.SelectedItem == null ||
                PhoneTB.Text.Length < 16 ||
                 (SplitSNP.Length == 1 ||
                 SplitSNP.Length == 2 && SplitSNP[1] == "" ||
                 SplitSNP.Length == 3 && SplitSNP[2] == ""))
            {
                AddClientBT.IsEnabled = false;
            }
            else
            {
                AddClientBT.IsEnabled = true;
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (AddClientBT.IsEnabled)
                {
                    AddClientBT_Click(sender, e);
                }
                else
                {
                    if (SNPClientTB.IsFocused)
                    {
                        PhoneTB.Focus();
                    }
                    else if (PhoneTB.IsFocused)
                    {
                        EmailTB.Focus();
                    }
                    else if (EmailTB.IsFocused)
                    {
                        DateOfRegDP.Focus();
                        DateOfRegDP.IsDropDownOpen = true;
                    }
                }
            }
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }

        private void SNPClientTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            string[] SplitSNP = SNPClientTB.Text.Split(' ');
            int strlen = SNPClientTB.Text.Length;

            if (SplitSNP.Length > 3)
            {
                SNPClientTB.Text = SNPClientTB.Text.Remove(strlen - 1);
                SNPClientTB.CaretIndex = strlen;
            }

            EnableButt();
        }



        private void PhoneTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox PhoneNumText = PhoneTB;

            try
            {
                if (!Keyboard.IsKeyDown(Key.Back))
                {
                    switch (PhoneNumText.Text.Length)
                    {
                        case 7:
                            PhoneNumText.Text = PhoneNumText.Text.Insert(6, "-");
                            PhoneNumText.CaretIndex = 8;
                            break;
                        case 11:
                            PhoneNumText.Text = PhoneNumText.Text.Insert(10, "-");
                            PhoneNumText.CaretIndex = 12;
                            break;
                        case 14:
                            PhoneNumText.Text = PhoneNumText.Text.Insert(13, "-");
                            PhoneNumText.CaretIndex = 15;
                            break;
                    }
                }

                else
                {
                    if (PhoneNumText.Text.Length < 4)
                    {
                        PhoneNumText.Text = "+7 ";
                        PhoneNumText.CaretIndex = 4;
                    }
                    switch (PhoneNumText.Text.Length)
                    {
                        case 7:
                            PhoneNumText.Text =
                                PhoneNumText.Text.Remove(PhoneNumText.Text.LastIndexOf("-"));
                            PhoneNumText.CaretIndex = 6;
                            break;
                        case 11:
                            PhoneNumText.Text =
                                PhoneNumText.Text.Remove(PhoneNumText.Text.LastIndexOf("-"));
                            PhoneNumText.CaretIndex = 10;
                            break;
                        case 14:
                            PhoneNumText.Text =
                                PhoneNumText.Text.Remove(PhoneNumText.Text.LastIndexOf("-"));
                            PhoneNumText.CaretIndex = 13;
                            break;
                    }
                }
            }
            catch (Exception)
            {
                return;
            }

            EnableButt();
        }




        private void StatusCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EnableButt();
        }




        private void AddStatusBT_Click(object sender, RoutedEventArgs e)
        {
            new AddStatusWindow().ShowDialog();

            if (VariableClass.newStatusCreated)
            {
                StatusCB.ItemsSource = DBEntities.GetContext().Status.ToList().OrderBy(u => u.StatusID);

                StatusCB.SelectedIndex = StatusCB.Items.Count - 1;
                VariableClass.newStatusCreated = false;

            }
        }


        private void StatusCB_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            if (e.ChangedButton == MouseButton.Right && StatusCB.SelectedValue != null)
            {

                VariableClass.StatusID = (int)StatusCB.SelectedValue;
                new EditStatusWindow().ShowDialog();

                StatusCB.ItemsSource = DBEntities.GetContext().Status.ToList()
                    .OrderBy(u => u.StatusID);

            }
        }


        private void AddClientBT_Click(object sender, RoutedEventArgs e)
        {
            string[] SplitSNP = SNPClientTB.Text.Split(' ');

            try
            {

                DataFolder.Client client = new DataFolder.Client();



                client.Surname = SplitSNP[0];
                client.Name = SplitSNP[1];
                if (SplitSNP.Length == 3)
                    client.Patronymic = SplitSNP[2];

                client.PhoneNum = PhoneTB.Text;
                client.Email = EmailTB.Text;

                if (DateOfRegDP.SelectedDate != null)
                {
                    client.DateOfReg = (DateTime)DateOfRegDP.SelectedDate;
                    if (DateOfEndDP.SelectedDate != null)
                    {
                        client.DateOfEnd = (DateTime)DateOfEndDP.SelectedDate;
                    }
                }

                if (SubscriptionCB.SelectedValue != null)
                    client.SubscriptionID = (int)SubscriptionCB.SelectedValue;

                client.StatusID = (int)StatusCB.SelectedValue;


                DBEntities.GetContext().Client.Add(client);

                DBEntities.GetContext().SaveChanges();


                MBClass.Info("Клиент успешно добавлен!");

                if (MWClientTI.IsSelected)
                {

                    MWListClientDG.ItemsSource = DBEntities.GetContext().
                                               Client.Where(u => u.Surname.StartsWith(MWSearchTB.Text)
                                            || u.Name.StartsWith(MWSearchTB.Text)
                                            || u.Patronymic.StartsWith(MWSearchTB.Text)
                                            || u.Subscription.NameSubscription.StartsWith(MWSearchTB.Text)
                                            || u.Status.NameStatus.StartsWith(MWSearchTB.Text))
                                              .ToList().OrderBy(u => u.ClientID);


                   MWCountLB.Content = "Количество клиентов: " + DBEntities.GetContext().Client.Where(u => u.StatusID != 4).ToArray().Length;
                }
            }
            catch (Exception ex)
            {
                MBClass.Error(ex);
            }
        }


        private void Window_Closing(object sender, CancelEventArgs e)
        {
            VariableClass.ClientWinisUsing = false;

            if (MWClientTI.IsSelected)
            {
                MWaddBT.IsEnabled = true;
            }
        }

        private void DateOfRegDP_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
           
            CountDate();
        }

        private void SubscriptionCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CountDate();
        }

        DataFolder.Subscription subscription;

        private void SubscriptionCB_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                SubscriptionCB.ItemsSource = DBEntities.GetContext().Subscription.ToList()
                                .OrderBy(u => u.SubscriptionID);

                SubscriptionCB.SelectedValue = null;
                DateOfEndDP.Text = null;
            }
            else if (e.ChangedButton == MouseButton.Right)
            {
                if(SubscriptionCB.SelectedValue != null)
                {
                    subscription = DBEntities.GetContext().Subscription.
                                            FirstOrDefault(u => u.SubscriptionID == (int)SubscriptionCB.SelectedValue);

                    if (VariableClass.editSubscriptionWindow != null)
                    {
                        if (VariableClass.SubscriptionID == subscription.SubscriptionID)
                        {
                            VariableClass.editSubscriptionWindow.Focus();
                            return;
                        }
                        else
                        {
                            VariableClass.editSubscriptionWindow.Close();
                        }
                    }


                    VariableClass.SubscriptionID = subscription.SubscriptionID;

                    VariableClass.editSubscriptionWindow =
                                        new EditSubscriptionWindow(VariableClass.ListSubscriptionDG,
                                                                   VariableClass.SearchTB, 
                                                                   VariableClass.SubscriptionTI,
                                                                   VariableClass.CountUsersLB);

                    VariableClass.editSubscriptionWindow.Show();
                }

            }
        }

        private void CountDate()
        {
            if (DateOfRegDP.SelectedDate != null && SubscriptionCB.SelectedValue != null)
            {

                var subId = DBEntities.GetContext().Subscription.FirstOrDefault(u => u.SubscriptionID == (int)SubscriptionCB.SelectedValue);


                DateTime dateOfEnd = Convert.ToDateTime(DateOfRegDP.Text);
                DateOfEndDP.Text = dateOfEnd.AddDays(subId.AmountOfDays).ToString();

            }
        }

        private void PhoneTB_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }



        private void AddSubscriptionBT_Click(object sender, RoutedEventArgs e)
        {
            if (VariableClass.addSubscriptionWindow == null)
            {
                VariableClass.addSubscriptionWindow = new AddSubscriptionWindow(VariableClass.ListSubscriptionDG,
                                                                       VariableClass.AddBT,
                                                                       VariableClass.SearchTB,
                                                                       VariableClass.SubscriptionTI,
                                                                       VariableClass.CountUsersLB);
                VariableClass.addSubscriptionWindow.Show();

                VariableClass.SubscriptionWinisUsing = true;

                VariableClass.AddBT.IsEnabled = false;
            }
            else
            {
                VariableClass.addSubscriptionWindow.Focus();
            }
        }

        private void SubscriptionCB_GotFocus(object sender, RoutedEventArgs e)
        {
            SubscriptionCB.ItemsSource = DBEntities.GetContext().Subscription.ToList()
                .OrderBy(u => u.SubscriptionID);
        }
    }
}
