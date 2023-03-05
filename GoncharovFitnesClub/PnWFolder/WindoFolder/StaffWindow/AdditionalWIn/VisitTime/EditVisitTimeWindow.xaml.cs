using GoncharovFitnesClub.ClassFolder;
using GoncharovFitnesClub.DataFolder;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace GoncharovFitnesClub.PnWFolder.WindoFolder.StaffWindow.AdditionalWIn.VisitTime
{
    /// <summary>
    /// Логика взаимодействия для EditVisitTimeWindow.xaml
    /// </summary>
    public partial class EditVisitTimeWindow : Window
    {

        DataFolder.VisitTime visitTime = new DataFolder.VisitTime();

        string oldName;

        public EditVisitTimeWindow()
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


        private void EditVisitTimeBT_Click(object sender, RoutedEventArgs e)
        {
            string[] SplitTime = VisitTimeTB.Text.Split('-');

            TimeSpan SpanStartTime = TimeSpan.Parse(SplitTime[0]);
            TimeSpan spanEndTime = TimeSpan.Parse(SplitTime[1].Trim(' '));

            var Time = DBEntities.GetContext().VisitTime
                .FirstOrDefault(u => u.TimeStart == SpanStartTime && u.TimeEnd == spanEndTime);


            if (Time != null && oldName != VisitTimeTB.Text)
            {
                MBClass.Error("Такой распорядок существует!");
            }
            else
            {
                try
                {

                    visitTime = DBEntities.GetContext().VisitTime.FirstOrDefault(u => u.VisitTimeID == VariableClass.TimeVisitID);


                    visitTime.TimeStart = SpanStartTime;
                    visitTime.TimeEnd = spanEndTime;
    

                    DBEntities.GetContext().SaveChanges();

                    MBClass.Info("Время успешно отредактировано!");

                    oldName = VisitTimeTB.Text;

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
                if (EditVisitTimeBT.IsEnabled)
                {
                    EditVisitTimeBT_Click(sender, e);
                }
            }
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }

        private void EnableButt()
        {
            if (string.IsNullOrWhiteSpace(VisitTimeTB.Text) ||
                 VisitTimeTB.Text.Length < 13)
            {
                EditVisitTimeBT.IsEnabled = false;
            }
            else
            {
                EditVisitTimeBT.IsEnabled = true;
            }
        }

        private void VisitDayTB_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void VisiTimeTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox TimeVisit = VisitTimeTB;


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

            EnableButt();
        }

        private void DeleteBT_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                var TimeIsUsing = DBEntities.GetContext().Subscription.FirstOrDefault(u => u.VisitTimeID == VariableClass.TimeVisitID);

                if (TimeIsUsing != null)
                {
                    MBClass.Error("Данное время установлено в абонементе!\n" +
                        "Убедитесь, что это время отсутствует.");
                }
                else if (MBClass.Question("Вы действительно хотите удалить это время?"))
                {
                    visitTime = DBEntities.GetContext().VisitTime.FirstOrDefault(u => u.VisitTimeID == VariableClass.TimeVisitID);

                    DBEntities.GetContext().VisitTime.Remove(visitTime);

                    DBEntities.GetContext().SaveChanges();

                    Close();
                }
            }
            catch (Exception ex)
            {

                MBClass.Error(ex);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            visitTime = DBEntities.GetContext().VisitTime.FirstOrDefault(u => u.VisitTimeID == VariableClass.TimeVisitID);

            VisitTimeTB.Text = visitTime.TimeStart.ToString().Remove(5) + " - " +
                               visitTime.TimeEnd.ToString().Remove(5);
        }
    }
}


