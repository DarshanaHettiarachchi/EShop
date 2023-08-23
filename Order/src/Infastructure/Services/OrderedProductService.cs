using Application.Interfaces;
using Application.Models;
using System.Text.Json;

namespace Infastructure.Services
{
    public class OrderedProductService : IOrderedProductService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly JsonSerializerOptions _options;
        public OrderedProductService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<OrderedProduct> GetOrderedProduct(Guid id)
        {
            var url = $"https://localhost:7001/products/{id}";
            var Uri = new Uri(url);

            var httpClient = _httpClientFactory.CreateClient();

            using var response = await httpClient.GetAsync(Uri, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();
            httpClient.Dispose();

#pragma warning disable CS8603 // Possible null reference return.
            return await JsonSerializer.DeserializeAsync<OrderedProduct>(stream, _options);
#pragma warning restore CS8603 // Possible null reference return.

        }
    }
}
