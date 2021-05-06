using System;
using System.Linq;
using System.Threading.Tasks;

namespace LameScooter
{
    class Program
    {
        static async Task Main(string[] args) {
            var containsNumber = args[0].Any(char.IsDigit);

            if (containsNumber) {
                throw new NotFoundException($"ERROR! format is wrong in {args[0]} you may not input a number...");
            }
            
            ILameScooterRental rental = new OfflineLameScooterRental();
            var count = await rental.GetScooterCountInStation(args[0]);
            
            Console.WriteLine($"Number of scooters in {args[0]}: {count}");
        }
    }
}
