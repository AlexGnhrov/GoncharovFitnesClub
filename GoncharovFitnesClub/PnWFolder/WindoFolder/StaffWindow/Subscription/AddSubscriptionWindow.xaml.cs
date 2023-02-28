using GoncharovFitnesClub.ClassFolder;
using GoncharovFitnesClub.DataFolder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            //string[] SplitSNP = SNPClientTB.Text.Split(' ');

            if (true)
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

        private void SNPClientTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            //string[] SplitSNP = SNPClientTB.Text.Split(' ');
            //int strlen = SNPClientTB.Text.Length;

            //if (SplitSNP.Length > 3)
            //{
            //    SNPClientTB.Text = SNPClientTB.Text.Remove(strlen - 1);
            //    SNPClientTB.CaretIndex = strlen;
            //}

            EnableButt();
        }






        private void StatusCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EnableButt();
        }





        private void StatusCB_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            //if (e.ChangedButton == MouseButton.Right && StatusCB.SelectedValue != null)
            //{

            //    VariableClass.StatusID = (int)StatusCB.SelectedValue;
            //    new EditStatusWindow().ShowDialog();

            //    StatusCB.ItemsSource = DBEntities.GetContext().Status.ToList()
            //        .OrderBy(u => u.StatusID);

            //}
        }


        private void AddClientBT_Click(object sender, RoutedEventArgs e)
        {
            //string[] SplitSNP = SNPClientTB.Text.Split(' ');

            try
            {

             

                //if (SubscriptionCB.SelectedValue != null)
                //    client.SubscriptionID = (int)SubscriptionCB.SelectedValue;

                //client.StatusID = (int)StatusCB.SelectedValue;


                //DBEntities.GetContext().Client.Add(client);

                //DBEntities.GetContext().SaveChanges();


                //MBClass.Info("Клиент успешно добавлен!");

                //if (MWSubscriptionTI.IsSelected)
                //{

                //}
            }
            catch (Exception ex)
            {
                MBClass.Error(ex);
            }
        }


        private void Window_Closing(object sender, CancelEventArgs e)
        {
            VariableClass.ClientWinisUsing = false;

            if (MWSubscriptionTI.IsSelected)
            {
                MWaddBT.IsEnabled = true;
            }
        }

        private void AmountOfDayTB_Copy_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
