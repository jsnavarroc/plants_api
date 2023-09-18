using _Net.Models;
using Newtonsoft.Json.Linq;

namespace _Net.TestData
{
    public static class ImageHelper
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private static readonly IConfiguration _configuration;

        static ImageHelper()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Development.json");

            _configuration = builder.Build();
        }

        public async static Task<List<ImageInfo>> GetAllImageInfosAsync()
        {
            try
            {
                string? keyPixabay = _configuration.GetValue<string>("KeyPixabay");
                if (string.IsNullOrEmpty(keyPixabay))
                {
                    return new List<ImageInfo>();
                }

                var response = await _httpClient.GetStringAsync($"https://pixabay.com/api/?key={keyPixabay}&q=plants+plantas+planta&image_type=photo&per_page=10&pretty=true");
                var data = JObject.Parse(response);

                if (data.TryGetValue("hits", out var hitsToken) && hitsToken is JArray hits)
                {
                    if (hits.HasValues)
                    {
                        var imageInfos = new List<ImageInfo>();

                        foreach (var hit in hits)
                        {
                            var imageUrl = hit.Value<string>("webformatURL");
                            var alt = hit.Value<string>("tags");

                            imageInfos.Add(new ImageInfo { Src = imageUrl ?? string.Empty, Alt = alt ?? string.Empty });
                        }

                        return imageInfos;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching image info: " + ex.Message);
            }

            return new List<ImageInfo>();
        }

    }
}
