using Newtonsoft.Json;
using System.Text;

namespace graphQLService.HttpClients
{
    /// <summary>
    /// This is the base class for all http clients used in mmai
    /// This class abstracts HTTP calls (including stringcontent creation) and its' error handling
    /// 
    /// todo: Add Polly for auto retries on network errors, token errors etc.
    /// todo: Add error handling
    /// </summary>
    public class MmaiHttpClient
    {
        protected HttpClient _client;

        protected MmaiHttpClient(HttpClient client)
        {
            _client = client;
        }

        protected async Task<T?> Get<T>(string path)
        {
            var response = await _client.GetAsync(path);
            return await DeserializeHttpContentResponse<T>(response.Content);
        }

        protected async Task<T?> Post<T>(string path, object content)
        {
            var httpContent = CreateContent(content);
            var response = await _client.PostAsync(path, httpContent);
            return await DeserializeHttpContentResponse<T>(response.Content);
        }

        protected async Task<T?> Put<T>(string path, object content)
        {
            var httpContent = CreateContent(content);
            var response = await _client.PutAsync(path, httpContent);
            return await DeserializeHttpContentResponse<T>(response.Content);
        }

        protected async Task<T?> Delete<T>(string path)
        {
            var response = await _client.DeleteAsync(path);
            return await DeserializeHttpContentResponse<T>(response.Content);
        }

        private HttpContent? CreateContent(object content)
        {
            if (content == null) return null;

            var json = JsonConvert.SerializeObject(content);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        private async Task<T?> DeserializeHttpContentResponse<T>(HttpContent content)
        {
            var json = await content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
