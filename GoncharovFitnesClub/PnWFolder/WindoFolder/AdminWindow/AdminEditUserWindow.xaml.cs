﻿using GoncharovFitnesClub.ClassFolder;
using GoncharovFitnesClub.DataFolder;
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

namespace GoncharovFitnesClub.WindoFolder.AdminWindow
{
    /// <summary>
    /// Логика взаимодействия для AdminEditUserWindow.xaml
    /// </summary>
    public partial class AdminEditUserWindow : Window
    {
        DataGrid dataGrid;
        TextBox MWSearchTB;

        User user = new User();

        int SaveUserID = VarriableClass.UserID;

        public AdminEditUserWindow(DataGrid dataGrid, TextBox MWSearchTB)
        {
            InitializeComponent();

            this.dataGrid = dataGrid;
            this.MWSearchTB = MWSearchTB;

            RoleCB.ItemsSource = DBEntities.GetContext().Role.ToList();
        }


        bool BlockDragWindow = false;

        bool CloseBIsUsing = false;
        bool HideBIsUsing = false;
        bool ResizeIsUsing = false;



        private void MainB_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!BlockDragWindow)
            {
                if (e.LeftButton == MouseButtonState.Pressed &&
                    ToolBarGrid.IsMouseOver)
                {
                    DragMove();
                }
            }
        }

        //------------------Кнопка закрытия приложения----------------------


        private async void CloseB_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CloseBIsUsing = true;

            await DisableDrag(e);

            if (!HideBIsUsing && !ResizeIsUsing && CloseB.IsMouseOver)
            {
                VarriableClass.AddUserWinisUsing = false;
                --VarriableClass.EditWindowCount;

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

        private void AddUserBT_Click(object sender, RoutedEventArgs e)
        {
            var Login = DBEntities.GetContext().User.FirstOrDefault(u => u.Login == LoginTB.Text);

            if (Login != null)
            {
                MBClass.Error("Такой логин уже существует!");
                LoginTB.Focus();

                return;
            }
            try
            {
                user = DBEntities.GetContext().User.FirstOrDefault(u => u.UserID == SaveUserID);

                user.Login = LoginTB.Text;
                user.Password = PasswordTB.Text;
                user.RoleID = (int)RoleCB.SelectedValue;
                

                MBClass.Info("Пользователь успешно отредактирован");

                DBEntities.GetContext().SaveChanges();

                dataGrid.ItemsSource = DBEntities.GetContext().
                        User.Where(u => u.Login.StartsWith(MWSearchTB.Text)
                        || u.Role.NameRole.StartsWith(MWSearchTB.Text)).ToList().OrderBy(u => u.UserID);



            }
            catch (Exception ex)
            {

                MBClass.Error(ex);
            }
        }

        private void EnableButt()
        {
            if (string.IsNullOrWhiteSpace(LoginTB.Text) ||
                string.IsNullOrWhiteSpace(PasswordTB.Text) ||
                RoleCB.SelectedItem == null)
            {
                EditUserBT.IsEnabled = false;
            }
            else
            {
                EditUserBT.IsEnabled = true;
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (EditUserBT.IsEnabled)
                {
                    AddUserBT_Click(sender, e);
                }
                else
                {
                    if (LoginTB.IsFocused)
                    {
                        PasswordTB.Focus();
                    }
                    else if(PasswordTB.IsFocused)
                    {
                        RoleCB.Focus();
                        RoleCB.IsDropDownOpen = true;
                    }
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            user = DBEntities.GetContext().User.FirstOrDefault(u => u.UserID == SaveUserID);

            LoginTB.Text = user.Login;
            PasswordTB.Text = user.Password;
            RoleCB.SelectedValue = user.RoleID;
        }
    }
}