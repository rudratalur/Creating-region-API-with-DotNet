using Microsoft.AspNetCore.Mvc;
using WebAPIConsume.Models;
using Newtonsoft;
using Newtonsoft.Json;

namespace WebAPIConsume.Controllers
{
    public class UserController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7104/api/Regions");
        HttpClient client;

        public UserController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }
        public IActionResult Index()
        {
            List<RegionViewModel> modelList = new List<RegionViewModel>(); 
            HttpResponseMessage response = client.GetAsync(client.BaseAddress).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
               modelList = JsonConvert.DeserializeObject<List<RegionViewModel>>(data);

            }
            return View(modelList);
        }
    }
}
