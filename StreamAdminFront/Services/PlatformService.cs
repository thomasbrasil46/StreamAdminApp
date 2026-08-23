using StreamAdminFront.Models;
using StreamAdminFront.Services.IServices;
using StreamAdminFront.Utils;

namespace StreamAdminFront.Services
{
    public class PlatformService : IPlatformService
    {
        private readonly HttpClient _httpClient;
        public const string BasePath = "api/v1/platform";

        public PlatformService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }
        public async Task<IEnumerable<PlatformModel>> FindAllPlatforms()
        {
            var response = await _httpClient.GetAsync(BasePath);
            return await response.ReadContentAs<List<PlatformModel>>();
        }
        public async Task<PlatformModel?> FindById(long id)
        {
            var response = await _httpClient.GetAsync($"{BasePath}/{id}");
            return await response.ReadContentAs<PlatformModel?>();
        }
        public async Task<PlatformModel> CreatePlatform(PlatformModel platform)
        {
            var response = await _httpClient.PostAsJson(BasePath, platform);
            if (response.IsSuccessStatusCode)
                return await response.ReadContentAs<PlatformModel>();
            else throw new Exception("Something went wrong when calling API");
        }
        public async Task<PlatformModel> UpdatePlatform(PlatformModel platform)
        {
            var response = await _httpClient.PutAsJson($"{BasePath}/{platform.Id}", platform);
            if (response.IsSuccessStatusCode)
                return await response.ReadContentAs<PlatformModel>();
            else throw new Exception("Something went wrong when calling API");
        }
        public async Task<bool> DeletePlatform(long id)
        {
            var response = await _httpClient.DeleteAsync($"{BasePath}/{id}");
            if (response.IsSuccessStatusCode)
                return await response.ReadContentAs<bool>();
            else throw new Exception("Something went wrong when calling API");
        }        
    }
}
