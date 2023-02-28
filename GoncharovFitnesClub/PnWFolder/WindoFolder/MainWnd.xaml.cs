using GoncharovFitnesClub.ClassFolder;
using GoncharovFitnesClub.PageFolder;
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

namespace GoncharovFitnesClub.WindoFolder
{
    /// <summary>
    /// Логика взаимодействия для MainWnd.xaml
    /// </summary>
    public partial class MainWnd : Window
    {
        public MainWnd()
        {
            InitializeComponent();
            MainFrame.Navigate(new AuthorizationPage(ToolMenuB));
        }


        bool BlockDragWindow = false;

        bool CloseBIsUsing = false;
        bool HideBIsUsing = false;
        bool ResizeIsUsing = false;

        private void MainB_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!BlockDragWindow &&
                e.LeftButton == MouseButtonState.Pressed && ToolBarGrid.IsMouseOver)
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


        //------------------Кнопка расширения приложения----------------------

        private async void ResizeB_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ResizeIsUsing = true;

            await DisableDrag(e);

            if (!HideBIsUsing && !CloseBIsUsing && ResizeB.IsMouseOver)
            {
                if (WindowState == WindowState.Normal)
                {
                    WindowState = WindowState.Maximized;
                }
                else
                {
                    WindowState = WindowState.Normal;
                }
            }

            ResizeIsUsing = false;
        }



        private void ResizeB_MouseEnter(object sender, MouseEventArgs e)
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

            if (!HideBIsUsing && !CloseBIsUsing)
            {
                while (ResizeB.IsMouseOver)
                {
                    if (BlockDragWindow)
                    {
                        ResizeB.Background = new SolidColorBrush(Color.FromRgb(150, 150, 150));
                    }
                    else
                    {
                        ResizeB.Background = new SolidColorBrush(Color.FromRgb(211, 211, 211));
                    }
                    await Task.Delay(1);
                }
                ResizeB.Background = null;
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


        private void InfoMI_Click(object sender, RoutedEventArgs e)
        {
            MBClass.VersionInfo();
        }

        private void ChangeUserMI_Click(object sender, RoutedEventArgs e)
        {
            if (MBClass.Question("Вы действительно хотите сменить запись?"))
            {
                MainFrame.Navigate(new AuthorizationPage(ToolMenuB));
                ToolMenuB.Visibility = Visibility.Hidden;


                foreach (Window item in App.Current.Windows)
                {
                    if (item != this)
                        item.Close();
                }

            }
        }



        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Middle)
            {
                foreach (Window item in App.Current.Windows)
                {
                    if (item != this)
                        item.Focus();
                }
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

            if (MBClass.Question("Вы действительно хотите закрыть программу?"))
            {
                App.Current.Shutdown();
            }
            else
            {
                e.Cancel = true;
            }

        }
    }
}
