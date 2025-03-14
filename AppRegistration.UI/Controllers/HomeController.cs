using System.Diagnostics;
using System.Net.Http.Headers;
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
        IHttpClientFactory httpClientFactory, IConfiguration configuration,
        ILogger<HomeController> logger)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        int[] array1 = [1, 2, 3];
        int[] array2 = [4, 5, 6];
        int[] array3 = [..array1, ..array2];
        
        if (User.Identity.IsAuthenticated)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token").ConfigureAwait(false);
            if (string.IsNullOrEmpty(accessToken))
            {
                return Unauthorized();
            }
            var apiBaseUrl = _configuration["ApiSettings:BaseUrl"];

            using var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await client.GetStringAsync($"{apiBaseUrl}/weatherforecast");
            Console.WriteLine($"Access Token: {accessToken}");
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