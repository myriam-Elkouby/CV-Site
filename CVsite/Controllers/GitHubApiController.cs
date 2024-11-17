using Microsoft.AspNetCore.Mvc;
using GitHubIntegration;
using Octokit;
using GitHubIntegration.DataEntities;

namespace CVsite.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GitHubApiController : ControllerBase
    {
        private readonly IGitHubService _gitHubService;
        public GitHubApiController(IGitHubService gitHubService)
        {
            _gitHubService = gitHubService;
        }

        [HttpGet("/GetPortfolio")]
        public async Task<ActionResult> GetPortfolio()
        {
            var result = await _gitHubService.GetUserPortfolio();
            if (result != null)
                return Ok(Portfolio.Repositories);
            return NoContent();
        }
        [HttpGet("/SearchRepositories")]
        public async Task<ActionResult>  SearchRepositories(string? repoName, Language? language,string? userName)
        {
            var repositories = await _gitHubService.SearchRepositoriesAsync(repoName, language, userName);

            if (repositories!=null)
            {
                return Ok(repositories);
            }
            return NoContent();
        }




    }
}
