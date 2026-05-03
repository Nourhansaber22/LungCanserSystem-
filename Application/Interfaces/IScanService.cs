using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IScanService
    {
        Task<string> UploadScanAsync(int patientId, IFormFile file, int uploadedBy);
        Task<string> ProcessScanAsync(string filePath);
    }
}