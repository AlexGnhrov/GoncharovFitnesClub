using GoncharovFitnesClub.ClassFolder;
using GoncharovFitnesClub.DataFolder;
using GoncharovFitnesClub.WindoFolder.AdminWindow;
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

namespace GoncharovFitnesClub.PnWFolder.WindoFolder.AdminWin.UserData
{
    /// <summary>
    /// Логика взаимодействия для EditStaffWindow.xaml
    /// </summary>
    public partial class EditStaffWindow : Window
    {
        DataGrid MWListStaffDG;
        TextBox MWSearchTB;
        Label MWCountLabel;

        int SaveUsing;

        public EditStaffWindow(DataGrid MWListStaffDG, TextBox MWSearchTB, Label MWCountLabel)
        {
            InitializeComponent();

            this.MWListStaffDG = MWListStaffDG;
            this.MWSearchTB = MWSearchTB;
            this.MWCountLabel = MWCountLabel;

            PhoneTB.Text = "+7 ";
            PhoneTB.CaretIndex = 4;
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

        private void AddCoachBT_Click(object sender, RoutedEventArgs e)
        {
            string[] SplitSNP = SNPStaffTB.Text.Split(' ');

            try
            {
                Staff staff = DBEntities.GetContext().Staff.FirstOrDefault(u => u.StaffID == VariableClass.StaffID);


                staff.Surname = SplitSNP[0];
                staff.Name = SplitSNP[1];
                if (SplitSNP.Length == 3)
                    staff.Patronymic = SplitSNP[2];

                staff.PhoneNum = PhoneTB.Text;
                staff.Email = EmailTB.Text;
                staff.UserID = SaveUsing = (int)UserDataCB.SelectedValue;



                DBEntities.GetContext().SaveChanges();

                MBClass.Info("Сотрудник успешно отредактирован!");


                MWListStaffDG.ItemsSource = DBEntities.GetContext().
                                        Staff.Where(u => u.Surname.StartsWith(MWSearchTB.Text) ||
                                                    u.Name.StartsWith(MWSearchTB.Text) ||
                                                    u.Patronymic.StartsWith(MWSearchTB.Text) ||
                                                    u.User.Login.StartsWith(MWSearchTB.Text) ||
                                                    u.User.Role.NameRole.StartsWith(MWSearchTB.Text))
                                                    .ToList().OrderBy(u => u.StaffID);


                MWCountLabel.Content = "Колиество сотрудников: " + DBEntities.GetContext().Staff.ToArray().Length;
            }
            catch (Exception ex)
            {

                MBClass.Error(ex);
            }
        }

        private void EnableButt()
        {
            string[] SplitSNP = SNPStaffTB.Text.Split(' ');

            if (string.IsNullOrWhiteSpace(SNPStaffTB.Text) ||
                string.IsNullOrWhiteSpace(PhoneTB.Text) ||
                UserDataCB.SelectedValue == null ||
                PhoneTB.Text.Length < 16 ||
                 (SplitSNP.Length == 1 ||
                 SplitSNP.Length == 2 && SplitSNP[1] == "" ||
                 SplitSNP.Length == 3 && SplitSNP[2] == ""))
            {
                AddStaffBT.IsEnabled = false;
            }
            else
            {
                AddStaffBT.IsEnabled = true;
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (AddStaffBT.IsEnabled)
                {
                    AddCoachBT_Click(sender, e);
                }
                else
                {
                    if (SNPStaffTB.IsFocused)
                    {
                        PhoneTB.Focus();
                    }
                    else if (PhoneTB.IsFocused)
                    {
                        EmailTB.Focus();
                    }
                    else if (EmailTB.IsFocused)
                    {
                        UserDataCB.Focus();
                        UserDataCB.IsDropDownOpen = true;
                    }
                }
            }
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }

        private void SNPCoachB_TextChanged(object sender, TextChangedEventArgs e)
        {
            string[] SplitSNP = SNPStaffTB.Text.Split(' ');
            int strlen = SNPStaffTB.Text.Length;

            if (SplitSNP.Length > 3)
            {
                SNPStaffTB.Text = SNPStaffTB.Text.Remove(strlen - 1);
                SNPStaffTB.CaretIndex = strlen;
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




        private void UserDataCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateLoadUserData();

            EnableButt();
        }



        private void AddUserDataBT_Click(object sender, RoutedEventArgs e)
        {
            new AdminAddUserWindow().ShowDialog();

            if (VariableClass.newUserDataCreated)
            {
                UpdateLoadUserData();

                UserDataCB.SelectedIndex = UserDataCB.Items.Count - 1;
                VariableClass.newUserDataCreated = false;

            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {

            if (SaveUsing != null)
            {
                User user = DBEntities.GetContext().User.FirstOrDefault(u => u.UserID == SaveUsing);

                user.IsUsing = true;
                DBEntities.GetContext().SaveChanges();
            }

            VariableClass.StaffID = 0;


        }

        private void UserDataCB_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Right && UserDataCB.SelectedValue != null)
            {

                VariableClass.UserID = (int)UserDataCB.SelectedValue;
                new AdminEditUserWindow(MWListStaffDG, MWSearchTB, MWCountLabel).ShowDialog();

                UpdateLoadUserData();


            }
        }

        private void PhoneTB_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            Staff staff = DBEntities.GetContext().Staff.FirstOrDefault(u => u.StaffID == VariableClass.StaffID);

            User user = DBEntities.GetContext().User.FirstOrDefault(u => u.UserID == staff.UserID);

            user.IsUsing = false;

            DBEntities.GetContext().SaveChanges();


            UpdateLoadUserData();

            LoadStaffData(staff);


        }

        private void UserDataCB_GotFocus(object sender, RoutedEventArgs e)
        {
            UpdateLoadUserData();
        }

        private void UpdateLoadUserData()
        {
            UserDataCB.ItemsSource = DBEntities.GetContext().
                User.Where(u => u.IsUsing == false).ToList().OrderBy(u => u.UserID);
        }

        private void LoadStaffData(Staff staff)
        {
            SNPStaffTB.Text = staff.Surname + " " + staff.Name;
            if (staff.Patronymic != null)
                SNPStaffTB.Text += " " + staff.Patronymic;

            PhoneTB.Text = staff.PhoneNum;
            EmailTB.Text = staff.Email;

            UserDataCB.SelectedValue = SaveUsing = (int)staff.UserID;
        }
    }
}
