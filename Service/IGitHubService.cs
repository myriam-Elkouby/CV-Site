using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GitHubIntegration.DataEntities;
using Octokit;

namespace GitHubIntegration
{
    public interface IGitHubService
    {
        public Task<Portfolio> GetUserPortfolio();
        public Task<List<RepositoryDTO>> SearchRepositoriesAsync(string? repoName, Language? language, string? userName);
    }
}
