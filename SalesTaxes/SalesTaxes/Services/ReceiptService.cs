using SalesTaxes.Models;
using System;
using System.Linq;

namespace SalesTaxes.Services
{
    public static class ReceiptService
    {
        // Service to get output for reciept
        public static string GetReceipt(string rawInput)
        {
            try
            {
                // splits Items by the comma
                var rawItems = rawInput.Split(',');

                var res = "";
                var total = 0.0;
                var totalTaxes = 0.0;

                // Creates items from raw imput and groups into 
                var myItems = rawItems.Where(x => !string.IsNullOrWhiteSpace(x))
                    .Select(item => Item.CreateItem(item))
                    .GroupBy(item => item.Name).ToList();


                // creates reciept entry for item or groups of items
                foreach (var itemGroup in myItems)
                {
                    var itemsPrice = itemGroup.First().Price * itemGroup.Count();
                    var itemsTaxes = itemGroup.First().Taxes * itemGroup.Count();

                    total += itemsPrice;
                    totalTaxes += itemsTaxes;

                    res += $"{itemGroup.First().Name}: {itemsPrice + itemsTaxes:0.00}{(itemGroup.Count() > 1 ? $" ({itemGroup.Count()} @ {itemGroup.First().Price + itemGroup.First().Taxes:.00})\r\n" : "\r\n")}";
                }

                res += $"Sales Taxes: {totalTaxes:0.00}\r\nTotal: {total + totalTaxes:0.00}";

                return res;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
    
}
