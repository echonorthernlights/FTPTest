using FTPTest.Models;
using FTPTest.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class FileTransferController : ControllerBase {
    private readonly IFileReceiverService _receiverService;
    private readonly IFileForwarderService _forwarderService;

    public FileTransferController(IFileReceiverService receiverService, IFileForwarderService forwarderService) {
        _receiverService = receiverService;
        _forwarderService = forwarderService;
    }

    [HttpPost("start")]
    public async Task<IActionResult> StartFileTransfer() {
        await _receiverService.ReceiveAndForwardFileAsync();
        return Ok("File transfer initiated");
    }

    // POST api/filetransfer/upload
    [HttpPost("upload")]
    public async Task<IActionResult> UploadFile(IFormFile file)
    {
        // Check if the file is null
        if (file == null || file.Length == 0)
        {
            return BadRequest("No file received.");
        }

        var fileTransferMetadata = new FileTransferMetadata
        {
            FileName = file.FileName,
            FileSize = file.Length,
            TransferStatus = "Pending"
        };

        // Create a local path to store the file temporarily
        var tempFilePath = Path.Combine(Path.GetTempPath(), file.FileName);

        try
        {
            // Save the file temporarily
            using (var stream = new FileStream(tempFilePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Forward the file to the FTP server
            bool isUploaded = await UploadToFtpServer(tempFilePath);

            // Update transfer status
            fileTransferMetadata.TransferStatus = isUploaded ? "Success" : "Failed";

            // Log the file transfer metadata
            LogTransferMetadata(fileTransferMetadata);

            return Ok(fileTransferMetadata);
        }
        catch (Exception ex)
        {
            // Log the exception (you might want to use a logging framework)
            LogError(ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
        finally
        {
            // Clean up the temporary file
            if (System.IO.File.Exists(tempFilePath))
            {
                System.IO.File.Delete(tempFilePath);
            }
        }
    }

    private async Task<bool> UploadToFtpServer(string filePath)
    {
        // Implementation for uploading the file to the FTP server
        // Return true if successful, otherwise false
        return true; // Replace with actual FTP upload logic
    }

    private void LogTransferMetadata(FileTransferMetadata metadata)
    {
        // Implement your logging mechanism here
    }

    private void LogError(string message)
    {
        // Implement your error logging mechanism here
    }

}
