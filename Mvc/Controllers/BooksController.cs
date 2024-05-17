using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Text.Json;
using System.Text;
using Mvc.Models.DTO;
using Microsoft.Extensions.Logging;
namespace Mvc.Controllers
{
    public class BooksController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ILogger<BooksController> _logger;
        public BooksController(ILogger<BooksController> logger, IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
            _logger = logger;
        }
        public async Task<IActionResult> Index([FromQuery] string filteron = null, string filterQuery = null, string sortBy = null, bool isAscending = true)
        {
            List<BookDTO> response = new List<BookDTO>();
            try
            {
                var client = httpClientFactory.CreateClient();
                var httpResponseMess = await client.GetAsync($"https://localhost:7031/api/Books/get-all-books?filterOn={filteron}&filterQuery={filterQuery}&sortBy={sortBy}&isAscending={isAscending}");
                httpResponseMess.EnsureSuccessStatusCode();
                response.AddRange(await httpResponseMess.Content.ReadFromJsonAsync<IEnumerable<BookDTO>>());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching books");
                return View("Error");
            }
            if (response.Count == 0)
            {
                _logger.LogWarning("No books were returned from the API");
            }
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> addBook(addBookDTO addBookDTO)
        {
            try
            {
                var client = httpClientFactory.CreateClient();
                var httpRequestMess = new HttpRequestMessage()
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri($"https://localhost:7031/api/Books"),
                    Content = new StringContent(JsonSerializer.Serialize(addBookDTO), Encoding.UTF8, MediaTypeNames.Application.Json)
                };
                //Console.WriteLine(JsonSerializer. Serialize (addBookDTO));
                var httpResponseMess = await client.SendAsync(httpRequestMess);
                httpResponseMess.EnsureSuccessStatusCode();
                var response = await httpResponseMess.Content.ReadFromJsonAsync<addBookDTO>();
                if (response != null)
                {
                }
                return RedirectToAction("Index", "Books");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View();
        }
        public async Task<IActionResult> listBook(int id)
        {
            BookDTO response = new BookDTO();
            try
            {
                // lấy dữ liệu books from API
                var client = httpClientFactory.CreateClient();
                var httpResponseMess = await client.GetAsync($"https://localhost:7031/api/Books/get-book-by-id/" + id);
                httpResponseMess.EnsureSuccessStatusCode();
                var stringResponseBody = await httpResponseMess.Content.ReadAsStringAsync();
                response = await httpResponseMess.Content.ReadFromJsonAsync<BookDTO>();
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View(response);
        }
        [HttpGet]
        public async Task<IActionResult> editBook(int id)
        {
            BookDTO responseBook = new BookDTO();
            var client = httpClientFactory.CreateClient();
            var httpResponseMess = await client.GetAsync($"https://localhost:7031/api/Books/get-book-by-id/" + id);
            httpResponseMess.EnsureSuccessStatusCode();
            responseBook = await httpResponseMess.Content.ReadFromJsonAsync<BookDTO>();
            ViewBag.Book = responseBook;
            List<authorDTO> responseAu = new List<authorDTO>();
            var httpResponseAu = await client.GetAsync($"https://localhost:7031/api/Author/get-all-author");
            httpResponseAu.EnsureSuccessStatusCode();
            responseAu.AddRange(await httpResponseAu.Content.ReadFromJsonAsync<IEnumerable<authorDTO>>());
            ViewBag.listAuthor = responseAu;
            List<publisherDTO> responsePu = new List<publisherDTO>();
            var httpResponsePu = await client.GetAsync($"https://localhost:7245/api/Publisher/get-all-publisher");
            httpResponsePu.EnsureSuccessStatusCode();
            responsePu.AddRange(await httpResponsePu.Content.ReadFromJsonAsync<IEnumerable<publisherDTO>>());
            ViewBag.listPublisher = responsePu;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> editBook([FromRoute] int id, editBookDTO bookDTO)
        {
            try
            {
                var client = httpClientFactory.CreateClient();
                var httpRequestMess = new HttpRequestMessage()
                {
                    Method = HttpMethod.Put,
                    RequestUri = new Uri($"https://localhost:7031/api/Books/update-book-by-id/" + id),
                    Content = new StringContent(JsonSerializer.Serialize(bookDTO), Encoding.UTF8, MediaTypeNames.Application.Json)
                };
                var httpResponseMess = await client.SendAsync(httpRequestMess);
                httpResponseMess.EnsureSuccessStatusCode();
                var response = await httpResponseMess.Content.ReadFromJsonAsync<addBookDTO>();
                if (response != null)
                {
                }
                return RedirectToAction("Index", "Books");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> delBook([FromRoute] int id)
        {
            try
            {
                var client = httpClientFactory.CreateClient();
                var httpResponseMess = await client.DeleteAsync($"https://localhost:7031/api/Books/delete-book-by-id/" + id);
                httpResponseMess.EnsureSuccessStatusCode();
                return RedirectToAction("Index", "Books");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View("Index");
        }
    }
}

