# WeatherApp

Bu proje, belirli şehirlerin hava durumu bilgilerini JSON dosyasından almak için bir HTTP istemcisi kullanır.

## Kullanılan Kütüphaneler

- System
- System.Net.Http
- System.Text.Json
- System.Text.Json.Serialization
- System.Threading.Tasks

## Kullanılan NuGet Paketleri

- System.Text.Json (Microsoft)

## Nasıl Çalışır

1. Şehir adlarından oluşan bir dizi tanımlanır.
2. Her bir şehir için döngü oluşturulur.
3. HttpClient nesnesi oluşturulur ve API'lerin bulunduğu kök url atanır.
4. Şehir adına göre API isteği gönderilir.
5. İstek başarılı ise, JSON içeriği okunur ve havadurumu sınıfındaki alanlara çevrilir.
6. Şehir için veriler yazdırılır ve önümüzdeki günlerin tahminleri ekrana yazdırılır.

## Metodlar

- PrintWeatherInfo: Şehir için havadurumu verilerini ekrana yazdırır. String türünden bir değişken ve WeatherData türünden bir nesne ile çalışır.
- PrintForecastInfo: Şehir için önümüzdeki günlerin havadurumu tahminlerini ekrana yazdırır. ForecastData türünden bir nesne ile çalışır.

## Sınıflar

- WeatherData: JSON belgesinde bulunan alan isimlerine göre değişkenler oluşturulur. JSON içeriği sıcaklık, rüzgar hızı, tanım ve gün bilgileri dizisidir. Bu sınıf kök JSON verisidir.
- ForecastData: JSON belgesinde bulunan alan isimlerine göre değişkenler oluşturulur. JSON içeriği gün, sıcaklık ve rüzgar hızı bilgisidir.