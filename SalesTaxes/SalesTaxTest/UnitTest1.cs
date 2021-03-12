using Microsoft.VisualStudio.TestTools.UnitTesting;
using SalesTaxes.Services;
using System;

namespace SalesTaxTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test()
        {
            var input = "Book at 12.49,1 Book at 12.49,1 Music CD at 14.99,1 Chocolate Bar at 0.85";
            //var input = "1 Imported box of chocolates at 10.00\r\n1 Imported bottle of perfume at 47.50";
            //var input = "1 Imported bottle of perfume at 47.50";
            //var input = "1 Imported bottle of perfume at 27.99\r\n1 Bottle of perfume at 18.99\r\nPacket of headache pills at 9.75\r\nImported box of chocolates at 11.25\r\n1 Imported box of chocolates at 11.25";
            //var input = "Imported box of chocolates at 11.25\r\n1 Imported box of chocolates at 11.25";

            var expectedOutput = "Book: 24.98 (2 @ 12.49)\r\nMusic CD: 16.49\r\nChocolate bar: 0.85\r\nSales Taxes: 1.50\r\nTotal: 42.32";
            Assert.AreEqual(expectedOutput, ReceiptService.GetReceipt(input));
        }
    }
}
