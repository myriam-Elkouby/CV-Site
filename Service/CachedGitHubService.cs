using GitHubIntegration.DataEntities;
using Microsoft.Extensions.Caching.Memory;
using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubIntegration
{
    public class CachedGitHubService : IGitHubService
    {
        private readonly IGitHubService _gitHubService;
        private readonly IMemoryCache _memoryCache;

        private const string UserPortfolioKey = "UserPortfolioKey";
        public CachedGitHubService(IGitHubService gitHubService, IMemoryCache memoryCache)
        {
            _gitHubService = gitHubService;
            _memoryCache = memoryCache;
        }
        

        public async Task<Portfolio> GetUserPortfolio()
        {
            if (_memoryCache.TryGetValue(UserPortfolioKey, out Portfolio portfolio))
                return portfolio;

            var cacheOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(5))
                .SetSlidingExpiration(TimeSpan.FromMinutes(3));

            portfolio = await _gitHubService.GetUserPortfolio();
            _memoryCache.Set(UserPortfolioKey, portfolio, cacheOptions);

            return portfolio;

        }
        public async Task<List<RepositoryDTO>> SearchRepositoriesAsync(string? repoName, Language? language, string? userName)
        {
            return await _gitHubService.SearchRepositoriesAsync(repoName, language, userName);
        }
    }
}
