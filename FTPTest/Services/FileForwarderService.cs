using FTPTest.Services.Interfaces;

namespace FTPTest.Services
{
    using Microsoft.Extensions.Configuration;
    using System.Net.Http;

    public class FileForwarderService : IFileForwarderService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public FileForwarderService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task ForwardFileAsync(string filePath)
        {
            var targetUrl = _config["ForwardingSettings:Url"];
            using var fileStream = new FileStream(filePath, FileMode.Open);
            var content = new StreamContent(fileStream);
            await _httpClient.PostAsync(targetUrl, content);
        }
    }

}
