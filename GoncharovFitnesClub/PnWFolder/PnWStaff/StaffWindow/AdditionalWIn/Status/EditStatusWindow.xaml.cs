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

namespace GoncharovFitnesClub.PnWFolder.WindoFolder.StaffWindow.AdditionalWIndow.StatusWindow
{
    /// <summary>
    /// Логика взаимодействия для EditStatusWindow.xaml
    /// </summary>
    public partial class EditStatusWindow : Window
    {

        Status status = new Status();

        string oldName;

        public EditStatusWindow()
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


        private void EditStatusBT_Click(object sender, RoutedEventArgs e)
        {

            var status = DBEntities.GetContext().Status.FirstOrDefault(u => u.NameStatus == StatusTB.Text);


            if (status != null && oldName != StatusTB.Text)
            {
                MBClass.Error("Такой статус существует!");
            }
            else
            {
                try
                {
                    status = DBEntities.GetContext().Status.FirstOrDefault(u => u.StatusID == VariableClass.StatusID);

                    status.NameStatus = StatusTB.Text;

                    DBEntities.GetContext().SaveChanges();

                    MBClass.Info("Статус успешно отредактирован!");

                    oldName = StatusTB.Text;

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
                if (EditStatusBT.IsEnabled)
                {
                    EditStatusBT_Click(sender, e);
                }
            }
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }



        private void StatusTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(StatusTB.Text)
)
            {
                EditStatusBT.IsEnabled = false;
            }
            else
            {
                EditStatusBT.IsEnabled = true;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            status = DBEntities.GetContext().Status.FirstOrDefault(u => u.StatusID == VariableClass.StatusID);


            if (status.StatusID == 1 || status.StatusID == 2 ||
                status.StatusID == 7)
                DeleteBT.IsEnabled = false;


            StatusTB.Text = oldName = status.NameStatus;
        }

        private void DeleteBT_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var StatusIsUsing = DBEntities.GetContext().Client.FirstOrDefault(u => u.StatusID == VariableClass.StatusID);

                if (StatusIsUsing != null)
                {
                    MBClass.Error("Данный статус установлен у клиента!\n" +
                        "Убедитесь, что этот статус отсутствует.");
                }
                else if(MBClass.Question("Вы действительно хотите удалить этот статус?"))
                {
                    Status status = DBEntities.GetContext().Status.FirstOrDefault(u => u.StatusID == VariableClass.StatusID);

                    DBEntities.GetContext().Status.Remove(status);

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
