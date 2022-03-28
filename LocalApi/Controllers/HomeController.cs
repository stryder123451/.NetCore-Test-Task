using LocalApi.Models;
using LocalApi.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LocalApi.Controllers
{
    public class HomeController : Controller
    {
        private IRepository _repository;
        
        public HomeController(IRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public IActionResult Index()
        {
            if (TempData.Keys.Count() > 0)
            {
                ViewBag.Requests = JsonConvert.DeserializeObject<List<JsonRequest>>((string)TempData["Requests"]);
            }
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Index(Request request)
        {
            if (request != null)
            {
                var result = _repository.GetRepositoryRequest(request.RequestUrl);
                if (result == null)
                {
                    var webRequest = await _repository.GetWebRequest(request.RequestUrl);
                    _repository.SaveRepositoryRequest(request.RequestUrl, webRequest.ToString()) ;       
                    TempData["Requests"] = JsonConvert.SerializeObject(_repository.CreateRepositoriesList(webRequest));
                    return RedirectToAction("Index");
                }
                else
                {
                    var _jsonResults = JsonConvert.DeserializeObject<Root>(result.RequestText);
                    TempData["Requests"] = JsonConvert.SerializeObject(_repository.CreateRepositoriesList(_jsonResults));
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return RedirectToAction("Index");
            }
            
        }
    }
}
