namespace GitHubExplorer {
    public class UserResponse {
        public string name{get; set; }
        public string company {get; set; }
        public string blog { get; set; }
        public string location { get; set; }
        public string email { get; set; }
        public int public_repos { get; set; }
        public int followers { get; set; }
        public int following { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
    }
}