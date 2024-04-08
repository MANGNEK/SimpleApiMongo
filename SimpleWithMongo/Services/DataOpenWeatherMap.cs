using SimpleWithMongo.Interface;
using SimpleWithMongo.Request;
using Newtonsoft.Json;
using System.Net.Http.Json;
using SimpleWithMongo.Model;
namespace SimpleWithMongo.Services;

public class DataOpenWeatherMap : IDataOpenWeatherMap
{
    private readonly HttpClient _httpClient;
    private readonly IDataNow dataNow;

    public DataOpenWeatherMap(HttpClient httpClient, IDataNow dataNow)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://api.openweathermap.org/data/2.5/");
        this.dataNow = dataNow;
    }
    public async Task<OpenWeatherMapRequest> GetData()
    {
        try
        {
            _httpClient.DefaultRequestHeaders.Add("Host", "api.openweathermap.org");

            HttpResponseMessage response = await _httpClient.GetAsync("weather?lat=10.823099&lon=106.629662&appid=4c2da921519d2cff3b435ad9964f22f4&units=imperial");

           
            if (response.IsSuccessStatusCode)
            {
                var jsondata = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<OpenWeatherMapRequest>(jsondata);
                return data;
            }
            else
            {
                Console.WriteLine($"Lỗi: {response.StatusCode}");
                return null;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Lỗi: {ex.Message}");
            return null;
        }
    }

    public async Task UpdateData()
    {
        var data = await GetData();
        DataDayNowModel model = new DataDayNowModel {
            Temperture = ((data.main.temp - 32) * 5 / 9).ToString("F1"),
            Humidity = data.main.humidity.ToString(),
            Cloud = data.clouds.all.ToString(),
            WindSpeed = data.wind.speed.ToString(),
        };
        await dataNow.Update(model);


    }
}
