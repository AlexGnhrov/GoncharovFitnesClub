using GoncharovFitnesClub.WindoFolder;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace GoncharovFitnesClub
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    /// 
  
    public partial class App : Application
    {
        Mutex mutex;
        App()
        {
            bool createdNew;

            mutex = new Mutex(true, "GoncharovFitnesClub", out createdNew);

            if (!createdNew)
            {
                App.Current.Shutdown();
                App.Current.MainWindow.Focus();
            }
        }


    }
}

