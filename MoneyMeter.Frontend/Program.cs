using MoneyMeter.Logic;
using System;

namespace MoneyMeter.Frontend
{
    class Program
    {
        static void Main(string[] args)
        {
            var balance = new Balance("Money.txt");
            var todoList = new TodoList("Todo.txt");
            var mainWindow = new MainWindow(balance, todoList);
        }
    }
}
