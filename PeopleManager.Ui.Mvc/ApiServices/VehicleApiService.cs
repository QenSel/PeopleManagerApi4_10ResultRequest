using PeopleManager.Dto.Requests;
using PeopleManager.Dto.Results;
using PeopleManager.Model;

namespace PeopleManager.Ui.Mvc.ApiServices
{
    public class VehicleApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public VehicleApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IList<VehicleResult>> Find()
        {
            var httpClient = _httpClientFactory.CreateClient("PeopleManagerApi");
            var route = "/api/vehicles";
            var httpResponse = await httpClient.GetAsync(route);

            httpResponse.EnsureSuccessStatusCode();

            var vehicles = await httpResponse.Content.ReadFromJsonAsync<IList<VehicleResult>>();

            return vehicles ?? new List<VehicleResult>();
        }

        public async Task<VehicleResult?> Get(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("PeopleManagerApi");
            var route = $"/api/vehicles/{id}";
            var httpResponse = await httpClient.GetAsync(route);

            httpResponse.EnsureSuccessStatusCode();

            return await httpResponse.Content.ReadFromJsonAsync<VehicleResult>();
        }

        public async Task<VehicleResult?> Create(VehicleRequest vehicle)
        {
            var httpClient = _httpClientFactory.CreateClient("PeopleManagerApi");
            var route = "/api/vehicles";
            var httpResponse = await httpClient.PostAsJsonAsync(route, vehicle);

            httpResponse.EnsureSuccessStatusCode();

            return await httpResponse.Content.ReadFromJsonAsync<VehicleResult>();
        }

        public async Task<VehicleResult?> Update(int id, VehicleRequest vehicle)
        {
            var httpClient = _httpClientFactory.CreateClient("PeopleManagerApi");
            var route = $"/api/vehicles/{id}";
            var httpResponse = await httpClient.PutAsJsonAsync(route, vehicle);

            httpResponse.EnsureSuccessStatusCode();

            return await httpResponse.Content.ReadFromJsonAsync<VehicleResult>();
        }

        public async Task Delete(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("PeopleManagerApi");
            var route = $"/api/vehicles/{id}";
            var httpResponse = await httpClient.DeleteAsync(route);

            httpResponse.EnsureSuccessStatusCode();
        }
    }
}
