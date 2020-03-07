using MoneyMeter.Logic;
using System;

namespace MoneyMeter.Frontend
{
    class Program
    {
        static void Main(string[] args)
        {
            var balance = new Balance("Data.txt");
            var mainWindow = new MainWindow(balance);
        }
    }
}
