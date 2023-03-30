using GoncharovFitnesClub.ClassFolder;
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

namespace GoncharovFitnesClub.PnWFolder.WindoFolder.StaffWindow.AdditionalWIndow
{
    /// <summary>
    /// Логика взаимодействия для EditSpecialityWindow.xaml
    /// </summary>
    public partial class EditSpecialityWindow : Window
    {
        Speciality speciality = new Speciality();

        string oldName;
        public EditSpecialityWindow()
        {
            InitializeComponent();
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

            if (!CloseBIsUsing && !ResizeIsUsing)
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


        private void EditSpecialityBT_Click(object sender, RoutedEventArgs e)
        {

            var speciality = DBEntities.GetContext().Speciality.FirstOrDefault(u => u.NameSpeciality == SpecialityTB.Text);
            
            if (speciality != null && oldName != SpecialityTB.Text)
            {
                MBClass.Error("Такая специальность существует!");
            }
            else
            {
                try
                {
                    speciality = DBEntities.GetContext().Speciality.FirstOrDefault(u => u.SpecialityID == VariableClass.SpecialityID);

                    speciality.NameSpeciality = SpecialityTB.Text;
                        
                    DBEntities.GetContext().SaveChanges();

                    MBClass.Info("Специальность успешно отредактирована!");

                    oldName = SpecialityTB.Text;


                }
                catch (Exception ex)
                {

                    MBClass.Error(ex);
                }
            }
        }


        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (EditSpecialityBT.IsEnabled)
                {
                    EditSpecialityBT_Click(sender, e);
                }
            }
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }



        private void SpecialityTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SpecialityTB.Text)
)
            {
                EditSpecialityBT.IsEnabled = false;
            }
            else
            {
                EditSpecialityBT.IsEnabled = true;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            speciality = DBEntities.GetContext().Speciality.
                         FirstOrDefault(u => u.SpecialityID == VariableClass.SpecialityID);

            SpecialityTB.Text = oldName = speciality.NameSpeciality;
        }

        private void DeleteBT_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var SpecialityIsUsing = DBEntities.GetContext().Coach.FirstOrDefault(u => u.SpecialityID == VariableClass.SpecialityID);

                if (SpecialityIsUsing != null)
                {
                    MBClass.Error("Данная специальность установлена у тренера!\n" +
                        "Убедитесь, что эта специальность отсутствует.");
                }
                else if (MBClass.Question("Вы действительно хотите удалить эту специальность?"))
                {
                    Speciality speciality = DBEntities.GetContext().Speciality.FirstOrDefault(u => u.SpecialityID == VariableClass.SpecialityID);

                    DBEntities.GetContext().Speciality.Remove(speciality);

                    DBEntities.GetContext().SaveChanges();

                    Close();
                }
            }
            catch (Exception ex)
            {

                MBClass.Error(ex);
            }
        }


    }
}
