using System.Threading.Tasks;

namespace LameScooter {
    public class OfflineLameScooterRental : ILameScooterRental {
        public async Task<int> GetScooterCountInStation(string stationName) {
            return await LameScooterList.GetScooterCountFromStation(stationName);
        }
    }
}