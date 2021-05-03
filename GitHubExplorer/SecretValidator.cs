using System;
using System.IO;
using System.Text.Json;

namespace GitHubExplorer {
    public static class SecretValidator {
        public static string LoadToken() {
            Secrets secrets;
            if (!File.Exists("secrets.json")) {
                secrets = new Secrets();
                File.WriteAllText("secrets.json", JsonSerializer.Serialize(secrets));
            }
            else {
                secrets = JsonSerializer.Deserialize<Secrets>(File.ReadAllText("secrets.json"));
            }

            if (string.IsNullOrEmpty(secrets.Token)) {
                throw new Exception("ERROR: Needs to define a Token in 'secrets.json' file.");
            }

            return secrets.Token;
        }
    }
}