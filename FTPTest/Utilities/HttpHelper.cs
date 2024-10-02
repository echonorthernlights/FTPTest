namespace FTPTest.Utilities
{
    public static class HttpHelper
    {
        public static async Task PostFileAsync(HttpClient client, string url, string filePath)
        {
            using var fileStream = new FileStream(filePath, FileMode.Open);
            var content = new StreamContent(fileStream);
            await client.PostAsync(url, content);
        }
    }

}
