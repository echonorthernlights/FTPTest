namespace FTPTest.Services.Interfaces
{
    public interface IFileReceiverService
    {
        Task ReceiveAndForwardFileAsync();
    }

}
