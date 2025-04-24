using Microsoft.AspNetCore.Mvc;
using MVC_HotelBooking.Models;
using Newtonsoft.Json;
using System.Text;

namespace MVC_HotelBooking.Controllers
{
    public class DichVuController : Controller
    {
        private readonly HttpClient _httpClient;

        public DichVuController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<IActionResult> Index()
        {
            var dichVus = await _httpClient.GetFromJsonAsync<List<DichVu>>("https://localhost:7077/api/DichVu");
            return View(dichVus);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DichVu model, IFormFile? imageFile)
        {
            if (imageFile != null)
            {
                // Save the image and get the URL
                var imageUrl = await SaveImageAsync(imageFile);
                model.ImageUrl = imageUrl;
            }

            var response = await _httpClient.PostAsJsonAsync("https://localhost:7077/api/DichVu", model);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            var content = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError(string.Empty, $"Lỗi API: {content}");
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var dichVu = await _httpClient.GetFromJsonAsync<DichVu>($"https://localhost:7077/api/DichVu/{id}");
            if (dichVu == null)
                return NotFound();
            return View(dichVu);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DichVu model, IFormFile? imageFile)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                var imageUrl = await SaveImageAsync(imageFile);
                model.ImageUrl = imageUrl;
            }

            // Nếu không chọn lại ảnh, giữ nguyên ảnh cũ (nếu có)
            if (string.IsNullOrEmpty(model.ImageUrl))
            {
                var existing = await _httpClient.GetFromJsonAsync<DichVu>($"https://localhost:7077/api/DichVu/{model.MaDV}");
                if (existing != null)
                {
                    model.ImageUrl = existing.ImageUrl;
                }
            }

            var response = await _httpClient.PutAsJsonAsync($"https://localhost:7077/api/DichVu/{model.MaDV}", model);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            var content = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError(string.Empty, $"Lỗi API: {content}");

            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var dichVu = await _httpClient.GetFromJsonAsync<DichVu>($"https://localhost:7077/api/DichVu/{id}");
            if (dichVu == null)
                return NotFound();
            return View(dichVu);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:7077/api/DichVu/{id}");
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            return View();
        }

        private async Task<string> SaveImageAsync(IFormFile imageFile)
        {
            // Lưu file ảnh và trả về URL (ví dụ: "images/filename.jpg")
            var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
            if (!Directory.Exists(uploads))
            {
                Directory.CreateDirectory(uploads);
            }

            var filePath = Path.Combine(uploads, imageFile.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            // Giả sử ảnh được lưu trong thư mục wwwroot/images
            return $"/images/{imageFile.FileName}";
        }
    }
}
