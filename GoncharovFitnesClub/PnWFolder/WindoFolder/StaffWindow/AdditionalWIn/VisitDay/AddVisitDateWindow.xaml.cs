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

namespace GoncharovFitnesClub.PnWFolder.WindoFolder.StaffWindow.AdditionalWIndow.VisitDay
{
    /// <summary>
    /// Логика взаимодействия для AddVisitDateWindow.xaml
    /// </summary>
    public partial class AddVisitDateWindow : Window
    {
        public AddVisitDateWindow()
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


        private void AddVisitDayBT_Click(object sender, RoutedEventArgs e)
        {

            var status = DBEntities.GetContext().VisitDate.FirstOrDefault(u => u.DayOfVisit == VisitDayTB.Text);

            if (status != null)
            {
                MBClass.Error("Такой распорядок существует!");

            }
            else
            {
                try
                {
                    DBEntities.GetContext().VisitDate.Add(new VisitDate()
                    {
                        DayOfVisit = VisitDayTB.Text
                    });

                    DBEntities.GetContext().SaveChanges();

                    MBClass.Info("Распорядок успешно добавлена!");

                    VariableClass.newVisitDayCreated = true;
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
                if (AddVisitDayBT.IsEnabled)
                {
                    AddVisitDayBT_Click(sender, e);
                }
            }
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }



        private void VisitDayBT_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(VisitDayTB.Text)
)
            {
                AddVisitDayBT.IsEnabled = false;
            }
            else
            {
                AddVisitDayBT.IsEnabled = true;
            }
        }

        //VisitDate visitDate = new VisitDate();

        //private void Window_Loaded(object sender, RoutedEventArgs e)
        //{
        //    if (isEditWin)
        //    {
        //        visitDate = DBEntities.GetContext().VisitDate.FirstOrDefault(u => u.VisitDateID == VariableClass.StatusID);

        //        VisitDayTB.Text = oldName = visitDate.DayOfVisit;
        //    }
        //}
    }
}