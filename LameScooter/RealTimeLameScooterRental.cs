using System.Threading.Tasks;

namespace LameScooter {
    public class RealTimeLameScooterRental : ILameScooterRental {
        public async Task<int> GetScooterCountInStation(string stationName) {
            return await LameScooterList.GetScooterCountRealtime(stationName);
        }
    }
}