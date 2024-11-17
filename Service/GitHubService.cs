using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GitHubIntegration.DataEntities;
using Microsoft.VisualBasic.FileIO;
using Octokit;
using Microsoft.Extensions.Options;
using Octokit.Internal;
using System.Net;


namespace GitHubIntegration
{
    public class GitHubService: IGitHubService
    {
        private readonly GitHubClient _client;
        private readonly InMemoryCredentialStore _credentials;
        private readonly GitHubIntegrationOptions _options;
        public GitHubService(IOptions<GitHubIntegrationOptions> options)
        {
            _options = options.Value;
            _credentials = new InMemoryCredentialStore(new Credentials(_options.GitHubToken));
            _client = new GitHubClient(new ProductHeaderValue("my-github-app"), _credentials);
        }
        public async Task<Portfolio> GetUserPortfolio()
        {
            return await Portfolio.GetUserPortfolio(_client);
        }
        public async Task<List<RepositoryDTO>> SearchRepositoriesAsync(string? repoName, Language? language, string? userName)
        {

            var request = new SearchRepositoriesRequest();
            if (repoName != null)
                request = new SearchRepositoriesRequest(repoName);
            if (language != null)
                request.Language = language;
            if (userName != null)
                request.User = userName;

            var result = await _client.Search.SearchRepo(request);
            List<RepositoryDTO> repositories = new List<RepositoryDTO>();
            foreach (var item in result.Items)
            {
                RepositoryDTO repo = new RepositoryDTO(item.Name, item.Language, item.Owner.Login);
                repositories.Add(repo);
            }
            return repositories;
        }
    }
}
