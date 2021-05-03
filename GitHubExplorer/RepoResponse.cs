using System.Collections.Generic;
using System.Text.Json;

namespace GitHubExplorer {
    public class RepoResponse {
        public List<string> names;
        public List<string> urls;
        public List<string> descriptions;

        public RepoResponse(JsonElement deserializedJson) {
            names = new List<string>();
            urls = new List<string>();
            descriptions = new List<string>();
            foreach (var element in deserializedJson.EnumerateArray()) {
                names.Add(element.GetProperty("name").GetString());
                urls.Add(element.GetProperty("url").GetString());
                descriptions.Add(element.GetProperty("description").GetString());
            }
        }
    }
}