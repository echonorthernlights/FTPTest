using FluentFTP;

namespace FTPTest.Utilities
{
    

    public static class FtpHelper
    {
        public static void DownloadFile(string ftpHost, string fileName, string localPath)
        {
            using (var client = new FtpClient(ftpHost))
            {
                client.Connect();
                client.DownloadFile(localPath, fileName);
            }
        }
    }

}
