using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalApi.Models.Interfaces
{
    public interface IRepository
    {
        Request GetRepositoryRequest(string requestUrl);
        void DeleteRepositoryRecord(int _reposityId);
        void SaveRepositoryRequest(string repositoryRequest, string requestText);
        Task<Root> GetWebRequest (string repositoryName);
        List<JsonRequest> CreateRepositoriesList(Root request);
    }
}
