using SalesTaxes.Models;
using SalesTaxes.Services;
using System;
using System.Linq;

namespace SalesTaxes
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Input Items Seperated by \",\" : ");
            string line = Console.ReadLine();

            Console.WriteLine();
            Console.WriteLine(ReceiptService.GetReceipt(line));
            Console.ReadKey();
        }
    }
}
