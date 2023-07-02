/*
Here, we test the JsonSerializer (JavaScript Object Notation) class.
Links:
• https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-jsonhow-to?pivots=dotnet-6-0
Before, a third-party package Newtonsoft.Json used to be popular. However, the new take
of.NET on JsonSerializer provides a faster and built-in (no need for NuGet Package
Manager) solution.However, if you would like to use Newtonsoft.Json: Solution Explorer
→ right-click project name → Manage NuGet Packages... → Browse (in the new opened
tab) → type “Newtonsoft.Json” → Install latest stable (right part). Links:
• http://www.newtonsoft.com/json/help/html/SerializingJSON.htm
• https://stackoverflow.com/questions/6201529/how-do-i-turn-a-c-sharp-object-into-ajson-string-in-net
• https://inspiration.nlogic.ca/en/a-comparison-of-newtonsoft.json-andsystem.text.json
• https://docs.microsoft.com/en-us/nuget/consume-packages/install-use-packagesvisual-studio
*/

using System;
using System.Text.Json; // for JsonSerializer
namespace Ex_08_03
{
    class Program
    {
        static void Main(string[] args)
        {
            Gift myGift = new Gift(.9, 1, "Box", new string[] { "Sam", "Joe" });
            Gift yourGiftA, yourGiftB;
            // Part 1a - object serialization to UTF-16 (string), this is slower
            string giftUtf16 = JsonSerializer.Serialize(myGift);
            Console.WriteLine(giftUtf16);
            // Part 1b - object serialization to UTF-8 (byte array), this is faster
            byte[] giftUtf8 = JsonSerializer.SerializeToUtf8Bytes(myGift);
            foreach (byte b in giftUtf8)
                Console.Write(b);
            Console.Write("\n\n");
            // Part 2a - object deserialization from UTF-16
            yourGiftA = JsonSerializer.Deserialize<Gift>(giftUtf16);
            // Part 2b - object deserialization from UTF-8
            yourGiftB = JsonSerializer.Deserialize<Gift>(giftUtf8);
            // Tests
            myGift.PrintInfo();
            yourGiftA.PrintInfo();
            yourGiftB.PrintInfo();
        }
    }
    /**********************************************************************/
    public class Gift
    {
        private double Price { get; set; } // private/protected isn't serialized!
        public int Quantity { get; set; }
        public string Name { get; set; }
        public string[] Givers { get; set; }
        // Json serialization needs a parameterless constructor
        public Gift() { }
        public Gift(double p, int q, string n, string[] g)
        {
            Price = p;
            Quantity = q;
            Name = n;
            Givers = g;
        }
        public void PrintInfo()
        {
            Console.WriteLine($"{Name}, {Quantity}, {Price} EUR");
            foreach (string s in Givers)
                Console.WriteLine(s);
        }
    }
}