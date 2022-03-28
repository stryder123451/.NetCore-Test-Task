using LocalApi.Models;
using LocalApi.Models.Context;
using LocalApi.Models.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LocalApi.ViewModels
{
    public class RepositoryManager : IRepository
    {
        private GitHubContext _context;
        public RepositoryManager(GitHubContext context)
        {
            _context = context;
        }

        public List<JsonRequest> CreateRepositoriesList(Root request)
        {
            List<JsonRequest> repositories = new List<JsonRequest>();
            for (int i = 0; i < request.items.Count; i++)
            {
                repositories.Add(new JsonRequest
                {
                    Author = request.items[i].full_name.Split('/')[0],
                    ProjectName = request.items[i].name,
                    StarGazers = request.items[i].stargazers_count,
                    Watchers = request.items[i].watchers_count,
                    Url = request.items[i].html_url,
                });
            }

            return repositories;
        }

        public void DeleteRepositoryRecord(int _reposityId)
        {
            throw new NotImplementedException();
        }

        public Request GetRepositoryRequest(string requestUrl)
        {
          return _context.Requests.FirstOrDefault(x => x.RequestUrl == requestUrl);
        }

        public async Task<Root> GetWebRequest(string repositoryName)
        {
            HttpClient _tokenclient = new HttpClient();
            _tokenclient.DefaultRequestHeaders.Add("User-Agent", "C# App");
            var _responseToken = await _tokenclient.GetStringAsync($"https://api.github.com/search/repositories?q={repositoryName}");
            var _jsonResults = JsonConvert.DeserializeObject<Root>(_responseToken);
            return _jsonResults;
        }

        public async void SaveRepositoryRequest(string repositoryRequest, string requestText)
        {
            var id = 0;
            if (!_context.Requests.Any())
            {
                id = 0;
            }
            else
            {
                id = _context.Requests.Max(x => x.Id) + 1;
            }
            _context.Requests.Add(new Request
            {
                Id = id,
                RequestText = requestText,
                RequestUrl = repositoryRequest,
            });
            await _context.SaveChangesAsync();
            
        }
    }
}
