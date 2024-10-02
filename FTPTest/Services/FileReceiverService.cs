using FTPTest.Services.Interfaces;
using FluentFTP;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace FTPTest.Services
{
    public class FileReceiverService : IFileReceiverService
    {
        private readonly IConfiguration _config;
        private readonly IFileForwarderService _forwarderService;

        public FileReceiverService(IConfiguration config, IFileForwarderService forwarderService)
        {
            _config = config;
            _forwarderService = forwarderService;
        }

        public async Task ReceiveAndForwardFileAsync()
        {
            string ftpHost = _config["FtpSettings:Host"];
            string ftpUsername = _config["FtpSettings:Username"];
            string ftpPassword = _config["FtpSettings:Password"];
            int ftpPort = int.Parse(_config["FtpSettings:Port"]); // Ensure port is read correctly
            string localPath = Path.Combine("temp", "file.txt");

            try
            {
                using (FtpClient client = new FtpClient(ftpHost, ftpUsername, ftpPassword, ftpPort))
                {
                    client.Connect();

                    if (client.IsConnected)
                    {
                        client.DownloadFile(localPath, "/remote/path/to/file.txt"); // Update with actual remote path
                        await _forwarderService.ForwardFileAsync(localPath);
                    }
                    else
                    {
                        throw new Exception("Failed to connect to the FTP server.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in ReceiveAndForwardFileAsync: {ex.Message}");
                throw;
            }
        }


    }
}
