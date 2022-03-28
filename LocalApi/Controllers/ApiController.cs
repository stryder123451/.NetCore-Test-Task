using LocalApi.Models;
using LocalApi.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private IRepository _repository;
        public ApiController(IRepository repository)
        {
            _repository = repository;
        }

        // post request https://localhost:44323/api/api/find
        /*
         * {
             "id": 0,
             "requestUrl": "AmayaSoft",
             "requestText": null
           }
         */
        [HttpPost("find")]
         public async Task<ActionResult> FindRepositories(Request request)
         {
            if (request != null)
            {
                var result = _repository.GetRepositoryRequest(request.RequestUrl);
                if (result == null)
                {
                    var webRequest = await _repository.GetWebRequest(request.RequestUrl);
                    _repository.SaveRepositoryRequest(request.RequestUrl, webRequest.ToString());
                    return Ok(_repository.CreateRepositoriesList(webRequest));
                }
                else
                {
                    var _jsonResults = JsonConvert.DeserializeObject<Root>(result.RequestText);
                    return Ok(_repository.CreateRepositoriesList(_jsonResults));
                }
            }
            else
            {
                return Ok("Ничего не найдено...");
            }
        }
    }
}
