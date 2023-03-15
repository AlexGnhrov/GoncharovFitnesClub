using GoncharovFitnesClub.ClassFolder;
using GoncharovFitnesClub.DataFolder;
using GoncharovFitnesClub.PnWFolder.WindoFolder.StaffWindow.AdditionalWIndow;
using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace GoncharovFitnesClub.PnWFolder.WindoFolder.StaffWindow
{
    /// <summary>
    /// Логика взаимодействия для AddCoachWindow.xaml
    /// </summary>
    public partial class AddCoachWindow : Window
    {
        OpenFileDialog openFileDialog;

        DataGrid MWListCoach;
        Button MWaddBT;
        TextBox MWSearchTB;
        TabItem MWCoachTI;

        string selectedFileName = "";

        public AddCoachWindow(DataGrid MWListCoach, Button MWaddBT, TextBox MWSearchTB, TabItem MWCoachTI)
        {
            InitializeComponent();

            this.MWListCoach = MWListCoach;
            this.MWaddBT = MWaddBT;
            this.MWSearchTB = MWSearchTB;
            this.MWCoachTI = MWCoachTI;

            PhoneTB.Text = "+7 ";
            PhoneTB.CaretIndex = 4;

            SpecialityCB.ItemsSource = DBEntities.GetContext().Speciality.ToList().OrderBy(u => u.SpecialityID);


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
            string[] SplitSNP = SNPCoachTB.Text.Split(' ');

            try
            {
                Coach coach = new Coach();

                if (selectedFileName != "")
                    coach.Photo = LoadAndReadImage.ConvertImageToByteArray(selectedFileName);


                coach.Surname = SplitSNP[0];
                coach.Name = SplitSNP[1];
                if (SplitSNP.Length == 3) 
                    coach.Patronymic = SplitSNP[2];

                coach.PhoneNum = PhoneTB.Text;
                coach.Email = EmailTB.Text;
                coach.SpecialityID = (int)SpecialityCB.SelectedValue;



                DBEntities.GetContext().Coach.Add(coach);

                MBClass.Info("Тренер успешно добавлен!");

                DBEntities.GetContext().SaveChanges();

                if (MWCoachTI.IsSelected)
                {

                    MWListCoach.ItemsSource = DBEntities.GetContext().Coach.Where(u => u.Surname.StartsWith(MWSearchTB.Text)
                            || u.Name.StartsWith(MWSearchTB.Text)
                            || u.Patronymic.StartsWith(MWSearchTB.Text)
                            || u.Speciality.NameSpeciality.StartsWith(MWSearchTB.Text))
                            .ToList().OrderBy(u => u.CoachID);
                }
            }
            catch (Exception ex)
            {

                MBClass.Error(ex);
            }
        }

        private void EnableButt()
        {
            string[] SplitSNP = SNPCoachTB.Text.Split(' ');

            if (string.IsNullOrWhiteSpace(SNPCoachTB.Text) ||
                string.IsNullOrWhiteSpace(PhoneTB.Text) ||
                SpecialityCB.SelectedItem == null ||
                PhoneTB.Text.Length < 16 ||
                 (SplitSNP.Length == 1 ||
                 SplitSNP.Length == 2 && SplitSNP[1] == "" ||
                 SplitSNP.Length == 3 && SplitSNP[2] == ""))
            {
                AddCoachBT.IsEnabled = false;
            }
            else
            {
                AddCoachBT.IsEnabled = true;
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (AddCoachBT.IsEnabled)
                {
                    AddCoachBT_Click(sender, e);
                }
                else
                {
                    if (SNPCoachTB.IsFocused)
                    {
                        PhoneTB.Focus();
                    }
                    else if (PhoneTB.IsFocused)
                    {
                       EmailTB.Focus();
                    }
                    else if (EmailTB.IsFocused)
                    {
                        SpecialityCB.Focus();
                        SpecialityCB.IsDropDownOpen = true;
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
            string[] SplitSNP = SNPCoachTB.Text.Split(' ');
            int strlen = SNPCoachTB.Text.Length;

            if (SplitSNP.Length > 3)
            {
                SNPCoachTB.Text = SNPCoachTB.Text.Remove(strlen - 1);
                SNPCoachTB.CaretIndex = strlen;
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

        


        private void SpecialityCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EnableButt();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AddPhoto();
        }

        private void AddPhoto()
        {
            openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png *.jpeg)|*.png;*.jpeg";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);


            Coach coach = new Coach();
            if (openFileDialog.ShowDialog() == true)
            {
                selectedFileName = openFileDialog.FileName;

                PhotoIB.ImageSource = 
                    LoadAndReadImage.ConvertByteArrayImage(LoadAndReadImage.ConvertImageToByteArray(selectedFileName));


            }
        }

        private void AddSpecialityBT_Click(object sender, RoutedEventArgs e)
        {
            new AddSpecialityWindow().ShowDialog();

            if(VariableClass.newSpecialityCreated)
            {
                SpecialityCB.ItemsSource = DBEntities.GetContext().Speciality.ToList().OrderBy(u => u.SpecialityID);

                SpecialityCB.SelectedIndex = SpecialityCB.Items.Count - 1;
                VariableClass.newSpecialityCreated = false;

            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            VariableClass.CoachWinisUsing = false;


            if (MWCoachTI.IsSelected)
            {
                MWaddBT.IsEnabled = true;
            }
        }

        private void SpecialityCB_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Right && SpecialityCB.SelectedValue != null)
            {

                VariableClass.SpecialityID = (int)SpecialityCB.SelectedValue;
                new EditSpecialityWindow().ShowDialog();

                SpecialityCB.ItemsSource = DBEntities.GetContext().Speciality.ToList().OrderBy(u => u.SpecialityID);

            }
        }

        private void PhoneTB_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }
    }
}
