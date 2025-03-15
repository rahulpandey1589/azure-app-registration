namespace AppRegistration.UI.Models;

public class WeatherResponseModel
{
    public string date { get; set; } = default!;
    public int temperatureC { get; set; } = default!;
    public int temperatureF { get; set; } = default!;
    public string summary { get; set; } = default!;
}