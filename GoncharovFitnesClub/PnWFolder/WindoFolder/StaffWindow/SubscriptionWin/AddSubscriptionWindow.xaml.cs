using GoncharovFitnesClub.ClassFolder;
using GoncharovFitnesClub.DataFolder;
using GoncharovFitnesClub.PnWFolder.WindoFolder.StaffWindow.AdditionalWIndow.VisitDay;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GoncharovFitnesClub.PnWFolder.WindoFolder.StaffWindow.Subscription
{
    /// <summary>
    /// Логика взаимодействия для AddSubscriptionWindow.xaml
    /// </summary>
    public partial class AddSubscriptionWindow : Window
    {

        DataGrid MWListSubscriptionDG;
        Button MWaddBT;
        TextBox MWSearchTB;
        TabItem MWSubscriptionTI;

        public AddSubscriptionWindow(DataGrid MWListSubscriptionDG, Button MWaddBT, TextBox MWSearchTB, TabItem MWSubscriptionTI)
        {
            InitializeComponent();

            this.MWListSubscriptionDG = MWListSubscriptionDG;
            this.MWaddBT = MWaddBT;
            this.MWSearchTB = MWSearchTB;
            this.MWSubscriptionTI = MWSubscriptionTI;


            SpecialityCB.ItemsSource = DBEntities.GetContext().Speciality.
                ToList().OrderBy(u => u.SpecialityID);
            VisitDateCB.ItemsSource = DBEntities.GetContext().VisitDate.
                ToList().OrderBy(u => u.VisitDateID);

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



        private void EnableButt()
        {

            if (string.IsNullOrWhiteSpace(NameSubscriptionTB.Text) ||
                string.IsNullOrWhiteSpace(AmountOfDayTB.Text) || 7 > Convert.ToInt32(AmountOfDayTB.Text)||
                VisitDateCB.SelectedValue == null || TimeVisitTB.Text.Length < 13 ||
                string.IsNullOrWhiteSpace(PriceTB.Text))
            {
                AddSubscriptiontBT.IsEnabled = false;
            }
            else
            {
                AddSubscriptiontBT.IsEnabled = true;
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (AddSubscriptiontBT.IsEnabled)
                {
                    AddSubscriptiontBT_Click(sender, e);
                }
                else
                {
                    //if (SNPClientTB.IsFocused)
                    //{
                    //    PhoneTB.Focus();
                    //}
                    //else if (PhoneTB.IsFocused)
                    //{
                    //    EmailTB.Focus();
                    //}
                    //else if (EmailTB.IsFocused)
                    //{
                    //    DateOfRegDP.Focus();
                    //    DateOfRegDP.IsDropDownOpen = true;
                    //}
                }
            }
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }






        private void AddSubscriptiontBT_Click(object sender, RoutedEventArgs e)
        {

            string[] SplitTime = TimeVisitTB.Text.Split('-');


            Console.WriteLine("1 " + SplitTime[0]);
            Console.WriteLine("2 " + SplitTime[1].Trim(' '));

            try
            {

                DataFolder.Subscription subscription = new DataFolder.Subscription();

                subscription.NameSubscription = NameSubscriptionTB.Text;
                if (SpecialityCB.SelectedValue != null)
                {
                    subscription.SpecialityID = (int)SpecialityCB.SelectedValue;
                }
                if (CoachCB.SelectedValue != null)
                {
                    subscription.CoachID = (int)CoachCB.SelectedValue;
                }

                subscription.AmountOfDays = Convert.ToInt32(AmountOfDayTB.Text);
                subscription.VisitDateID = (int)VisitDateCB.SelectedValue;

                subscription.TimeVisitStart = TimeSpan.Parse(SplitTime[0]);
                subscription.TimeVisitEnd = TimeSpan.Parse(SplitTime[1].Trim(' '));

                subscription.Price = Convert.ToDecimal(PriceTB.Text);


                DBEntities.GetContext().Subscription.Add(subscription);

                DBEntities.GetContext().SaveChanges();


                MBClass.Info("Абонемент успешно добавлен!");

                if (MWSubscriptionTI.IsSelected)
                {
                    MWListSubscriptionDG.ItemsSource = DBEntities.GetContext().
                                 Subscription.Where(u => u.NameSubscription.StartsWith(MWSearchTB.Text)
                                 || u.Speciality.NameSpeciality.StartsWith(MWSearchTB.Text))
                                 .ToList().OrderBy(u => u.SubscriptionID);
                }
            }
            catch (Exception ex)
            {
                MBClass.Error(ex);
            }
        }


        private void Window_Closing(object sender, CancelEventArgs e)
        {
            VariableClass.SubscriptionWinisUsing = false;

            if (MWSubscriptionTI.IsSelected)
            {
                MWaddBT.IsEnabled = true;
            }
        }



        private void NameSubscriptionTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            EnableButt();
        }

        private void SpecialityCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SpecialityCB.SelectedValue != null)
            {

                CoachCB.ItemsSource = DBEntities.GetContext().Coach.Where(u => u.SpecialityID == (int)SpecialityCB.SelectedValue).ToList();

                CoachCB.IsEnabled = true;

            }
            else
            {
                CoachCB.IsEnabled = false;
            }
        }

        private void SpecialityCB_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                SpecialityCB.ItemsSource = DBEntities.GetContext().Speciality.ToList().OrderBy(u => u.SpecialityID);

                SpecialityCB.SelectedValue = null;
                CoachCB.SelectedValue = null;
            }    
        }

        private void CoachCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(SpecialityCB.SelectedValue != null)
            {
                var Coach = DBEntities.GetContext().Coach.FirstOrDefault(u => u.SpecialityID == (int)SpecialityCB.SelectedValue);

                CoachCB.ItemsSource = DBEntities.GetContext().Coach.Where(u => u.SpecialityID == (int)SpecialityCB.SelectedValue).ToList();

            }
        }

        private void AmountOfDayTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            EnableButt();
            if(!string.IsNullOrWhiteSpace(AmountOfDayTB.Text) && 365 < Convert.ToInt32(AmountOfDayTB.Text))
            {
                AmountOfDayTB.Text = "365";
                AmountOfDayTB.CaretIndex = 3;
            }
        }


        private void AmountOfDayTB_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(AmountOfDayTB.Text) &&  7 > Convert.ToInt32(AmountOfDayTB.Text))
            {
                AmountOfDayTB.Text = "7";
                AmountOfDayTB.CaretIndex = 1;
            }
        }

        private void VisitDateCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EnableButt();
        }

        private void TimeVisitTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox TimeVisit = TimeVisitTB;


            try
            {
                if (!Keyboard.IsKeyDown(Key.Back))
                {
                    switch (TimeVisit.Text.Length)
                    {
                        case 2:
                            TimeVisit.Text = TimeVisit.Text.Insert(2, ":");
                            TimeVisit.CaretIndex = 3;
                            break;
                        case 5:
                            TimeVisit.Text = TimeVisit.Text.Insert(5, " - ");
                            TimeVisit.CaretIndex = 8;
                            break;
                        case 10:
                            TimeVisit.Text = TimeVisit.Text.Insert(10, ":");
                            TimeVisit.CaretIndex = 11;
                            break;
                    }
                }

                else
                {
                    switch (TimeVisit.Text.Length)
                    {
                        case 4:
                            TimeVisit.Text.Remove(TimeVisit.Text.LastIndexOf(":"));
                            TimeVisit.CaretIndex = 4;
                            break;
                        case 7:
                            TimeVisit.Text.Remove(TimeVisit.Text.LastIndexOf(" - "));
                            TimeVisit.CaretIndex = 5;
                            break;
                        case 10:
                            TimeVisit.Text.Remove(TimeVisit.Text.LastIndexOf(":"));
                            TimeVisit.CaretIndex = 10;
                            break;
                    }

                }
            }
            catch (Exception)
            {
                return;
            }
        }

        private void PriceTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            EnableButt();
        }


        private void VisitDateCB_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Right && VisitDateCB.SelectedValue != null)
            {

                VariableClass.VisitDateID = (int)VisitDateCB.SelectedValue;
                new EditVisitDateWindow().ShowDialog();

                VisitDateCB.ItemsSource = DBEntities.GetContext().VisitDate.
                    ToList().OrderBy(u => u.VisitDateID);

            }
        }

        private void AmountOfDayTB_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            OnlyNumTextBoxes(e);
        }

        private void TimeVisitTB_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            OnlyNumTextBoxes(e);
        }

        private void OnlyNumTextBoxes(TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void AddVisitDayBT_Click(object sender, RoutedEventArgs e)
        {
            new AddVisitDateWindow().ShowDialog();

            if (VariableClass.newVisitDayCreated)
            {
                VisitDateCB.ItemsSource = DBEntities.GetContext().VisitDate.
                    ToList().OrderBy(u => u.VisitDateID);

                VisitDateCB.SelectedIndex = VisitDateCB.Items.Count - 1;
                VariableClass.newVisitDayCreated = false;

            }
        }
    }
}
