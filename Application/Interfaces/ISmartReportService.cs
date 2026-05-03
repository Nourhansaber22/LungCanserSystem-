using Application.DTOs;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ISmartReportService
    {
        Task<SmartReportDto> GetReportAsync(int scanId); // FR-11
        Task<byte[]> GeneratePdfAsync(int scanId);       // FR-12
    }
}