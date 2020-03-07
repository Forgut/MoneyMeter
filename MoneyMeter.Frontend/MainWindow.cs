using MoneyMeter.Logic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MoneyMeter.Frontend
{
    public class MainWindow
    {
        private Balance _balance;
        private void PrintCommands()
        {
            Console.WriteLine("List of commands:");
            Console.WriteLine("help                  shows this list");
            Console.WriteLine("income <value>        adds value to balance with type of income");
            Console.WriteLine("outcome <value>       adds outcome to balance with type selected (default unknown)");
            Console.WriteLine("status                prints actual balance status");
            Console.WriteLine("save                  saves file content");
            Console.WriteLine("clear                 clears current file");
            Console.WriteLine("history               prints history of transactions");
            Console.WriteLine("exit                  exits program");
        }
        private void PrintLogo()
        {
            var previousColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine
                (
                @" __       __                                          __       __              __                         " + "\n" +
                @"/  \     /  |                                        /  \     /  |            /  |                        " + "\n" +
                @"$$  \   /$$ |  ______   _______    ______   __    __ $$  \   /$$ |  ______   _$$ |_     ______    ______  " + "\n" +
                @"$$$  \ /$$$ | /      \ /       \  /      \ /  |  /  |$$$  \ /$$$ | /      \ / $$   |   /      \  /      \ " + "\n" +
                @"$$$$  /$$$$ |/$$$$$$  |$$$$$$$  |/$$$$$$  |$$ |  $$ |$$$$  /$$$$ |/$$$$$$  |$$$$$$/   /$$$$$$  |/$$$$$$  |" + "\n" +
                @"$$ $$ $$/$$ |$$ |  $$ |$$ |  $$ |$$    $$ |$$ |  $$ |$$ $$ $$/$$ |$$    $$ |  $$ | __ $$    $$ |$$ |  $$/ " + "\n" +
                @"$$ |$$$/ $$ |$$ \__$$ |$$ |  $$ |$$$$$$$$/ $$ \__$$ |$$ |$$$/ $$ |$$$$$$$$/   $$ |/  |$$$$$$$$/ $$ |      " + "\n" +
                @"$$ | $/  $$ |$$    $$/ $$ |  $$ |$$       |$$    $$ |$$ | $/  $$ |$$       |  $$  $$/ $$       |$$ |      " + "\n" +
                @"$$/      $$/  $$$$$$/  $$/   $$/  $$$$$$$/  $$$$$$$ |$$/      $$/  $$$$$$$/    $$$$/   $$$$$$$/ $$/       " + "\n" +
                @"                                           /  \__$$ |                                                     " + "\n" +
                @"                                           $$    $$/                                                      " + "\n" +
                @"                                            $$$$$$/                                                       "
                );
            Console.ForegroundColor = previousColor;
        }
        private void AddValueToBalance(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                Console.WriteLine("Incorrect parameter");
                return;
            }
            if (decimal.TryParse(value, out decimal result))
                _balance.AddDailyValue(result);
            else
                Console.WriteLine("Couldn't parse data");
        }
        private void AddOutcomeToBalance(string value, string type = null)
        {
            if (string.IsNullOrEmpty(value))
            {
                Console.WriteLine("Incorrect parameter");
                return;
            }
            if (decimal.TryParse(value, out decimal result))
                _balance.AddSpentMoney(result);
            else
                Console.WriteLine("Couldn't parse data");
        }
        private void ShowStatus()
        {
            var previousColor = Console.ForegroundColor;
            Console.WriteLine("MaxValue: " + _balance.MaxValue);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("SpentMoney: " + _balance.SpentMoney);
            Console.ForegroundColor = previousColor;
            if (_balance.MoneyToSpare < 0)
                Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("MoneyToSpare " + _balance.MoneyToSpare);
            Console.ForegroundColor = previousColor;
        }
        public void SaveFile()
        {
            Console.Write("Saving file...");
            _balance.OverrideFileValues();
            Console.WriteLine(" succeded!");
        }
        private void ClearFile()
        {
            Console.WriteLine("Are you sure, you want to clear content of this file? [Y/N]");
            bool stop = false;
            while (!stop)
            {
                var result = Console.ReadLine();
                switch (result)
                {
                    case "Y":
                        Console.WriteLine("Erasing content of file...");
                        _balance.ClearFile();
                        Console.WriteLine(" succeded");
                        stop = true;
                        break;
                    case "N":
                        stop = true;
                        Console.WriteLine("Ok.");
                        break;
                    default:
                        Console.WriteLine("I need clear answear. Yes or No");
                        break;

                }
            }

        }
        private void RandomMessage()
        {
            Console.WriteLine("Sorry, I can't understand you");
        }
        private void PrintTransactionsHistory()
        {
            var previousColor = Console.ForegroundColor;
            foreach (var operation in _balance.Operations)
            {
                if (operation.Category == ECategory.Income)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("+");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("-");
                }
                Console.WriteLine(operation.Value);
            }
            Console.ForegroundColor = previousColor;
            Console.WriteLine("----");
            Console.WriteLine(_balance.MoneyToSpare);
        }
        private string StandarizeInput(string input)
        {
            return input.ToLower().Trim(' ').Trim('\n');
        }
        public MainWindow(Balance balance)
        {
            _balance = balance;
            PrintLogo();
            bool exit = false;
            while (!exit)
            {
                Console.Write(" > ");
                var result = StandarizeInput(Console.ReadLine()).Split(" ");
                switch (result[0])
                {
                    case "help":
                        PrintCommands();
                        break;
                    case "income":
                        AddValueToBalance(result.Length > 1 ? result[1] : null);
                        break;
                    case "outcome":
                        AddOutcomeToBalance(result[1], result.Length > 2 ? result[2] : null);
                        break;
                    case "status":
                        ShowStatus();
                        break;
                    case "save":
                        SaveFile();
                        break;
                    case "clear":
                        ClearFile();
                        break;
                    case "history":
                        PrintTransactionsHistory();
                        break;
                    case "exit":
                        exit = true;
                        break;
                    default:
                        RandomMessage();
                        break;
                }
            }
        }
    }
}
