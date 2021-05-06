using System;
using System.Threading.Tasks;

namespace LameScooter
{
    class Program
    {
        static async Task Main(string[] args) {
            ILameScooterRental rental = new OfflineLameScooterRental();
            var count = await rental.GetScooterCountInStation(args[0]);
            Console.WriteLine($"Number of scooters in {args[0]}: {count}");
        }
    }
}
