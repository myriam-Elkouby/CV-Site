using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Octokit;

namespace GitHubIntegration.DataEntities
{
    public class RepositoryDTO
    {
        public string? Name { get; set; }
        public string? Language { get; set; }
        public string? UserName { get; set; }
        public RepositoryDTO(string? name, string? language, string? userName)
        {
            Name = name;
            Language = language;
            UserName = userName;
        }

    }
}
