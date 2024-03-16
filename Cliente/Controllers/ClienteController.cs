using Cliente.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace PROYECTO_CLIENTE.Controllers
{
    public class ClienteController : Controller
    {
        private readonly HttpClient _httpClient;

        public ClienteController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("http://localhost:5281/api");
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("/api/Personas/Lista");
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var responseObject = JsonConvert.DeserializeObject<dynamic>(responseContent);
                var cl = JsonConvert.DeserializeObject<List<ClienteViewModel>>(responseObject.response.ToString());

                return View("Index", cl);
            }
            else
            {
                return View(new List<ClienteViewModel>());
            }

        }
        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Details(int id)
        {

            var response = await _httpClient.GetAsync($"/api/Personas/verCliente?id={id}");


            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var client = JsonConvert.DeserializeObject<ClienteViewModel>(content);

                return View(client);
            }
            else
            {
                return RedirectToAction("Details");
            }
        }


        [HttpPost]
        public async Task<IActionResult> Create(ClienteViewModel client)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(client);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("/api/Personas/Guardar", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error al crear");
                }
            }
            return View(client);
        }

        public async Task<IActionResult> Edit(int id)
        {

            var response = await _httpClient.GetAsync($"/api/Personas/verCliente?id={id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var client = JsonConvert.DeserializeObject<ClienteViewModel>(content);

                return View(client);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ClienteViewModel cl)
        {
            if (ModelState.IsValid)
            {


                var json = JsonConvert.SerializeObject(cl);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"/api/Personas/Editar?id={id}", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", new { id });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error al actualizar el Cliente.");
                }
            }

            return View(cl);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"/api/Personas/Eliminar?id={id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Error"] = "Error al eliminar el Cliente.";
                return RedirectToAction("Index");
            }
        }
    }
}
