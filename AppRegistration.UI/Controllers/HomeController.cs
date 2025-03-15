using System.Diagnostics;
using Microsoft.Identity.Web;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using AppRegistration.UI.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace AppRegistration.UI.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    public HomeController(
        IHttpClientFactory httpClientFactory, 
        IConfiguration configuration,
        ILogger<HomeController> logger)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
        _logger = logger;
    }

    [Authorize]
    public async Task<IActionResult> Index()
    {
        if (User.Identity.IsAuthenticated)
        {
            string[] scopes = new string[] { _configuration.GetValue<string>("ApiSettings:Scope") };
            
           var accessToken = await HttpContext.GetTokenAsync("access_token").ConfigureAwait(false);
            if (string.IsNullOrEmpty(accessToken))
            {
                return Unauthorized();
            }
            var apiBaseUrl = _configuration["ApiSettings:BaseUrl"];

            using var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await client.GetStringAsync($"{apiBaseUrl}/weatherforecast");

            var weatherModel = JsonSerializer.Deserialize<List<WeatherResponseModel>>(response);
            
            return View(weatherModel);

        }
        else
        {
            Console.WriteLine("User is not authenticated.");
        }
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    public async Task<IActionResult> SecureData()
    {
        var accessToken = await HttpContext.GetTokenAsync("access_token");
        var apiBaseUrl = _configuration["ApiSettings:BaseUrl"];

        using var client = _httpClientFactory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        var response = await client.GetStringAsync($"{apiBaseUrl}/secure-data");

        ViewBag.Data = response;
        return View();
    }
}