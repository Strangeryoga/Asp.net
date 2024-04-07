using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using WebAPIConsumeBatch1.Models;

namespace WebAPIConsumeBatch1.Controllers
{
    public class ProductsController : Controller
    {
        HttpClient client;
        string url;
        public ProductsController() 
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            client = new HttpClient(clientHandler);
            url = "https://localhost:7029/api/Products/";
        }
        public IActionResult Index()
        {
            List<Product> products = new List<Product>();
            HttpResponseMessage response=client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                var d=JsonConvert.DeserializeObject<List<Product>>(data);
                if(d!=null)
                {
                    products = d;
                }
            }

            return View(products);
        }

        public IActionResult AddProduct() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddProduct(Product p)
        {
            var d=JsonConvert.SerializeObject(p);
            StringContent content = new StringContent(d, Encoding.UTF8, "application/json");
            HttpResponseMessage response=client.PostAsync(url, content).Result;
            if(response.IsSuccessStatusCode) 
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult DeleteProduct(int id)
        {
            HttpResponseMessage response = client.DeleteAsync(url+id).Result;
            if(response.IsSuccessStatusCode) 
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        public IActionResult EditProduct(int id)
        {
            Product prod = new Product();
            HttpResponseMessage response = client.GetAsync(url+id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                var d = JsonConvert.DeserializeObject<Product>(data);
                if (d != null)
                {
                    prod = d;
                }
            }
            return View(prod);
        }

        [HttpPost]
        public IActionResult EditProduct(Product p)
        {
            var d = JsonConvert.SerializeObject(p);
            StringContent content = new StringContent(d, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync(url+p.id, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

    }
}
