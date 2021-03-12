using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTaxes.Models
{
    public class Item
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public double Taxes { get; set; }

        private static readonly double _salesTax = 0.1;
        private static readonly double _importedTax = 0.05;

        public static List<string> Foods = new List<string>();
        public static List<string> MedProducts = new List<string>();

        public Item()
        {
            GetFoods();
        }

        public static Item CreateItem(string input)
        {
            try
            {
                // splits line to get items word for word
                var inputParts = input.Split(' ');

                // if less than 3 words cant get format {name} at {price}
                if (inputParts.Length < 3)
                    throw new Exception("Item string not in correct format");

                // checks if format has 1 before name of item
                var nameSections = inputParts.Where(x => int.TryParse(inputParts[0], out _) 
                    ? Array.IndexOf(inputParts, x) > 0 && Array.IndexOf(inputParts, x) <= inputParts.Length - 3
                    :  Array.IndexOf(inputParts, x) <= inputParts.Length - 3);

                // puts name back together
                var name = nameSections.Aggregate((i, j) => $"{i} {j}");
                var price = double.Parse(inputParts.Last());
                return new Item
                {
                    Name = name,
                    Price = price,
                    Taxes = GetTaxes(name, price, nameSections.First().ToLower() == "imported")
                };
            }
            catch (Exception e)
            {
                throw new Exception($"{e.Message}");
            }
        }

        private static double GetTaxes(string name, double price, bool isImported)
        {
            // Check if its a book or a food item
            var isFood = name.ToLower().Split(' ').Any(x => Foods.Any(f => x.Contains(f)));
            var isMed = name.ToLower().Split(' ').Any(x => MedProducts.Any(m => x.Contains(m)));
            var isBook = name.ToLower() == "book";

            var sales = isBook || isFood || isMed 
                ? 0.0 
                : price * _salesTax;
            var imported = price * _importedTax;

            // rounds to the nearest 0.05
            var rSales = Math.Ceiling(sales * 20) / 20;
            var rImported = Math.Ceiling(imported * 20) / 20;

            return isImported
                ? rSales + rImported
                : rSales;
        }

        private static void GetFoods()
        {
            // Gets list of items from text files
            var foods = new StreamReader(@"Foods.txt");
            var meds = new StreamReader(@"MedicalProducts.txt");
            Foods.AddRange(foods.ReadToEnd().Split(new [] { "\r\n" }, StringSplitOptions.None).ToArray());
            MedProducts.AddRange(meds.ReadToEnd().Split(new [] { "\r\n" }, StringSplitOptions.None).ToArray());
            foods.Close();
            meds.Close();
        }
    }
}
