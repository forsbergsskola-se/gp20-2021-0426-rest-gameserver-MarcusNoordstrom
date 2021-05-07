using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace LameScooter {
    public static class LameScooterList {

        public static async Task<int> GetScooterCountRealtime(string stationName) {
            var scooterData = await Task.Run(GetScooterRealtimeData);
            scooterData.TryGetValue(stationName, out var count);
            return count;
        }

        static async Task<Dictionary<string, int>> GetScooterRealtimeData() {
            var httpClient = new HttpClient();
            
            var json = await httpClient.GetStringAsync(
                "https://raw.githubusercontent.com/marczaku/GP20-2021-0426-Rest-Gameserver/main/assignments/scooters.json");

            var data = JsonSerializer.Deserialize<JsonElement>(json);
            
            return data.GetProperty("stations").EnumerateArray().ToDictionary<JsonElement, string, int>
            (element => element.GetProperty("name").GetString()
                        ?? throw new Exception("Could not load JSON!"),
                element => element.GetProperty("bikesAvailable").GetInt16());
        }

        public static async Task<int> GetScooterCountFromStation(string stationName) {
            var scooterData = await Task.Run(GetScooterData);
            scooterData.TryGetValue(stationName, out var count);
            return count;
        }

        static async Task<Dictionary<string, int>> GetScooterData() {
            var data = await Task.Run(InitializeJson);

            return data.EnumerateArray().ToDictionary<JsonElement, string, int>
                (element => element.GetProperty("name").GetString()
                            ?? throw new Exception("Could not load JSON!"),
                element => element.GetProperty("bikesAvailable").GetInt16());
        }
        
        static async Task<JsonElement> InitializeJson() {
            var sr = new StreamReader("scooters.json");
            var json = await sr.ReadToEndAsync();
            var data = JsonSerializer.Deserialize<JsonElement>(json);
            return data;
        }   
    }
}