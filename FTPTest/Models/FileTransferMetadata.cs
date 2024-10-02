namespace FTPTest.Models
{
    public class FileTransferMetadata
    {
        public string FileName { get; set; } = String.Empty;
        public long FileSize { get; set; }
        public string TransferStatus { get; set; } = String.Empty;
    }

}
