using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GitHubIntegration.DataEntities
{
    public class Portfolio
    {
        private static Portfolio _portfolio = new Portfolio();
        public static List<RepositoryInfo> Repositories { get; set; }
        private Portfolio()
        {
            Repositories = new List<RepositoryInfo>();
        }    
        public static async Task<Portfolio> GetUserPortfolio(GitHubClient client)
        {
            var user = await client.User.Current();
            var username = user.Login;
            var repositories = await client.Repository.GetAllForUser(username);
            foreach (var repo in repositories)
            {
                var repoInfo = new RepositoryInfo(repo, client);
                await repoInfo.Initialize();
                Repositories.Add(repoInfo);
            }
            return _portfolio;
        }
        
    }
}
