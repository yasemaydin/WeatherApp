using System; // Girdi - çıktı işlemleri için kütüphane
using System.Net.Http; // Http işlemleri için kütüphane
using System.Text.Json; // JSON işlemleri için kütüphane
using System.Text.Json.Serialization; // JSON dosyasından sınıflara çevirme işlemleri için kütüphane
using System.Threading.Tasks; // Task türündeki işlemler için kütüphane

namespace WeatherApp
{
    internal class Program
    {
        // HTTP istekleri kullanılacağı için main metodu modefiye ediliyor.
        static async Task Main(string[] args)
        {
            // Şehirleri tutan bir dizi tanımlanıyor.
            string[] cities = { "istanbul", "izmir", "ankara" };

            // Her bir şehir için döngü kuruluyor.
            foreach (string city in cities)
            {
                // HTTPClient sınıfından bir nesne oluşturuluyor.
                using (HttpClient client = new HttpClient())
                {
                    // Kök adres bu nesneye belirtiliyor.
                    client.BaseAddress = new Uri("https://goweather.herokuapp.com/weather/");

                    // Şehir'e göre veri çekiliyor.
                    HttpResponseMessage response = await client.GetAsync(city);

                    // İşlemin başarılı olup olmadığı kontrol ediliyor.
                    if (response.IsSuccessStatusCode)
                    {
                        // İçerik string türünde bir nesneye aktarılıyor.
                        string content = await response.Content.ReadAsStringAsync();

                        // Veriler oluşturulan nesneye göre biçimlendiriliyor ve uygun alanlara aktarılıyor.
                        WeatherData weatherData = JsonSerializer.Deserialize<WeatherData>(content);

                        // Hava durumu bilgisi yazdırılıyor.
                        PrintWeatherInfo(city, weatherData);

                        // Her bir gün için çalışacak döngü kuruluyor.
                        foreach (ForecastData forecast in weatherData.Forecast)
                        {
                            // İleri günlerdeki hava durumunu bilgisi yazdırılıyor.
                            PrintForecastInfo(forecast);
                        }

                        Console.WriteLine();
                    }
                    // İşlemin neden başarısız olduğu yazdırılıyor.
                    else
                    {
                        Console.WriteLine("İstek Başarısız: " + response.ReasonPhrase);
                    }
                }
            }

            // Program 10 saniye bekletiliyor ve kapanıyor.
            await Task.Delay(10000);
        }

        // Hava durumunu yazdıran metod tanımlanıyor.
        private static void PrintWeatherInfo(string city, WeatherData weatherData)
        {
            Console.WriteLine(city);
            Console.WriteLine($"Sıcaklık: {weatherData.Temperature} Rüzgar: {weatherData.Wind} Açıklama: {weatherData.Description}");
            Console.WriteLine("Tahminler:");
        }

        // İleri günlerdeki hava durumunu yazdıran metod tanımlanıyor.
        private static void PrintForecastInfo(ForecastData forecast)
        {
            Console.WriteLine($"{forecast.Day}. Gün Sıcaklık: {forecast.Temperature} Rüzgar: {forecast.Wind}");
        }
    }

    // [JsonPropertyName("JSON dosyasındaki başlık adı")] ile JSON dosyasındaki başlıklara göre değişkenlere değer atıyoruz.

    // Havadurumu bilgisi için sınıf tanımlanıyor.
    public class WeatherData
    {
        [JsonPropertyName("temperature")]
        public string Temperature { get; set; }
        [JsonPropertyName("wind")]
        public string Wind { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("forecast")]
        public ForecastData[] Forecast { get; set; }
    }

    // İleri günlerdeki hava durumunu bilgisi için sınıf tanımlanıyor.
    public class ForecastData
    {
        [JsonPropertyName("day")]
        public string Day { get; set; }
        [JsonPropertyName("temperature")]
        public string Temperature { get; set; }
        [JsonPropertyName("wind")]
        public string Wind { get; set; }
    }
}
