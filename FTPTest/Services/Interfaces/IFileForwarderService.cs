namespace FTPTest.Services.Interfaces
{
    public interface IFileForwarderService
    {
        Task ForwardFileAsync(string filePath);
    }

}
