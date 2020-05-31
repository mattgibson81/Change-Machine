using System;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Change Machine Test")]
namespace Change_Machine
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.TreatControlCAsInput = false;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Enter Ctrl+C to quit at any time.");
            requestNewInput();
        }

        private static void requestNewInput()
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("------------------------------------------------");

                //Get currency
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(string.Format("Enter currency ({0})...", CalculateChangeFactory.GetAvailableCurrencies()));
                Console.ForegroundColor = ConsoleColor.White;
                string currency = Console.ReadLine();
                Console.WriteLine();

                //Get total cost
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Enter total cost...");
                Console.ForegroundColor = ConsoleColor.White;
                string totalCost = Console.ReadLine();
                Console.WriteLine();

                //Get amount given
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Enter total amount received...");
                Console.ForegroundColor = ConsoleColor.White;
                string totalAmountReceived = Console.ReadLine();
                Console.WriteLine();

                //Calculate change
                CalculateChangeFactory calculateChange = new CalculateChangeFactory();
                calculateChange.CalculateChangeToGive(currency, totalCost, totalAmountReceived);

                Console.ForegroundColor = ConsoleColor.Yellow;

                //Display total change
                decimal totalChange = calculateChange.TotalChange;
                Console.WriteLine(string.Format("Total Change = {0}{1}", calculateChange.CurrencySymbol, totalChange));
                if (totalChange > 0)
                {
                    Console.WriteLine();

                    //Display denominations - using LINQ to filter out denominations without any quantity
                    var denominations = from denomination in calculateChange.Denominations
                                        where denomination.Quantity > 0
                                        orderby denomination.Amount descending
                                        select denomination;
                    foreach (var item in denominations)
                    {
                        Console.WriteLine(string.Format("{0} x {1}{2} = {1}{3}", item.Quantity, calculateChange.CurrencySymbol, item.Amount, item.Total));
                    }
                }
            }
            catch (CalculateChangeException e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
                Console.WriteLine("Please try again.");
            }
            finally
            {
                Console.WriteLine();
                requestNewInput();
            }
        }
    }
}
