using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GoncharovFitnesClub.ClassFolder
{
    class MBClass
    {
        public static void Error(string text)
        {
            MessageBox.Show(text, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static void Error(Exception ex)
        {
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static void Info(string text)
        {
            MessageBox.Show(text, "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public static void VersionInfo()
        {
            MessageBox.Show("Создатель: Гончаров А.Д.\n\n" +
                            "Верссия: 0.72\n" +
                            "Дата: 28:02:2023",
                            "Создатели", MessageBoxButton.OK, MessageBoxImage.None);
        }


        public static bool Question(string text)
        {
            return MessageBoxResult.Yes ==
                  MessageBox.Show(text, "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Information);
        }


    }
}
