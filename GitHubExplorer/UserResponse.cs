using System.Collections;
using System.Collections.Generic;

namespace GitHubExplorer {
    public class UserResponse {
        public string name { get; set; }
        public string company { get; set; }
        public string blog { get; set; }
        public string location { get; set; }
        public string email { get; set; }
        public int public_repos { get; set; }
        public int followers { get; set; }
        public int following { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        
        public UserInfo info => new UserInfo(this);
    }
    
    public class UserInfo : UserResponse, IEnumerable, IEnumerator{
        int current = -1;
        List<string> infos = new List<string>();
        UserResponse response;
        public UserInfo(UserResponse Response) {
            response = Response;
        }

        public IEnumerator GetEnumerator() {
            if (!string.IsNullOrEmpty(response.name)) {
                infos.Add($"Name: {response.name}");
            }
            else infos.Add("Name: Name was not found / User doesn't exist.");
            
            if (!string.IsNullOrEmpty(response.company)) {
                infos.Add($"Company: {response.company}");
            }
            else infos.Add("Company: unemployed.");

            if (!string.IsNullOrEmpty(response.blog)) {
                infos.Add($"Blog: {response.blog}");
            }
            else infos.Add("Blog: user has no blog.");

            if (!string.IsNullOrEmpty(response.location)) {
                infos.Add($"Location: {response.location}");
            }
            else infos.Add("Location: unknown.");

            if (!string.IsNullOrEmpty(response.email)) {
                infos.Add($"Email: {response.email}");
            }
            else infos.Add("Email: unknown..");

            if (!string.IsNullOrEmpty(response.public_repos.ToString())) {
                infos.Add($"Repos: {response.public_repos}");
            }
            else infos.Add("Repos: user has no public repositories.");

            if (!string.IsNullOrEmpty(response.followers.ToString())) {
                infos.Add($"Followers: {response.followers.ToString()}");
            }
            else infos.Add("Followers: 0");

            if (!string.IsNullOrEmpty(response.following.ToString())) {
                infos.Add($"Following: {response.following}");
            }
            else infos.Add("Following: 0");

            if (!string.IsNullOrEmpty(response.created_at)) {
                infos.Add($"Joined: {response.created_at}");
            }
            else infos.Add("Joined: unknown.");

            if (!string.IsNullOrEmpty(response.updated_at)) {
                infos.Add($"Last updated: {response.updated_at}");
            }
            else infos.Add("Last updated: unknown.");

            return this;
        }

        public bool MoveNext() {
            current++;
            return current < infos.Count;
        }

        public void Reset() {
            current = 0;
        }

        public object Current => infos[current];
    }
}