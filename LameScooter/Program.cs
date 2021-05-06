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

            if (args.Length > 1) {
                if (args[1] == "offline" || args[1] == "Offline") {
                    ILameScooterRental rental = new OfflineLameScooterRental();
                    var count = await rental.GetScooterCountInStation(args[0]);
            
                    Console.WriteLine($"Number of scooters in {args[0]}: {count}");
                }
                else if (args[1] == "deprecated" || args[1] == "Deprecated") {
                    ILameScooterRental rental = new DeprecatedLameScooterRental();
                    var count = await rental.GetScooterCountInStation(args[0]);
                
                    Console.WriteLine($"Number of scooters in {args[0]}: {count}");
                    Console.WriteLine("THIS IS DEPRECATED! , consider using -offline or -realtime");
                }
                else if (args[1] == "realtime" || args[1] == "Realtime") {
                    ILameScooterRental rental = new RealTimeLameScooterRental();
                    var count = await rental.GetScooterCountInStation(args[0]);
                
                    Console.WriteLine($"Number of scooters in {args[0]}: {count}");
                }
            }
            else {
                ILameScooterRental rental = new OfflineLameScooterRental();
                var count = await rental.GetScooterCountInStation(args[0]);
            
                Console.WriteLine($"Number of scooters in {args[0]}: {count}");
            }
        }
    }
}
